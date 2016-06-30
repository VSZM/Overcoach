
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Overcoach
{
    class ImageProcessor
    {
        private const int WIDTH_OF_PLAYER = 210; //(0.109375 * width)
        private const int HEIGHT_OF_PLAYER = 250; //(0.23148 * height)
        private const int X_UPPER_LEFT_CORNER_OF_LEFT_PLAYER = 340; // (0.177083 * width)
        private const int Y_UPPER_LEFT_CORNER_OF_LEFT_PLAYER_IN_ENEMY_TEAM = 180; // (0.16 * height)
        private const int Y_UPPER_LEFT_CORNER_OF_LEFT_PLAYER_IN_ALLY_TEAM = 550; // (0.50925 * height)
        private const double TEMPLATE_MATCH_TRESHOLD = 0.9;

        private static readonly string HeroPortraitsFolder =
            Assembly.GetExecutingAssembly().Location.Replace("Overcoach.exe", "") + @"..\..\Image_Resources\Hero_Portraits";

        private static readonly Dictionary<Hero, Image<Gray, float>> AlivePortraits;
        private static readonly Dictionary<Hero, Image<Gray, float>> DeadPortraits;


        static ImageProcessor()
        {
            AlivePortraits = new Dictionary<Hero, Mat>();
            DeadPortraits = new Dictionary<Hero, Mat>();

            foreach (Hero hero in Enum.GetValues(typeof(Hero)))
            {
                string alive_path = HeroPortraitsFolder + hero + "_ALIVE.jpg";
                string dead_path = HeroPortraitsFolder + hero + "_DEAD.jpg";

                if (File.Exists(alive_path))
                {
                    Mat alive_hero_portrait = CvInvoke.Imread(alive_path,
                        LoadImageType.Color);
                    AlivePortraits.Add(hero, alive_hero_portrait);
                }
                else
                    Console.Error.WriteLine("Missing hero portrait > " + alive_path);

                if (File.Exists(dead_path))
                {
                    Mat dead_hero_portrait = CvInvoke.Imread(dead_path, LoadImageType.Color);
                    DeadPortraits.Add(hero, dead_hero_portrait);
                }else
                    Console.Error.WriteLine("Missing hero portrait > " + dead_path);
            }
        }


        public List<Player> Recognize_Players(Mat source)
        {
            List<Player> ret = new List<Player>();

            // Process enemy players
            Parallel.For(0,
                6,
                i =>
                {
                    // cutting actual player icons from original image
                    var roi = new Rectangle(X_UPPER_LEFT_CORNER_OF_LEFT_PLAYER + i * WIDTH_OF_PLAYER, 
                                            Y_UPPER_LEFT_CORNER_OF_LEFT_PLAYER_IN_ENEMY_TEAM, 
                                            WIDTH_OF_PLAYER,
                                            HEIGHT_OF_PLAYER);

                    using (Image<Gray, float> actual_player_card = new Mat(source, roi).ToImage<Gray, float>())
                    {
                        Hero hero_of_player = Find_Hero(actual_player_card);
                        string name_of_player = Find_Name(actual_player_card);

                        ret.Add(new Player { hero = hero_of_player, Name = name_of_player, side = Side.OPPONENT});
                    }
                    
                });

            // Process ally players
            Parallel.For(0,
                6,
                i =>
                {
                    // cutting actual player icons from original image
                    var roi = new Rectangle(X_UPPER_LEFT_CORNER_OF_LEFT_PLAYER + i * WIDTH_OF_PLAYER,
                                            Y_UPPER_LEFT_CORNER_OF_LEFT_PLAYER_IN_ALLY_TEAM,
                                            WIDTH_OF_PLAYER,
                                            HEIGHT_OF_PLAYER);

                    using (Image<Gray, float> actual_player_card = new Mat(source, roi).ToImage<Gray, float>())
                    {
                        Hero hero_of_player = Find_Hero(actual_player_card);
                        string name_of_player = Find_Name(actual_player_card);

                        ret.Add(new Player { hero = hero_of_player, Name = name_of_player, side = name_of_player.Equals(SELFNAME) });
                    }

                });



            return ret;
        }

        private string Find_Name(Image<Gray, float> playerCard)
        {
            return "Pista";
        }


        private Hero Find_Hero(Image<Gray, float> playerCard)
        {
            foreach (var hero_portrait in AlivePortraits)
            {
                if (Match_Template(playerCard, hero_portrait.Value))
                    return hero_portrait.Key;
            }

            foreach (var hero_portrait in DeadPortraits)
            {
                if (Match_Template(playerCard, hero_portrait.Value))
                    return hero_portrait.Key;
            }

            return Hero.NONE;
        }

        /// <summary>
        /// http://stackoverflow.com/questions/16406958/emgu-finding-image-a-in-image-b
        /// </summary>
        private bool Match_Template(Image<Gray, float> source, Image<Gray, float> template)
        {
            using (Image<Gray, float> result = source.MatchTemplate(template, TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                if (maxValues[0] > TEMPLATE_MATCH_TRESHOLD)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

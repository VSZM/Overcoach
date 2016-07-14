using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Overcoach.Model;

namespace Overcoach.Logic
{
    class ImageProcessor
    {
        private const int WIDTH_OF_PLAYER = 210; //(0.109375 * width)
        private const int HEIGHT_OF_PLAYER = 250; //(0.23148 * height)
        private const int X_UPPER_LEFT_CORNER_OF_LEFT_PLAYER = 340; // (0.177083 * width)
        private const int Y_UPPER_LEFT_CORNER_OF_LEFT_PLAYER_IN_ENEMY_TEAM = 180; // (0.16 * height)
        private const int Y_UPPER_LEFT_CORNER_OF_LEFT_PLAYER_IN_ALLY_TEAM = 550; // (0.50925 * height)
        private const double TEMPLATE_MATCH_TRESHOLD = 0.9;
        private const int X_UPPER_LEFT_CORNER_OF_PLAYER_NAME = 0; 
        private const int Y_UPPER_LEFT_CORNER_OF_PLAYER_NAME = 830; // (0.76851 * height)
        private const int MAX_NAME_WIDTH = 250; // (0.1302083 * width)
        private const int NAME_HEIGHT = 50; // (0.04629 * height)


        private static readonly string assemblyLocation =
            Assembly.GetExecutingAssembly().Location.Replace("Overcoach.exe", "");
        private static readonly string HeroPortraitsFolder = assemblyLocation + @"..\..\Image_Resources\Hero_Portraits\";

        private static readonly Dictionary<Hero, Image<Gray, float>> AlivePortraits;
        private static readonly Dictionary<Hero, Image<Gray, float>> DeadPortraits;

        static ImageProcessor()
        {
            //ocr.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ-1234567890");

            AlivePortraits = new Dictionary<Hero, Image<Gray, float>>();
            DeadPortraits = new Dictionary<Hero, Image<Gray, float>>();

            foreach (Hero hero in Enum.GetValues(typeof(Hero)))
            {
                string alive_path = HeroPortraitsFolder + hero + "_ALIVE.png";
                string dead_path = HeroPortraitsFolder + hero + "_DEAD.png";

                if (File.Exists(alive_path))
                {
                    var alive_hero_portrait = CvInvoke.Imread(alive_path,
                        LoadImageType.Color).ToImage<Gray, float>();
                    AlivePortraits.Add(hero, alive_hero_portrait);
                }
                else
                    Console.Error.WriteLine("Missing hero portrait > " + alive_path);

                if (File.Exists(dead_path))
                {
                    var dead_hero_portrait = CvInvoke.Imread(dead_path, LoadImageType.Color).ToImage<Gray, float>();
                    DeadPortraits.Add(hero, dead_hero_portrait);
                }else
                    Console.Error.WriteLine("Missing hero portrait > " + dead_path);
            }
        }


        public void Recognize_Players(Mat source, out TeamComposition friendly_team, out TeamComposition enemy_team)
        {
            TeamComposition friendly = new TeamComposition();
            TeamComposition enemy = new TeamComposition();

            string selfname = Find_Selfname(source);

            // Process enemy players
            Parallel.For(0,
                6,
                i =>
                {
                    enemy.players.Add(Process_Player(i, Y_UPPER_LEFT_CORNER_OF_LEFT_PLAYER_IN_ENEMY_TEAM, source, selfname));
                });

            // Process ally players
            Parallel.For(0,
                6,
                i =>
                {
                    friendly.players.Add(Process_Player(i, Y_UPPER_LEFT_CORNER_OF_LEFT_PLAYER_IN_ALLY_TEAM, source, selfname));
                });

            friendly_team = friendly;
            enemy_team = enemy;
        }

        private string Find_Selfname(Mat source)
        {
            // cutting player name from original image
            var roi = new Rectangle(X_UPPER_LEFT_CORNER_OF_PLAYER_NAME,
                                    Y_UPPER_LEFT_CORNER_OF_PLAYER_NAME,
                                    MAX_NAME_WIDTH,
                                    NAME_HEIGHT);

            using (Mat roi_mat = new Mat(source, roi))
            {
                return Find_Name(roi_mat);
            }
        }


        private Player Process_Player(int i, int y_coord, Mat source, string selfname)
        {
            // cutting actual player icons from original image
            var roi = new Rectangle(X_UPPER_LEFT_CORNER_OF_LEFT_PLAYER + i * WIDTH_OF_PLAYER,
                                    y_coord,
                                    WIDTH_OF_PLAYER,
                                    HEIGHT_OF_PLAYER);
            using (Mat roi_mat = new Mat(source, roi))
            {
                using (Image<Gray, float> actual_player_card = roi_mat.ToImage<Gray, float>())
                {
                    Hero hero_of_player = Find_Hero(actual_player_card);
                    string name_of_player = Find_Name(roi_mat);
                    Side side_of_player;

                    if (y_coord == Y_UPPER_LEFT_CORNER_OF_LEFT_PLAYER_IN_ENEMY_TEAM)
                        side_of_player = Side.OPPONENT;
                    else
                        side_of_player = Side.TEAMMATE;


                    return new Player
                    {
                        hero = hero_of_player,
                        Name = name_of_player,
                        side = side_of_player
                    };
                }
            }
        }
        
        private string Find_Name(Mat playerCard)
        {
            // We can use template matching here as well. Just create an image for each english letter and numbers and recognize those.
            return "Joska";
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

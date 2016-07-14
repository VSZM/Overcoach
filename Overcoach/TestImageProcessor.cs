using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Emgu.CV;
using Emgu.CV.CvEnum;
using NUnit.Framework;
using Overcoach.Logic;
using Overcoach.Model;

namespace Overcoach
{
    [TestFixture]
    class TestImageProcessor
    {
        private readonly string _testFileFolder = Assembly.GetExecutingAssembly().Location.Replace("Overcoach.exe", "") + @"..\..\test_files\";
        private Mat _hardTest;
        private ImageProcessor _imageProcessor;

        [SetUp]
        public void Initialize()
        {
            _hardTest = CvInvoke.Imread(_testFileFolder + "hard_sample.jpg", LoadImageType.AnyColor);
            _imageProcessor = new ImageProcessor();
        }

        [Test]
        public void Test_Recognizes_Tracer()
        {
            TeamComposition ally_team, enemy_team;

            _imageProcessor.Recognize_Players(_hardTest, out ally_team, out enemy_team);

            Assert.Contains(Hero.TRACER, ally_team.players.Select(x => x.hero).ToList());
            Assert.AreEqual(1, ally_team.players.Count(x => Hero.TRACER.Equals(x.hero)));
            Assert.AreEqual(/*"BECSO"*/ "Joska", ally_team.players.Find(x => Hero.TRACER.Equals(x.hero)).Name);
        }

        [Test, Timeout(1000)]
        public void Test_Recognizes_Players()
        {
            TeamComposition ally_team, enemy_team;

            _imageProcessor.Recognize_Players(_hardTest, out ally_team, out enemy_team);
            var recognized_players = new List<Player>(ally_team.players);
            recognized_players.AddRange(enemy_team.players);

            var expected_players = new List<Player>(
                new[]
                {
                    new Player {Name = "Joska" /*"CRIZUN"*/, hero = Hero.SOLDIER76, side = Side.OPPONENT},
                    new Player {Name = "Joska" /*"ROOPURR"*/, hero = Hero.WIDOWMAKER, side = Side.OPPONENT},
                    new Player {Name = "Joska" /*"OREZ"*/, hero = Hero.ROADHOG, side = Side.OPPONENT},
                    new Player {Name = "Joska" /*"BUSHEL"*/, hero = Hero.MCCREE, side = Side.OPPONENT},
                    new Player {Name = "Joska" /*"FIDELC"*/, hero = Hero.JUNKRAT, side = Side.OPPONENT},
                    new Player {Name = "Joska" /*"CRAZYBRAIN"*/, hero = Hero.MERCY, side = Side.OPPONENT},
                    new Player {Name = "Joska" /*"VSZM"*/, hero = Hero.ZENYATTA, side = Side.TEAMMATE},
                    new Player {Name = "Joska" /*"BECSO"*/, hero = Hero.TRACER, side = Side.TEAMMATE},
                    new Player {Name = "Joska" /*"PAPASHEEV"*/, hero = Hero.GENJI, side = Side.TEAMMATE},
                    new Player {Name = "Joska" /*"S4RGON"*/, hero = Hero.REINHARDT, side = Side.TEAMMATE},
                    new Player {Name = "Joska" /*"SCAMAZ"*/, hero = Hero.DVA, side = Side.TEAMMATE},
                    new Player {Name = "Joska" /*"ASTAMOR"*/, hero = Hero.HANZO, side = Side.TEAMMATE},
                });


            Assert.NotNull(recognized_players);    
            Assert.AreEqual(12, recognized_players.Count);
            Assert.AreEqual(0, expected_players.Except(recognized_players).Count());
        }
    }
}

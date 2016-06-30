using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Emgu.CV;
using Emgu.CV.CvEnum;
using NUnit.Framework;

namespace Overcoach
{
    [TestFixture]
    class TestImageProcessor
    {
        private readonly string test_file_folder = Assembly.GetExecutingAssembly().Location.Replace("Overcoach.exe", "") + @"..\..\test_files\";
        private Mat HARD_TEST;
        private ImageProcessor _imageProcessor;

        [SetUp]
        public void Initialize()
        {
            HARD_TEST = CvInvoke.Imread(test_file_folder + "hard_sample.jpg", LoadImageType.AnyColor);
            _imageProcessor = new ImageProcessor();
        }

        [Test]
        public void Test_Recognizes_Tracer()
        {
            var recognized_players = _imageProcessor.Recognize_Players(HARD_TEST);

            Assert.Contains(Hero.TRACER, recognized_players.Select(x => x.hero).ToList());
            Assert.Equals(1, recognized_players.Count(x => Hero.TRACER.Equals(x.hero)));
        }

        [Test, Timeout(100)]
        public void Test_Recognizes_Players()
        {
            var recognized_players = _imageProcessor.Recognize_Players(HARD_TEST);
            
            Assert.NotNull(recognized_players);    
            Assert.AreEqual(12, recognized_players.Count);
            Assert.AreEqual(new List<Player>(
                new []
                {
                    new Player{Name = "CRIZUN", hero = Hero.SOLDIER76, side = Side.OPPONENT},
                    new Player{Name = "ROOPURR", hero = Hero.WIDOWMAKER, side = Side.OPPONENT},
                    new Player{Name = "OREZ", hero = Hero.ROADHOG, side = Side.OPPONENT},
                    new Player{Name = "BUSHEL", hero = Hero.MCCREE, side = Side.OPPONENT},
                    new Player{Name = "FIDELC", hero = Hero.JUNKRAT, side = Side.OPPONENT},
                    new Player{Name = "CRAZYBRAIN", hero = Hero.MERCY, side = Side.OPPONENT},
                    new Player{Name = "VSZM", hero = Hero.ZENYATTA, side = Side.SELF},
                    new Player{Name = "BECSO", hero = Hero.TRACER, side = Side.TEAMMATE},
                    new Player{Name = "PAPASHEEV", hero = Hero.GENJI, side = Side.TEAMMATE},
                    new Player{Name = "S4RGON", hero = Hero.REINHARDT, side = Side.TEAMMATE},
                    new Player{Name = "SCAMAZ", hero = Hero.DVA, side = Side.TEAMMATE},
                    new Player{Name = "ASTAMOR", hero = Hero.HANZO, side = Side.TEAMMATE},
                }
                ), recognized_players);
        }
    }
}

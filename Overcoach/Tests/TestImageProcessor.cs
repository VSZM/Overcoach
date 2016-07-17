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

namespace Overcoach.Tests
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

            Assert.Contains(Hero.TRACER, ally_team.Players.ToList());
            Assert.AreEqual(1, ally_team.Players.Count(x => Hero.TRACER.Equals(x)));
        }

        [Test, Timeout(1000)]
        public void Test_Recognizes_Players()
        {
            TeamComposition ally_team, opponent_team;

            _imageProcessor.Recognize_Players(_hardTest, out ally_team, out opponent_team);

            Assert.NotNull(ally_team);
            Assert.NotNull(opponent_team);

            var expected_allies = new TeamComposition(new List<Hero>(
                new[]
                {
                    Hero.ZENYATTA,
                    Hero.TRACER,
                    Hero.GENJI,
                    Hero.REINHARDT,
                    Hero.DVA,
                    Hero.HANZO
                }));
            var expected_opponents = new TeamComposition(new List<Hero>(
                new[] {
                    Hero.SOLDIER76,
                    Hero.WIDOWMAKER,
                    Hero.ROADHOG,
                    Hero.MCCREE,
                    Hero.JUNKRAT,
                    Hero.MERCY
                }));

            Assert.AreEqual(0, expected_allies.Players.Except(ally_team.Players).Count());
            Assert.AreEqual(0, expected_opponents.Players.Except(opponent_team.Players).Count());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using NUnit.Framework;
using Overcoach.Logic;
using Overcoach.Model;

namespace Overcoach.Tests
{
    [TestFixture]
    public sealed class TestHero : TestBase
    { 

        [Test]
        public void TestHeroValuesAgainstCompositions()
        {
            Assert.AreEqual(-6, Hero.ZENYATTA.ValueAgainstTeam(compoHardSampleOpponentTeam));
            Assert.AreEqual(1, Hero.TRACER.ValueAgainstTeam(compoHardSampleOpponentTeam));
            Assert.AreEqual(6, Hero.GENJI.ValueAgainstTeam(compoHardSampleOpponentTeam));
            Assert.AreEqual(2, Hero.REINHARDT.ValueAgainstTeam(compoHardSampleOpponentTeam));
            Assert.AreEqual(2, Hero.DVA.ValueAgainstTeam(compoHardSampleOpponentTeam));
            Assert.AreEqual(-1, Hero.HANZO.ValueAgainstTeam(compoHardSampleOpponentTeam));

            Assert.AreEqual(0, Hero.SOLDIER76.ValueAgainstTeam(compoHardSampleAllyTeam));
            Assert.AreEqual(-4, Hero.WIDOWMAKER.ValueAgainstTeam(compoHardSampleAllyTeam));
            Assert.AreEqual(3, Hero.ROADHOG.ValueAgainstTeam(compoHardSampleAllyTeam));
            Assert.AreEqual(3, Hero.MCCREE.ValueAgainstTeam(compoHardSampleAllyTeam));
            Assert.AreEqual(1, Hero.JUNKRAT.ValueAgainstTeam(compoHardSampleAllyTeam));
            Assert.AreEqual(-7, Hero.MERCY.ValueAgainstTeam(compoHardSampleAllyTeam));
        }

    }
}

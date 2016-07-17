using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overcoach.Model;
using NUnit.Framework;

namespace Overcoach.Tests
{
    [TestFixture]
    class TestTeamComposition
    {
        private TeamComposition compo2tank2supp1offense1defense = new TeamComposition(
            new[] { Hero.BASTION, Hero.SOLDIER76, Hero.REINHARDT, Hero.WINSTON, Hero.MERCY, Hero.LUCIO });
        private TeamComposition compo6tanks = new TeamComposition(
            new[] { Hero.DVA, Hero.REINHARDT, Hero.ROADHOG, Hero.WINSTON, Hero.ZARYA, Hero.REINHARDT });
        private TeamComposition compoHardSampleOpponentTeam = new TeamComposition(
            new[] { Hero.SOLDIER76, Hero.WIDOWMAKER, Hero.ROADHOG, Hero.MCCREE, Hero.JUNKRAT, Hero.MERCY });
        private TeamComposition compoHardSampleAllyTeam = new TeamComposition(
            new[] { Hero.ZENYATTA, Hero.TRACER, Hero.GENJI, Hero.REINHARDT, Hero.DVA, Hero.HANZO });

        [Test]
        public void TestCompositionContainsDuplicates()
        {
            Assert.IsFalse(compo2tank2supp1offense1defense.ContainsDuplicates);
            Assert.IsTrue(compo6tanks.ContainsDuplicates);
            Assert.IsFalse(compoHardSampleOpponentTeam.ContainsDuplicates);
            Assert.IsFalse(compoHardSampleAllyTeam.ContainsDuplicates);
        }

        [Test]
        public void TestCompositionOffenseCount()
        {
            Assert.AreEqual(1, compo2tank2supp1offense1defense.OffensiveCount);
            Assert.AreEqual(0, compo6tanks.OffensiveCount);
            Assert.AreEqual(2, compoHardSampleOpponentTeam.OffensiveCount);
            Assert.AreEqual(2, compoHardSampleAllyTeam.OffensiveCount);
        }

        [Test]
        public void TestCompositionDefenseCount()
        {
            Assert.AreEqual(1, compo2tank2supp1offense1defense.DefensiveCount);
            Assert.AreEqual(0, compo6tanks.DefensiveCount);
            Assert.AreEqual(2, compoHardSampleOpponentTeam.DefensiveCount);
            Assert.AreEqual(1, compoHardSampleAllyTeam.DefensiveCount);
        }

        [Test]
        public void TestCompositionTankCount()
        {
            Assert.AreEqual(2, compo2tank2supp1offense1defense.TankCount);
            Assert.AreEqual(6, compo6tanks.TankCount);
            Assert.AreEqual(1, compoHardSampleOpponentTeam.TankCount);
            Assert.AreEqual(2, compoHardSampleAllyTeam.TankCount);
        }

        [Test]
        public void TestCompositionSupportCount()
        {
            Assert.AreEqual(2, compo2tank2supp1offense1defense.SupportCount);
            Assert.AreEqual(0, compo6tanks.SupportCount);
            Assert.AreEqual(1, compoHardSampleOpponentTeam.SupportCount);
            Assert.AreEqual(1, compoHardSampleAllyTeam.SupportCount);
        }

    }
}

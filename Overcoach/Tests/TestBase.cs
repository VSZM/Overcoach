using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Overcoach.Model;

namespace Overcoach.Tests
{
    public abstract class TestBase
    {
        protected TeamComposition compo2tank2supp1offense1defense = new TeamComposition(
            new[] { Hero.BASTION, Hero.SOLDIER76, Hero.REINHARDT, Hero.WINSTON, Hero.MERCY, Hero.LUCIO });

        protected TeamComposition compo6tanks = new TeamComposition(
            new[] { Hero.DVA, Hero.REINHARDT, Hero.ROADHOG, Hero.WINSTON, Hero.ZARYA, Hero.REINHARDT });

        protected TeamComposition compoHardSampleOpponentTeam = new TeamComposition(
            new[] { Hero.SOLDIER76, Hero.WIDOWMAKER, Hero.ROADHOG, Hero.MCCREE, Hero.JUNKRAT, Hero.MERCY });

        protected TeamComposition compoHardSampleAllyTeam = new TeamComposition(
            new[] { Hero.ZENYATTA, Hero.TRACER, Hero.GENJI, Hero.REINHARDT, Hero.DVA, Hero.HANZO });


        [SetUp]
        public void SetHeroCounterValues()
        {
            SetupHeroCounterValues();
        }

        private void SetupHeroCounterValues()
        {
            foreach (Hero hero in Hero.AllHeroes)
            {
                hero.CounterValue = new Dictionary<Hero, int>();
            }

            Hero.BASTION.CounterValue[Hero.ZENYATTA] = 2;
            Hero.BASTION.CounterValue[Hero.TRACER] = -1;
            Hero.BASTION.CounterValue[Hero.GENJI] = -1;
            Hero.BASTION.CounterValue[Hero.REINHARDT] = 2;
            Hero.BASTION.CounterValue[Hero.DVA] = 0;
            Hero.BASTION.CounterValue[Hero.HANZO] = 0;

            Hero.WINSTON.CounterValue[Hero.ZENYATTA] = -1;
            Hero.WINSTON.CounterValue[Hero.TRACER] = 1;
            Hero.WINSTON.CounterValue[Hero.GENJI] = 1;
            Hero.WINSTON.CounterValue[Hero.REINHARDT] = 1;
            Hero.WINSTON.CounterValue[Hero.DVA] = 0;
            Hero.WINSTON.CounterValue[Hero.HANZO] = -1;

            Hero.LUCIO.CounterValue[Hero.ZENYATTA] = 1;
            Hero.LUCIO.CounterValue[Hero.TRACER] = 0;
            Hero.LUCIO.CounterValue[Hero.GENJI] = 0;
            Hero.LUCIO.CounterValue[Hero.REINHARDT] = 0;
            Hero.LUCIO.CounterValue[Hero.DVA] = 0;
            Hero.LUCIO.CounterValue[Hero.HANZO] = -1;

            Hero.ZARYA.CounterValue[Hero.ZENYATTA] = -1;
            Hero.ZARYA.CounterValue[Hero.TRACER] = 0;
            Hero.ZARYA.CounterValue[Hero.GENJI] = 1;
            Hero.ZARYA.CounterValue[Hero.REINHARDT] = -1;
            Hero.ZARYA.CounterValue[Hero.DVA] = 1;
            Hero.ZARYA.CounterValue[Hero.HANZO] = 1;

            // Enemy team
            Hero.SOLDIER76.CounterValue[Hero.ZENYATTA] = 2;
            Hero.SOLDIER76.CounterValue[Hero.TRACER] = 0;
            Hero.SOLDIER76.CounterValue[Hero.GENJI] = -2;
            Hero.SOLDIER76.CounterValue[Hero.REINHARDT] = -2;
            Hero.SOLDIER76.CounterValue[Hero.DVA] = 0;
            Hero.SOLDIER76.CounterValue[Hero.HANZO] = 2;

            Hero.WIDOWMAKER.CounterValue[Hero.ZENYATTA] = 2;
            Hero.WIDOWMAKER.CounterValue[Hero.TRACER] = -2;
            Hero.WIDOWMAKER.CounterValue[Hero.GENJI] = -2;
            Hero.WIDOWMAKER.CounterValue[Hero.REINHARDT] = -1;
            Hero.WIDOWMAKER.CounterValue[Hero.DVA] = -2;
            Hero.WIDOWMAKER.CounterValue[Hero.HANZO] = 1;

            Hero.ROADHOG.CounterValue[Hero.ZENYATTA] = 0;
            Hero.ROADHOG.CounterValue[Hero.TRACER] = 1;
            Hero.ROADHOG.CounterValue[Hero.GENJI] = 1;
            Hero.ROADHOG.CounterValue[Hero.REINHARDT] = 0;
            Hero.ROADHOG.CounterValue[Hero.DVA] = 0;
            Hero.ROADHOG.CounterValue[Hero.HANZO] = 1;

            Hero.MCCREE.CounterValue[Hero.ZENYATTA] = 2;
            Hero.MCCREE.CounterValue[Hero.TRACER] = 2;
            Hero.MCCREE.CounterValue[Hero.GENJI] = -1;
            Hero.MCCREE.CounterValue[Hero.REINHARDT] = 0;
            Hero.MCCREE.CounterValue[Hero.DVA] = 0;
            Hero.MCCREE.CounterValue[Hero.HANZO] = 0;

            Hero.JUNKRAT.CounterValue[Hero.ZENYATTA] = 1;
            Hero.JUNKRAT.CounterValue[Hero.TRACER] = -1;
            Hero.JUNKRAT.CounterValue[Hero.GENJI] = -1;
            Hero.JUNKRAT.CounterValue[Hero.REINHARDT] = 2;
            Hero.JUNKRAT.CounterValue[Hero.DVA] = 1;
            Hero.JUNKRAT.CounterValue[Hero.HANZO] = -1;

            Hero.MERCY.CounterValue[Hero.ZENYATTA] = -1;
            Hero.MERCY.CounterValue[Hero.TRACER] = -1;
            Hero.MERCY.CounterValue[Hero.GENJI] = -1;
            Hero.MERCY.CounterValue[Hero.REINHARDT] = -1;
            Hero.MERCY.CounterValue[Hero.DVA] = -1;
            Hero.MERCY.CounterValue[Hero.HANZO] = -2;


            // Ally team (symmetric values)
            Hero.ZENYATTA.CounterValue[Hero.SOLDIER76] = -2;
            Hero.ZENYATTA.CounterValue[Hero.WIDOWMAKER] = -2;
            Hero.ZENYATTA.CounterValue[Hero.ROADHOG] = 0;
            Hero.ZENYATTA.CounterValue[Hero.MCCREE] = -2;
            Hero.ZENYATTA.CounterValue[Hero.JUNKRAT] = -1;
            Hero.ZENYATTA.CounterValue[Hero.MERCY] = 1;
            Hero.ZENYATTA.CounterValue[Hero.BASTION] = -2;
            Hero.ZENYATTA.CounterValue[Hero.REINHARDT] = 1;
            Hero.ZENYATTA.CounterValue[Hero.WINSTON] = 1;
            Hero.ZENYATTA.CounterValue[Hero.LUCIO] = -1;
            Hero.ZENYATTA.CounterValue[Hero.ZENYATTA] = 0;
            Hero.ZENYATTA.CounterValue[Hero.TRACER] = -1;
            Hero.ZENYATTA.CounterValue[Hero.GENJI] = -1;
            Hero.ZENYATTA.CounterValue[Hero.REINHARDT] = 1;
            Hero.ZENYATTA.CounterValue[Hero.DVA] = 1;
            Hero.ZENYATTA.CounterValue[Hero.HANZO] = -2;
            Hero.ZENYATTA.CounterValue[Hero.ZARYA] = 1;


            Hero.TRACER.CounterValue[Hero.SOLDIER76] = 0;
            Hero.TRACER.CounterValue[Hero.WIDOWMAKER] = 2;
            Hero.TRACER.CounterValue[Hero.ROADHOG] = -1;
            Hero.TRACER.CounterValue[Hero.MCCREE] = -2;
            Hero.TRACER.CounterValue[Hero.JUNKRAT] = 1;
            Hero.TRACER.CounterValue[Hero.MERCY] = 1;
            Hero.TRACER.CounterValue[Hero.BASTION] = 1;
            Hero.TRACER.CounterValue[Hero.REINHARDT] = 1;
            Hero.TRACER.CounterValue[Hero.WINSTON] = -1;
            Hero.TRACER.CounterValue[Hero.LUCIO] = 0;
            Hero.TRACER.CounterValue[Hero.ZENYATTA] = 1;
            Hero.TRACER.CounterValue[Hero.TRACER] = 0;
            Hero.TRACER.CounterValue[Hero.GENJI] = 0;
            Hero.TRACER.CounterValue[Hero.REINHARDT] = 1;
            Hero.TRACER.CounterValue[Hero.DVA] = 1;
            Hero.TRACER.CounterValue[Hero.HANZO] = -1;
            Hero.TRACER.CounterValue[Hero.ZARYA] = 0;

            Hero.GENJI.CounterValue[Hero.SOLDIER76] = 2;
            Hero.GENJI.CounterValue[Hero.WIDOWMAKER] = 2;
            Hero.GENJI.CounterValue[Hero.ROADHOG] = -1;
            Hero.GENJI.CounterValue[Hero.MCCREE] = 1;
            Hero.GENJI.CounterValue[Hero.JUNKRAT] = 1;
            Hero.GENJI.CounterValue[Hero.MERCY] = 1;
            Hero.GENJI.CounterValue[Hero.BASTION] = 1;
            Hero.GENJI.CounterValue[Hero.REINHARDT] = 1;
            Hero.GENJI.CounterValue[Hero.WINSTON] = -1;
            Hero.GENJI.CounterValue[Hero.LUCIO] = 0;
            Hero.GENJI.CounterValue[Hero.ZENYATTA] = 1;
            Hero.GENJI.CounterValue[Hero.TRACER] = 0;
            Hero.GENJI.CounterValue[Hero.GENJI] = 0;
            Hero.GENJI.CounterValue[Hero.REINHARDT] = 0;
            Hero.GENJI.CounterValue[Hero.DVA] = 0;
            Hero.GENJI.CounterValue[Hero.HANZO] = 1;
            Hero.GENJI.CounterValue[Hero.ZARYA] = -1;


            Hero.REINHARDT.CounterValue[Hero.SOLDIER76] = 2;
            Hero.REINHARDT.CounterValue[Hero.WIDOWMAKER] = 1;
            Hero.REINHARDT.CounterValue[Hero.ROADHOG] = 0;
            Hero.REINHARDT.CounterValue[Hero.MCCREE] = 0;
            Hero.REINHARDT.CounterValue[Hero.JUNKRAT] = -2;
            Hero.REINHARDT.CounterValue[Hero.MERCY] = 1;
            Hero.REINHARDT.CounterValue[Hero.BASTION] = -2;
            Hero.REINHARDT.CounterValue[Hero.ZENYATTA] = -1;
            Hero.REINHARDT.CounterValue[Hero.TRACER] = -1;
            Hero.REINHARDT.CounterValue[Hero.GENJI] = -1;
            Hero.REINHARDT.CounterValue[Hero.DVA] = 2;
            Hero.REINHARDT.CounterValue[Hero.HANZO] = -1;
            Hero.REINHARDT.CounterValue[Hero.REINHARDT] = 0;
            Hero.REINHARDT.CounterValue[Hero.WINSTON] = -1;
            Hero.REINHARDT.CounterValue[Hero.LUCIO] = 0;
            Hero.REINHARDT.CounterValue[Hero.ZENYATTA] = -1;
            Hero.REINHARDT.CounterValue[Hero.TRACER] = -1;
            Hero.REINHARDT.CounterValue[Hero.GENJI] = 0;
            Hero.REINHARDT.CounterValue[Hero.ZARYA] = 1;


            Hero.DVA.CounterValue[Hero.SOLDIER76] = 0;
            Hero.DVA.CounterValue[Hero.WIDOWMAKER] = 2;
            Hero.DVA.CounterValue[Hero.ROADHOG] = 0;
            Hero.DVA.CounterValue[Hero.MCCREE] = 0;
            Hero.DVA.CounterValue[Hero.JUNKRAT] = -1;
            Hero.DVA.CounterValue[Hero.MERCY] = 1;
            Hero.DVA.CounterValue[Hero.BASTION] = 0;
            Hero.DVA.CounterValue[Hero.REINHARDT] = -2;
            Hero.DVA.CounterValue[Hero.WINSTON] = 0;
            Hero.DVA.CounterValue[Hero.LUCIO] = 0;
            Hero.DVA.CounterValue[Hero.ZENYATTA] = -1;
            Hero.DVA.CounterValue[Hero.TRACER] = -1;
            Hero.DVA.CounterValue[Hero.GENJI] = 0;
            Hero.DVA.CounterValue[Hero.DVA] = 0;
            Hero.DVA.CounterValue[Hero.HANZO] = 1;
            Hero.DVA.CounterValue[Hero.ZARYA] = -1;


            Hero.HANZO.CounterValue[Hero.SOLDIER76] = -2;
            Hero.HANZO.CounterValue[Hero.WIDOWMAKER] = -1;
            Hero.HANZO.CounterValue[Hero.ROADHOG] = -1;
            Hero.HANZO.CounterValue[Hero.MCCREE] = 0;
            Hero.HANZO.CounterValue[Hero.JUNKRAT] = 1;
            Hero.HANZO.CounterValue[Hero.MERCY] = 2;
            Hero.HANZO.CounterValue[Hero.BASTION] = 0;
            Hero.HANZO.CounterValue[Hero.REINHARDT] = 1;
            Hero.HANZO.CounterValue[Hero.WINSTON] = 1;
            Hero.HANZO.CounterValue[Hero.LUCIO] = 1;
            Hero.HANZO.CounterValue[Hero.ZENYATTA] = 2;
            Hero.HANZO.CounterValue[Hero.TRACER] = 1;
            Hero.HANZO.CounterValue[Hero.GENJI] = -1;
            Hero.HANZO.CounterValue[Hero.DVA] = -1;
            Hero.HANZO.CounterValue[Hero.HANZO] = 0;
            Hero.HANZO.CounterValue[Hero.ZARYA] = -1;

            // Assert we have filled the dataset well enough
            Hero.AllHeroes.ForEach(hero =>
            {
                Assert.IsEmpty(hero.CounterValue.Keys.Where(counter =>
                    !counter.CounterValue.ContainsKey(hero) || counter.CounterValue[hero] != -1 * hero.CounterValue[counter])
                    , "Counters are not symmetric for: " + hero.Name);
            });
        }
    }
}

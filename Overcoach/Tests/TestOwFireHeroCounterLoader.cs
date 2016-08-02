using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using NUnit.Framework;
using Overcoach.Logic;
using Overcoach.Model;

namespace Overcoach.Tests
{
    [TestFixture]
    public class TestOwFireHeroCounterLoader
    {


        [SetUp]
        public void Initialize()
        {
            var counter_loader = new OWFireHeroCounterLoader();
            counter_loader.SetHeroCounterValues();
        }

        [Test]
        public void TestAllIsInitialized()
        {
            Assert.IsEmpty(Hero.AllHeroes.Where(hero => hero.CounterValue.Count == 0));
            foreach (Hero hero in Hero.AllHeroes)
            {
                foreach (Hero counter in hero.CounterValue.Keys)
                {
                    Assert.AreEqual(hero.CounterValue[counter], counter.CounterValue[hero] * -1);
                }
            }
        }

        [Test]
        public void TestSomeSpecificValues()
        {
            Assert.AreEqual(Hero.SOLDIER76.CounterValue[Hero.PHARAH], 636);
        }
    }
}

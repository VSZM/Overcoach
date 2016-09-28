using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics;
using NUnit.Framework;
using Overcoach.Logic;
using Overcoach.Model;

namespace Overcoach.Tests
{
    [TestFixture]
    public sealed class TestCompositionFinder : TestBase
    {
        private List<TeamComposition> everyComposition;

        [SetUp]
        public void Initialize()
        {
            everyComposition = CompositionFinder.EveryPossibleComposition;
        }

        [TearDown]
        public void Finalize()
        {
            CompositionFinder.EveryPossibleComposition = everyComposition;
        }

        [Test]
        public void TestEveryPossibleCompositionCount()
        {
            Assert.AreEqual(
                Combinatorics.CombinationsWithRepetition(Hero.AllHeroes.Count, 6),
                CompositionFinder.EveryPossibleComposition.Count
                );
        }

        [Test]
        public void TestOrderCounterCompositions()
        {
            CompositionFinder.EveryPossibleComposition = new List<TeamComposition>(new[]
            {
                compo2tank2supp1offense1defense,
                compoHardSampleAllyTeam,
                compo6tanks,
                compoHardSampleOpponentTeam
            });

            var compositionFinder = new CompositionFinder.CompositionFinderBuilder().Build();
            var dict =  new Dictionary<TeamComposition, int>();

            foreach (var compo in CompositionFinder.EveryPossibleComposition)
            {
                dict[compo] = compo.Value_Against(compoHardSampleAllyTeam);
            }



            var order = compositionFinder.OrderCounterCompositions(compoHardSampleAllyTeam);
            Assert.AreEqual(compo2tank2supp1offense1defense, order.Last());
            CollectionAssert.DoesNotContain(order, compo6tanks); // Max number of tanks is 4 in config
            Assert.AreEqual(new List<TeamComposition>(
                new []
                {
                    compoHardSampleAllyTeam, compoHardSampleOpponentTeam, compo2tank2supp1offense1defense
                }), order);    
        }

    }
}

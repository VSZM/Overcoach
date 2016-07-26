using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using Overcoach.Model;

namespace Overcoach.Logic
{
    public class CompositionFinder
    {
        public static List<TeamComposition> EveryPossibleComposition;

        static CompositionFinder()
        {
            EveryPossibleComposition =
                Hero.AllHeroes.CombinationsWithRepition(6).Select(list_of_heroes => new TeamComposition(list_of_heroes)).ToList();
        }

        public int OffensiveCountMin { get; set; }
        public int OffensiveCountMax { get; set; }
        public int DefensiveCountMin { get; set; }
        public int DefensiveCountMax { get; set; }
        public int TankCountMin { get; set; }
        public int TankCountMax { get; set; }
        public int SupportCountMin { get; set; }
        public int SupportCountMax { get; set; }
        public bool AreDuplicatesAllowed { get; set; }
        private readonly List<TeamComposition> _constrainedCompositions;

        private CompositionFinder(int offensiveCountMin, int offensiveCountMax, int defensiveCountMin, int defensiveCountMax, int tankCountMin, int tankCountMax, int supportCountMin, int supportCountMax, bool are_duplicates_allowed)
        {
            OffensiveCountMin = offensiveCountMin;
            OffensiveCountMax = offensiveCountMax;
            DefensiveCountMin = defensiveCountMin;
            DefensiveCountMax = defensiveCountMax;
            TankCountMin = tankCountMin;
            TankCountMax = tankCountMax;
            SupportCountMin = supportCountMin;
            SupportCountMax = supportCountMax;
            AreDuplicatesAllowed = are_duplicates_allowed;
            _constrainedCompositions = GenerateValidCompositions();
        }

        private List<TeamComposition> GenerateValidCompositions()
        {
            return EveryPossibleComposition.Where(teamcompo => teamcompo.OffensiveCount >= OffensiveCountMin && teamcompo.OffensiveCount <= OffensiveCountMax &&
                                                               teamcompo.DefensiveCount >= DefensiveCountMin && teamcompo.DefensiveCount <= DefensiveCountMax &&
                                                               teamcompo.TankCount >= TankCountMin && teamcompo.TankCount <= TankCountMax &&
                                                               teamcompo.SupportCount >= SupportCountMin && teamcompo.SupportCount <= SupportCountMax &&
                                                               AreDuplicatesAllowed ? true : !teamcompo.ContainsDuplicates).ToList();
        }

        public List<TeamComposition> OrderCounterCompositions(TeamComposition enemy)
        {
            return _constrainedCompositions.OrderByDescending(compositon => compositon.Value_Against(enemy)).ToList();
        }

        public class CompositionFinderBuilder
        {
            private int _offensiveCountMin = 0;
            private int _offensiveCountMax = 4;
            private int _defensiveCountMin = 0;
            private int _defensiveCountMax = 4;
            private int _tankCountMin = 1;
            private int _tankCountMax = 4;
            private int _supportCountMin = 1;
            private int _supportCountMax = 3;
            private bool _areDuplicatesAllowed = true;

            public CompositionFinderBuilder OffensiveCountMin(int offensive_count_min)
            {
                _offensiveCountMin = offensive_count_min;
                return this;
            }

            public CompositionFinderBuilder OffensiveCountMax(int offensive_count_max)
            {
                _offensiveCountMax = offensive_count_max;
                return this;
            }

            public CompositionFinderBuilder DefensiveCountMin(int defensive_count_min)
            {
                _defensiveCountMin = defensive_count_min;
                return this;
            }

            public CompositionFinderBuilder DefensiveCountMax(int defensive_count_max)
            {
                _defensiveCountMax = defensive_count_max;
                return this;
            }

            public CompositionFinderBuilder TankCountMin(int tank_count_min)
            {
                _tankCountMin = tank_count_min;
                return this;
            }

            public CompositionFinderBuilder TankCountMax(int tank_count_max)
            {
                _tankCountMax = tank_count_max;
                return this;
            }

            public CompositionFinderBuilder SupportCountMin(int support_count_min)
            {
                _supportCountMin = support_count_min;
                return this;
            }

            public CompositionFinderBuilder SupportCountMax(int support_count_max)
            {
                _supportCountMax = support_count_max;
                return this;
            }

            public CompositionFinderBuilder AreDuplicatesAllowed(bool are_duplicates_allowed)
            {
                _areDuplicatesAllowed = are_duplicates_allowed;
                return this;
            }

            public CompositionFinder Build()
            {
                return new CompositionFinder(_offensiveCountMin, _offensiveCountMax, _defensiveCountMin, _defensiveCountMax, _tankCountMin, _tankCountMax, _supportCountMin, _supportCountMax, _areDuplicatesAllowed);
            }
        }
    }
}

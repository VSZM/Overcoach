using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using Overcoach.Model;

namespace Overcoach.Logic
{
    class CompositionFinder
    {
        private static readonly List<TeamComposition> EveryPossibleComposition;

        static CompositionFinder()
        {
            EveryPossibleComposition =
                Hero.AllHeroes.Combinations(6).Select(list_of_heroes => new TeamComposition(list_of_heroes)).ToList();
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
        private List<TeamComposition> _constrainedCompositions; 

        public CompositionFinder(int offensiveCountMin, int offensiveCountMax, int defensiveCountMin, int defensiveCountMax, int tankCountMin, int tankCountMax, int supportCountMin, int supportCountMax)
        {
            OffensiveCountMin = offensiveCountMin;
            OffensiveCountMax = offensiveCountMax;
            DefensiveCountMin = defensiveCountMin;
            DefensiveCountMax = defensiveCountMax;
            TankCountMin = tankCountMin;
            TankCountMax = tankCountMax;
            SupportCountMin = supportCountMin;
            SupportCountMax = supportCountMax;
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
    }
}

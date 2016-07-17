using System.Collections.Generic;
using System.Linq;
using System;

namespace Overcoach.Model
{
    public class TeamComposition
    {
        public IReadOnlyCollection<Hero> Players;
        public int OffensiveCount { get; private set; }
        public int DefensiveCount { get; private set; }
        public int TankCount { get; private set; }
        public int SupportCount { get; private set; }
        public bool ContainsDuplicates { get; private set; }


        public TeamComposition(IEnumerable<Hero> players)
        {
            if (players.Count() != 6)
                throw new ArgumentException("A TeamComposition must be initialized with 6 heroes");

            Players = players.ToList().AsReadOnly();
            OffensiveCount = players.Count(hero => hero.HeroRole == HeroRole.OFFENSIVE);
            DefensiveCount = players.Count(hero => hero.HeroRole == HeroRole.DEFENSIVE);
            TankCount = players.Count(hero => hero.HeroRole == HeroRole.TANK);
            SupportCount = players.Count(hero => hero.HeroRole == HeroRole.SUPPORT);
            ContainsDuplicates = players.Count() != new HashSet<Hero>(players).Count;
        }

        public int Value_Against(TeamComposition enemy)
        {
            return Players.Sum(player => player.ValueAgainstTeam(enemy));
        }
    }
}

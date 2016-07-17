using System.Collections.Generic;
using System.Linq;

namespace Overcoach.Model
{
    public class Hero
    {
        public static readonly Hero NONE = new Hero("NONE", HeroRole.NONE);
        public static readonly Hero BASTION = new Hero("BASTION", HeroRole.DEFENSIVE);
        public static readonly Hero DVA = new Hero("DVA", HeroRole.TANK);
        public static readonly Hero GENJI = new Hero("GENJI", HeroRole.OFFENSIVE);
        public static readonly Hero HANZO = new Hero("HANZO", HeroRole.DEFENSIVE);
        public static readonly Hero JUNKRAT = new Hero("JUNKRAT", HeroRole.DEFENSIVE);
        public static readonly Hero LUCIO = new Hero("LUCIO", HeroRole.SUPPORT);
        public static readonly Hero MCCREE = new Hero("MCCREE", HeroRole.OFFENSIVE);
        public static readonly Hero MEI = new Hero("MEI", HeroRole.DEFENSIVE);
        public static readonly Hero MERCY = new Hero("MERCY", HeroRole.SUPPORT);
        public static readonly Hero PHARAH = new Hero("PHARAH", HeroRole.OFFENSIVE);
        public static readonly Hero REAPER = new Hero("REAPER", HeroRole.OFFENSIVE);
        public static readonly Hero REINHARDT = new Hero("REINHARDT", HeroRole.TANK);
        public static readonly Hero ROADHOG = new Hero("ROADHOG", HeroRole.TANK);
        public static readonly Hero SOLDIER76 = new Hero("SOLDIER76", HeroRole.OFFENSIVE);
        public static readonly Hero SYMMETRA = new Hero("SYMMETRA", HeroRole.SUPPORT);
        public static readonly Hero TORBJORN = new Hero("TORBJORN", HeroRole.DEFENSIVE);
        public static readonly Hero TRACER = new Hero("TRACER", HeroRole.OFFENSIVE);
        public static readonly Hero WIDOWMAKER = new Hero("WIDOWMAKER", HeroRole.DEFENSIVE);
        public static readonly Hero WINSTON = new Hero("WINSTON", HeroRole.TANK);
        public static readonly Hero ZARYA = new Hero("ZARYA", HeroRole.TANK);
        public static readonly Hero ZENYATTA = new Hero("ZENYATTA", HeroRole.SUPPORT);
        public static List<Hero> AllHeroes = new List<Hero>(new [] { BASTION, DVA, GENJI, HANZO, JUNKRAT,
            LUCIO, MCCREE, MEI, MERCY, PHARAH, REAPER, REINHARDT, ROADHOG, SOLDIER76,
            SYMMETRA, TORBJORN, TRACER, WIDOWMAKER, WINSTON, ZARYA, ZENYATTA });


        public static Dictionary<string, Hero> HeroByName = new Dictionary<string, Hero>(AllHeroes.Count);

        static Hero()
        {
            foreach (var hero in AllHeroes)
            {
                HeroByName.Add(hero.Name, hero);
            }
        }

        /// <summary>
        /// How good is this hero against the key?
        /// </summary>
        public Dictionary<Hero, int> CounterValue { get;private set; }

        public string Name { get; private set; }
        public HeroRole HeroRole { get; private set; }

        private Hero(string name, HeroRole heroRole)
        {
            Name = name;
            HeroRole = heroRole;
            CounterValue = new Dictionary<Hero, int>(25);
        }

        public int ValueAgainstTeam(TeamComposition enemy)
        {
            return enemy.Players.Sum(enemy_hero => CounterValue[enemy_hero]);
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public enum HeroRole
    {
        NONE,
        OFFENSIVE,
        DEFENSIVE,
        TANK,
        SUPPORT
    }

}

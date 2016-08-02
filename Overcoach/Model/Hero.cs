using System;
using System.Collections.Generic;
using System.Linq;

namespace Overcoach.Model
{
    public class Hero
    {
        public static readonly Hero NONE = new Hero("NONE", "NONE", HeroRole.NONE);
        public static readonly Hero ANA = new Hero("ANA", "Ana", HeroRole.SUPPORT);
        public static readonly Hero BASTION = new Hero("BASTION", "Bastion", HeroRole.DEFENSIVE);
        public static readonly Hero DVA = new Hero("DVA", "D.Va", HeroRole.TANK);
        public static readonly Hero GENJI = new Hero("GENJI", "Genji", HeroRole.OFFENSIVE);
        public static readonly Hero HANZO = new Hero("HANZO", "Hanzo", HeroRole.DEFENSIVE);
        public static readonly Hero JUNKRAT = new Hero("JUNKRAT", "Junkrat", HeroRole.DEFENSIVE);
        public static readonly Hero LUCIO = new Hero("LUCIO", "Lúcio", HeroRole.SUPPORT);
        public static readonly Hero MCCREE = new Hero("MCCREE", "McCree", HeroRole.OFFENSIVE);
        public static readonly Hero MEI = new Hero("MEI", "Mei", HeroRole.DEFENSIVE);
        public static readonly Hero MERCY = new Hero("MERCY", "Mercy", HeroRole.SUPPORT);
        public static readonly Hero PHARAH = new Hero("PHARAH", "Pharah", HeroRole.OFFENSIVE);
        public static readonly Hero REAPER = new Hero("REAPER", "Reaper", HeroRole.OFFENSIVE);
        public static readonly Hero REINHARDT = new Hero("REINHARDT", "Reinhardt", HeroRole.TANK);
        public static readonly Hero ROADHOG = new Hero("ROADHOG", "Roadhog", HeroRole.TANK);
        public static readonly Hero SOLDIER76 = new Hero("SOLDIER76", "Soldier: 76", HeroRole.OFFENSIVE);
        public static readonly Hero SYMMETRA = new Hero("SYMMETRA", "Symmetra", HeroRole.SUPPORT);
        public static readonly Hero TORBJORN = new Hero("TORBJORN", "Torbjörn", HeroRole.DEFENSIVE);
        public static readonly Hero TRACER = new Hero("TRACER", "Tracer", HeroRole.OFFENSIVE);
        public static readonly Hero WIDOWMAKER = new Hero("WIDOWMAKER", "Widowmaker", HeroRole.DEFENSIVE);
        public static readonly Hero WINSTON = new Hero("WINSTON", "Winston", HeroRole.TANK);
        public static readonly Hero ZARYA = new Hero("ZARYA", "Zarya", HeroRole.TANK);
        public static readonly Hero ZENYATTA = new Hero("ZENYATTA", "Zenyatta", HeroRole.SUPPORT);
        public static List<Hero> AllHeroes = new List<Hero>(new [] { ANA, BASTION, DVA, GENJI, HANZO, JUNKRAT,
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
        public Dictionary<Hero, int> CounterValue { get; set; }

        public string Name { get; private set; }
        public string PrettyName { get; private set; }
        public HeroRole HeroRole { get; private set; }

        private Hero(string name, string prettyName, HeroRole heroRole)
        {
            Name = name;
            HeroRole = heroRole;
            PrettyName = prettyName;
            CounterValue = new Dictionary<Hero, int>(25);
        }

        public int ValueAgainstTeam(TeamComposition enemy)
        {
            return enemy.Players.Sum(enemy_hero =>
            {
                if (CounterValue.ContainsKey(enemy_hero))
                    return CounterValue[enemy_hero];

                throw new InvalidOperationException("Counter value missing for |" + this + "| Against: |" + enemy_hero + "|");
            });
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

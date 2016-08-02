using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Overcoach.Model;

namespace Overcoach.Logic
{
    public class OWFireHeroCounterLoader
    {
        private const string base_uri = "http://www.owfire.com/overwatch/wiki/heroes/";
        private const int scale = 10;

        private string HeroToURI(Hero hero)
        {
            if (Hero.SOLDIER76.Equals(hero))
                return base_uri + "soldier-76";

            return base_uri + hero.Name.ToLower();
        }

        private Dictionary<Hero, HtmlNode> HeroPages = new Dictionary<Hero, HtmlNode>(Hero.AllHeroes.Count);

        public OWFireHeroCounterLoader()
        {
            HtmlWeb web = new HtmlWeb();
            var options = new ParallelOptions();
            options.MaxDegreeOfParallelism = 8;
            Parallel.ForEach(Hero.AllHeroes, options,
                hero =>
                {
                    var page = web.Load(HeroToURI(hero));
                    var root = page.GetElementbyId("counter-lists");
                    root.Descendants()
                     .Where(n => n.NodeType == HtmlNodeType.Comment || n.NodeType == HtmlNodeType.Text && string.IsNullOrWhiteSpace(n.InnerText))
                     .ToList()
                     .ForEach(n => n.Remove());
                    HeroPages[hero] = root;
                });
        }

        public void SetHeroCounterValues()
        {
            foreach (Hero hero in Hero.AllHeroes)
            {
                foreach (Hero vshero in Hero.AllHeroes)
                {
                    SetMatchupValue(hero, vshero);
                }
            }
        }

        private void SetMatchupValue(Hero hero, Hero against)
        {
            int value, value_mirror;

            if (hero.Equals(against))
                value = value_mirror = 0;
            else
            {
                var counter_node = HeroPages[hero].FirstChild.Descendants().First(
                        node => 
                            !string.IsNullOrWhiteSpace(node.InnerText) && against.PrettyName.Equals(node.InnerText));


                value = int.Parse(counter_node.NextSibling.InnerText) -
                        int.Parse(counter_node.NextSibling.NextSibling.InnerText);

                var mirror_node = HeroPages[against].FirstChild.Descendants()
                    .First(node => 
                    !string.IsNullOrWhiteSpace(node.InnerText) && hero.PrettyName.Equals(node.InnerText));

                value_mirror = int.Parse(mirror_node.NextSibling.InnerText) -
                               int.Parse(mirror_node.NextSibling.NextSibling.InnerText);
            }

            hero.CounterValue[against] = value - value_mirror;
        }
    }
}

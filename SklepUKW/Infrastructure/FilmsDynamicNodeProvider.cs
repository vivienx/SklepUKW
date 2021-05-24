using MvcSiteMapProvider;
using SklepUKW.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SklepUKW.Infrastructure
{
    public class FilmsDynamicNodeProvider : DynamicNodeProviderBase
    {
        FilmsContext db = new FilmsContext();
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            var filmsDN = new List<DynamicNode>();
            //przeszukiwanie bazy danych pod katem  filmow
            //dostep do bazy danych w kontrolerach poprzez klase films context

            foreach (var film in db.Films)
            {
                DynamicNode dn = new DynamicNode()
                {
                    Title = film.Title,//tytul dynamic node. to sa parametry dla node z Mvc.sitemap
                    Key = "Film_" + film.FilmId, //nazwa wlasna filmu
                    ParentKey = "Category_" + film.CategoryId//rodzicem filmu jest kategoria. w filmie jest klucz obcy idkategorii categoryid
                };
                dn.RouteValues.Add("id", film.FilmId); //dla akcji details przyjmujemy id konkretnego filmu

                //node trzeba wrzucic do listy node'ow
                filmsDN.Add(dn); //wrzucamy tego node
            }

            //po wyjsciu z foreach trzeba zwrocic
            return filmsDN; //node fla Filmow. jeszcze node dla kategorii
        }
    }
}
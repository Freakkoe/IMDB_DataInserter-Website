using imdb_app.Models;
using IMDB_Website.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace IMDB_Website.Services
{
    public class Service : Interface
    {

        private readonly imdbContext context;

        public Service(imdbContext service)
        {
            context = service;
        }

        #region Title Services
        public List<Title> GetTopTitles(int amount)
        {
            return context.Titles.Include(t => t.Genres).OrderByDescending(t => t.Tconst).Take(amount).ToList();
        }

        public Title FindTitleByTconst(string tconst)
        {
            return context.Titles.Where(t => t.Tconst == tconst).Include(t => t.Genres).FirstOrDefault() ?? throw new Exception();
        }

        public List<Title> SearchTitles(string criteria)
        {
            Task<List<findTitleResult>> task = context.Procedures.findTitleAsync(criteria);
            task.Wait();
            List<findTitleResult> resultList = task.Result;

            List<Title> list = TransformItems<findTitleResult, Title>(
                resultList, t => new Title
                {
                    Tconst = t.tconst,
                    TitleType = t.titleType,
                    PrimaryTitle = t.primaryTitle,
                    OriginalTitle = t.originalTitle,
                    IsAdult = t.isAdult,
                    StartYear = t.startYear,
                    EndYear = t.endYear,
                    RuntimeMinutes = t.runtimeMinutes
                });

            return list;
        }

        public void AddTitle(Title title, string genres)
        {
            context.Procedures.addTitleAsync(
                title.TitleType,
                title.PrimaryTitle,
                title.OriginalTitle,
                title.IsAdult,
                title.StartYear,
                title.EndYear,
                title.RuntimeMinutes,
                genres).Wait();
        }

        public void UpdateTitle(Title title, string genres)
        {
            context.Procedures.updateTitleAsync(
                title.Tconst,
                title.TitleType,
                title.PrimaryTitle,
                title.OriginalTitle,
                title.IsAdult,
                title.StartYear,
                title.EndYear,
                title.RuntimeMinutes,
                genre: genres
                ).Wait();
        }

        public void DeleteTitle(string tconst)
        {
            context.Procedures.deleteTitleAsync(tconst).Wait();
        }
        #endregion

        #region Name Services
        public void AddName(Name name, string professions)
        {
            context.Procedures.addNameAsync(
                name.PrimaryName,
                name.BirthYear,
                name.DeathYear,
                professions).Wait();
        }

        public List<Name> GetTopNames(int amount)
        {
            return context.Names.Include(n => n.Professions).OrderByDescending(n => n.Nconst).Take(amount).ToList();
        }

        public List<Name> SearchNames(string criteria)
        {
            Task<List<findNameResult>> task = context.Procedures.findNameAsync(criteria);
            task.Wait();
            List<findNameResult> resultList = task.Result;

            List<Name> list = TransformItems<findNameResult, Name>(
                resultList, n => new Name
                {
                    Nconst = n.nconst,
                    PrimaryName = n.primaryName,
                    BirthYear = n.birthYear,
                    DeathYear = n.deathYear
                });

            return list;
        }
        #endregion

        #region Generic
        private static List<V> TransformItems<T, V>(List<T> items, Func<T, V> transformer)
        {
            List<V> transformedItems = new();
            foreach (T item in items)
            {
                V transformedItem = transformer(item);
                transformedItems.Add(transformedItem);
            }

            return transformedItems;
        }
        #endregion
    }
}

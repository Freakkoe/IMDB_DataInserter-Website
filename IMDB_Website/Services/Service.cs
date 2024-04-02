using imdb_app.Models;
using IMDB_Website.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace IMDB_Website.Services
{
    public class Service : Interface
    {
        // Context for interacting with the database
        private readonly imdbContext context;

        // Constructor to initialize the Service with the provided imdbContext
        public Service(imdbContext service)
        {
            context = service;
        }

        // Method to retrieve top titles from the database
        public List<Title> GetTopTitles(int amount)
        {
            // Retrieve top titles including associated genres, ordered by Tconst, and take specified amount
            return context.Titles.Include(t => t.Genres).OrderByDescending(t => t.Tconst).Take(amount).ToList();
        }

        // Method to find a title by its Tconst
        public Title FindTitleByTconst(string tconst)
        {
            // Find a title by Tconst including associated genres
            return context.Titles.Where(t => t.Tconst == tconst).Include(t => t.Genres).FirstOrDefault() ?? throw new Exception();
        }

        // Method to search titles based on criteria
        public List<Title> SearchTitles(string criteria)
        {
            // Perform a stored procedure to find titles based on criteria and transform the results into Title objects
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

        // Method to add a new title to the database
        public void AddTitle(Title title, string genres)
        {
            // Perform a stored procedure to add a new title with associated genres
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

        // Method to update an existing title in the database
        public void UpdateTitle(Title title, string genres)
        {
            // Perform a stored procedure to update an existing title with associated genres
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

        // Method to delete a title from the database
        public void DeleteTitle(string tconst)
        {
            // Perform a stored procedure to delete a title
            context.Procedures.deleteTitleAsync(tconst).Wait();
        }

        // Method to add a new name to the database
        public void AddName(Name name, string professions)
        {
            // Perform a stored procedure to add a new name with associated professions
            context.Procedures.addNameAsync(
                name.PrimaryName,
                name.BirthYear,
                name.DeathYear,
                professions).Wait();
        }

        // Method to retrieve top names from the database
        public List<Name> GetTopNames(int amount)
        {
            // Retrieve top names including associated professions, ordered by Nconst, and take specified amount
            return context.Names.Include(n => n.Professions).OrderByDescending(n => n.Nconst).Take(amount).ToList();
        }

        // Method to search names based on criteria
        public List<Name> SearchNames(string criteria)
        {
            // Perform a stored procedure to find names based on criteria and transform the results into Name objects
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

        // Helper method to transform a list of items using a specified transformer function
        private static List<V> TransformItems<T, V>(List<T> items, Func<T, V> transformer)
        {
            // Transform each item in the list using the provided transformer function
            List<V> transformedItems = new();
            foreach (T item in items)
            {
                V transformedItem = transformer(item);
                transformedItems.Add(transformedItem);
            }

            return transformedItems;
        }
    }
}

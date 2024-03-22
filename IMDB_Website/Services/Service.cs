using IMDB_Website.Models;
using Microsoft.AspNetCore.SignalR;

namespace IMDB_Website.Services
{
    public class Service : Interface
    {

        private readonly imdbContext context;

        public Service(imdbContext service)
        {
            context = service;
        }
        public Title FindTitleByTconst(string tconst)
        {
            throw new NotImplementedException();
        }

        public List<Name> GetTopNames(int amount)
        {
            throw new NotImplementedException();
        }

        public List<Title> GetTopTitles(int amount)
        {
            throw new NotImplementedException();
        }

        public List<Name> SearchNames(string criteria)
        {
            throw new NotImplementedException();
        }

        public List<Title> SearchTitles(string criteria)
        {
            throw new NotImplementedException();
        }
    }
}

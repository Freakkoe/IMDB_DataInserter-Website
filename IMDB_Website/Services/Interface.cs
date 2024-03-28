using IMDB_Website.Models;

namespace IMDB_Website.Services
{
    public interface Interface
    {
        List<Title> GetTopTitles(int amount);

        List<Title> SearchTitles(string criteria);

        Title FindTitleByTconst(string tconst);

        void AddTitle(Title title, string genres);

        void UpdateTitle(Title title, string genres);

        void DeleteTitle(string tconst);

        List<Name> GetTopNames(int amount);

        List<Name> SearchNames(string criteria);

        void AddName(Name name, string professions);
    }
}

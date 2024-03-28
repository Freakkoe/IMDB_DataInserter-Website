using IMDB_Website.Models;
using IMDB_Website.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IMDB_Website.Pages
{
    public class TitlesModel : PageModel
    {
        private readonly Interface service;

        public TitlesModel(Interface service)
        {
            this.service = service;
        }

        [BindProperty(SupportsGet = true)]
        public int Amount { get; set; } = 10;

        [BindProperty(SupportsGet = true)]
        public string Criteria { get; set; }

        public List<Title> Titles { get; set; }

        public void OnGet()
        {
            if (String.IsNullOrEmpty(Criteria))
            {
                Titles = service.GetTopTitles(Amount);
            }
            else
            {
                Titles = service.SearchTitles(Criteria);
            }
        }
    }
}

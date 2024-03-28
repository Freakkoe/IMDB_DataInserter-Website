using IMDB_Website.Models;
using IMDB_Website.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IMDB_Website.Pages
{
    public class NamesModel : PageModel
    {
        private readonly Interface service;

        public NamesModel(Interface service)
        {
            this.service = service;
        }

        [BindProperty(SupportsGet = true)]
        public int Amount { get; set; } = 10;

        [BindProperty(SupportsGet = true)]
        public string Criteria { get; set; }

        public List<Name> Names { get; set; }

        public void OnGet()
        {
            if (String.IsNullOrEmpty(Criteria))
            {
                Names = service.GetTopNames(Amount);
            }
            else
            {
                Names = service.SearchNames(Criteria);
            }
        }
    }
}

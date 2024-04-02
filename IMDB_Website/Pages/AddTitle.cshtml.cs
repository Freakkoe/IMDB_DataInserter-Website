using IMDB_Website.Models;
using IMDB_Website.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IMDB_Website.Pages
{
    public class AddTitleModel : PageModel
    {
        private readonly Interface service;

        public AddTitleModel(Interface service)
        {

            this.service = service;

        }

        [BindProperty]
        public Title Title { get; set; }

        [BindProperty]
        public string Genres { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            service.AddTitle(Title, Genres);
            return RedirectToPage("/Titles");
        }
    }
}

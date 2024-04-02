using IMDB_Website.Models;
using IMDB_Website.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IMDB_Website.Pages
{
    public class AddNameModel : PageModel
    {
        private readonly Interface service;

        public AddNameModel(Interface service)
        {
            this.service = service;
        }

        [BindProperty]
        public Name Name { get; set; }

        [BindProperty]
        public string Professions { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {

            service.AddName(Name, Professions);
            return RedirectToPage("/Names");
        }
    }
}

using IMDB_Website.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IMDB_Website.Pages
{
    public class DeleteTitleModel : PageModel
    {
        private readonly Interface service;

        public DeleteTitleModel(Interface service)
        {
            this.service = service;
        }

        public IActionResult OnGet(string tconst)
        {
            service.DeleteTitle(tconst);
            return RedirectToPage("/Titles");
        }
    }
}

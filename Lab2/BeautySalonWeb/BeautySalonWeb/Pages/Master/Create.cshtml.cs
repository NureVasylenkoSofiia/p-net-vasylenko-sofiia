using BeautySalonWeb.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeautySalonWeb.Pages.Master
{
    public class CreateModel : PageModel
    {
        private readonly IMasterService _masterService;

        public CreateModel(IMasterService masterService)
        {
            _masterService = masterService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BeautySalonWeb.DAL.Models.Master Master { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

         
            Master.Information = null;

            await _masterService.CreateAsync(Master);
            return RedirectToPage("./Index");
        }

    }
}

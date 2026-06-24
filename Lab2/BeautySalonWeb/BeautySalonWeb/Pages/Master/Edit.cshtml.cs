using BeautySalonWeb.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeautySalonWeb.Pages.Master
{
    public class EditModel : PageModel
    {
        private readonly IMasterService _masterService;

        public EditModel(IMasterService masterService)
        {
            _masterService = masterService;
        }

        [BindProperty]
        public BeautySalonWeb.DAL.Models.Master Master { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var master = await _masterService.GetByIdAsync(id);
            if (master == null)
            {
                return NotFound();
            }
            Master = master;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _masterService.UpdateAsync(Master);
            return RedirectToPage("./Index");
        }

    }
}

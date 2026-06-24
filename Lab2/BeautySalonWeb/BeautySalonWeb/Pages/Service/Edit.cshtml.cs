using BeautySalonWeb.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeautySalonWeb.Pages.Service
{
    public class EditModel : PageModel
    {
        private readonly IServiceService _serviceService;

        public EditModel(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [BindProperty]
        public BeautySalonWeb.DAL.Models.Service Service { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var service = await _serviceService.GetByIdAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            Service = service;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _serviceService.UpdateAsync(Service);

            return RedirectToPage("./Index");
        }

    }
}

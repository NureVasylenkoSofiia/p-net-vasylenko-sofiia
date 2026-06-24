using BeautySalonWeb.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeautySalonWeb.Pages.Service
{
    public class DeleteModel : PageModel
    {
        private readonly IServiceService _serviceService;

        public DeleteModel(IServiceService serviceService)
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _serviceService.DeleteAsync(id);
            return RedirectToPage("./Index");
        }

    }
}

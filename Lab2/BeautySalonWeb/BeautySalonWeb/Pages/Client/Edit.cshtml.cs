using BeautySalonWeb.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeautySalonWeb.Pages.Client
{
    public class EditModel : PageModel
    {
        private readonly IClientService _clientService;

        public EditModel(IClientService clientService)
        {
            _clientService = clientService;
        }

        [BindProperty]
        public BeautySalonWeb.DAL.Models.Client Client { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = await _clientService.GetByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            Client = client;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _clientService.UpdateAsync(Client);
            return RedirectToPage("./Index");
        }

    }
}

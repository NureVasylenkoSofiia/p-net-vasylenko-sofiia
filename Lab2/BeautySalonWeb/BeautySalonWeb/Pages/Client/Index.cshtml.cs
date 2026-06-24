using BeautySalonWeb.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeautySalonWeb.Pages.Client
{
    public class IndexModel : PageModel
    {
        private readonly IClientService _clientService;

        public IndexModel(IClientService clientService)
        {
            _clientService = clientService;
        }

        public IEnumerable<BeautySalonWeb.DAL.Models.Client> ClientsList { get; set; } = default!;

        public async Task OnGetAsync()
        {
            ClientsList = await _clientService.GetAllAsync();
        }

    }
}

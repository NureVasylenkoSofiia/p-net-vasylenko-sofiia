using BeautySalonWeb.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeautySalonWeb.Pages.Service
{
    public class IndexModel : PageModel
    {
        private readonly IServiceService _serviceService;

        public IndexModel(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        public IEnumerable<BeautySalonWeb.DAL.Models.Service> ServicesList { get; set; } = default!;

        public async Task OnGetAsync()
        {
            ServicesList = await _serviceService.GetAllAsync();
        }

    }
}

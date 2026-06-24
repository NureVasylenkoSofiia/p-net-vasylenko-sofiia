using BeautySalonWeb.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeautySalonWeb.Pages.Master
{
    public class IndexModel : PageModel
    {
        private readonly IMasterService _masterService;

        public IndexModel(IMasterService masterService)
        {
            _masterService = masterService;
        }

        public IEnumerable<BeautySalonWeb.DAL.Models.Master> MastersList { get; set; } = default!;

        public async Task OnGetAsync()
        {
            MastersList = await _masterService.GetAllAsync();
        }

    }
}

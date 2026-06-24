using BeautySalonWeb.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BeautySalonWeb.Pages.AppointmentDetail
{
    public class IndexModel : PageModel
    {
        private readonly IAppointmentDetailService _detailService;
        private readonly IServiceService _serviceService;

        public IndexModel(IAppointmentDetailService detailService, IServiceService serviceService)
        {
            _detailService = detailService;
            _serviceService = serviceService;
        }

        public IEnumerable<BeautySalonWeb.DAL.Models.AppointmentDetail> CurrentDetails { get; set; } = default!;

        [BindProperty]
        public BeautySalonWeb.DAL.Models.AppointmentDetail NewDetail { get; set; } = default!;


        public SelectList AvailableServices { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int AppointmentId { get; set; }

        public async Task<IActionResult> OnGetAsync(int appointmentId)
        {
            AppointmentId = appointmentId;

            CurrentDetails = await _detailService.GetByAppointmentIdAsync(AppointmentId);

            var allServices = await _serviceService.GetAllAsync();
            AvailableServices = new SelectList(allServices, "Id", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            NewDetail.AppointmentId = AppointmentId;

            await _detailService.CreateAsync(NewDetail);

            return RedirectToPage("./Index", new { appointmentId = AppointmentId });
        }

    }
}

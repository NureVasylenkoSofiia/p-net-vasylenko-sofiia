using BeautySalonWeb.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeautySalonWeb.Pages.Appointment
{
    public class IndexModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;

        public IndexModel(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public IEnumerable<BeautySalonWeb.DAL.Models.Appointment> AppointmentsList { get; set; } = default!;

        public async Task OnGetAsync()
        {
            AppointmentsList = await _appointmentService.GetAllAsync();
        }
    }
}

using BeautySalonWeb.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BeautySalonWeb.Pages.Appointment
{
    public class CreateModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IClientService _clientService;
        private readonly IMasterService _masterService;

        public CreateModel(
            IAppointmentService appointmentService,
            IClientService clientService,
            IMasterService masterService)
        {
            _appointmentService = appointmentService;
            _clientService = clientService;
            _masterService = masterService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // Завантажуємо дані для випадаючих списків (FK на практиці!)
            var clients = await _clientService.GetAllAsync();
            var masters = await _masterService.GetAllAsync();

            ClientSelectList = new SelectList(clients, "Id", "FullName");
            MasterSelectList = new SelectList(masters, "Id", "FullName");

            return Page();
        }

        [BindProperty]
        public BeautySalonWeb.DAL.Models.Appointment Appointment { get; set; } = default!;

        public SelectList ClientSelectList { get; set; } = default!;
        public SelectList MasterSelectList { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var clients = await _clientService.GetAllAsync();
                var masters = await _masterService.GetAllAsync();
                ClientSelectList = new SelectList(clients, "Id", "FullName");
                MasterSelectList = new SelectList(masters, "Id", "FullName");
                return Page();
            }

            Appointment.Status = "Очікується";

            await _appointmentService.CreateAsync(Appointment);
            return RedirectToPage("./Index");
        }

    }
}

using BeautySalonWeb.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BeautySalonWeb.Pages.Appointment
{
    public class EditModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;

        public EditModel(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [BindProperty]
        public BeautySalonWeb.DAL.Models.Appointment Appointment { get; set; } = default!;

        public SelectList StatusList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var appointment = await _appointmentService.GetByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            Appointment = appointment;

            StatusList = new SelectList(new[] { "Очікується", "Завершено", "Скасовано" });

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                StatusList = new SelectList(new[] { "Очікується", "Завершено", "Скасовано" });
                return Page();
            }

            await _appointmentService.UpdateAsync(Appointment);
            return RedirectToPage("./Index");

        }
    }
}

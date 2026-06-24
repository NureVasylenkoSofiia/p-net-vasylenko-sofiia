using BeautySalonWeb.BLL.Interfaces;
using BeautySalonWeb.DAL.Data;
using BeautySalonWeb.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonWeb.BLL.Services
{

    public class AppointmentService : IAppointmentService
    {
        private readonly BeautySalonContext _context;

        public AppointmentService(BeautySalonContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await _context.Appointments
                .Include(a => a.Client)  
                .Include(a => a.Master)  
                .ToListAsync();
        }

        public async Task<Appointment?> GetByIdAsync(int id)
        {
            return await _context.Appointments
                .Include(a => a.Client)
                .Include(a => a.Master)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task CreateAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            _context.Attach(appointment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

}

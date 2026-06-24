using BeautySalonWeb.BLL.Interfaces;
using BeautySalonWeb.DAL.Data;
using BeautySalonWeb.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonWeb.BLL.Services
{
    public class AppointmentDetailService : IAppointmentDetailService
    {
        private readonly BeautySalonContext _context;

        public AppointmentDetailService(BeautySalonContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AppointmentDetail>> GetByAppointmentIdAsync(int appointmentId)
        {
            return await _context.AppointmentDetails
                .Include(ad => ad.Service) 
                .Where(ad => ad.AppointmentId == appointmentId)
                .ToListAsync();
        }

        public async Task CreateAsync(AppointmentDetail detail)
        {
            await _context.AppointmentDetails.AddAsync(detail);
            await _context.SaveChangesAsync(); 
        }
    }

}

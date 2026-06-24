using BeautySalonWeb.BLL.Interfaces;
using BeautySalonWeb.DAL.Data;
using BeautySalonWeb.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonWeb.BLL.Services
{
    public class ServiceService : IServiceService
    {
        private readonly BeautySalonContext _context;

        public ServiceService(BeautySalonContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<Service?> GetByIdAsync(int id)
        {
            return await _context.Services.FindAsync(id);
        }

        public async Task CreateAsync(Service service)
        {
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Service service)
        {
            _context.Attach(service).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
            }
        }
    }
}

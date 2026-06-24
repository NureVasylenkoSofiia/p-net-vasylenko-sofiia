using BeautySalonWeb.BLL.Interfaces;
using BeautySalonWeb.DAL.Data;
using BeautySalonWeb.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonWeb.BLL.Services
{
    public class MasterService : IMasterService
    {
        private readonly BeautySalonContext _context;

        public MasterService(BeautySalonContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Master>> GetAllAsync()
        {
            return await _context.Masters.ToListAsync();
        }

        public async Task<Master?> GetByIdAsync(int id)
        {
            return await _context.Masters.FindAsync(id);
        }

        public async Task CreateAsync(Master master)
        {
            await _context.Masters.AddAsync(master);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Master master)
        {
            _context.Attach(master).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

}

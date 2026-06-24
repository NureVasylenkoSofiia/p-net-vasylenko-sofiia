using BeautySalonWeb.DAL.Models;

namespace BeautySalonWeb.BLL.Interfaces
{
    public interface IMasterService
    {
        Task<IEnumerable<Master>> GetAllAsync();
        Task<Master?> GetByIdAsync(int id);
        Task CreateAsync(Master master);
        Task UpdateAsync(Master master);
    }

}

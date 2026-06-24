using BeautySalonWeb.DAL.Models;

namespace BeautySalonWeb.BLL.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAllAsync();
        Task<Appointment?> GetByIdAsync(int id);
        Task CreateAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
    }
}

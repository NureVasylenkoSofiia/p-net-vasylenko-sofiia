using BeautySalonWeb.DAL.Models;

namespace BeautySalonWeb.BLL.Interfaces
{
    public interface IAppointmentDetailService
    {
        Task<IEnumerable<AppointmentDetail>> GetByAppointmentIdAsync(int appointmentId);
        Task CreateAsync(AppointmentDetail detail);
    }
}

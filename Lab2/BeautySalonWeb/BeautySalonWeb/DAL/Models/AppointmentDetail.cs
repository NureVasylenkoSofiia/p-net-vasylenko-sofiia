using System.ComponentModel.DataAnnotations;

namespace BeautySalonWeb.DAL.Models
{
    public class AppointmentDetail
    {
        public int Id { get; set; }

        [Required]
        public int AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }

        [Required]
        public int ServiceId { get; set; }
        public Service? Service { get; set; }
    }
}

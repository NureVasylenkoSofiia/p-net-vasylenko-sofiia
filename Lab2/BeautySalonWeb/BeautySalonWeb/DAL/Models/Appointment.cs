using System.ComponentModel.DataAnnotations;

namespace BeautySalonWeb.DAL.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }
        public Client? Client { get; set; }

        [Required]
        public int MasterId { get; set; }
        public Master? Master { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [StringLength(30)]
        public string Status { get; set; } = "Очікується";

        public ICollection<AppointmentDetail> AppointmentDetails { get; set; } = new List<AppointmentDetail>();
    }
}

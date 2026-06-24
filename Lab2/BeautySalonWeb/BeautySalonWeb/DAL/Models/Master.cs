using System.ComponentModel.DataAnnotations;

namespace BeautySalonWeb.DAL.Models
{
    public class Master
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        public string? Information { get; set; }    

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}

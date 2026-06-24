using BeautySalonWeb.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonWeb.DAL.Data
{
    public class BeautySalonContext : DbContext
    {
        public BeautySalonContext(DbContextOptions<BeautySalonContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; } 
        public DbSet<Master> Masters { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentDetail> AppointmentDetails { get; set; }
    }
}

using BeautySalonWeb.BLL.Interfaces;
using BeautySalonWeb.BLL.Services;
using BeautySalonWeb.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<BeautySalonContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IServiceService, ServiceService>();
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<IMasterService, MasterService>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();
            builder.Services.AddScoped<IAppointmentDetailService, AppointmentDetailService>();

            builder.Services.AddRazorPages();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}

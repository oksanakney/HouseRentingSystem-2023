namespace HouseRentingSystem.WebAPI
{
    using HouseRentingSystem.Data;
    using HouseRentingSystem.Services.Data;
    using HouseRentingSystem.Services.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Web.Infrastructure.Extensions;
    public class Program
    {
        
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            //v api niamame dbContext
            builder.Services.AddDbContext<HouseRentingDbContext>(opt => 
            opt.UseSqlServer(connectionString));

            builder.Services.AddApplicationServices(typeof(IHouseService));
              

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}


using Microsoft.EntityFrameworkCore;

namespace Selu383.SP25.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DataContext")));

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
           

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {

                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                context.Database.Migrate();
            }

            app.Run();
        }
    }
}

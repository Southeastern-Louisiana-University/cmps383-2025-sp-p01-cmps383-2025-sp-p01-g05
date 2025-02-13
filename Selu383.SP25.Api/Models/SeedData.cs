using Microsoft.EntityFrameworkCore;

namespace Selu383.SP25.Api.Models


{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider) 
        {
            using (var context = new DataContext(
                serviceProvider.GetRequiredService<DbContextOptions<DataContext>>()))
            {
                
                if (context.Theaters.Any()) return;

               
                context.Theaters.AddRange(
                    new Theater { Name = "Grand Opera House", Address = "123 Main St", SeatCount = 500 },
                    new Theater { Name = "Broadway Center", Address = "456 Broadway Ave", SeatCount = 750 },
                    new Theater { Name = "Sunset Theater", Address = "789 Sunset Blvd", SeatCount = 300 }
                );

                context.SaveChanges();
            }


        }
    }
}

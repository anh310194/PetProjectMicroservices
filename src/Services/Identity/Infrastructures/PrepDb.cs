using Identity.Core.Interfaces;

namespace Identity.Infrastructures;

public static class PrepDB
{
    public static void PrepPopulation(IApplicationBuilder application)
    {
        using (var serviceScope = application.ApplicationServices.CreateScope())
        {
#pragma warning disable CS8604 // Possible null reference argument.
            SeedData(serviceScope.ServiceProvider.GetService<IUnitOfWork>());
#pragma warning restore CS8604 // Possible null reference argument.
        }
    }

    private static void SeedData(IUnitOfWork unitOfWork)
    {
        if (!unitOfWork.UserRepository.Queryable().Any())
        {
            Console.WriteLine("=> seeding data");

            unitOfWork.UserRepository.Insert(new User()
            {
                Email = "anh310194@gmail.com",
                FirstName = "Anh",
                LastName = "Nguyen",
                UserName = "anhnguyen",
            },
            0);
            unitOfWork.SaveChanges();
        }
        else
        {
            Console.WriteLine("=> we already have data");
        }
    }
}
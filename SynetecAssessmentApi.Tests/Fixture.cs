using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SynetecAssessmentApi.Persistence;
using SynetecAssessmentApi.Repositories;
using SynetecAssessmentApi.Services;

namespace SynetecAssessmentApi.Tests
{
    public class Fixture
    {
        public Fixture()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: "HrDb"));

            serviceCollection.AddAutoMapper(typeof(Startup).Assembly);

            serviceCollection.AddTransient<IBonusPoolService, BonusPoolService>();
            serviceCollection.AddTransient<IEmployeeRepository, EmployeeRepository>();
            serviceCollection.AddTransient<IBonusPoolCalculator, BonusPoolCalculator>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }

}

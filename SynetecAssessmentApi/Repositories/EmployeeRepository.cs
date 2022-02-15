using Microsoft.EntityFrameworkCore;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _dbContext;

        public EmployeeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            //var dbContextOptionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            //dbContextOptionBuilder.UseInMemoryDatabase(databaseName: "HrDb");

            //_dbContext = new AppDbContext(dbContextOptionBuilder.Options);
        }
        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _dbContext
                .Employees
                .Include(e => e.Department)
                .ToListAsync();
        }

        public async Task<Employee> GetById(int id)
        {
            return await _dbContext.Employees
                        .Include(e => e.Department)
                        .FirstOrDefaultAsync(item => item.Id == id);

        }

        public async Task<int> GetTotalEmployeesSalary()
        {
            return await _dbContext.Employees.SumAsync(item => item.Salary);
        }
    }
}

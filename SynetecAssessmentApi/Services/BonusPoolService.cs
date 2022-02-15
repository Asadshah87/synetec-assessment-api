using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Persistence;
using SynetecAssessmentApi.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Services
{

    public class BonusPoolService : IBonusPoolService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IBonusPoolCalculator _bonusPoolCalculator;
        private readonly IMapper _mapper;

        public BonusPoolService(IEmployeeRepository employeeRepository, IBonusPoolCalculator bonusPoolCalculator, IMapper mapper)
        {
            this._employeeRepository = employeeRepository;
            this._bonusPoolCalculator = bonusPoolCalculator;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            IEnumerable<Employee> employees = await this._employeeRepository.GetAll();

            List<EmployeeDto> result = new List<EmployeeDto>();

            return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<BonusPoolCalculatorResultDto> CalculateAsync(int bonusPoolAmount, int selectedEmployeeId)
        {
            if (selectedEmployeeId <= 0)
                return null;
            //load the details of the selected employee using the Id
            Employee employee = await this._employeeRepository.GetById(selectedEmployeeId);
            if (employee == null)
                return null;

            //get the total salary budget for the company
            int totalSalary = await this._employeeRepository.GetTotalEmployeesSalary(); ;
            //calculate the bonus allocation for the employee
            int bonusAllocation = _bonusPoolCalculator.CalculateBonusAllocation(bonusPoolAmount, employee.Salary, totalSalary);

            return new BonusPoolCalculatorResultDto
            {
                Employee = _mapper.Map<Employee, EmployeeDto>(employee),

                Amount = bonusAllocation
            };
        }
    }
}

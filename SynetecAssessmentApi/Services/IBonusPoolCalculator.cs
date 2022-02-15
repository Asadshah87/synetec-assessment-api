namespace SynetecAssessmentApi.Services
{
    public interface IBonusPoolCalculator
    {
        int CalculateBonusAllocation(int bonusPoolAmount, int employeeSalary, int totalSalary);
    }
}

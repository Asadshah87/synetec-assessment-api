namespace SynetecAssessmentApi.Services
{

    public class BonusPoolCalculator : IBonusPoolCalculator
    {
        public int CalculateBonusAllocation(int bonusPoolAmount, int employeeSalary, int totalSalary)
        {
            decimal bonusPercentage = (decimal)employeeSalary / (decimal)totalSalary;
            return (int)(bonusPercentage * bonusPoolAmount);
        }
    }
}

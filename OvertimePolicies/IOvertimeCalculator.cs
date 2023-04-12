namespace OvertimePolicies
{
    public interface IOvertimeCalculator
    {
        decimal CalcurlatorA(decimal basicSalary,decimal allowance);

        decimal CalcurlatorB(decimal basicSalary, decimal allowance);
        
        decimal CalcurlatorC(decimal basicSalary, decimal allowance);
        
    }
}

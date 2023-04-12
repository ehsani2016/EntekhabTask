namespace DataDapper
{
    public interface ISalaryRepository : Base.IRepository<Models.Salary>
    {
        Models.Salary? GetSalary(Guid idPerson, DateTime date);
        IList<Models.Salary> GetSalary(Guid idPerson, DateTime fromDate, DateTime toDate);
    }
}

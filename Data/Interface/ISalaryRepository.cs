namespace Data
{
    public interface ISalaryRepository : Base.IRepository<Models.Salary>
    {
        Task<Models.Salary?> GetSalaryAsync(Guid idPerson, DateTime date);
        Task<bool> ExistSalaryAsync(Guid idPerson, DateTime date);
        ViewModel.ResultApi<ViewModel.SalaryViewModel> ProcessCustomFormat(string data, string dataType);
    }
}

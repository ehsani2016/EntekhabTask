namespace DataDapper
{
    public interface IUnitOfWork 
    {
        #region Tables
        IPersonnelRepository PersonnelRepository { get; }

        ISalaryRepository SalaryRepository { get; }
        #endregion /Tables
    }
}

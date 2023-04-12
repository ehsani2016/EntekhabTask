namespace Data
{
    public interface IUnitOfWork : Base.IUnitOfWork
    {
        #region Tables
        IPersonnelRepository PersonnelRepository { get; }

        ISalaryRepository SalaryRepository { get; }
        #endregion /Tables
    }
}

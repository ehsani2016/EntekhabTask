namespace Data
{
    public class UnitOfWork : Base.UnitOfWork, IUnitOfWork
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public UnitOfWork(Tools.Options options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }



        #region Tables
        private IPersonnelRepository personnelRepository;

        private ISalaryRepository salaryRepository;
        #endregion /Tables


        #region Repositories
        public IPersonnelRepository PersonnelRepository =>
            personnelRepository ??= new PersonnelRepository(DatabaseContext);
        public ISalaryRepository SalaryRepository =>
            salaryRepository ??= new SalaryRepository(DatabaseContext);
        #endregion /Repositories


    }
}

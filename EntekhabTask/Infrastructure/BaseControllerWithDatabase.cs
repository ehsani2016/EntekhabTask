namespace Infrastructure
{
    public class BaseControllerWithDatabase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        public BaseControllerWithDatabase(
            Data.IUnitOfWork unitOfWork, 
            DataDapper.IUnitOfWork unitOfWorkDapper,
            IConfiguration configuration) : base()
        {
            UnitOfWork = unitOfWork;
            Configuration = configuration;
            UnitOfWorkDapper = unitOfWorkDapper;
        }

        protected Data.IUnitOfWork UnitOfWork { get; }
        protected IConfiguration Configuration { get; }
        protected DataDapper.IUnitOfWork UnitOfWorkDapper { get; }

        
    }
}

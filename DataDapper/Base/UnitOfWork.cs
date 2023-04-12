namespace DataDapper.Base
{
    public abstract class UnitOfWork : object, IUnitOfWork
    {
        public UnitOfWork(Tools.Options options) : base()
        {
            Options = options;
        }

        protected Tools.Options Options { get; set; }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

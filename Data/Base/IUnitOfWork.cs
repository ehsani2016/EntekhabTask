namespace Data.Base
{
    public interface IUnitOfWork:System.IDisposable
    {
        bool IsDisposed { get; }

        void Save();

        System.Threading.Tasks.Task SaveAsync();
    }
}

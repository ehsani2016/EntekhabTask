namespace DataDapper.Base
{
    public interface IRepository<T> where T :Models.SeedWork.Entity
    {
        T? GetById(System.Guid id);

        System.Collections.Generic.IList<T> GetAll();
    }
}

namespace Data
{
    public class Repository<T> :Base.Repository<T> where T:Models.SeedWork.Entity
    {
        internal Repository(DatabaseContext databaseContext):base(databaseContext) 
        { 
        }
       
    }
}

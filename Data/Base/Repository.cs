using Microsoft.EntityFrameworkCore;

namespace Data.Base
{
    public class Repository<T> : object, IRepository<T> where T : Models.SeedWork.Entity
    {
        internal Repository(DatabaseContext databaseContext) : base()
        {
            DatabaseContext =
                databaseContext ?? throw new System.ArgumentNullException(paramName: nameof(databaseContext));

            DbSet = DatabaseContext.Set<T>();
        }

        // **************
        internal DatabaseContext DatabaseContext { get; }

        internal Microsoft.EntityFrameworkCore.DbSet<T> DbSet { get; set; }
        // **************

        public virtual void Insert(T entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(entity));
            }


            DbSet.Add(entity);
        }

        public virtual async System.Threading.Tasks.Task InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(entity));
            }


            await DbSet.AddAsync(entity);
        }

        public virtual async System.Threading.Tasks.Task InsertAsync(System.Collections.Generic.List<T> entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(entity));
            }

            await DbSet.AddRangeAsync(entity);
        }

        public virtual void Update(T entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(entity));
            }

            DbSet.Update(entity);
        }

        public virtual async System.Threading.Tasks.Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(entity));
            }

            await System.Threading.Tasks.Task.Run(() =>
            {
                DbSet.Update(entity);
            });
        }

        public virtual void Delete(T entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(entity));
            }

            DbSet.Remove(entity);
        }

        public virtual async System.Threading.Tasks.Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(entity));
            }

            await System.Threading.Tasks.Task.Run(() =>
            {
                DbSet.Remove(entity);
            });
        }

        public virtual async System.Threading.Tasks.Task DeleteAsync(System.Collections.Generic.IList<T> entitis)
        {
            if (entitis.Any() == false)
            {
                throw new System.ArgumentNullException(paramName: nameof(entitis));
            }

            await System.Threading.Tasks.Task.Run(() =>
            {
                DbSet.RemoveRange(entitis);
            });
        }

        public virtual T? GetById(System.Guid id)
        {
            var result = DbSet.Find(keyValues: id);

            return result;
        }

        public virtual async System.Threading.Tasks.Task<T?> GetByIdAsync(System.Guid id)
        {
            var result = await DbSet.FindAsync(keyValues: id);

            return result;
        }

        public virtual bool DeleteById(System.Guid id)
        {
            var entity = GetById(id);

            if (entity == null)
            {
                return false;
            }

            Delete(entity);

            return true;
        }

        public virtual async System.Threading.Tasks.Task<bool> DeleteByIdAsync(System.Guid id)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
            {
                return false;
            }

            await DeleteAsync(entity);

            return true;
        }

        public virtual System.Collections.Generic.IList<T> GetAll()
        {
            var result = DbSet
                .ToList();

            return result;
        }

        public virtual async System.Threading.Tasks.Task<System.Collections.Generic.IList<T>> GetAllAsync()
        {
            var result = await DbSet
                .ToListAsync();

            return result;
        }


        public virtual async System.Threading.Tasks.Task<System.Collections.Generic.IList<T>> GetFromSqlRaw(
           string storedProcedureName,
           params object[] parameters)
        {
            System.Globalization.CultureInfo currentCulture =
                   System.Threading.Thread.CurrentThread.CurrentCulture;
            try
            {
                System.Globalization.CultureInfo englishCulture =
                    new System.Globalization.CultureInfo(name: "en-US");

                System.Threading.Thread.CurrentThread.CurrentCulture = englishCulture;

                for (var i = 0; i < parameters.Length; i++)
                {
                    storedProcedureName += " @p" + i + (i != parameters.Length - 1 ? "," : "");
                }

                var result = await DbSet.FromSqlRaw(storedProcedureName, parameters)
                    .ToListAsync();


                return result;
            }
            finally
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = currentCulture;
            }
        }


        
    }
}

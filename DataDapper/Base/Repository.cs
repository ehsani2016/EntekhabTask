using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataDapper.Base
{
    public class Repository<T> : object, IRepository<T> where T : Models.SeedWork.Entity
    {
        internal Repository(string connectionString) : base()
        {
            DatabaseContext = new SqlConnection(connectionString);
        }

        // **************
        internal IDbConnection DatabaseContext { get; }
        // **************


        public virtual IList<T> GetAll()
        {
            var sql = $"SELECT * FROM tbl_{nameof(T)}";

            var result = DatabaseContext
                .Query<T>(sql)
                .ToList();

            return result;
        }

        public virtual T? GetById(Guid id)
        {
            var sql = $"SELECT * FROM tbl_{nameof(T)} WHERE id = @id";

            var result = DatabaseContext
                .Query<T>(sql,new {id = id})
                .FirstOrDefault();

            return result;
        }
    }
}

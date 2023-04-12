using Dapper;

namespace DataDapper
{
    public class SalaryRepository : Base.Repository<Models.Salary>, ISalaryRepository
    {
        internal SalaryRepository(string connectionString) : base(connectionString)
        {
        }

        public Models.Salary? GetSalary(Guid idPerson, DateTime date)
        {
            var sql = $"SELECT * FROM tbl_{nameof(Models.Salary)} " +
                        $"WHERE (IdPersonnel = @idPerson AND Date = @date)";

            var result = DatabaseContext
                .Query<Models.Salary>(sql, new { idPerson = idPerson, date = date.Date })
                .FirstOrDefault();

            return result;
        }

        public IList<Models.Salary> GetSalary(Guid idPerson, DateTime fromDate, DateTime toDate)
        {
            var sql = $"SELECT * FROM tbl_{nameof(Models.Salary)} " +
                        $"WHERE (IdPersonnel = @idPerson AND Date between @fromDate AND @toDate)";

            var result = DatabaseContext
                .Query<Models.Salary>(sql, new { idPerson = idPerson, fromDate = fromDate.Date, toDate = toDate.Date })
                .ToList();

            return result;
        }
    }
}

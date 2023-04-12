using Microsoft.EntityFrameworkCore;
using System.Net;
using ViewModel;

namespace Data
{
    public class SalaryRepository : Base.Repository<Models.Salary>, ISalaryRepository
    {
        internal SalaryRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<Models.Salary?> GetSalaryAsync(Guid idPerson, DateTime date)
        {
            var result = await DbSet
                .Where(c =>
                    c.IdPersonnel == idPerson &&
                    c.Date.Date == date.Date)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<bool> ExistSalaryAsync(Guid idPerson, DateTime date)
        {
            var result = await DbSet
                .Where(c =>
                    c.IdPersonnel == idPerson &&
                    c.Date.Date == date.Date)
                .AnyAsync();

            return result;
        }


        public ViewModel.ResultApi<ViewModel.SalaryViewModel> ProcessCustomFormat(string data,string dataType)
        {
            var lines = data.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length < 2)
            {
                return new ViewModel.ResultApi<ViewModel.SalaryViewModel>
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = string.Format(Resources.Messages.DataNotInFormat, nameof(dataType)),
                };
            }

            var paramsValue = lines[1].Split("/", StringSplitOptions.RemoveEmptyEntries);

            if (paramsValue.Length < 6)
            {
                return new ViewModel.ResultApi<ViewModel.SalaryViewModel>
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = string.Format(Resources.Messages.DataNotInFormat, nameof(dataType)),
                };
            }

            var salaryViewModel = new ViewModel.SalaryViewModel
            {
                FirstName = paramsValue[0],
                LastName = paramsValue[1],
                BasicSalary = Convert.ToDecimal(paramsValue[2].Trim()),
                Allowance = Convert.ToDecimal(paramsValue[3].Trim()),
                Transportation = Convert.ToDecimal(paramsValue[4].Trim()),
                Date = PersianDate.Standard.ConvertDate.ToEn($"{paramsValue[5].Trim().Substring(0, 4)}/{paramsValue[5].Trim().Substring(4, 2)}/{paramsValue[5].Trim().Substring(6, 2)}")
            };

            return new ResultApi<SalaryViewModel>()
            {
                Code = (int)HttpStatusCode.OK,
                Message = Resources.Messages.Success,
                Result = salaryViewModel
            };
        }
    }
}

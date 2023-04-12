using Microsoft.Extensions.Configuration;
using OvertimePolicies;
using System.Net;

namespace test.EntekhabTask.RepositoryData
{
    public class UnitTestSalary
    {
        public UnitTestSalary()
         : base()
        {
        }
        public string ConnectionString
        {
            get
            {
                return "Initial Catalog=EntekhabTaskdb;TrustServerCertificate=True;Password=123456;Persist Security Info=True;User ID=tsk;Data Source=.";
            }
        }

        [Fact]
        public void Test_10()
        {
            //Arrange
            var unitOfwork = new Data.UnitOfWork(new Data.Tools.Options(ConnectionString)
            {
                Provider = Data.Tools.Enums.Provider.SqlServer
            });
            var unitOfworkDapper = new DataDapper.UnitOfWork(new DataDapper.Tools.Options(ConnectionString));
            var overtimeCalculator = new OvertimeCalculator();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var mock = new Moq.Mock<Data.IUnitOfWork>();
            mock.Setup(c => c.PersonnelRepository).Returns(unitOfwork.PersonnelRepository);
            mock.Setup(c => c.SalaryRepository).Returns(unitOfwork.SalaryRepository);
            mock.Setup(c => c.SalaryRepository.ExistSalaryAsync(Moq.It.IsAny<Guid>(), Moq.It.IsAny<DateTime>())).Returns(Task.FromResult(true));



            //Act
            var dataType = Resources.DataTypes.custom;
            var data = $"FirstName/LastName/BasicSalary/Allowance/Transportation/Date{Environment.NewLine}Ali/Ahmadi/1200000/400000/350000/14010801";
            var actual = unitOfwork.SalaryRepository.ProcessCustomFormat(data: data, dataType: dataType);



            //Assert
            Assert.NotNull(actual);
            Assert.IsType<ViewModel.ResultApi<ViewModel.SalaryViewModel>>(actual);

            var excepted = new ViewModel.ResultApi<ViewModel.SalaryViewModel>()
            {
                Code = (int)HttpStatusCode.OK,
                Message = Resources.Messages.Success,
                Result = new ViewModel.SalaryViewModel
                {
                    Allowance = 400000,
                    BasicSalary = 1200000,
                    Date = PersianDate.Standard.ConvertDate.ToEn("1401/08/01"),
                    FirstName = "Ali",
                    LastName = "Ahmadi",
                    Transportation = 350000,
                }
            };

            var model = Assert.IsType<ViewModel.ResultApi<ViewModel.SalaryViewModel>>(actual);
            Assert.Equal(excepted.Code, model.Code);
            Assert.Equal(excepted.Message, model.Message);
            Assert.Equal(excepted.Result.Allowance, model.Result.Allowance);
            Assert.Equal(excepted.Result.FirstName, model.Result.FirstName);
            Assert.Equal(excepted.Result.LastName, model.Result.LastName);
            Assert.Equal(excepted.Result.Date, model.Result.Date);
            Assert.Equal(excepted.Result.BasicSalary, model.Result.BasicSalary);
            Assert.Equal(excepted.Result.Tax, model.Result.Tax);
            Assert.Equal(excepted.Result.Transportation, model.Result.Transportation);
            Assert.Equal(excepted.Results, model.Results);
        }
    }
}
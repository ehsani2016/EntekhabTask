using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OvertimePolicies;
using System.Net;

namespace test.EntekhabTask.ControllerApiFolder
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
                ConnectionString = ConnectionString,
                Provider = Data.Tools.Enums.Provider.SqlServer
            });
            var unitOfworkDapper = new DataDapper.UnitOfWork(new DataDapper.Tools.Options(ConnectionString));
            var overtimeCalculator = new OvertimeCalculator();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            api.EntekhabTask.Controllers.SalaryController salaryController =
                new api.EntekhabTask.Controllers.SalaryController(unitOfwork, unitOfworkDapper, configuration, overtimeCalculator);


            //Act
            var dataType = "";//Resources.DataTypes.custom;
            var data = $"FirstName/LastName/BasicSalary/Allowance/Transportation/Date{Environment.NewLine}Ali/Ahmadi/1200000/400000/350000/14010801";
            var overTimeCalculator = "CalcurlatorA";
            var result = (salaryController.Add(dataType: dataType, data: data, overTimeCalculator: overTimeCalculator).Result) as ObjectResult;



            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);

            var actual = result.Value;
            var excepted = new ViewModel.ResultApi<string>()
            {
                Code = (int)HttpStatusCode.BadRequest,
                Message = string.Format(Resources.Messages.IsNullOrEmpty, nameof(dataType)),
            };

            var model = Assert.IsType<ViewModel.ResultApi<string>>(actual);
            Assert.Equal(excepted.Code, model.Code);
            Assert.Equal(excepted.Message, model.Message);
            Assert.Equal(excepted.Result, model.Result);
            Assert.Equal(excepted.Results, model.Results);
        }


        [Fact]
        public void Test_20()
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
            mock.Setup(c => c.SalaryRepository.ProcessCustomFormat(Moq.It.IsAny<string>(), Moq.It.IsAny<string>()))
                .Returns(new ViewModel.ResultApi<ViewModel.SalaryViewModel>()
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
                });


            api.EntekhabTask.Controllers.SalaryController salaryController =
                new api.EntekhabTask.Controllers.SalaryController(mock.Object, unitOfworkDapper, configuration, overtimeCalculator);

            //Act
            var dataType = Resources.DataTypes.custom;
            var data = $"FirstName/LastName/BasicSalary/Allowance/Transportation/Date{Environment.NewLine}Ali/Ahmadi/1200000/400000/350000/14010801";
            var overTimeCalculator = "CalcurlatorA";
            var result = (salaryController.Add(dataType: dataType, data: data, overTimeCalculator: overTimeCalculator).Result) as ObjectResult;



            //Assert
            Assert.NotNull(result);
            Assert.IsType<ConflictObjectResult>(result);

            var actual = result.Value;
            var excepted = new ViewModel.ResultApi<string>()
            {
                Code = (int)HttpStatusCode.Conflict,
                Message = Resources.Messages.Conflict,
            };

            var model = Assert.IsType<ViewModel.ResultApi<string>>(actual);
            Assert.Equal(excepted.Code, model.Code);
            Assert.Equal(excepted.Message, model.Message);
            Assert.Equal(excepted.Result, model.Result);
            Assert.Equal(excepted.Results, model.Results);
        }


        [Fact]
        public void Test_30()
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
            mock.Setup(c => c.SalaryRepository.ExistSalaryAsync(Moq.It.IsAny<Guid>(), Moq.It.IsAny<DateTime>())).Returns(Task.FromResult(false));
            mock.Setup(c => c.SalaryRepository.ProcessCustomFormat(Moq.It.IsAny<string>(), Moq.It.IsAny<string>()))
               .Returns(new ViewModel.ResultApi<ViewModel.SalaryViewModel>()
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
               });

            api.EntekhabTask.Controllers.SalaryController salaryController =
                new api.EntekhabTask.Controllers.SalaryController(mock.Object, unitOfworkDapper, configuration, overtimeCalculator);

            //Act
            var dataType = Resources.DataTypes.custom;
            var data = $"FirstName/LastName/BasicSalary/Allowance/Transportation/Date{Environment.NewLine}Ali/Ahmadi/1200000/400000/350000/14010801";
            var overTimeCalculator = "CalcurlatorA";
            var result = (salaryController.Add(dataType: dataType, data: data, overTimeCalculator: overTimeCalculator).Result) as ObjectResult;



            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var actual = result.Value;
            var excepted = new ViewModel.ResultApi<string>()
            {
                Code = (int)HttpStatusCode.OK,
                Message = Resources.Messages.Success,
            };

            var model = Assert.IsType<ViewModel.ResultApi<string>>(actual);
            Assert.Equal(excepted.Code, model.Code);
            Assert.Equal(excepted.Message, model.Message);
            Assert.Equal(excepted.Result, model.Result);
            Assert.Equal(excepted.Results, model.Results);
        }
    }
}
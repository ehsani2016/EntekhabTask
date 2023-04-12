using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace api.EntekhabTask.Controllers
{
    [ApiController]
    public class SalaryController :
        Infrastructure.BaseControllerWithDatabase
    {
        public SalaryController(
                Data.IUnitOfWork unitOfWork,
                DataDapper.IUnitOfWork unitOfWorkDapper,
                IConfiguration configuration,
                OvertimePolicies.IOvertimeCalculator overtimeCalculator
            )
            : base(unitOfWork, unitOfWorkDapper, configuration)
        {
            OvertimeCalculator = overtimeCalculator;
        }

        private OvertimePolicies.IOvertimeCalculator OvertimeCalculator { get; }


        [HttpPost]
        [Route(template: "{dataType}/[controller]/[action]")]
        public async Task<IActionResult> Add(string dataType, string data, string overTimeCalculator)
        {
            try
            {
                #region Intro Validation 
                if (string.IsNullOrWhiteSpace(dataType))
                {
                    return BadRequest(new ViewModel.ResultApi<string>
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Format(Resources.Messages.IsNullOrEmpty, nameof(dataType)),
                    });
                }

                if (string.IsNullOrWhiteSpace(data))
                {
                    return BadRequest(new ViewModel.ResultApi<string>
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Format(Resources.Messages.IsNullOrEmpty, nameof(data)),
                    });
                }

                if (string.IsNullOrWhiteSpace(overTimeCalculator))
                {
                    return BadRequest(new ViewModel.ResultApi<string>
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Format(Resources.Messages.IsNullOrEmpty, nameof(overTimeCalculator)),
                    });
                }
                #endregion /Intro Validation 

                ViewModel.SalaryViewModel? salaryViewModel = null;
                switch (dataType.ToUpper())
                {
                    case "JSON":
                        #region DeserializeObject 
                        try
                        {
                            salaryViewModel = JsonConvert.DeserializeObject<ViewModel.SalaryViewModel>(data);
                        }
                        catch
                        {
                            return BadRequest(new ViewModel.ResultApi<string>
                            {
                                Code = (int)HttpStatusCode.BadRequest,
                                Message = string.Format(Resources.Messages.DataNotInFormat, nameof(dataType)),
                            });
                        }
                        #endregion /DeserializeObject
                        break;

                    case "XML":
                        #region DeserializeObject 
                        //**** Validate XML STRING ****//
                        #endregion /DeserializeObject 
                        break;

                    case "CS":
                        #region DeserializeObject 
                        //**** Validate CS STRING ****//
                        #endregion /DeserializeObject 
                        break;

                    case "CUSTOM":
                        #region DeserializeObject 

                        var resultProcessString =  UnitOfWork.SalaryRepository.ProcessCustomFormat(data: data, dataType: dataType);

                        if (resultProcessString.Code != (int)HttpStatusCode.OK)
                        {
                            return BadRequest(resultProcessString);
                        }

                        salaryViewModel = resultProcessString.Result;
                        
                        #endregion /DeserializeObject 
                        break;

                    default:
                        return BadRequest(new ViewModel.ResultApi<string>
                        {
                            Code = (int)HttpStatusCode.BadRequest,
                            Message = string.Format(Resources.Messages.DataNotInFormat, nameof(dataType)),
                        });
                }

                if (salaryViewModel == null)
                {
                    return BadRequest(new ViewModel.ResultApi<string>
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Format(Resources.Messages.DataNotInFormat, nameof(dataType)),
                    });
                }

                var idPerson = await UnitOfWork.PersonnelRepository.GetPerson(
                    firstName: salaryViewModel.FirstName,
                    lastName: salaryViewModel.LastName);

                if (idPerson == null)
                {
                    return BadRequest(new ViewModel.ResultApi<string>
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Format(Resources.Messages.PersonNotFound, salaryViewModel.DisplayName),
                    });
                }

                var salaryExist = await UnitOfWork.SalaryRepository
                    .ExistSalaryAsync(idPerson: idPerson.Value, date: salaryViewModel.Date);

                if (salaryExist)
                {
                    return Conflict(new ViewModel.ResultApi<string>
                    {
                        Code = (int)HttpStatusCode.Conflict,
                        Message = Resources.Messages.Conflict,
                    });
                }

                decimal overTimeAmount;
                switch (overTimeCalculator)
                {
                    case "CalcurlatorA":
                        overTimeAmount = OvertimeCalculator.CalcurlatorA
                                (basicSalary: salaryViewModel.BasicSalary, allowance: salaryViewModel.Allowance);
                        break;
                    case "CalcurlatorB":
                        overTimeAmount = OvertimeCalculator.CalcurlatorB
                                (basicSalary: salaryViewModel.BasicSalary, allowance: salaryViewModel.Allowance);
                        break;
                    case "CalcurlatorC":
                        overTimeAmount = OvertimeCalculator.CalcurlatorC
                                (basicSalary: salaryViewModel.BasicSalary, allowance: salaryViewModel.Allowance);
                        break;

                    default:
                        return BadRequest(new ViewModel.ResultApi<string>
                        {
                            Code = (int)HttpStatusCode.BadRequest,
                            Message = Resources.Messages.overTimeCalculatorError,
                        });
                }

                var salary = new Models.Salary
                {
                    IdPersonnel = idPerson.Value,
                    BasicSalary = salaryViewModel.BasicSalary,
                    Allowance = salaryViewModel.Allowance,
                    Transportation = salaryViewModel.Transportation,
                    Tax = salaryViewModel.Tax,
                    Date = salaryViewModel.Date,
                    OverTimeAmount = overTimeAmount,

                    FinalSalaryAmount =
                        salaryViewModel.BasicSalary +
                        salaryViewModel.Allowance +
                        salaryViewModel.Transportation +
                        overTimeAmount -
                        salaryViewModel.Tax,
                };

                await UnitOfWork.SalaryRepository.InsertAsync(salary);
                await UnitOfWork.SaveAsync();


                return Ok(new ViewModel.ResultApi<string>
                {
                    Code = (int)HttpStatusCode.OK,
                    Message = Resources.Messages.Success
                });
            }
            catch (Exception ex) //nLog Logging 
            {
                return BadRequest(new ViewModel.ResultApi<string>
                {
                    Code = (int)HttpStatusCode.ExpectationFailed,
                    Message =
                        Resources.Messages.Exception +
                        Environment.NewLine +
                        ex.Message
                });
            }
        }


        [HttpPut]
        [Route(template: "[controller]/[action]")]
        public async Task<IActionResult> Update(string dataType, string data, string overTimeCalculator)
        {
            try
            {
                #region Intro Validation 
                if (string.IsNullOrWhiteSpace(dataType))
                {
                    return BadRequest(new ViewModel.ResultApi<string>
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Format(Resources.Messages.IsNullOrEmpty, nameof(dataType)),
                    });
                }

                if (string.IsNullOrWhiteSpace(data))
                {
                    return BadRequest(new ViewModel.ResultApi<string>
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Format(Resources.Messages.IsNullOrEmpty, nameof(data)),
                    });
                }

                if (string.IsNullOrWhiteSpace(overTimeCalculator))
                {
                    return BadRequest(new ViewModel.ResultApi<string>
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Format(Resources.Messages.IsNullOrEmpty, nameof(overTimeCalculator)),
                    });
                }
                #endregion /Intro Validation 

                ViewModel.SalaryViewModel? salaryViewModel = null;
                switch (dataType.ToUpper())
                {
                    case nameof(Resources.DataTypes.json):
                        #region DeserializeObject 
                        try
                        {
                            salaryViewModel = JsonConvert.DeserializeObject<ViewModel.SalaryViewModel>(data);
                        }
                        catch
                        {
                            return BadRequest(new ViewModel.ResultApi<string>
                            {
                                Code = (int)HttpStatusCode.BadRequest,
                                Message = string.Format(Resources.Messages.DataNotInFormat, nameof(dataType)),
                            });
                        }
                        #endregion /DeserializeObject
                        break;

                    case nameof(Resources.DataTypes.xml):
                        #region DeserializeObject 
                        //**** Validate XML STRING ****//
                        #endregion /DeserializeObject 
                        break;

                    case nameof(Resources.DataTypes.cs):
                        #region DeserializeObject 
                        //**** Validate CS STRING ****//
                        #endregion /DeserializeObject 
                        break;

                    case nameof(Resources.DataTypes.custom):
                        #region DeserializeObject 
                        var lines = data.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                        if (lines.Length < 2)
                        {
                            return BadRequest(new ViewModel.ResultApi<string>
                            {
                                Code = (int)HttpStatusCode.BadRequest,
                                Message = string.Format(Resources.Messages.DataNotInFormat, nameof(dataType)),
                            });
                        }

                        var paramsValue = lines[1].Split("/", StringSplitOptions.RemoveEmptyEntries);

                        if (paramsValue.Length < 6)
                        {
                            return BadRequest(new ViewModel.ResultApi<string>
                            {
                                Code = (int)HttpStatusCode.BadRequest,
                                Message = string.Format(Resources.Messages.DataNotInFormat, nameof(dataType)),
                            });
                        }

                        salaryViewModel = new ViewModel.SalaryViewModel
                        {
                            FirstName = paramsValue[0],
                            LastName = paramsValue[1],
                            BasicSalary = Convert.ToDecimal(paramsValue[2].Trim()),
                            Allowance = Convert.ToDecimal(paramsValue[3].Trim()),
                            Transportation = Convert.ToDecimal(paramsValue[4].Trim()),
                            Date = PersianDate.Standard.ConvertDate.ToEn(paramsValue[5].Trim())
                        };
                        #endregion /DeserializeObject 
                        break;

                    default:
                        return BadRequest(new ViewModel.ResultApi<string>
                        {
                            Code = (int)HttpStatusCode.BadRequest,
                            Message = string.Format(Resources.Messages.DataNotInFormat, nameof(dataType)),
                        });
                }

                if (salaryViewModel == null)
                {
                    return BadRequest(new ViewModel.ResultApi<string>
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Format(Resources.Messages.DataNotInFormat, nameof(dataType)),
                    });
                }

                var IdPerson = await UnitOfWork.PersonnelRepository.GetPerson(
                    firstName: salaryViewModel.FirstName,
                    lastName: salaryViewModel.LastName);

                if (IdPerson == null)
                {
                    return BadRequest(new ViewModel.ResultApi<string>
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Format(Resources.Messages.PersonNotFound, salaryViewModel.DisplayName),
                    });
                }

                decimal overTimeAmount;
                switch (overTimeCalculator)
                {
                    case "CalcurlatorA":
                        overTimeAmount = OvertimeCalculator.CalcurlatorA
                                (basicSalary: salaryViewModel.BasicSalary, allowance: salaryViewModel.Allowance);
                        break;
                    case "CalcurlatorB":
                        overTimeAmount = OvertimeCalculator.CalcurlatorB
                                (basicSalary: salaryViewModel.BasicSalary, allowance: salaryViewModel.Allowance);
                        break;
                    case "CalcurlatorC":
                        overTimeAmount = OvertimeCalculator.CalcurlatorC
                                (basicSalary: salaryViewModel.BasicSalary, allowance: salaryViewModel.Allowance);
                        break;

                    default:
                        return BadRequest(new ViewModel.ResultApi<string>
                        {
                            Code = (int)HttpStatusCode.BadRequest,
                            Message = Resources.Messages.overTimeCalculatorError,
                        });
                }


                var salary = await UnitOfWork.SalaryRepository.GetSalaryAsync(
                        date: salaryViewModel.Date,
                        idPerson: IdPerson.Value);

                if (salary == null)
                {
                    return BadRequest(new ViewModel.ResultApi<string>
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Format(Resources.Messages.SalaryNotFound, salaryViewModel.DisplayName),
                    });
                }



                salary.BasicSalary = salaryViewModel.BasicSalary;
                salary.Allowance = salaryViewModel.Allowance;
                salary.Transportation = salaryViewModel.Transportation;
                salary.Tax = salaryViewModel.Tax;
                salary.OverTimeAmount = overTimeAmount;
                salary.FinalSalaryAmount =
                   salaryViewModel.BasicSalary +
                   salaryViewModel.Allowance +
                   salaryViewModel.Transportation +
                   overTimeAmount -
                   salaryViewModel.Tax;


                await UnitOfWork.SalaryRepository.UpdateAsync(salary);
                await UnitOfWork.SaveAsync();


                return Ok(new ViewModel.ResultApi<string>
                {
                    Code = (int)HttpStatusCode.OK,
                    Message = Resources.Messages.Success
                });
            }
            catch (Exception ex) //nLog Logging 
            {
                return BadRequest(new ViewModel.ResultApi<string>
                {
                    Code = (int)HttpStatusCode.ExpectationFailed,
                    Message =
                        Resources.Messages.Exception +
                        Environment.NewLine +
                        ex.Message
                });
            }
        }


        [HttpDelete]
        [Route(template: "[controller]/[action]")]
        public async Task<IActionResult> Delete(Guid idPerson, DateTime date)
        {
            if (idPerson == Guid.Empty)
            {
                return BadRequest(new ViewModel.ResultApi<string>
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = Resources.Messages.PersonEmpty,
                });
            }

            var salary = await UnitOfWork.SalaryRepository.GetSalaryAsync(idPerson, date);

            if (salary == null)
            {
                return BadRequest(new ViewModel.ResultApi<string>
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = Resources.Messages.SalaryNotFound,
                });
            }

            await UnitOfWork.SalaryRepository.DeleteAsync(salary);
            await UnitOfWork.SaveAsync();


            return Ok(new ViewModel.ResultApi<string>
            {
                Code = (int)HttpStatusCode.OK,
                Message = Resources.Messages.Success,
            });
        }


        [HttpGet]
        [Route(template: "[controller]/[action]")]
        public IActionResult Get(Guid idPerson, DateTime date)
        {
            if (idPerson == Guid.Empty)
            {
                return BadRequest(new ViewModel.ResultApi<string>
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = Resources.Messages.PersonEmpty,
                });
            }

            var salary = UnitOfWorkDapper.SalaryRepository.GetSalary(idPerson, date);

            return Ok(new ViewModel.ResultApi<Models.Salary>
            {
                Code = (int)HttpStatusCode.OK,
                Message = Resources.Messages.Success,
                Result = salary
            });
        }


        [HttpGet]
        [Route(template: "[controller]/[action]")]
        public IActionResult GetRange(Guid idPerson, DateTime fromDate, DateTime toDate)
        {
            if (idPerson == Guid.Empty)
            {
                return BadRequest(new ViewModel.ResultApi<string>
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = Resources.Messages.PersonEmpty,
                });
            }

            var salaries = UnitOfWorkDapper.SalaryRepository.GetSalary(idPerson, fromDate, toDate);

            return Ok(new ViewModel.ResultApi<Models.Salary>
            {
                Code = (int)HttpStatusCode.OK,
                Message = Resources.Messages.Success,
                Results = salaries
            });
        }
    }
}

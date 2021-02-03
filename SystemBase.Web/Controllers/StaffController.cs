using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SystemBase.Repository.Models;
using SystemBase.Service.Interfaces;

namespace SystemBase.Web.Controllers
{
    [Route("api/[controller]")]
    public class StaffController : Controller
    {
        private readonly ILogger<StaffController> _logger;

        private readonly IStaffService Service;

        public StaffController(ILogger<StaffController> logger, IStaffService staffService)
        {
            _logger = logger;
            Service = staffService;
        }

        [HttpGet]
        public ResultModel Get(string name)
        {
            var result = new ResultModel();
            result.Data = Service.GetByName(name);
            result.IsSuccess = true;

            return result;
        }

        [HttpGet("{id}")]
        public ResultModel Get(int id)
        {
            var result = new ResultModel();
            result.Data = Service.Get(id);
            result.IsSuccess = true;

            return result;
        }

        [HttpPost]
        public ResultModel Post([FromBody] Staff user)
        {
            var result = new ResultModel();
            result.Data = Service.Create(user);
            result.IsSuccess = true;

            return result;
        }

        [HttpPut("{id}")]
        public ResultModel Put([FromBody] Staff user)
        {
            var result = new ResultModel();
            var oriUser = Service.Get(user.Id);
            if (oriUser != null)
            {
                Service.Update(user);
                result.IsSuccess = true;
            }

            return result;
        }

        [HttpDelete("{id}")]
        public ResultModel Delete(int id)
        {
            var result = new ResultModel();
            result.IsSuccess = Service.Delete(id);

            return result;
        }
    }
}

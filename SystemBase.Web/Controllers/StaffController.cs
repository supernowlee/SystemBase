using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SystemBase.Repository;
using SystemBase.Repository.Models;
using SystemBase.Web.Models;

namespace SystemBase.Web.Controllers
{
    [Route("api/[controller]s")]
    public class StaffController : Controller
    {
        private readonly ILogger<StaffController> _logger;

        private readonly StaffContext Reposty;

        public StaffController(ILogger<StaffController> logger, StaffContext staffReposty)
        {
            _logger = logger;
            Reposty = staffReposty;
        }

        [HttpGet]
        public ResultModel Get(string q)
        {
            var result = new ResultModel();
            result.Data = Reposty.Users.Where(x => string.IsNullOrEmpty(q)
                                                 || Regex.IsMatch(x.Name, q, RegexOptions.IgnoreCase));
            result.IsSuccess = true;
            return result;
        }

        [HttpGet("{id}")]
        public ResultModel Get(int id)
        {
            var result = new ResultModel();
            result.Data = Reposty.Users.SingleOrDefault(x => x.Id == id);
            result.IsSuccess = true;
            return result;
        }

        [HttpPost]
        public ResultModel Post([FromBody] Staff user)
        {
            var result = new ResultModel();
            Reposty.Users.Add(user);
            Reposty.SaveChanges();
            result.Data = user.Id;
            result.IsSuccess = true;
            return result;
        }

        [HttpPut("{id}")]
        public ResultModel Put([FromBody] Staff user)
        {
            var result = new ResultModel();
            var oriUser = Reposty.Users.SingleOrDefault(x => x.Id == user.Id);
            if (oriUser != null)
            {
                Reposty.Entry(oriUser).CurrentValues.SetValues(user);
                Reposty.SaveChanges();
                result.IsSuccess = true;
            }
            return result;
        }

        [HttpDelete("{id}")]
        public ResultModel Delete(int id)
        {
            var result = new ResultModel();
            var oriUser = Reposty.Users.SingleOrDefault(x => x.Id == id);
            if (oriUser != null)
            {
                Reposty.Users.Remove(oriUser);
                Reposty.SaveChanges();
                result.IsSuccess = true;
            }
            return result;
        }
    }
}

using System.Linq;
using SystemBase.Repository.Models;

namespace SystemBase.Web.Controllers
{
    public class ResultModel
    {
        public object Data { get; set; }

        public bool IsSuccess { get; internal set; }

        public string Message { get; set; }
    }
}
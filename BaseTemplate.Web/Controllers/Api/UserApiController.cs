using BaseTemplate.Core.Interface;
using BaseTemplate.Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BaseTemplate.Web.Controllers.Api {
    [Route("api/UserApi")]
    public class UserApiController : Controller {
        private readonly IWorkUnit _workUnit;
        
        public UserApiController(IWorkUnit workUnit)
        {
            _workUnit = workUnit;
        }
        [HttpGet("[action]")]
        public IActionResult GetUsers() {
            return Json(new { data =  _workUnit.User.GetAll()});
        }
    }
}

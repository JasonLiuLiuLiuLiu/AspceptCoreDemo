using Microsoft.AspNetCore.Mvc;

namespace AspceptCoreDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IUserService _userService;

        public ValuesController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public ActionResult<string> Post(ApiRequest req)
        {
            return _userService.GetUserName(req);
        }
    }
}

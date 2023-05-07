using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using WorkrsBackend.DataHandling;
using WorkrsBackend.DTOs;

namespace WorkrsBackend.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class ServiceTaskController : ControllerBase
    {
        ISharedResourceHandler _sharedResourceHandler;
        IIdentity? _identity;

        public ServiceTaskController(ISharedResourceHandler sharedResourceHandler, IHttpContextAccessor httpContextAccessor)
        {
            _sharedResourceHandler = sharedResourceHandler;
            _identity = httpContextAccessor?.HttpContext?.User?.Identity;
        }

        [HttpGet]
        public ActionResult<List<ServiceTaskDTO>> Tasks() 
        {
            if (_identity != null)
            {
                var username = _sharedResourceHandler.FindClientByUserName(_identity.Name);
                if (username != null)
                {
                    return Ok(_sharedResourceHandler.GetTaskForClient(username.ClientId));
                }
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Create(ServiceTaskDTO serviceTask)
        {
            if(_identity != null)
            {
                var username = _sharedResourceHandler.FindClientByUserName(_identity.Name);
                if (username != null)
                {
                    _sharedResourceHandler.AddTask(serviceTask);
                    return Ok();
                }
            }

            return NotFound();
        }
    }
}

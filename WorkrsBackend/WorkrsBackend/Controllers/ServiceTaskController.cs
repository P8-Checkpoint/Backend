using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using System.Text;
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

        [HttpGet]
        public ActionResult<ServiceTaskDTO> Task(Guid taskId)
        {
            var result = _sharedResourceHandler.GetTaskFromId(taskId);
            if (result != null) 
                return Ok(result);

            return NotFound();
        }

        [HttpPut]
        public IActionResult Cancel(Guid taskId)
        {
            var result = _sharedResourceHandler.GetTaskFromId(taskId);
            if (result != null)
            {
                if(result.Status < ServiceTaskStatus.Cancel )
                {
                    result.Status = ServiceTaskStatus.Cancel;
                    _sharedResourceHandler.UpdateTask(result);
                    return StatusCode(202);
                }
                return BadRequest();
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Create(string serviceTaskName)
        {
            if(_identity != null)
            {
                var client = _sharedResourceHandler.FindClientByUserName(_identity.Name);
                if (client != null)
                {
                    var t = new ServiceTaskDTO(
                                        Guid.NewGuid(),
                                        client.ClientId,
                                        serviceTaskName,
                                        ServiceTaskStatus.Created);
                    _sharedResourceHandler.AddTask(t);
                    return StatusCode(201,t);
                }
            }

            return NotFound();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkrsBackend.DataHandling;
using WorkrsBackend.DTOs;

namespace WorkrsBackend.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class ClientController : ControllerBase
    {
        ISharedResourceHandler _sharedResourceHandler;
        IHttpContextAccessor _httpContextAccessor;
        public ClientController(/*ISharedResourceHandler sharedResourceHandler,*/ IHttpContextAccessor httpContextAccessor)
        {
            //_sharedResourceHandler = sharedResourceHandler;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult CreateClient(ClientDTO client)
        {
            _sharedResourceHandler.AddClientToClientDHT(client);
            return Ok();
        }

        [HttpGet]
        public ActionResult GetClient(string clientName) 
        {
            var username = _httpContextAccessor.HttpContext.User.Identity.Name;
            ClientDTO c  =_sharedResourceHandler.FindClientByUserName(clientName);
            if(c != null)
            {
                return Ok(c);
            }

            return NotFound();
        }

    }
}
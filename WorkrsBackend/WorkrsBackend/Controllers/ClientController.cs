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
        public ClientController(ISharedResourceHandler sharedResourceHandler)
        {
            _sharedResourceHandler = sharedResourceHandler;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult CreateClient(ClientDTO client)
        {
            _sharedResourceHandler.AddClientToClientDHT(client);
            return Ok();
        }
    }
}

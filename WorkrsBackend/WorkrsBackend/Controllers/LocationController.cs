using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkrsBackend.DataHandling;
using WorkrsBackend.DTOs;

namespace WorkrsBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    [Authorize]
    public class LocationController : ControllerBase
    {
        ISharedResourceHandler _sharedResourceHandler;
        public LocationController(ISharedResourceHandler sharedResourceHandler)
        {
            _sharedResourceHandler = sharedResourceHandler;
        }

        [HttpGet]
        public ActionResult<LocationDTO> GetNearbyWorkers(LocationDTO locationDTO) 
        {
            return Ok(new LocationDTO() { Latitude = 0, Longitude = 0 });
        }
    }
}

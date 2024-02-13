using APBD_5_S20414.Data;
using APBD_5_S20414.Models.DTOs;
using APBD_5_S20414.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APBD_5_S20414.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripsService _tripsService;
        public TripsController(ITripsService tripsService) 
        {
            _tripsService = tripsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrips() 
        {
            var trips = await _tripsService.GetTripWithAdditionalData2();
            return Ok(trips);
        }

        [HttpPost("{idTrip}/clients")]
        public async Task<IActionResult> AssignClientToTrip(int idTrip, [FromBody] ClientToTripRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            request.TripID = idTrip;
            var success = await _tripsService.AssignClientToTrip(request);
            if (success)
            {
                return Ok();
            }
            else
            {
                return NotFound(); 
            }
        }

        [HttpDelete("clients/{idClient}")]
        public async Task<IActionResult> RemoveClient(int idClient)
        {
            var success = await _tripsService.RemoveClient(idClient);
            if (success)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}

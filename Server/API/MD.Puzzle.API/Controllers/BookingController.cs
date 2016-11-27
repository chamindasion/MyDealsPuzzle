using MD.Puzzle.Models.Dto;
using MD.Puzzle.Service.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace MD.Puzzle.API.Controllers
{
    [RoutePrefix("api")]
    public class BookingController : ApiController
    {
        private readonly IBookingService _bookingService;

        public BookingController(IServiceFactory serviceFactory)
        {
            _bookingService = serviceFactory.GetService<IBookingService>();
        }

        [Route("recordlocators")]
        public IEnumerable<RecordLocatorDto> Get(string searchCriteria = "")
        {
            return _bookingService.Get(!string.IsNullOrWhiteSpace(searchCriteria) ? searchCriteria.ToLower() : searchCriteria);
        }

        // POST api/booking        
        [Route("passengers")]
        public void Post([FromBody]PassengerDto passenger)
        {
            if (passenger == null || string.IsNullOrWhiteSpace(passenger.FirstName) ||
                string.IsNullOrWhiteSpace(passenger.LastName) ||
                string.IsNullOrWhiteSpace(passenger.RecordLocatorName))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            _bookingService.Add(passenger);
        }
    }
}

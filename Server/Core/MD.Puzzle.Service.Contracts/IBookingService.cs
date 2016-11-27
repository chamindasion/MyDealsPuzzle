
using MD.Puzzle.Models.Dto;
using System.Collections.Generic;

namespace MD.Puzzle.Service.Contracts
{
    public interface IBookingService
    {
        IEnumerable<RecordLocatorDto> Get(string searchCriteria);
        void Add(PassengerDto passenger);
    }
}

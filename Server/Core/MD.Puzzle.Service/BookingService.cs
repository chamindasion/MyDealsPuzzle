
using MD.Puzzle.Data.Contracts;
using MD.Puzzle.Models;
using MD.Puzzle.Models.DomainObjects;
using MD.Puzzle.Models.Dto;
using MD.Puzzle.Service.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace MD.Puzzle.Service
{
    public class BookingService : IBookingService
    {
        private readonly IFileRepository _fileRepository;

        public BookingService(IServiceFactory serviceFactory)
        {
            _fileRepository = serviceFactory.GetService<IFileRepository>();
        }

        public IEnumerable<RecordLocatorDto> Get(string searchCriteria)
        {
            var recordLocators = _fileRepository.Get(searchCriteria);
            if (recordLocators != null)
            {
                return recordLocators.Select(s =>
                {
                    var recordLocator = s.BasicConvertTo<RecordLocatorDto>();
                    recordLocator.Passengers = s.Passengers.Select(r => r.BasicConvertTo<PassengerDto>()).ToList();
                    return recordLocator;
                }).ToList();
            }
            return new List<RecordLocatorDto>();
        }

        public void Add(PassengerDto passenger)
        {
            var tempPassenger = passenger.BasicConvertTo<Passenger>();
            _fileRepository.Add(tempPassenger, passenger.RecordLocatorName);
        }
    }
}


using System.Collections.Generic;

namespace MD.Puzzle.Models.Dto
{
    public class RecordLocatorDto : BaseDto
    {
        public string Name { get; set; }
        public IList<PassengerDto> Passengers { get; set; }
    }
}

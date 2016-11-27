

using System.Collections.Generic;

namespace MD.Puzzle.Models.DomainObjects
{
    public class RecordLocator : BaseDomainObject
    {
        public string Name { get; set; }
        public IList<Passenger> Passengers { get; set; }
    }
}

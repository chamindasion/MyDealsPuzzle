using MD.Puzzle.Models.DomainObjects;
using System.Collections.Generic;

namespace MD.Puzzle.Data.Contracts
{
    public interface IFileRepository
    {
        IEnumerable<RecordLocator> Get(string searchCriteria);
        void Add(Passenger passenger, string recordLocatorName);
    }
}

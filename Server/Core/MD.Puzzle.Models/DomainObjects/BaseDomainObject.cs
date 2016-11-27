using System;

namespace MD.Puzzle.Models.DomainObjects
{
    public abstract class BaseDomainObject : IIdentity
    {
        public Guid Id { get; set; }
    }
}

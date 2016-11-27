
namespace MD.Puzzle.Service.Contracts
{
    public interface IServiceFactory
    {
        TService GetService<TService>();
    }
}

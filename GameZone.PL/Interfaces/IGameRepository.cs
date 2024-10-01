namespace GameZone.PL.Interfaces
{
    public interface IGameRepository : IGenericRepository<Game>
    {
        IEnumerable<Game> GetAllGames();
    }
}

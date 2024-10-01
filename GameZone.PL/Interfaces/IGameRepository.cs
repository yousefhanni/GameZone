namespace GameZone.PL.Interfaces
{
    public interface IGameRepository : IGenericRepository<Game>
    {
    Game? GetById(int id);
        IEnumerable<Game> GetAllGames();
    }
}

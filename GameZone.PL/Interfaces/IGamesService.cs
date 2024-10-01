using GameZone.PL.ViewModels;

namespace GameZone.PL.Interfaces
{
public interface IGamesService
{
    IEnumerable<Game> GetAll();
    Game? GetById(int id);
    Game? GetByIdToDetails(int id);
     Game? GetByIdToEdit(int id);

        Task CreateAsync(CreateGameFormViewModel model);
    Task<Game?> UpdateAsync(EditGameFormViewModel model);
    Task DeleteAsync(int id);
}
}
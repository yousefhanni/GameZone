namespace GameZone.PL.Interfaces;

public interface ICategoriesService
{
    IEnumerable<SelectListItem> GetSelectList();//game controller only

    //=> all deal with Category controller =>
    Task AddCategoryAsync(Category category);
    Task UpdateCategoryAsync(Category category);
    Category GetById(int id);
    Task DeleteCategoryAsync(int id);
    IEnumerable<Category> GetAllCategories(); // New method

}
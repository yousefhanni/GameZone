   namespace GameZone.PL.Services;
    public class CategoriesService : ICategoriesService
    {
    private readonly IGenericRepository<Category> _categoryRepository;
    private readonly IGenericRepository<Game> _gameRepository;

    public CategoriesService(IGenericRepository<Category> categoryRepository,
                             IGenericRepository<Game> gameRepository)
    {
        _categoryRepository = categoryRepository;
        _gameRepository = gameRepository;
    }
    //To Game Controller only  
       public IEnumerable<SelectListItem> GetSelectList()
        {
            return _categoryRepository.GetAll()
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .OrderBy(c => c.Text)
                .ToList();
        }

        public async Task AddCategoryAsync(Category category)
        {
            _categoryRepository.Add(category);
            await _categoryRepository.SaveAsync();
        }
        public async Task UpdateCategoryAsync(Category category)
        {
            _categoryRepository.Update(category);
            await _categoryRepository.SaveAsync();
        }
        public Category GetById(int id) 
        {
            return _categoryRepository.GetById(id);
        }
        public async Task DeleteCategoryAsync(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category != null)
            {
                _categoryRepository.Delete(category);
                await _categoryRepository.SaveAsync();
            }
        }
       public IEnumerable<Category> GetAllCategories()
    {
        return _categoryRepository.GetAll().Select(category =>
        {
            category.Games = _gameRepository.GetAll().Where(g => g.CategoryId == category.Id).ToList();
            return category;
        });
    }
}
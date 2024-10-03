using Microsoft.AspNetCore.Authorization;

namespace GameZone.PL.Controllers
{
	[Authorize] 
	public class CategoriesController : Controller
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
        var categories = _categoriesService.GetAllCategories()
             .Select(c => new CategoryViewModel
             {
                 Id = c.Id,
                 Name = c.Name,
                 GameCount = c.Games.Count()  // Calculate the number of games
             }).ToList();


            return View(categories); // Ensure the correct model is being passed to the view
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View(new CategoryViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var category = new Category { Name = viewModel.Name };
                await _categoriesService.AddCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _categoriesService.GetById(id); // Assuming GetById method in service
            return View(new CategoryViewModel { Id = category.Id, Name = category.Name });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var category = new Category { Id = viewModel.Id, Name = viewModel.Name };
                await _categoriesService.UpdateCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _categoriesService.DeleteCategoryAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}

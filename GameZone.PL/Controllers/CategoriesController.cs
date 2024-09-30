using GameZone.PL.Interfaces;

namespace GameZone.PL.Controllers
{
    //public class CategoriesController : Controller
    //{
    //    private readonly ApplicationDbContext _context;

    //    public CategoriesController(ApplicationDbContext context)
    //    {
    //        _context = context;
    //    }

    //    public IActionResult Index()
    //    {
    //        var categories = _context.Categories
    //            .Select(c => new CategoryViewModel
    //            {
    //                Id = c.Id,
    //                Name = c.Name,
    //                GameCount = _context.Games.Count(g => g.CategoryId == c.Id) // Calculating the GameCount
    //            })
    //            .ToList();

    //        return View(categories);
    //    }


    //    [HttpGet]
    //    public IActionResult Create()
    //    {
    //        return View(new CategoryViewModel());
    //    }

    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Create(CategoryViewModel viewModel)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var category = new Category
    //            {
    //                Name = viewModel.Name
    //            };

    //            _context.Categories.Add(category);
    //            await _context.SaveChangesAsync();
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(viewModel);
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> Edit(int id)
    //    {
    //        var category = await _context.Categories.FindAsync(id);
    //        if (category == null)
    //        {
    //            return NotFound();
    //        }

    //        var viewModel = new CategoryViewModel
    //        {
    //            Id = category.Id,
    //            Name = category.Name
    //        };

    //        return View(viewModel);
    //    }

    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Edit(CategoryViewModel viewModel)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var category = await _context.Categories.FindAsync(viewModel.Id);
    //            if (category == null)
    //            {
    //                return NotFound();
    //            }

    //            category.Name = viewModel.Name;
    //            _context.Update(category);
    //            await _context.SaveChangesAsync();
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(viewModel);
    //    }
    //    // GET: Categories/Delete/5
    //    public IActionResult Delete(int id)
    //    {
    //        var category = _context.Categories.Find(id);
    //        if (category == null)
    //        {
    //            return NotFound();
    //        }

    //        var model = new CategoryViewModel { Id = category.Id, Name = category.Name };
    //        return View(model);
    //    }

    //    // POST: Categories/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public IActionResult DeleteConfirmed(int id)
    //    {
    //        var category = _context.Categories.Find(id);
    //        if (category == null)
    //        {
    //            return NotFound();
    //        }

    //        _context.Categories.Remove(category);
    //        _context.SaveChanges();
    //        return RedirectToAction(nameof(Index));
    //    }

    //}
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
                    GameCount = c.Games.Count(g => g.CategoryId == c.Id)  // Assuming this is calculated
                })
                .ToList();

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

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoriesService.DeleteCategoryAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

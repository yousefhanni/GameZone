using GameZone.PL.Interfaces;

namespace GameZone.PL.Controllers
{
    //public class GamesController : Controller
    //{
    //    private readonly ICategoriesService _categoriesService;
    //    private readonly IDevicesService _devicesService;

    //    public GamesController(ICategoriesService categoriesService,
    //        IDevicesService devicesService
    //       )
    //    {
    //        _categoriesService = categoriesService;
    //        _devicesService = devicesService;
    //    }

    //    public IActionResult Index()
    //    {
    //        return View();
    //    }

    //    [HttpGet]
    //    public IActionResult Create()
    //    {
    //        CreateGameFormViewModel viewModel = new()
    //        {
    //            Categories = _categoriesService.GetSelectList(),

    //            Devices = _devicesService.GetSelectList(),  
    //        };

    //        return View(viewModel);
    //    }

    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Create(CreateGameFormViewModel model)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            model.Categories = _categoriesService.GetSelectList();

    //            model.Devices = _devicesService.GetSelectList();

    //            return View(model);
    //        }

    //        // save the game to the database
    //        // save cover to the server

    //        return RedirectToAction(nameof(Index));
    //    }
    //}
    public class GamesController : Controller
    {
        private readonly IGamesService _gamesService;
        private readonly ICategoriesService _categoriesService;
        private readonly IDevicesService _devicesService;

        public GamesController(IGamesService gamesService,
                               ICategoriesService categoriesService,
                               IDevicesService devicesService)
        {
            _gamesService = gamesService;
            _categoriesService = categoriesService;
            _devicesService = devicesService;
        }

        public IActionResult Index()
        {
            var games = _gamesService.GetAll();
            return View(games);
        }

        public IActionResult Details(int id)
        {
            var game = _gamesService.GetById(id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new CreateGameFormViewModel
            {
                Categories = _categoriesService.GetSelectList(),
                Devices = _devicesService.GetSelectList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _gamesService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }

            model.Categories = _categoriesService.GetSelectList();
            model.Devices = _devicesService.GetSelectList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game = _gamesService.GetById(id);
            if (game == null)
            {
                return NotFound();
            }

            var viewModel = new EditGameFormViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                CategoryId = game.CategoryId,
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                CurrentCover = game.Cover,
                Categories = _categoriesService.GetSelectList(),
                Devices = _devicesService.GetSelectList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var game = await _gamesService.UpdateAsync(model);
                if (game != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Unable to update the game.");
            }

            model.Categories = _categoriesService.GetSelectList();
            model.Devices = _devicesService.GetSelectList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _gamesService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

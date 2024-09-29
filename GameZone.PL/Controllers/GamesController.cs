namespace GameZone.PL.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GamesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormViewModel viewModel = new()
            {
                Categories = _context.Categories
                             .Select(c => new SelectListItem
                             {
                                 Value = c.Id.ToString(),
                                 Text = c.Name
                             }).OrderBy(c => c.Text)
                             .ToList(),

                Devices = _context.Devices
                             .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                             .OrderBy(d => d.Text)
                             .ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _context.Categories
                             .Select(c => new SelectListItem
                             {
                                 Value = c.Id.ToString(),
                                 Text = c.Name
                             }).OrderBy(c => c.Text)
                             .ToList();

                model.Devices = _context.Devices
                             .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                             .OrderBy(d => d.Text)
                             .ToList();

                return View(model);
            }

            // save the game to the database
            // save cover to the server

            return RedirectToAction(nameof(Index));
        }
    }
}

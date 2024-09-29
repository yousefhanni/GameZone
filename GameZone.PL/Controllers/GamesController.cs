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
                //Fetch Categories from database and Select() method transforms each Category into a SelectListItem
                Categories = _context.Categories 
                             .Select(c => new SelectListItem //convert each category from db to SelectListItem
                             {
                                 Value = c.Id.ToString(),
                                 Text = c.Name
                             }).OrderBy(c=>c.Text)//To order Categories by name
                             .ToList(),

                    Devices = _context.Devices
                             .Select(d => new SelectListItem  {Value = d.Id.ToString(),Text = d.Name })
                             .OrderBy(d => d.Text)
                             .ToList()
            };

            return View(viewModel);
        }
    }
}

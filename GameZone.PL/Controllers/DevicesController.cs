namespace GameZone.PL.Controllers
{
    public class DevicesController : Controller
    {
        private readonly IDevicesService _devicesService;

        public DevicesController(IDevicesService devicesService)
        {
            _devicesService = devicesService;
        }
        public IActionResult Index()
        {
            var devices = _devicesService.GetAllDevices()
                .Select(d => new DeviceViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    Icon = d.Icon,
                    SupportedGameCount = _devicesService.GetSupportedGameCount(d.Id)  // Calculate supported game count
                })
                .ToList();

            return View(devices);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new DeviceViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeviceViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var device = new Device { Name = viewModel.Name, Icon = viewModel.Icon };
                await _devicesService.AddDeviceAsync(device);
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var device = _devicesService.GetById(id); // Assuming GetById method in service
            return View(new DeviceViewModel { Id = device.Id, Name = device.Name, Icon = device.Icon });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DeviceViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var device = new Device { Id = viewModel.Id, Name = viewModel.Name, Icon = viewModel.Icon };
                await _devicesService.UpdateDeviceAsync(device);
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _devicesService.DeleteDeviceAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

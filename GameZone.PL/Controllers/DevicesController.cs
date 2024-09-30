using GameZone.PL.Interfaces;

namespace GameZone.PL.Controllers
{
    //public class DevicesController : Controller
    //{
    //    private readonly ApplicationDbContext _context;

    //    public DevicesController(ApplicationDbContext context)
    //    {
    //        _context = context;
    //    }
    //    public IActionResult Index()
    //    {
    //        var devices = _context.Devices
    //            .Select(d => new DeviceViewModel
    //            {
    //                Id = d.Id,
    //                Name = d.Name,
    //                Icon = d.Icon,
    //                SupportedGameCount = _context.GameDevices.Count(gd => gd.DeviceId == d.Id) // Calculating the SupportedGameCount
    //            })
    //            .ToList();

    //        return View(devices);
    //    }

    //    [HttpGet]
    //    public IActionResult Create()
    //    {
    //        return View(new DeviceViewModel());
    //    }

    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Create(DeviceViewModel viewModel)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var device = new Device
    //            {
    //                Name = viewModel.Name,
    //                Icon = viewModel.Icon
    //            };

    //            _context.Devices.Add(device);
    //            await _context.SaveChangesAsync();
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(viewModel);
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> Edit(int id)
    //    {
    //        var device = await _context.Devices.FindAsync(id);
    //        if (device == null)
    //        {
    //            return NotFound();
    //        }

    //        var viewModel = new DeviceViewModel
    //        {
    //            Id = device.Id,
    //            Name = device.Name,
    //            Icon = device.Icon
    //        };

    //        return View(viewModel);
    //    }

    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Edit(DeviceViewModel viewModel)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var device = await _context.Devices.FindAsync(viewModel.Id);
    //            if (device == null)
    //            {
    //                return NotFound();
    //            }

    //            device.Name = viewModel.Name;
    //            device.Icon = viewModel.Icon;
    //            _context.Update(device);
    //            await _context.SaveChangesAsync();
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(viewModel);
    //    } 

    //    // GET: Devices/Delete/5
    //    public IActionResult Delete(int id)
    //    {
    //        var device = _context.Devices.Find(id);
    //        if (device == null)
    //        {
    //            return NotFound();
    //        }

    //        var model = new DeviceViewModel { Id = device.Id, Name = device.Name, Icon = device.Icon };
    //        return View(model);
    //    }

    //    // POST: Devices/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public IActionResult DeleteConfirmed(int id)
    //    {
    //        var device = _context.Devices.Find(id);
    //        if (device == null)
    //        {
    //            return NotFound();
    //        }

    //        _context.Devices.Remove(device);
    //        _context.SaveChanges();
    //        return RedirectToAction(nameof(Index));
    //    }

    //}
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

namespace GameZone.PL.Services;
public class DevicesService : IDevicesService
{
    private readonly IGenericRepository<Device> _deviceRepository;
    private readonly IGenericRepository<GameDevice> _gameDeviceRepository;

    public DevicesService(IGenericRepository<Device> deviceRepository,
                          IGenericRepository<GameDevice> gameDeviceRepository)
    {
        _deviceRepository = deviceRepository;
        _gameDeviceRepository = gameDeviceRepository;
    }

    //To Game Controller only  
    public IEnumerable<SelectListItem> GetSelectList()
    {
        return _deviceRepository.GetAll()
            .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
            .OrderBy(d => d.Text)
            .ToList();
    }

    public async Task AddDeviceAsync(Device device)
    {
        _deviceRepository.Add(device);
        await _deviceRepository.SaveAsync();
    }
    public async Task UpdateDeviceAsync(Device device)
    {
        _deviceRepository.Update(device);
        await _deviceRepository.SaveAsync();
    }
    public Device GetById(int id)
    {
        return _deviceRepository.GetById(id);
    }
    public async Task DeleteDeviceAsync(int id)
    {
        var device = _deviceRepository.GetById(id);
        if (device != null)
        {
            _deviceRepository.Delete(device);
            await _deviceRepository.SaveAsync();
        }
    }
    public IEnumerable<Device> GetAllDevices()
    {
        return _deviceRepository.GetAll();
    }
    public int GetSupportedGameCount(int deviceId)
    {
        return _gameDeviceRepository.GetAll()
            .Count(gd => gd.DeviceId == deviceId);
    }

}


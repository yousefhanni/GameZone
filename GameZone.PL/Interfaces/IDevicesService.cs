namespace GameZone.PL.Interfaces;

public interface IDevicesService
{
    IEnumerable<SelectListItem> GetSelectList();//game controller only

    Task AddDeviceAsync(Device device);
    Task UpdateDeviceAsync(Device device);
    Device GetById(int id);
    Task DeleteDeviceAsync(int id);
    IEnumerable<Device> GetAllDevices(); 
    int GetSupportedGameCount(int deviceId);  // Add this method to calculate the supported games

}

namespace GameZone.PL.ViewModels
{
    public class DeviceViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Icon { get; set; } = string.Empty;

        public int SupportedGameCount { get; set; } 
    }
}

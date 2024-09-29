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

        // أي بيانات إضافية تحتاجها في الـView يمكنك إضافتها هنا
        public int SupportedGameCount { get; set; } // عدد الألعاب التي تدعم هذا الجهاز
    }
}

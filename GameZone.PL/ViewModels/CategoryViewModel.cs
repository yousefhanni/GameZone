namespace GameZone.PL.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;

        // أي بيانات إضافية تحتاجها في الـView يمكنك إضافتها هنا
        public int GameCount { get; set; } // عدد الألعاب في هذه الفئة
    }
}

﻿
namespace GameZone.ViewModels
{
    public class CreateGameFormViewModel
    {
        [MaxLength(250)]
        public string Name { get; set; }
        [Display(Name = "Category")]
        public int CaregoryId { get; set; }
        public IEnumerable<SelectListItem>? Categories { get; set; } = Enumerable.Empty<SelectListItem>();

        [Display(Name = "Support Device")]
        public List<int> SelectedDevices { get; set; } = default!;
        public IEnumerable<SelectListItem>? Devices { get; set; } = default!; 

        [MaxLength(2500)]
        public string Description { get; set; }
        public IFormFile Cover { get; set; }
    }
}


using GameZone.PL.Attributes;

namespace GameZone.PL.ViewModels
{
    public class CreateGameFormViewModel
    {
        [MaxLength(250)]
        public string Name { get; set; }    
        [Display(Name = "Category")]
        public int CaregoryId { get; set; }//ID of the selected category for the game
        //Get available categories in a dropdown list.
        public IEnumerable<SelectListItem>? Categories { get; set; } = Enumerable.Empty<SelectListItem>();

        [Display(Name = "Support Device")]
        public List<int> SelectedDevices { get; set; } = default!;
        public IEnumerable<SelectListItem>? Devices { get; set; } = default!; 

        [MaxLength(2500)]
        public string Description { get; set; }
        
        [FileExtensions(Extensions = "jpg,jpeg,png", ErrorMessage = "Only .jpg, .jpeg, .png files are allowed.")]
        [MaxFileSize(FileSettings.MaxFileSizeInMB)]
        public IFormFile Cover { get; set; }
    }
}

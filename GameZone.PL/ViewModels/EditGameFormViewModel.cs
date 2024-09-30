

using GameZone.PL.Attributes;

namespace GameZone.PL.ViewModels;

public class EditGameFormViewModel : GameFormViewModel
{
    public int Id { get; set; }

    public string? CurrentCover { get; set; }

    [FileExtensions(Extensions = "jpg,jpeg,png", ErrorMessage = "Only .jpg, .jpeg, .png files are allowed.")]
    [MaxFileSize(FileSettings.MaxFileSizeInMB)]
    public IFormFile? Cover { get; set; } = default!;
}
namespace GameZone.PL.Attributes;

//Custom Data Annotation(Attribute) to Validation of AllowedExtensions
public class AllowedExtensionsAttribute : ValidationAttribute
{
    //private field recieve diff allowedExtensions with dynamic way not constant Extensions 
    private readonly string _allowedExtensions;

    //recieve allowedExtensions as parameter
    public AllowedExtensionsAttribute(string allowedExtensions)
    {
        _allowedExtensions = allowedExtensions;
    }

    //override on IsValid method that exist at ValidationAttribute 
    protected override ValidationResult? IsValid
        (object? value, ValidationContext validationContext)
    {
        //Cast value from object type to IFormFile
        var file = value as IFormFile;
        
        if(file is not null)
        {
            //Get extension of File come from Form 
            var extension = Path.GetExtension(file.FileName);

            //Check that extension of File is Allowed or not 
            var isAllowed = _allowedExtensions.Split(',').Contains(extension, StringComparer.OrdinalIgnoreCase);

            if (!isAllowed)
            {
                return new ValidationResult($"Only {_allowedExtensions} are allowed!");
            }
        }

        return ValidationResult.Success;
    }
}
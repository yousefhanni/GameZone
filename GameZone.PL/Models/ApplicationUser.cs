using Microsoft.AspNetCore.Identity;

namespace GameZone.PL.Models
{
	public class ApplicationUser :IdentityUser
	{
		[Required]
		public string FName { get; set; }
		[Required]
		public string LName { get; set; }
		[Required]
		public bool IsAgree { get; set; }
	}
}

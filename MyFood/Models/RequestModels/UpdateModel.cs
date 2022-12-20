using System.Data;

namespace MyFood.Models.RequestModels
{
    public class UpdateModel
    {
        
        [StringLength(20, MinimumLength = 3)]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(20)]
        public string? LastName { get; set; }

        
        [StringLength(50)]
        public string? Email { get; set; }

        
        [StringLength(10)]
        public string? PhoneNumber { get; set; }

       

    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BandageLanding.Infrastructure.Models
{
    public class Lead
    {
        [Key]
        public int LeadID { get; set; }
        [Column(TypeName = "NVARCHAR")]
        [StringLength(450)]
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9])+$", ErrorMessage = "You must enter a valid email address.")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        public string IPAddress { get; set; }
        public DateTime DateCreated { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BandageLanding.Infrastructure.Models
{
    public class Session
    {
        [Key]
        public string Token { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(450)]
        public string UserName { get; set; }     
        public DateTime Expiration { get; set; }
        
        public bool IsExpired {
            get { return Expiration <= DateTime.UtcNow; }
        }
    }
}
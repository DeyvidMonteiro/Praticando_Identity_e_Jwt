using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiIdentityJwt.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Column("Usr_RG")]
        public string Rg { get; set; }

    }
}

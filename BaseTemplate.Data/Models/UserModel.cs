using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseTemplate.Data.Models {
    public class UserModel : IdentityUser {
        public string FirstName { get; set; } = string.Empty;
        public string LastName  { get; set; } = string.Empty;
        
        [NotMapped]
        public string? FullName => $"{FirstName.Trim()} {LastName.Trim()}";
    }
}

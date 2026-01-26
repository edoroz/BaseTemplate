using BaseTemplate.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseTemplate.Data.Contexts {
    public class AppDbContext : IdentityDbContext<UserModel, RoleModel, string> {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
             
        }

        public DbSet<UserModel> Users { get; set; } 
        public DbSet<RoleModel> Roles { get; set; }
    }
}

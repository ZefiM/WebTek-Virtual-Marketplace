using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebTek.Models
{
    public class AppDataBase : IdentityDbContext<UserAccount, IdentityRole<Guid>, Guid>

    {
        public AppDataBase(DbContextOptions<AppDataBase> options) :
       base(options)
        { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
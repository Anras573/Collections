using Collections.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Collections.Domain.Contexts
{
    public class ApplicationIdentityDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ApplicationIdentityDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}

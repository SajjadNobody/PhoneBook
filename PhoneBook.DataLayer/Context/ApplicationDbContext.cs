using Microsoft.EntityFrameworkCore;
using PhoneBook.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.DataLayer.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .HasOne(x => x.UserBook)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.UserId);
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<User> User { get; set; }

    }
}

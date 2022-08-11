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
        // make Context (we need this code for creating data base in Code Firt )
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // make relation with Fluent API 
            modelBuilder.Entity<Book>()
                .HasOne(x => x.UserBook)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.UserId);
        }

        #region Add our entity to Application DbContext
        public DbSet<Book> Book { get; set; }
        public DbSet<User> User { get; set; }
        #endregion
    }
}

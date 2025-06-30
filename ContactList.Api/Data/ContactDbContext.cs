using ContactList.Api.Model;
using ContactList.Shared.Contacts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContactList.Api.Data
{
    public class ContactDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Contact> Contacts => Set<Contact>();
        public DbSet<Subcategory> Subcategories => Set<Subcategory>();

        public ContactDbContext(DbContextOptions<ContactDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contact>()
                .HasIndex(c => c.Email)
                .IsUnique();
            modelBuilder.Entity<Contact>()
                .Navigation(c => c.Subcategory)
                .AutoInclude();

            modelBuilder.Entity<Subcategory>()
                .HasData(
                    new Subcategory { Id = 1, Name = "CEO", Category = Category.Professional },
                    new Subcategory { Id = 2, Name = "Manager", Category = Category.Professional },
                    new Subcategory { Id = 3, Name = "Employee", Category = Category.Professional },
                    new Subcategory { Id = 4, Name = "Poet", Category = Category.Other });

            modelBuilder.Entity<Contact>()
                .HasData(
                    new Contact
                    {
                        Id = 1,
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "john.doe@example.com",
                        PhoneNumber = "123-456-7890",
                        Password = "password123",
                        DateOfBirth = new DateTime(1990, 1, 1),
                        Category = Category.Private,
                    },
                    new Contact
                    {
                        Id = 2,
                        FirstName = "Jane",
                        LastName = "Smith",
                        Email = "jane.smith@example.com",
                        PhoneNumber = "987-654-3210",
                        Password = "password456",
                        DateOfBirth = new DateTime(1992, 2, 2),
                        Category = Category.Professional,
                        SubcategoryId = 1,
                    },
                    new Contact
                    {
                        Id = 3,
                        FirstName = "Tom",
                        LastName = "Fielding",
                        Email = "tom@example.com",
                        PhoneNumber = "555-123-4567",
                        Password = "password789",
                        DateOfBirth = new DateTime(1985, 5, 15),
                        Category = Category.Other,
                        SubcategoryId = 4,
                    });
        }
    }
}

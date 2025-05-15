using LibrarySystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySystem.DAL.Data.Configuration
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.FullName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(a => a.Bio)
                .HasMaxLength(300);
            builder.HasData(
                new Author { Id = 1, FullName = "Joanne Kathleen Rowling John", Email = "jkrowling@example.com" },
                new Author { Id = 2, FullName = "George Orwell John Doe", Email = "orwell@example.com" },
                new Author { Id = 3, FullName = "Charles Dickens John Doe", Email = "charles@example.com" }
                );
        }
    }
}

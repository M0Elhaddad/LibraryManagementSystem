using LibrarySystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.DAL.Data.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Author)
                 .WithMany(p => p.Books)
                 .HasForeignKey(p => p.AuthorId)
                 .OnDelete(DeleteBehavior.Cascade);
            builder.Property(p => p.Title).IsRequired();
            builder.Property(p => p.Genre).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(300);
            builder.HasData(
                new Book { Id = 1, Title = "Harry Potter", Genre = GenreType.Adventure, AuthorId = 1, IsAvailable = true },
                new Book { Id = 2, Title = "1984", Genre = GenreType.Fantasy, AuthorId = 2, IsAvailable = true },
                new Book { Id = 3, Title = "Animal Farm", Genre = GenreType.Fantasy, AuthorId = 2, IsAvailable = false },
                new Book { Id = 4, Title = "A Tale of Two Cities", Genre = GenreType.Fantasy, AuthorId = 3, IsAvailable = true },
                new Book { Id = 5, Title = "Oliver Twist", Genre = GenreType.Fantasy, AuthorId = 3, IsAvailable = true },
                new Book { Id = 6, Title = "A Christmas Carol", Genre = GenreType.Fantasy, AuthorId = 3, IsAvailable = true },
                new Book { Id = 7, Title = "Harry Potter", Genre = GenreType.Adventure, AuthorId = 1, IsAvailable = true },
                new Book { Id = 8, Title = "1984", Genre = GenreType.Fantasy, AuthorId = 2, IsAvailable = true },
                new Book { Id = 9, Title = "Animal Farm", Genre = GenreType.Fantasy, AuthorId = 2, IsAvailable = false },
                new Book { Id = 10, Title = "A Tale of Two Cities", Genre = GenreType.Fantasy, AuthorId = 3, IsAvailable = true },
                new Book { Id = 11, Title = "A Tale of Two Cities", Genre = GenreType.Fantasy, AuthorId = 3, IsAvailable = true },
                new Book { Id = 12, Title = "Oliver Twist", Genre = GenreType.Fantasy, AuthorId = 3, IsAvailable = true },
                new Book { Id = 13, Title = "Harry Potter", Genre = GenreType.Adventure, AuthorId = 1, IsAvailable = true },
                new Book { Id = 14, Title = "1984", Genre = GenreType.Fantasy, AuthorId = 2, IsAvailable = true },
                new Book { Id = 15, Title = "Animal Farm", Genre = GenreType.Fantasy, AuthorId = 2, IsAvailable = false },
                new Book { Id = 16, Title = "A Tale of Two Cities", Genre = GenreType.Fantasy, AuthorId = 3, IsAvailable = true },
                new Book { Id = 17, Title = "Oliver Twist", Genre = GenreType.Fantasy, AuthorId = 3, IsAvailable = true },
                new Book { Id = 18, Title = "Harry Potter", Genre = GenreType.Adventure, AuthorId = 1, IsAvailable = true },
                new Book { Id = 19, Title = "1984", Genre = GenreType.Fantasy, AuthorId = 2, IsAvailable = true },
                new Book { Id = 20, Title = "Animal Farm", Genre = GenreType.Fantasy, AuthorId = 2, IsAvailable = false },
                new Book { Id = 21, Title = "A Tale of Two Cities", Genre = GenreType.Fantasy, AuthorId = 3, IsAvailable = true },
                new Book { Id = 22, Title = "Oliver Twist", Genre = GenreType.Fantasy, AuthorId = 3, IsAvailable = true });
        }
    }
}

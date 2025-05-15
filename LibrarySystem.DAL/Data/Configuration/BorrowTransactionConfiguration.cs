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
    public class BorrowTransactionConfiguration :IEntityTypeConfiguration<BorrowTransaction>
    {
        public void Configure(EntityTypeBuilder<BorrowTransaction> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Book)
                .WithOne(p => p.BorrowTransaction)
                .HasForeignKey<BorrowTransaction>(p => p.BookId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(p => p.BorrowDate).IsRequired();
            builder.HasData(
                new BorrowTransaction { Id = 1, BorrowDate = DateTime.Now, ReturnDate = null, BookId = 3 }
                );
        }
    }
    
}

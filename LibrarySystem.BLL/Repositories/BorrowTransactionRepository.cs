using LibrarySystem.BLL.Interfaces;
using LibrarySystem.DAL.Data;
using LibrarySystem.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.BLL.Repositories
{
    public class BorrowTransactionRepository : GenericRepository<BorrowTransaction>, IBorrowTransactionRepository
    {
        public BorrowTransactionRepository(AppDbContext context) : base(context)
        {
        }


        public async Task<bool> StartReturnTransaction(int bookId)
        {
            Book? targetBook = (await _context.Books.Include(b => b.BorrowTransaction)
                                           .FirstOrDefaultAsync(b => b.Id == bookId));

            BorrowTransaction? targetBorrowTransaction = (targetBook?.BorrowTransaction);
                                                                            
            if (targetBorrowTransaction is null)
            {
                return false;
            }
            targetBorrowTransaction.ReturnDate = DateTime.Now;
            targetBorrowTransaction.Book.IsAvailable = true;

            bool IsSuccessful = await UpdateAsync(targetBorrowTransaction);

            return IsSuccessful;
        }
    }
    
}

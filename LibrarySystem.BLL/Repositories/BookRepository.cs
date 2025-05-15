using LibrarySystem.BLL.Interfaces;
using LibrarySystem.DAL.Data;
using LibrarySystem.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.BLL.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Book>> GetAllBooksWithAuthor()
        {
            return await _context.Books.Include(b => b.Author).ToListAsync();
        }

        public async Task<List<Book>> GetAllBooksWithAuthorAndTransactions()
        {
            return await _context.Books.Include(b => b.Author).Include(b => b.BorrowTransaction).ToListAsync();

        }

        public async Task<Book?> GetBookByIdWithAuthor(int id)
        {
            return await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book?> GetBookByIdWithAuthorAndTransactions(int id)
        {
            return await _context.Books.Include(b => b.Author).Include(b => b.BorrowTransaction).FirstOrDefaultAsync(b => b.Id == id);
        }

        public IQueryable<Book> GetBooksQuery()
        {
           return _context.Books.Include(b => b.Author).Include(b => b.BorrowTransaction);
        }

        public IQueryable<Book> GetBooksQueryWithAuthor()
        {
            return  _context.Books.Include(b => b.Author).Include(b => b.BorrowTransaction);

        }
    }

}

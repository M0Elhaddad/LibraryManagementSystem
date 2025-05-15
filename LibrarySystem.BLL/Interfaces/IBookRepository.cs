using LibrarySystem.DAL.Models;

namespace LibrarySystem.BLL.Interfaces
{
    public interface IBookRepository :IGenericRepository<Book>
    {
        Task<List<Book>> GetAllBooksWithAuthor();
        Task<List<Book>> GetAllBooksWithAuthorAndTransactions();
        Task<Book?> GetBookByIdWithAuthor(int id);
        Task<Book?> GetBookByIdWithAuthorAndTransactions(int id);
        IQueryable<Book> GetBooksQuery();
        IQueryable<Book> GetBooksQueryWithAuthor();
    }
}

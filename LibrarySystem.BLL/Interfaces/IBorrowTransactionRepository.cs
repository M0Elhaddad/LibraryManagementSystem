using LibrarySystem.DAL.Models;

namespace LibrarySystem.BLL.Interfaces
{
    public interface IBorrowTransactionRepository : IGenericRepository<BorrowTransaction>
    {
        Task<bool> StartReturnTransaction(int bookId);
    }
}
using LibrarySystem.BLL.Interfaces;
using LibrarySystem.BLL.Repositories;
using LibrarySystem.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.PL.Controllers
{
    public class BorrowTransactionController : Controller
    {
        private readonly IBorrowTransactionRepository _borrowTransactionRepo;
        private readonly IBookRepository _bookRepository;

        public BorrowTransactionController(IBorrowTransactionRepository borrowTransactionRepo,IBookRepository bookRepository)
        {
            _borrowTransactionRepo = borrowTransactionRepo;
            _bookRepository = bookRepository;
        }
        public async Task<IActionResult> Index()
        {
            var books = await _bookRepository.GetAllBooksWithAuthorAndTransactions();
            var booksVM = new List<BookViewModel>();
            foreach (var book in books)
            {
                booksVM.Add(new BookViewModel(book));
            }
            return View(booksVM);
        }
        public async Task<IActionResult>UpdateBooksAvailable(int id)
        {
            var BorrowedBook = await _bookRepository.GetBookByIdWithAuthorAndTransactions(id);
            if (BorrowedBook is null)
                return NotFound();
            BorrowedBook.IsAvailable = false;
            BorrowedBook.BorrowTransaction = new()
            {
                BookId = BorrowedBook.Id,
                BorrowDate = DateTime.Now,
                ReturnDate = null
            };
            await _bookRepository.UpdateAsync(BorrowedBook);
            return RedirectToAction(nameof(Index));
        }
    }
}

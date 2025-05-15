using System.Diagnostics;
using LibrarySystem.BLL.Interfaces;
using LibrarySystem.PL.Helpers;
using LibrarySystem.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LibrarySystem.PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookRepository _bookRepo;
        private readonly IBorrowTransactionRepository _borrowTransactionRepo;

        public HomeController(IBookRepository bookRepo, IBorrowTransactionRepository borrowTransactionRepo)
        {
            _bookRepo = bookRepo;
            _borrowTransactionRepo = borrowTransactionRepo;
        }

        public async Task<IActionResult> Index()
        {
            var query = _bookRepo.GetBooksQueryWithAuthor()
                     .Select(b => new BookViewModel(b));

            var paginatedList = await PaginatedList<BookViewModel>.CreateAsync(query, 1, 5);

            return View(paginatedList);
        }
        public async Task<IActionResult> Paging(int pageIndex,int pageSize)
        {
            var query = _bookRepo.GetBooksQueryWithAuthor()
                    .Select(b => new BookViewModel(b));
            var paginatedList = await PaginatedList<BookViewModel>.CreateAsync(query, pageIndex, pageSize);

            return PartialView("PartialViews/_BookListPartial", paginatedList);
        }


        public IActionResult StartReturnTransaction(int bookId)
        {
            _borrowTransactionRepo.StartReturnTransaction(bookId);
            return RedirectToAction("index");
        }
        public async Task<IActionResult> Filter(BookFilterViewModel bookVM, int pageIndex = 1, int pageSize = 5)
        {
            var query = _bookRepo.GetBooksQuery();

            if (bookVM != null)
            {
                if (bookVM.BorrowDate.HasValue)
                {
                    var borrowDate = bookVM.BorrowDate.Value.Date;
                    query = query.Where(b =>
                        b.BorrowTransaction != null &&
                        b.BorrowTransaction.BorrowDate.Date == borrowDate);
                }

                if (bookVM.ReturnDate.HasValue)
                {
                    var returnDate = bookVM.ReturnDate.Value.Date;
                    query = query.Where(b =>
                        b.BorrowTransaction.ReturnDate.HasValue &&
                        b.BorrowTransaction.ReturnDate.Value.Date == returnDate);
                }

                if (bookVM.chkAvailable == true)
                {
                    query = query.Where(b => b.IsAvailable == true);
                }

                if (bookVM.chkBorrowed == true)
                {
                    query = query.Where(b => b.IsAvailable == false);
                }
            }

            var books =  query;
            var booksVM = books.Select(book => new BookViewModel(book));
            var paginated = await PaginatedList<BookViewModel>.CreateAsync(booksVM, pageIndex, pageSize);

            return PartialView("PartialViews/_BookListPartial", paginated);
        }


    }


}

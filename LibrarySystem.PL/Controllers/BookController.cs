using LibrarySystem.BLL.Interfaces;
using LibrarySystem.DAL.Models;
using LibrarySystem.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.PL.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepo;
        private readonly IAuthorRepository _authorRepo;

        public BookController(IBookRepository bookRepo, IAuthorRepository authorRepo)
        {
            _bookRepo = bookRepo;
            _authorRepo = authorRepo;
        }
        public async Task<IActionResult> Index()
        {
            var books = await _bookRepo.GetAllBooksWithAuthor();
            var booksVM = new List<BookViewModel>();
            foreach (var book in books)
            {
                booksVM.Add(new BookViewModel(book));
            }
            return View(booksVM);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Authors = await _authorRepo.GetAllAsync();
            return PartialView("PartialViews/_AddEditBookPartial", new BookViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookViewModel bookVM)
        {
            if (ModelState.IsValid)
            {
                var book = ViewModelToModel.BookViewModelToModel(bookVM);
                await _bookRepo.AddAsync(book);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Authors = await _authorRepo.GetAllAsync();
            return View(bookVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            if (id < 0)
                return NotFound();
            Book? book = await _bookRepo.GetBookByIdWithAuthor(id);
            if (book is null)
                return NotFound();
            BookViewModel bookViewModel = new BookViewModel(book);
            ViewBag.Authors = await _authorRepo.GetAllAsync();

            return PartialView("PartialViews/_AddEditBookPartial", bookViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, BookViewModel bookVM)
        {
            if (id != bookVM.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                var book = await _bookRepo.GetByIdAsync(id);
                if (book is null)
                    return NotFound();
                book.Title = bookVM.Title;
                book.Description = bookVM.Description;
                book.AuthorId = bookVM.AuthorId;
                book.Genre = bookVM.Genre;
                book.IsAvailable = bookVM.IsAvailable;

                bool success = await _bookRepo.UpdateAsync(book);
                if (!success)
                    ModelState.AddModelError("", "Something went wrong");
                return RedirectToAction(nameof(Index));
            }
            return View(bookVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookRepo.GetByIdAsync(id);
            if (book == null)
                return NotFound();
            await _bookRepo.DeleteAsync(book);

            return RedirectToAction("Index");
        }
      
    }
}

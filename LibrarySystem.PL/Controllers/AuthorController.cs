using LibrarySystem.BLL.Interfaces;
using LibrarySystem.DAL.Models;
using LibrarySystem.PL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LibrarySystem.PL.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _authorRepo;

        public AuthorController(IAuthorRepository authorRepo)
        {
            _authorRepo = authorRepo;
        }
        public async Task<IActionResult> Index()
        {
            var authors = await _authorRepo.GetAllAsync();
            var authorVM = new List<AuthorViewModel>();
            foreach (var author in authors)
            {
                authorVM.Add(new AuthorViewModel(author));
            }
            return View(authorVM);
        }

        public IActionResult Create()
        {
            return PartialView("PartialViews/_EditAuthorPartial", new AuthorViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorViewModel authorVM)
        {
            if (ModelState.IsValid)
            {
                var author = ViewModelToModel.AuthorViewModelToModel(authorVM);
                await _authorRepo.AddAsync(author);
                return RedirectToAction(nameof(Index));
            }
            return View(authorVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int Id)
        {
            if (Id < 0)
                return NotFound();
            Author? author = await _authorRepo.GetByIdAsync(Id);
            if (author is null)
                return NotFound();
            AuthorViewModel authorViewModel = new AuthorViewModel(author);

            return PartialView("PartialViews/_EditAuthorPartial", authorViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, AuthorViewModel authorVM)
        {
            if (id != authorVM.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                var author = await _authorRepo.GetByIdAsync(id);
                if (author is null)
                    return NotFound();
                author.FullName = authorVM.FullName;
                author.Email = authorVM.Email;
                author.Website = authorVM.Website;
                author.Bio = authorVM.Bio;

                bool success = await _authorRepo.UpdateAsync(author);
                if (!success)
                    ModelState.AddModelError("", "Something went wrong");
                return RedirectToAction(nameof(Index));
            }
            return View(authorVM);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _authorRepo.GetByIdAsync(id);
            if (author == null)
                return NotFound();
            await _authorRepo.DeleteAsync(author);

            return RedirectToAction("Index");
        }
        public IActionResult IsFullNameUnique(string fullName,int id)
        {
            var author = _authorRepo.GetAllAsync().Result.FirstOrDefault(a => a.FullName == fullName&&a.Id!=id);
            if (author == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Author with name {fullName} already exists.");
            }
        }
        public IActionResult IsEmailUnique(string email,int id)
        {
            var author = _authorRepo.GetAllAsync().Result.FirstOrDefault(a => a.Email == email&&a.Id!=id);
            if (author == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Author with email {email} already exists.");
            }
        }
    }
}


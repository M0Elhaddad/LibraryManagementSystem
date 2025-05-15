using LibrarySystem.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.PL.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; } = null!;
        public GenreType Genre { get; set; }
        public static List<GenreType> GenreList { get; set; } = Enum.GetValues(typeof(GenreType)).Cast<GenreType>().ToList();
        public string? Description { get; set; }
        public bool IsAvailable { get; set; }
        public int AuthorId { get; set; }
        [Display(Name = "Author Name")]
        public string? AuthorName { get; set; }
        public DateTime? BorrowDate { get; set; }
        public BookViewModel() { }
        public BookViewModel(Book book)
        {
            if (book is not null)
            {
                Id = book.Id;
                AuthorId = book.AuthorId;
                Title = book.Title;
                Genre = book.Genre;
                Description = book.Description;
                IsAvailable = book.IsAvailable;
                AuthorName = book.Author?.FullName;
                BorrowDate=book.BorrowTransaction?.BorrowDate;
            }
        }
    }
}

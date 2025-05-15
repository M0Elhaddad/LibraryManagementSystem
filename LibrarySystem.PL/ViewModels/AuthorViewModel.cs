using LibrarySystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.PL.ViewModels
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        [RegularExpression(@"^(\w{2,}\s){3}\w{2,}$", ErrorMessage = "Must be 4 names with at least 2 characters each.")]
        [Display(Name = "Full Name")]
        [Remote(action: "IsFullNameUnique", controller: "Author", ErrorMessage = "This name already exists.", AdditionalFields = nameof(Id))]
        public string FullName { get; set; } = null!;
        [EmailAddress(ErrorMessage ="It is not an Email Address.")]
        [Required(ErrorMessage = "Email is required.")]
        [Remote(action: "IsEmailUnique", controller: "Author", ErrorMessage = "This email already exists.",AdditionalFields = nameof(Id))]
        public string Email { get; set; } = null!;
        [Url(ErrorMessage = "It is not a valid URL.")]
        public string? Website { get; set; }
        [MaxLength(300, ErrorMessage = "Bio cannot exceed 300 characters.")]
        public string? Bio { get; set; }
        public AuthorViewModel() { }
        public AuthorViewModel(Author author)
        {
            if (author is not null)
            {
                Id = author.Id;
                FullName = author.FullName;
                Email = author.Email;
                Website = author.Website;
                Bio = author.Bio;
                
            }
        }
    }
}

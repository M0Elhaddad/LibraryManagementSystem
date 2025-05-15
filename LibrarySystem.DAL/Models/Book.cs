namespace LibrarySystem.DAL.Models
{
    public enum GenreType : byte
    {
        Unknown, Adventure, Mystery, Thriller, Romance, SciFi, Fantasy, Biography, History, SelfHelp, Children,
        YoungAdult, Poetry, Drama, NonFiction
    }
    public class Book : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public GenreType Genre { get; set; }
        public string? Description { get; set; }
        public bool IsAvailable { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;
       
        public BorrowTransaction BorrowTransaction { get; set; } =null!;
    }
}

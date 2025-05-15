namespace LibrarySystem.PL.ViewModels
{
    public class BookFilterViewModel
    {
        public DateTime? ReturnDate { get; set; }
        public DateTime? BorrowDate { get; set; }
        public bool chkAvailable { get; set; }
        public bool chkBorrowed { get; set; }

    }
}

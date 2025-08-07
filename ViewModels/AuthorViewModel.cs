namespace LibraryManagement.ViewModels
{
    public class AuthorViewModel
    {
        public int AuthorId { get; set; }

        public string Name { get; set; } = string.Empty;

        // 包含所有该作者的书籍名称
        public List<string> Books { get; set; } = new List<string>(); 
    }
}

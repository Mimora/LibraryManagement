using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryManagement.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        // Navigation Property - reference to list of books
        public List<Book> Books { get; set; } = new List<Book>(); //One - to - many
    }
}

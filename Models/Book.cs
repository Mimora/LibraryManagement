using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryManagement.Models
{
    public class Book  //EFCore会把这个Book类mapping到数据库中的Books表
    {
        [Key]
        public int BookId { get; set; } //AUTOINCREMENT in db

        [Required]
        public string Title { get; set; } = string.Empty; //确保非空

        [Required]
        public string Genre { get; set; } = string.Empty; //确保非空

        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        [JsonIgnore]
        public Author? Author { get; set; } //navigation property

        [ForeignKey("LibraryBranch")]
        public int LibraryBranchId { get; set; }

        [JsonIgnore]
        public LibraryBranch? LibraryBranch { get; set; }
    }
}


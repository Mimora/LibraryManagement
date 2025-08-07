using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Models
{
    public class LibraryBranch
    {
        public int LibraryBranchId { get; set; }

        public string BranchName { get; set; } = string.Empty;
    }
}

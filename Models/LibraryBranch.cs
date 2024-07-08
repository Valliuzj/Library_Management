using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class LibraryBranch
    {
        [Key]
        public int LibraryBranchId { get; set; }
        public string BranchName { get; set; }
    }
}
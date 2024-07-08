using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; }
    }
}
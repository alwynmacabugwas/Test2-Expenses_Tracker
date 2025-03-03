﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test2.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public decimal Price { get; set; }

        [Required]
        public string? Description { get; set; }

        public string Email { get; set; }

        public string? ImageName { get; set; }

        public string Category { get; set; }

    }
}

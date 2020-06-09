using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace WebTek.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Please enter a Product Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a Description")]
        public string Description { get; set; }
        public int Quantity { get; set; } = 1;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please specify a Category")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Please specify Condition")]
        public string Condition { get; set; }
        public string Seller { get; set; }
    }
}

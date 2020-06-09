using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebTek.Models
{
    public class Order
    {
        [BindNever]
        public int OrderID { get; set; }
        [BindNever]
        public ICollection<CartLine> Lines { get; set; }
        [BindNever]
        public bool Shipped { get; set; }

        [Required(ErrorMessage = "Please enter a First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter a Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter the Street Address")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Please enter a City Name")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter a State Name")]
        public string State { get; set; }
        [Required(ErrorMessage = "Please enter a Zip Code")]
        public string Zip { get; set; }
        [Required(ErrorMessage = "Please enter a Country Name")]
        public string Country { get; set; }
        public string UserName { get; set; }
    }
}
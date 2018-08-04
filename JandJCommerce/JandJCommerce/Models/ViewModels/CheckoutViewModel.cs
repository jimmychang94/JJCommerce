using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Models.ViewModels
{
    /// <summary>
    /// This class holds information besides the order which we need to process the order 
    /// but it is sensitive information we don't want to store.
    /// </summary>
    public class CheckoutViewModel
    {
        public Order Order { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Address")]
        public string Street { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Credit Card")]
        public CardType Card { get; set; }

    }

    public enum CardType : long
    {
        Discover = 6011000000000012,
        Visa = 4111111111111111,
        Mastercard = 5424000000000015
    }
}

using System.ComponentModel.DataAnnotations;

namespace CPW219_eCommerceSite.Models
{
    /// <summary>
    /// Represents an individual software available for purchase
    /// </summary>
    public class Product
    {
        /// <summary>
        /// The unique identifier for each software product
        /// </summary>
        [Key]
        public int ProductId { get; set; }

        /// <summary>
        /// The title of the software program
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// The sale price of the software program
        /// </summary>
        [Range(0, 9999)]
        public decimal Price { get; set; }


        /// <summary>
        /// The category of the software eg. Operating System, Office, and security, to name a few.
        /// </summary>
        [Required]
        public string Category { get; set; }

        /// <summary>
        /// The product description
        /// </summary>
        [Required]
        public string Description { get; set; }

    }
}

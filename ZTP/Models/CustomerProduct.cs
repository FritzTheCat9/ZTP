using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP.Models
{
    public class CustomerProduct
    {
        /* POLA */
        [Key]
        [Display(Name = "KlientProduktID")]
        public int CustomerProductID { get; set; }
        [Required]
        [ForeignKey("Customer")]
        [Display(Name = "KlientId")]
        public int CustomerID { get; set; }
        [Required]
        [ForeignKey("Product")]
        [Display(Name = "ProduktId")]
        public int ProductID { get; set; }

        /* POLA - ENTITY FRAMEWORK */
        [Display(Name = "KlientId")]
        public Customer Customer { get; set; }
        [Display(Name = "ProduktId")]
        public Product Product { get; set; }
    }
}

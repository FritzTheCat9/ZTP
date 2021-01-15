using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP.Models
{
    public class PaymentMethod
    {
        /* POLA */
        [Key]
        [Display(Name = "PlatnoscId")]
        public int PaymentMethodID { get; set; }
        [Required]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        /* POLA - ENTITY FRAMEWORK */
        public ICollection<Order> Orders { get; set; } = new ObservableCollection<Order>();
    }
}

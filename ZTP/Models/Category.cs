using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP
{
    public class Category
    {
        /* POLA */
        [Key]
        [Display(Name = "KategoriaId")]
        public int CategoryID { get; set; }
        [Required]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        /* POLA - ENTITY FRAMEWORK */
        public ICollection<Product> Products { get; set; } = new ObservableCollection<Product>();
    }
}

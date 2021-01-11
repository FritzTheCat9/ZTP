using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP
{
    public class Customer
    {
        /* POLA */
        [Key]
        public int CustomerID { get; set; }
        [Required]
        [ForeignKey("Address")]
        [Display(Name = "AdresId")]
        public int AddressID { get; set; }
        [Required]
        [Display(Name = "Imie")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        [Display(Name = "Login")]
        [Required]
        public string Login { get; set; }
        [Display(Name = "Hasło")]
        [Required]
        public string Password { get; set; }
        [Display(Name = "Prawa admina")]
        [Required]
        public bool AdminRights { get; set; }

        /* POLA - ENTITY FRAMEWORK */
        [Display(Name = "AdresId")]
        public Address Address { get; set; }
        public ICollection<Order> Orders { get; set; } = new ObservableCollection<Order>();

        /* METODY */
    }
}

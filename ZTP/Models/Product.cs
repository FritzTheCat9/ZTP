using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP.Patterns;
using ZTP.Patterns.Observer;

namespace ZTP.Models
{
    public class Product : ProductBase, INotifyPropertyChanged, ISubject
    {
        /* POLA */
        [Key]
        [Display(Name = "ProduktId")]
        public int ProductID { get; set; }
        /*[Required]
        [ForeignKey("Category")]
        [Display(Name = "KategoriaId")]
        public int CategoryID { get; set; }*/
        [Required]
        [Display(Name = "Nazwa")]
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged("ProductDisplay"); NotifyPropertyChanged("ProductDescription"); }
        }

        /*[Required]
        [Display(Name = "Opis")]
        public string Description { get; set; }*/
        /*[Required]
        [Display(Name = "Obraz")]
        public string Image { get; set; }*/
        /*[Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data dodania")]
        public DateTime DateAdded { get; set; }*/
        [Required]
        [Display(Name = "Promocja")]
        public bool Promotion { get; set; }
        [Required]
        [Display(Name = "VAT")]
        public int VAT { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Cena")]
        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set { _price = value; NotifyPropertyChanged("ProductDisplay"); }
        }
        [Required]
        [Display(Name = "Ilość")]
        public int Quantity { get; set; }


        public string ProductDisplay
        {
            get { return Name + " (" + Price + ") "; }
        }

        public string ProductDescription
        {
            get { return getDescription(); }
        }


        /* POLA - ENTITY FRAMEWORK */
        /*[Display(Name = "KategoriaId")]
        public Category Category { get; set; }*/
        public ICollection<ProductOrder> ProductOrders { get; set; } = new ObservableCollection<ProductOrder>();
        public ICollection<CustomerProduct> CustomerProducts { get; set; } = new ObservableCollection<CustomerProduct>();


        /*Observator*/

        private List<IObserver> _observers = new List<IObserver>();

        // Методы управления подпиской.
        public void Attach(IObserver observer)
        {
            Console.WriteLine("Subject: Attached an observer.");
            this._observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            this._observers.Remove(observer);
            Console.WriteLine("Subject: Detached an observer.");
        }

        // Запуск обновления в каждом подписчике.
        public void Notify()
        {
            Console.WriteLine("Subject: Notifying observers...");

            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }



        /* METODY */

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public override decimal getPrice()
        {
            return Price;
        }

        public override string getDescription()
        {
            return Name + "\n";
        }
    }
}

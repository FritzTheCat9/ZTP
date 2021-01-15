using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP.Models
{
    public enum State
    {
        Preparing = 0,
        OnTheWay = 1,
        Delivered = 2
    }

    public class Order : INotifyPropertyChanged
    {
        /* POLA */
        [Key]
        [Display(Name = "ZamówienieId")]
        private int _orderID;
        public int OrderID
        {
            get { return _orderID; }
            set { _orderID = value; NotifyPropertyChanged("OrderDisplay"); }
        }
        [Required]
        [ForeignKey("Customer")]
        [Display(Name = "KlientId")]
        public int CustomerID { get; set; }
        [Required]
        [ForeignKey("ShippingMethod")]
        [Display(Name = "Dostawa")]
        public int ShippingMethodID { get; set; }
        [Required]
        [ForeignKey("PaymentMethod")]
        [Display(Name = "Płatność")]
        public int PaymentMethodID { get; set; }
        [Required]
        [EnumDataType(typeof(State))]
        [Display(Name = "Stan")]
        public State OrderStatus { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Cena")]
        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set { _price = value; NotifyPropertyChanged("OrderDisplay"); }
        }


        public string OrderDisplay
        {
            get { return OrderID + " " + Price; }
        }


        /* POLA - ENTITY FRAMEWORK */
        public Customer Customer { get; set; }
        [Display(Name = "Dostawa")]
        public ShippingMethod ShippingMethod { get; set; }
        [Display(Name = "Płatność")]
        public PaymentMethod PaymentMethod { get; set; }
        public ICollection<ProductOrder> ProductOrders { get; set; } = new ObservableCollection<ProductOrder>();

        /* METODY */

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}

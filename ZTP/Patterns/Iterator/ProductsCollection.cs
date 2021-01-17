using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using ZTP.Models;
using System.Collections.ObjectModel;

namespace ZTP.Patterns.Iterator
{
    class ProductsCollection : IteratorAggregate
    {
        public IList<Product> _productsList { get; set; } = new ObservableCollection<Product>();
        int _nth = 1;

        public ProductsCollection(IList<Product> productsList, int nth)
        {
            _productsList = productsList;
            _nth = nth;
        }

        public void ChangeNth(int nth)
        {
            _nth = nth;
        }

        public IList<Product> getItems()
        {
            return _productsList;
        }

        public override IEnumerator GetEnumerator()
        {
            return new AlphabeticalOrderIterator(this, _nth);
        }
    }
}

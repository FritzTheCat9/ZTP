using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ZTP.Patterns.Iterator
{
    class ProductsNthIterator : Iterator
    {
        private ProductsCollection _collection;
        private int _position = -1;
        private int _nth = 1;

        public ProductsNthIterator(ProductsCollection collection, int nth = 1)
        {
            this._collection = collection;
            this._nth = nth;
            this._position = 0 - nth;           
        }

        public override object Current()
        {
            return this._collection.getItems()[_position];
        }

        public override int Key()
        {
            return this._position;
        }

        public override bool MoveNext()
        {            
            int updatedPosition = this._position + this._nth;

            if (updatedPosition >= 0 && updatedPosition < this._collection.getItems().Count)
            {
                this._position = updatedPosition;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Reset()
        {           
            this._position = 0;
        }
    }
}

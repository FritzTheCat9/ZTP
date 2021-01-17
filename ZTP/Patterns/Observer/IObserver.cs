using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP.Patterns.Observer
{
    public interface IObserver
    {
        void Update(ISubject subject);
    }
}

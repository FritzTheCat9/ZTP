using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP.Patterns.Observer
{
    public interface ISubject
    {
        void Attach(IObserver observer);

        // Отсоединяет наблюдателя от издателя.
        void Detach(IObserver observer);

        // Уведомляет всех наблюдателей о событии.
        void Notify();
    }
}

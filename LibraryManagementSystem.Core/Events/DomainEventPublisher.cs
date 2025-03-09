using LibraryManagementSystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.Events
{
    public class DomainEventPublisher : IDomainEventPublisher
    {
        private readonly List<IBookAddedObserver> _observers = new List<IBookAddedObserver>();

        public void Subscribe(IBookAddedObserver observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
        }

        public void Unsubscribe(IBookAddedObserver observer)
        {
            if (_observers.Contains(observer))
                _observers.Remove(observer);
        }

        public void PublishBookAdded(BookAddedEvent eventData)
        {
            foreach (var observer in _observers)
            {
                observer.OnBookAdded(eventData);
            }
        }
    }
}

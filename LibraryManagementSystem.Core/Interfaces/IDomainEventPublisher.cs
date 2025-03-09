using LibraryManagementSystem.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.Interfaces
{
    public interface IDomainEventPublisher
    {
        void Subscribe(IBookAddedObserver observer);
        void Unsubscribe(IBookAddedObserver observer);
        void PublishBookAdded(BookAddedEvent eventData);
    }
}

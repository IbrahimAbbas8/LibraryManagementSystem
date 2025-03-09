using LibraryManagementSystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.Events
{
    public class BookAddedNotificationObserver : IBookAddedObserver
    {
        public void OnBookAdded(BookAddedEvent eventData)
        {
            Console.WriteLine($"[Observer] A new book has been added: {eventData.Book.Title} To the author {eventData.Book.Author}");
        }
    }
}

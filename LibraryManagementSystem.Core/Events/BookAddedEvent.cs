using LibraryManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.Events
{
    public class BookAddedEvent : EventArgs
    {
        public Book Book { get; }

        public BookAddedEvent(Book book)
        {
            Book = book;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.Entities
{
    public class BorrowingRecord : BaseEntity<int>
    {
        public int BookId { get; set; }
        public int PatronId { get; set; }

        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        [ForeignKey("BookId")]
        public Book? Book { get; set; }

        [ForeignKey("PatronId")]
        public Patron? Patron { get; set; }
    }
}

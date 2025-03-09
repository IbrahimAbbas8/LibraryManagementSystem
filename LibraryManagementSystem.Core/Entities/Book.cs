using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.Entities
{
    public class Book : BaseEntity<int>
    {

        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        public DateTime PublicationYear { get; set; } = DateTime.UtcNow;

        public string ISBN { get; set; } = null!;

        public List<BorrowingRecord> BorrowingRecords { get; set;} = new List<BorrowingRecord>();
    }
}


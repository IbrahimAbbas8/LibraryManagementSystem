using LibraryManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.Dtos
{
    public class BorrowingRecordDto
    {
        public int BookId { get; set; }
        public int PatronId { get; set; }

        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        [JsonIgnore]
        public BookDto? Book { get; set; }
        public PatronDto? Patron { get; set; }
    }
}

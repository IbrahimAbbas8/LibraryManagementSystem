using LibraryManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.Dtos
{
    public class BookDto
    {

        [Required(ErrorMessage = "Book title required")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Author name required")]
        public string Author { get; set; } = null!;

        public DateTime PublicationYear { get; set; }

        [Required(ErrorMessage = "ISBN required")]
        public string ISBN { get; set; } = null!;
      //  public List<BorrowingRecordDto> BorrowingRecords { get; set; } = new List<BorrowingRecordDto>();
    }

    public class GetBookDto : BookDto
    {
        public int Id { get; set; }

        public bool IsBorrowed { get; set; }
    }

    public class UpdateBookDto : BookDto
    {
        public int Id { get; set; }
    }
}

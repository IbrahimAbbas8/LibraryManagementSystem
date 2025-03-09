using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.Entities
{
    public class Patron : BaseEntity<int>
    {
        public string Name { get; set; }

        public ContactInfo ContactInfo { get; set; } = null!;
    }
}

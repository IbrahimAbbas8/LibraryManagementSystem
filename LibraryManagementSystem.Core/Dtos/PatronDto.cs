using LibraryManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.Dtos
{
    public class PatronDto
    {
        public string Name { get; set; }

        public ContactInfoDto? ContactInfo { get; set; }
    }
}

﻿using LibraryManagementSystem.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.Interfaces
{
    public interface IBookAddedObserver
    {
        void OnBookAdded(BookAddedEvent eventData);
    }
}

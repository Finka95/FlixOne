﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlixOne.InventoryManagement.Interfaces
{
    internal interface IInventoryContext
    {
        Book[] GetBooks();
        bool AddBook(string name);
        bool UpdateQuantity(string name, int quantity);
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.DAL.Models
{
    public class BorrowTransaction : BaseEntity
    {
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
    }
}

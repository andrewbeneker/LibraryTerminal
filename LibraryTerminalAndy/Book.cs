using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTerminalAndy
{
    internal class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string ISBN { get; set; }
        public Status Status { get; set; }
        public DateTime DueDate { get; set; }
        

        public Book(string title, string author)
        {

            Title = title;
            Author = author;
        }

       public Status CheckoutBook(Book book)
        {
            Status = Status.CheckedOut;
            return Status;
        }

        


    }
}

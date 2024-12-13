using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTerminal_Project
{
    internal class LibraryCatalog
    {
        public List<Book> Books { get; set; }

        public LibraryCatalog()
        {
            Books = new List<Book>();
        }

        
        
        public void DisplayAvailableBooks()
        {
            foreach (Book book in Books)
            {
                if (book.Status == Status.OnShelf)
                {
                    Console.WriteLine($"{book.Title} by {book.Author}");
                }
            }
        }
        public void DisplayAllBooks()
        {
            foreach (Book book in Books)
            {
                Console.WriteLine($"{book.Title} by {book.Author} \n\tStatus: {book.Status} {(book.Status == Status.CheckedOut ? $"Estimated Date of Return {book.DueDate}" : "")}");
                Console.WriteLine("");
            }
        }

        //make a public void DonateBook method

    } 
}

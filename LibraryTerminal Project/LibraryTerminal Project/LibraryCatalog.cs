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
            Books = new List<Book>()
            {
            new Book("Harry Potter","J.K. Rowling",Status.OnShelf), new Book("Divergent", "Veronica Roth", Status.OnShelf),
            new Book("Insurgent", "Veronica Roth", Status.CheckedOut, DateTime.Parse("12/12/1212")), //CHECK FOR INSERTING A DATETIME LITERAL
            new Book("Allegiant", "Veronica Roth", Status.OnShelf),
            new Book("Four", "Veronica Roth", Status.OnShelf), new Book("920 London", "Remy Boydell", Status.OnShelf),
            new Book("To Kill A Mockingbird", "Harper Lee", Status.OnShelf), new Book("The Glass Castle", "Jeannette Walls", Status.OnShelf),
            new Book("Deep Work", "Cal Newport", Status.OnShelf), new Book("Animal Farm", "George Orwell", Status.OnShelf),
            new Book("The Great Gatsby", "F. Scott Fitzgerald", Status.OnShelf), new Book("The New Jim Crow: Mass Incarceration In The Age Of Colorblindness", "Michelle Alexander", Status.OnShelf),
            new Book("Night","Elie Wiesel",Status.OutOfStock), new Book("This Is Not a Personal Statement", "Tracy Badua", Status.CheckedOut),
            new Book("The Odyssey","Homer",Status.OnShelf),
            };
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTerminalAndy
{
    internal class Catalogue
    {
        public List<Book> Books { get; set; } = new List<Book>();

        public void displayBookList()
        {
            Console.WriteLine("Here are the books in our Library:");
            foreach (Book book in Books)
            {
                Console.WriteLine($"{book.Title} by {book.Author}");
            }
        }

        public Book FindBookByAuthor(string authorSearch)
        {
            Console.WriteLine("Please enter the name of the author.");
            authorSearch = Console.ReadLine().ToLower().Trim();
            return Books.Find(x => x.Author == authorSearch);
            

        }

        public string FindBookByKeyword(string keywordSearch)
        {
            Console.WriteLine("Please enter a keyword");
            keywordSearch = Console.ReadLine().ToLower().Trim();
            if (Books.Where(x => x.Title.Contains(keywordSearch)) == null)
            {
                return "No books found matching keyword, please try again.";
            }
            else
            {
                return Books.Where(x => x.Title.Contains(keywordSearch)).ToString();
            }
        }

        
    }
}

using LibraryTerminal_Project;
using System.Runtime.CompilerServices;
Console.BackgroundColor = ConsoleColor.White;
Console.ForegroundColor = ConsoleColor.Black;

/*
Write a console program which allows a user to search a library catalog and check out books.
    *-Your solution must include some kind of a book class with a title, author, status, and due date if checked out.
        -Status should be On Shelf or Checked Out (or other statuses you can imagine). 
    *-12 items minimum; All stored in a list.
    *Allow the user to:
        *Display the entire list of books.  Format it nicely.
        *Search for a book by author.
        *Search for a book by title keyword.
        *Select a book from the list to check out.
            *If it’s already checked out, let them know.
            *If not, check it out to them and set the due date to 2 weeks from today.
    Return a book.  (You can decide how that looks/what questions it asks.)

Optional enhancements:
(Moderate) When the user quits, save the current library book list (including due dates and statuses) to the text file so the next time the program runs, it remembers.
(Julius Caesar) Burn down the library of Alexandria and set human Civilization back by a few hundred years. 

 */

//TODO: Make program less stream of text and options
//TODO: SET UP A CART WITH EACH BOOK CHOSEN TO BE CHECKED OUT -
//When checking out, first step is ask user if they're sure if they wanna add this book to their cart,
// if y then add book to cart( stage the CheckOut), if no move on;
//then ask if they wanna add anything else
//if yes repeat, if no then move onto the CheckOut(IF THEY HAVE ANYTHING IN THEIR CART) process which will likely include confirming checkouts once more and after all confirmations are done then 
//change status of chosen books in catalog to CheckedOut and Assign a DueDate to each of the books as Two Weeks from now
//then empty the cart and create a sort of receipt.

//Note: use selected Status so that user knows not to select the same book to check out. Only OnShelf items can be selected and Selected items can only be seleceted temporarily
// they must either become CheckedOut or will turn back into OnShelf.

//ADD STREAMWRITER AND READER TO SAVE INFO
//Add greeting
LibraryCatalog catalog = new LibraryCatalog();
List<Book> cart = new List<Book>();
int index = 1;
int userNumSelect = -1;
LibraryCatalog queriedMatchingBooks = new LibraryCatalog();
queriedMatchingBooks.Books.Clear();
//instantiate this data in class because it's mock data
List<Book> testList = new List<Book>();
string userCatalogQuery = "";
string choice = "";

//READ DATA 
//ADD USER INFO COLLECTION BEFORE MENU




try
{
    using (StreamReader reader = new StreamReader("catalog_File.txt"))
    {
        Book defaultBook = new Book();
        catalog.Books.Clear();
        string line;
        while ((line = reader.ReadLine()) != null)
        {

            string[] parts = line.Split("|");
            if (parts.Length == 5 || parts.Length == 4)
            {
                //COME BACK TO LATER
                Book book = new Book(parts[0], parts[1], (Status)int.Parse(parts[2]), string.IsNullOrEmpty(parts[3]) ? null : parts[3], !string.IsNullOrEmpty(parts[4]) ? (DeweyDecimal)int.Parse(parts[4]) : null);
                catalog.Books.Add(book);
            }
        }
    }
}
catch (FileNotFoundException)
{
    Console.WriteLine("File not found");
    Console.WriteLine("Using prepicked list of Books");
    List<Book> prePickedBooks = new List<Book>()
    {
        new Book("Harry Potter", "J.K. Rowling", Status.OnShelf), new Book("Divergent", "Veronica Roth", Status.OnShelf),
            new Book("Insurgent", "Veronica Roth", Status.CheckedOut/*, DateTime.Parse("12/12/1212")*/), //CHECK FOR INSERTING A DATETIME LITERAL
            new Book("Allegiant", "Veronica Roth", Status.OnShelf),
            new Book("Four", "Veronica Roth", Status.OnShelf), new Book("920 London", "Remy Boydell", Status.OnShelf),
            new Book("To Kill A Mockingbird", "Harper Lee", Status.OnShelf), new Book("The Glass Castle", "Jeannette Walls", Status.OnShelf),
            new Book("Deep Work", "Cal Newport", Status.OnShelf), new Book("Animal Farm", "George Orwell", Status.OnShelf),
            new Book("The Great Gatsby", "F. Scott Fitzgerald", Status.OnShelf), new Book("The New Jim Crow: Mass Incarceration In The Age Of Colorblindness", "Michelle Alexander", Status.OnShelf),
            new Book("Night", "Elie Wiesel", Status.OutOfStock), new Book("This Is Not a Personal Statement", "Tracy Badua", Status.CheckedOut),
            new Book("The Odyssey", "Homer", Status.OnShelf),
    };
    catalog.Books.AddRange(prePickedBooks);
}

Console.WriteLine("Welcome to the T.A.C Library!");
Console.ReadKey();
Console.Clear();

bool continueMenu = true;

while (continueMenu == true)
{
    MainMenu();
    Console.WriteLine("Would you like to continue browsing for books? (y/n)");
    string continueBrowse = Console.ReadLine();
    if (continueBrowse == "n" && cart.Count == 0)
    {
        continueMenu = false;
        Console.WriteLine("Press any key to exit the library.");
        Console.ReadKey();
        Console.Clear();

    }
    else if (continueBrowse == "n" && cart.Count > 0)
    {
        continueMenu = false;
        CartMenu();
        CheckOut(cart);
        Console.Clear();

    }
    else
    {
        Console.Clear();
    }
};


void MainMenu()
{
    Console.WriteLine("MAIN MENU");
    Console.WriteLine("Please choose one of the following options, by selecting the number for that option:");

    Console.WriteLine("1. Search for titles");
    Console.WriteLine("2. Display Library");
    Console.WriteLine("3. Display Cart");
    Console.WriteLine("4. Checkout");
    Console.WriteLine("5. Return a Book/Books");
    Console.WriteLine("6. Annihilate Library...");
    string menuChoice = Console.ReadLine();
    switch (menuChoice)
    {
        case "1":
            Console.Clear();
            QueryCatalog();

            break;
        case "2":
            Console.Clear();
            catalog.AlphabetizeCatalog();
            catalog.DisplayAllBooks();

            break;
        case "3":
            Console.Clear();
            CartMenu();

            break;
        case "4":
            Console.Clear();
            continueMenu = false;
            CartMenu();
            break;
        case "5":
            Console.Clear();
            ReturnBook();
            break;
        case "6":
            Console.Clear();
            Console.WriteLine("Are you sure you want to destroy the library? (This action cannot be undone) Select (y/n)");
            string destroyLibrary = Console.ReadLine();
            if (destroyLibrary == "y")
            {
                continueMenu = false;
                Console.Clear();
                DestroyLibrary();
                catalog.Books.Clear();
                cart.Clear();
                Console.WriteLine("All the books in the library have been obliterated. The world is now safe from learning.");
                Console.WriteLine("Press any key to forget libraries ever existed.");
                Console.ReadKey();
                Console.Clear();
            }
            else { }
            break;
        default:
            Console.WriteLine("That is not a valid option. Please select a number which corresponds to a menu item");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
            break;
    }

}


void DestroyLibrary()
{
    while (catalog.Books.Count > 0)
    {
        for (int i = 0; i < catalog.Books.Count; i--)
        {
            Console.WriteLine("Press any key to burn the books.");
            if (catalog.Books.Count > 0)
            {
                catalog.Books.RemoveAt(0);
                foreach (Book book in catalog.Books)
                {
                    Console.WriteLine(book.Title);

                }
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("All the books have been burned. I hope you're happy!");
                Console.ReadKey(false);
                break;
            }
        }
    }
}




/*
while (true)
{
    Console.WriteLine("");
    Console.WriteLine("Do you want a list of our catalog displayed before your search?(\"y\" or \"n\")");
    choice = AnswerYOrN();
    Console.Clear();
    userCatalogQuery = "";
    if (choice == "y")
    {
        catalog.DisplayAllBooks();
        Console.WriteLine("Would you like to select a book directly from this list?(\"y\" for list selection and \"n\" for keyword query)");
        choice = AnswerYOrN();
        if (choice == "y")
        {
            Console.Clear();
            StageCheckOut(SelectFromList(catalog.Books));
        }
        else if (choice == "n")
        {
            Console.Clear();
            QueryCatalog();
        }

    }
    else if (choice == "n")
    {
        QueryCatalog();
    }
    Console.WriteLine("Would you like to select anything else to to add to your cart?");
    choice = AnswerYOrN();
    if (choice == "y")
    {
        queriedMatchingBooks.Books.Clear();
    }
    else
    {
        break;
    }
}
*/
Console.Clear();

void CartMenu()
    {
    if (cart.Count > 0)
    {
        Console.WriteLine("Your cart has:");
        foreach (Book book in cart)
        {
            Console.WriteLine($"{book.Title} by {book.Author} \n\tStatus: {book.Status} {(book.Status == Status.CheckedOut ? $"Estimated Date of Return {book.DueDate}" : "")}");
            Console.WriteLine("");

        }
        while (true)
        {
            Console.WriteLine("Are there any items you would like to remove from your cart? (\"y\" or \"n\")");
            choice = AnswerYOrN();
            if (choice == "y")
            {
                Console.WriteLine("Select an item by number to remove it from your cart.");
                RemoveFromCart(cart);
                Console.WriteLine("Would you like to remove any more?(\"y\"  or \"n\")");
                if (choice == "y")
                {
                    continue;
                }
                else if (choice == "n")
                {
                    CheckOut(cart);
                    break;
                }

            }
            else if (choice == "n")
            {
                CheckOut(cart);
                break;
            }
        }
    }
    else
    {
        Console.WriteLine("oops! cart is empty!\n Goodybye!");
        Console.WriteLine("return a book");

        ReturnBook();
    }
}





//WRITE DATA


using (StreamWriter catalogWriter = new StreamWriter("catalog_File.txt", false))
{
    foreach (Book book in catalog.Books)
    {
        catalogWriter.WriteLine(book.ToString());

    }  
}








Console.ReadKey();
void QueryCatalog()
{
    //Could prob simplify by Validating and Selecting Book Title or Author into joined methods
    Console.WriteLine("Please enter the title, author or keywords of any books you're interested in checking out.");
    userCatalogQuery = Console.ReadLine();
    /**/
    if (ValidateBookTitleSelection(userCatalogQuery)) //make one validation method that informs user of bad data.
    {
        Console.WriteLine($"You have selected {SelectBook(userCatalogQuery).Title} by {SelectBook(userCatalogQuery).Author} ");
        Console.WriteLine("Would you like to add this book to the cart?");
        choice = AnswerYOrN();
        switch (choice)
        {
            case "y":
                StageCheckOut(SelectBook(userCatalogQuery));
                break;
            case "n":
                Console.WriteLine("Okay!");
                break;
        }
    }
    else // keyword query functionality
    {

        //reduced repetitive uses of foreach loops to display books in lists by making queriedResults into a LibraryCatalog item 
        QueryByKeyword(userCatalogQuery);
        ValidateQueryByKeyword(queriedMatchingBooks.Books);
        if (ValidateQueryByKeyword(queriedMatchingBooks.Books))
        {


            Console.WriteLine("Titles matching keyword(s):");
            queriedMatchingBooks.DisplayAllBooks();
            if (queriedMatchingBooks.Books.Count() > 1)
            {
                Console.WriteLine("Would you like to select any of these titles?(\"y\" or \"n\"):");
                choice = AnswerYOrN();
                if (choice == "y")
                {
                    StageCheckOut(SelectFromList(queriedMatchingBooks.Books));
                }
                else if (choice == "n")
                {
                    queriedMatchingBooks.Books.Clear();
                }
            }
            else if (queriedMatchingBooks.Books.Count() == 1)
            {
                Console.WriteLine($"The only title matching your keyword is {queriedMatchingBooks.Books[0].Title} by {queriedMatchingBooks.Books[0].Author}\n" +
                    $"Would you like to add it to your cart?");
                choice = AnswerYOrN();
                switch (choice)
                {
                    case "y":
                        StageCheckOut(queriedMatchingBooks.Books[0]);
                        break;
                    case "n":
                        break;
                }

            }
        }
        else
        {
            Console.WriteLine("\nNo book matching your input was found.");
        }
    }
}

void ReturnBook() //Tommy's
{
    Console.Write("Enter the title of the book you want to return: ");
    string title = Console.ReadLine();
    var book = catalog.Books.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
    if (book == null) { Console.WriteLine("Book not found."); }
    else if (book.Status == Status.OnShelf)
    {
        Console.WriteLine("The book is already on the shelf.");
    }
    else
    {
        book.Status = Status.OnShelf;
        book.AssignDueDate();
        Console.WriteLine($"You have returned \"{book.Title}\". Thank you!");
    }
}


void StageCheckOut(Book userChosenBook)
{
    if (userChosenBook.Status == Status.OnShelf)
    {
        cart.Add(userChosenBook);
        Console.WriteLine($"Successfully added {userChosenBook.Title} to cart!");
        userChosenBook.Status = Status.Selected;
    }
    else if (userChosenBook.Status == Status.CheckedOut)
    {
        Console.WriteLine($"Couldn't add {userChosenBook.Title} to cart. It's checked out until {userChosenBook.DueDate}, sorry!");
    }
    else if (userChosenBook.Status == Status.Selected)
    {
        Console.WriteLine($"Coudldn't add {userChosenBook.Title} to cart. It's already in your cart.");
    } //this is where I'd add the ability to check if a book is OutOfStock and ask the user if they wanna be notified if it's ever in stock; not really sure how exactly to implement that.  
}

void RemoveFromCart(List<Book> cart)
{
    Book book = SelectFromList(cart);
    Console.WriteLine($"{book.Title} has been removed.");
    cart.Remove(book);
}
List<Book> CheckOut(List<Book> cart)
{
    List<Book> CheckedOutBooks = new List<Book>();
    foreach (Book xBook in cart)
    {
        xBook.Status = Status.CheckedOut;
        foreach (Book yBook in catalog.Books)
        {
            if (yBook.Title == xBook.Title)
            {
                yBook.Status = Status.CheckedOut;
                yBook.AssignDueDate();
            }
        }
        xBook.AssignDueDate();
        CheckedOutBooks.Add(xBook);
    }
    cart.Clear();
    Console.WriteLine("The following books have been checked out:\n");
    foreach (Book book in CheckedOutBooks)
    {
        Console.WriteLine($"{book.Title} by {book.Author}");

    }
    Console.WriteLine($"They are all due: {CheckedOutBooks[0].DueDate}");
    return CheckedOutBooks;
}







/*else if (ValidateBookAuthorSelection(userCatalogQuery))
    {
        QueryByAuthor(userCatalogQuery);
        foreach (Book book in queriedMatchingBooks.Books) //make this into a method
        {
            Console.WriteLine($"{book.Title} by {book.Author} \n\tStatus:{book.Status} {(book.DueDate != null ? $"Estimated book return date {book.DueDate}" : "")} ");
        }
    }*/









//Make it so that matching book titles aren't automatically added to the cart without confirmation.


//SELECTION METHODS

Book SelectFromList(List<Book> catalog)
{
    Book selectedBook;
    while (true)
    {
        index = 1;
        foreach (Book book in catalog)
        {
            Console.WriteLine($" Input {index} for {book.Title}");
            index += 1;
        }
        if (int.TryParse(Console.ReadLine(), out userNumSelect))
        {
            try { selectedBook = SelectBook(catalog[userNumSelect - 1].Title); }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine($"Invalid input please put a number within the 1-{catalog.Count()} range. ");
                continue;
            }
            index = 1;
            return selectedBook;
        }
        else
        {
            Console.WriteLine($"Invalid input please put a number within the 1-{catalog.Count()} range. ");
            Console.ReadKey(); Console.Clear();
            index = 1;
            continue;
        }
    }
}
Book SelectBook(string userChosenBook)
{
    Book book = catalog.Books.Single(x => x.Title.ToLower().Trim() == userChosenBook.ToLower().Trim());
    return book;
}

List<Book> QueryByAuthor(string userChosenAuthor)
{

    queriedMatchingBooks.Books = catalog.Books.Where(x => x.Author == userChosenAuthor).ToList();
    Console.WriteLine($"Matching titles by {queriedMatchingBooks.Books[0].Author}:\n");
    return queriedMatchingBooks.Books; // returns list of books that match the users query if it matches an author name or key wordro
}

List<Book> QueryByKeyword(string userKeywordQuery)
{
    foreach (Book book in catalog.Books)
    {
        List<string> titleKeywords = book.Title.Split().ToList();
        foreach (string keyword in titleKeywords)
        {
            if ((userKeywordQuery.ToLower().Trim() == keyword.ToLower().Trim()) && !queriedMatchingBooks.Books.Contains(book))
            {
                queriedMatchingBooks.Books.Add(book);
            }
        }
        List<string> authorKeywords = book.Author.Split().ToList();
        foreach (string keyword in authorKeywords)
        {
            if ((userKeywordQuery.ToLower().Trim() == keyword.ToLower().Trim()) && !queriedMatchingBooks.Books.Contains(book))
            {
                queriedMatchingBooks.Books.Add(book);
            }
        }
    }
    return queriedMatchingBooks.Books;
}








//VALIDATION METHODS
bool ValidateBookTitleSelection(string userChosenBookTitle)
{
    foreach (Book book in catalog.Books)
    {
        if (book.Title.ToLower().Trim() == userChosenBookTitle.ToLower().Trim())
        {
            return true;
        }
    }
    return false;
}

bool ValidateBookAuthorSelection(string userChosenAuthorName)
{
    foreach (Book book in catalog.Books)
    {
        if (book.Author.ToLower().Trim() == userChosenAuthorName.ToLower().Trim())
        {
            return true;
        }
    }
    return false;
}
bool ValidateQueryByKeyword(List<Book> booksMatchingByQuery)
{
    if (booksMatchingByQuery.Count() > 0)
    {
        return true;
    }
    else return false;
}
string AnswerYOrN()
{
    while (true)
    {
        Console.WriteLine("Answer \"y\" or \"n\"");
        string answer = Console.ReadLine().ToLower();
        switch (answer)
        {
            case "y":
            case "n":
                return answer;

            default:
                Console.WriteLine("Invalid input: please enter \"y\" or \"n\"");
                continue;
        }
    }
}


//Trashed Methods
/*
Book ValidateBookTitleSelection(string userChosenBookTitle)
{
    Book selectedBook;
    foreach (Book book in catalog.Books)
    {
        if (book.Title.ToLower().Trim() == userChosenBookTitle.ToLower().Trim())
        {
            return book;
        }
    }
    bool selection = false;
    while (!selection)
    {
        Console.WriteLine("No book matching this title was found. Please enter a title from our selection. \nPress any key to continue. ");
        Console.ReadKey(); Console.Clear();
        Console.WriteLine("We have:");
        catalog.DisplayAvailableBooks();
        Console.WriteLine("Select a book you would like to check out by title");
        userChosenBookTitle = Console.ReadLine();
        foreach (Book book in catalog.Books)
        {
            if (book.Title.ToLower().Trim() == userChosenBookTitle.ToLower().Trim())
            {
                selectedBook = book;
                selection = true;
                break;
            }
            else continue;
        }break;
    } return selectedBook;
}*/
//This code sucks; I made it way too complicated when it should be what it was before: a bool

/*Book SelectBook(Book userChosenBook)
{
    //potentially used for NumSelectedBooks
    Book book = catalog.Books.Single(x => x.Title.ToLower().Trim() == userChosenBook.Title.ToLower().Trim());
    return book;
}*/ //overloading is hard >-<

/*
static void SaveLibrary(List<Book> library)
{ // Save library to a file (Optional Enhancement) Console.WriteLine("Library data saved. Goodbye!"); } } class Book { public string Title { get; set; }public string Author { get; set; } public string Status { get; set; } public string DueDate { get; set; } public Book(stringtitle, string author) { Title = title; Author = author; Status = "On Shelf"; DueDate = null; } public override stringToString() { return $"{Title} by {Author} - Status: {Status} {(DueDate != null ? $"(Due: {DueDate})" : "")}"; } }
}*/





//WAS INSIDE OF QUERYCATALOG METHOD
/*if (ValidateBookTitleSelection(userCatalogQuery)) //make one validation method that informs user of bad data.
    {
        Console.WriteLine($"You have selected {SelectBook(userCatalogQuery).Title} by {SelectBook(userCatalogQuery).Author} ");
        StageCheckOut(SelectBook(userCatalogQuery));

    }
    else if (ValidateBookAuthorSelection(userCatalogQuery))
    {
        QueryByAuthor(userCatalogQuery);
        foreach (Book book in queriedMatchingBooks.Books) //make this into a method
        {
            Console.WriteLine($"{book.Title} by {book.Author} \n\tStatus:{book.Status} {(book.DueDate != null ? $"Estimated book return date {book.DueDate}" : "")} ");
        }
    }
    else // keyword query functionality
    {*/
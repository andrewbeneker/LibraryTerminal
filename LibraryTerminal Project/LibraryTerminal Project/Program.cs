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

string userCatalogQuery = "";
string choice = "";

//READ DATA 
//ADD USER INFO COLLECTION BEFORE MENU


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

Console.Clear();
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
}

//ADD RETURN METHOD || CHCECK



//WRITE DATA










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
                Console.WriteLine($"{SelectBook(userCatalogQuery).Title} has been added to your cart.");
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
static void SaveLibrary(List<Book> library)
{ // Save library to a file (Optional Enhancement) Console.WriteLine("Library data saved. Goodbye!"); } } class Book { public string Title { get; set; }public string Author { get; set; } public string Status { get; set; } public string DueDate { get; set; } public Book(stringtitle, string author) { Title = title; Author = author; Status = "On Shelf"; DueDate = null; } public override stringToString() { return $"{Title} by {Author} - Status: {Status} {(DueDate != null ? $"(Due: {DueDate})" : "")}"; } }
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
    foreach (Book book in cart)
    {
        book.Status = Status.CheckedOut;
        book.AssignDueDate();
        CheckedOutBooks.Add(book);
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
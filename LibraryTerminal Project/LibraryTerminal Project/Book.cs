using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LibraryTerminal_Project
{
    internal class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public Status Status { get; set; } //TODO: make into an enum with different statuses  
        public DateTime? DueDate { get; private set; } = null;

        public Book(string title, string author, Status status)
        {
            Title = title;
            Author = author;
            Status = status;
            DueDate = AssignDueDate();
        }

        public DateTime? AssignDueDate(Book book)
        {
            if (book.Status == Status.CheckedOut && DueDate == null)
            {
                DateTime dueDate;
                dueDate = DateTime.Today.AddDays(2 * 7);
                //wanna add a specific time of 5:00PM but can't figure out rn.
                return DueDate = dueDate;
            }
            else return null;

        }
        private DateTime? AssignDueDate()
        {
            if (Status == Status.CheckedOut && DueDate == null)
            {
                DateTime dueDate;
                Console.WriteLine($"{Title} is checked out; Enter the due date of it's return (e.g. 12/12/2012):");
                while (true)
                {
                    if (DateTime.TryParse(Console.ReadLine(), out dueDate))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Invalid Input; please enter a due date for {Title} (e.g. 12/12/2012):");
                    }
                }
                return DueDate = dueDate;   
            }else return null;
        }

    }
}











/*
       // Checks the status of a book and returns a duedate if applicable
       public DateTime? CheckStatus()
       {
           if (Status == Status.CheckedOut && DueDate == null )
           {
               //might have to change how this code works later. Poetoentially turning it into a method that finds the DueDate if applicable and returning that 
               //assuming it's found because as this method works as of now it's only useful for the intial initializations of each Book. I think...
               //Will keep the function, but it's name has to be changed because it name isn't accurate for what it actually does.
               //maybe something like <AssignDueDate>
               //NVM prob gonna scrap
               DateTime dueDate;
               Console.WriteLine($"{Title} is checked out; Enter the due date of it's return (e.g. 12/12/2012):");
               while (true)
               {
                   if (DateTime.TryParse(Console.ReadLine(), out dueDate))
                   {
                       break;
                   }
                   else
                   {
                       Console.WriteLine($"Invalid Input; please enter a due date for {Title} (e.g. 12/12/2012):");
                   }
               }
               return DueDate = dueDate;
           }
           else return null;
       }   */
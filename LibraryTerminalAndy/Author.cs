using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTerminalAndy
{
    internal class Author
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Author (string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}

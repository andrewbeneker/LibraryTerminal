using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTerminal_Project
{
    internal class User
    {
        public string UserName { get; set; }
        public Guid Guid { get; private set; }
        public string Password { get; set; }
        public List<Book> BorrowedBooks { get; set; }

        public User(string userName)
        {
            UserName = userName;
            Guid = new Guid();
        }

        

    }
}

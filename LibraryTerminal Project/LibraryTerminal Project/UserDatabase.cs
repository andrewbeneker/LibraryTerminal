using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTerminal_Project
{
    //will hold unique data required to differentiate users
    internal class UserDatabase
    {
        public List<User> Users { get; private set; }


        public UserDatabase() 
        { 
            Users = new List<User>();
        }


        public void GetUser(string userName)
        {
            foreach (User user in Users)
            {
                //Will go through each user and find a corresponding userName that matches the user's input; CASE SENSITIVE/ Maybe put a character limit on how long names can be. 
            }
        }

    }
}

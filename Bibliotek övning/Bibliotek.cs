using System;
using System.Collections.Generic;
using System.Text;

namespace Bibliotek_övning
{
    class Bibliotek
    {
        internal static void Användare(string username, string password, string name)
        {
            Användare(username, password, name);
        }
    }

    class Användare
    {
        public string username;
        public string password;
        public string name;

        public Användare(string username, string password, string name)
        {
            this.username = username;
            this.password = password;
            this.name = name;
        }
    }
    /*
    class Biblotekarie : Användare
    {
        
        public Biblotekarie(string username, string password)
        {
            //this = användare
            this.username = username;
            this.password = password;

        }
        
    }

    class Medlem : Användare
    {
        
        public Medlem(string username, string password  )
        {
            this.username = username;
            this.password = password;
        }


    }

    */
}

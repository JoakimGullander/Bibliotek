using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Bibliotek_övning
{
    public class Data
    {
        public string userfilename = "usernames.txt";
        public string passfilename = "passwords.txt";

        public string[] getusernameLines() 
        {
            
            string[] usernamefilelines = File.ReadAllLines(userfilename);
            
            return usernamefilelines;
        }
        public string[] getPasswordLines()
        {

            string[] passwordfilelines = File.ReadAllLines(passfilename);

            return passwordfilelines;
        }

    }
}

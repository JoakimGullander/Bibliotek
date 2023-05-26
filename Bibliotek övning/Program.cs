using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings;
using System.Threading;
using Bibliotek_övning;
using System.Text.RegularExpressions;

namespace Bibliotek_övning
{
    class Program
    {
        static void Main()
        {
            string FileName = (@"C:\Users\user\source\repos\Bibliotek övning\Bibliotek övning\usernames.txt");
            FileInfo fi = new FileInfo(FileName);
            string fullFileName = fi.FullName;
            Console.WriteLine("File Name: {0}", fullFileName);
            string[] usernamefile = File.ReadAllLines(@"C:\Users\joakim.gullander\source\repos\Bibliotek övning\Bibliotek övning\usernames.txt");
            string[] passwordfile = File.ReadAllLines(@"C:\Users\joakim.gullander\source\repos\Bibliotek övning\Bibliotek övning\passwords.txt");
            string[] namefile = File.ReadAllLines(@"C:\Users\joakim.gullander\source\repos\Bibliotek övning\Bibliotek övning\names.txt");
            string[] books = File.ReadAllLines(@"C:\Users\joakim.gullander\source\repos\Bibliotek övning\Bibliotek övning\books.txt");
            List<string> bookfile = new List<string>(books);
            Bibliotek b = new Bibliotek();

            string choice = "0";
            int position = 10;
            string access;
            List<List<Användare>> users = new List<List<Användare>>();
            while (choice != "2")
            {
                usernamefile = File.ReadAllLines(@"C:\Users\joakim.gullander\source\repos\Bibliotek övning\Bibliotek övning\usernames.txt");
                passwordfile = File.ReadAllLines(@"C:\Users\joakim.gullander\source\repos\Bibliotek övning\Bibliotek övning\passwords.txt");
                namefile = File.ReadAllLines(@"C:\Users\joakim.gullander\source\repos\Bibliotek övning\Bibliotek övning\names.txt");

                Console.WriteLine("Vill du skapa ett konto(1), logga in(2) eller avsluta programmet(3)?");
                choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("Skriv in personnummer.");
                    string username = Console.ReadLine();
                    Console.WriteLine("Välj ett lösenord.");
                    string password = Console.ReadLine();
                    Console.WriteLine("Vad heter du? Förnamn och efternamn.");
                    string name = Console.ReadLine();
                    string[] usernamea = { $"{username}" };
                    string[] passworda = { $"{password}" };
                    string[] namea = { $"{name}" };

                    Bibliotek.Användare(username, password, name);


                    File.AppendAllLines(@"C:\Users\joakim.gullander\source\repos\Bibliotek övning\Bibliotek övning\usernames.txt", usernamea);
                    File.AppendAllLines(@"C:\Users\joakim.gullander\source\repos\Bibliotek övning\Bibliotek övning\passwords.txt", passworda);
                    File.AppendAllLines(@"C:\Users\joakim.gullander\source\repos\Bibliotek övning\Bibliotek övning\names.txt", namea);




                }
                else if (choice == "2")
                {
                    Console.WriteLine("Skriv in användarnamn.");
                    string loginun = Console.ReadLine();
                    Console.WriteLine("Skriv in lösenord.");
                    string loginpw = Console.ReadLine();
                    for (int i = 0; i < usernamefile.Length; i++)
                    {
                        if (loginun == usernamefile[i] && loginpw == passwordfile[i])
                        {
                            position = i;
                            if (position < 2)
                                access = "Bibliotikarie";
                            else
                                access = "Medlem";
                            Console.Clear();
                            Console.WriteLine($"Välkommen {namefile[position]}. \nDu är inloggad som en {access}");
                            usernamefile = File.ReadAllLines(@"C:\Users\joakim.gullander\source\repos\Bibliotek övning\Bibliotek övning\usernames.txt");
                            passwordfile = File.ReadAllLines(@"C:\Users\joakim.gullander\source\repos\Bibliotek övning\Bibliotek övning\passwords.txt");
                            namefile = File.ReadAllLines(@"C:\Users\joakim.gullander\source\repos\Bibliotek övning\Bibliotek övning\names.txt");
                            if (access == "Bibliotikarie")
                                Admin(usernamefile, passwordfile, namefile, bookfile, position);
                            if (access == "Medlem")
                                Member(bookfile, passwordfile, position, usernamefile);

                        }
                        else if (i == usernamefile.Length - 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Fel användarnamn eller lösenord.");
                            choice = "0";
                        }
                    }
                }
                else
                    Environment.Exit(0);
            }
        }
        static void Admin(string[] usernamefile, string[] passwordfile, string[] namefile, List<string> bookfile, int position)
        {

            bool login = true;
            while (login == true && position <= 1)
            {
                Console.WriteLine($"Vill du redigera användare(1), redigera böcker(2) eller logga ut(3)? ");
                string choice2 = Console.ReadLine();
                int choice3;
                string change = "";

                if (choice2 == "1")
                {
                    Console.WriteLine("Vilken användare vill du redigera?");
                    for (int i = 2; i < namefile.Length; i++)
                        Console.WriteLine($"({i - 1}) {namefile[i]}");
                    try
                    {
                        choice3 = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.Clear();
                        Console.WriteLine("Välj en möjlig användare.");
                        choice3 = namefile.Length;
                    }
                    try
                    {
                        Console.WriteLine($"Det här är {namefile[choice3 + 1]}s uppgifter\nNamn: {namefile[choice3 + 1]} (1) \nAnvändarnamn: {usernamefile[choice3 + 1]} (2)\nLösenord: {passwordfile[choice3 + 1]} (3)\nVad vill du ändra?");
                        change = Console.ReadLine();
                    }
                    catch
                    {
                        Console.Clear();
                        Console.WriteLine("Välj en möjlig användare.");
                    }
                    if (change == "1")
                    {
                        Console.WriteLine("Skriv det nya namnet.");
                        string changename = Console.ReadLine();
                        namefile[choice3 + 1] = changename;
                        File.WriteAllText(@"C:\Users\joakim.gullander\source\repos\Bibliotek övning\Bibliotek övning\names.txt", String.Empty);
                        File.AppendAllLines(@"C:\Users\joakim.gullander\source\repos\Bibliotek övning\Bibliotek övning\names.txt", namefile);
                    }
                    else if (change == "2")
                    {
                        Console.WriteLine("Skriv det nya användarnamnet.");
                        string changeuser = Console.ReadLine();
                        usernamefile[choice3 + 1] = changeuser;
                        File.WriteAllText(@"C:\Users\joakim.gullander\source\repos\Bibliotek övning\Bibliotek övning\names.txt", String.Empty);
                        File.AppendAllLines(@"C:\Users\joakim.gullander\source\repos\Bibliotek övning\Bibliotek övning\names.txt", usernamefile);
                    }
                    else if (change == "3")
                    {
                        Console.WriteLine("Skriv det nya lösenordet.");
                        string changepass = Console.ReadLine();
                        passwordfile[choice3 + 1] = changepass;
                        File.WriteAllText(@"C:\Users\joakim.gullander\source\repos\Bibliotek övning\Bibliotek övning\names.txt", String.Empty);
                        File.AppendAllLines(@"C:\Users\joakim.gullander\source\repos\Bibliotek övning\Bibliotek övning\names.txt", passwordfile);
                    }

                }
                if (choice2 == "2")
                {
                    Console.WriteLine("Vill du lägga till böcker(1), redigera böcker(2) eller ta bort böcker(3)?");
                    change = Console.ReadLine();
                    if (change == "1")
                    {
                        Console.WriteLine("Skriv in namn på boken.");
                        string bookname = Console.ReadLine();
                        string replacename = bookname.Replace(' ', '_');

                        Console.WriteLine("Skriv in författare för boken.");
                        string bookauthor = Console.ReadLine();
                        string replaceauthor = bookauthor.Replace(' ', '_');

                        Console.WriteLine("Skriv in bokens utgivningsår.");
                        string bookyear = Console.ReadLine();

                        Console.WriteLine("Skriv in hur många av boken som finns.");
                        int bookamount = int.Parse(Console.ReadLine());

                        Console.WriteLine("Skriv in bokens ISBN");
                        string bookisbn = Console.ReadLine();

                        bookfile.Add($"{replacename} {replaceauthor} {bookyear} {bookamount} {bookisbn} {0}");

                        File.WriteAllLines(@"C:\Users\joakim.gullander\source\repos\Bibliotek övning\Bibliotek övning\books.txt", bookfile);

                    }
                    else if (change == "2")
                    {
                        Console.WriteLine("Vilken bok vill du redigera?");
                        for (int i = 0; i < bookfile.Count; i++)
                            Console.WriteLine($"({i + 1}) {bookfile[i]}");
                        try
                        {
                            choice3 = int.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            Console.Clear();
                            Console.WriteLine("Välj en möjlig bok.");
                            choice3 = bookfile.Count;
                        }
                        try
                        {
                            string[] bookinfo = bookfile[choice3 - 1].Split(' ');
                            string bookname = bookinfo[0];
                            string replacename = bookname.Replace('_', ' ');
                            string bookauthor = bookinfo[1];
                            string replaceauthor = bookauthor.Replace('_', ' ');
                            string bookyear = bookinfo[2];
                            string replaceyear = bookyear.Replace('_', ' ');
                            string bookamount = bookinfo[3];
                            string replaceamount = bookamount.Replace('_', ' ');
                            Console.WriteLine($"Det här är bokens uppgifter:\nNamn: {replacename} (1)\nFörfattare: {replaceauthor} (2)\nUtgivningsår: {replaceyear} (3)\nAntal: {replaceamount} (4)\nVad vill du ändra?");
                            string change2 = Console.ReadLine();
                            if (change2 == "1")
                            {
                                Console.WriteLine("Skriv det nya namnet.");
                                string changebname = Console.ReadLine();
                                bookinfo[0] = changebname;
                            }
                            else if (change2 == "2")
                            {
                                Console.WriteLine("Skriv det nya användarnamnet.");
                                string changebauthor = Console.ReadLine();
                                bookinfo[1] = changebauthor;
                            }
                            else if (change2 == "3")
                            {
                                Console.WriteLine("Skriv det nya lösenordet.");
                                string changebyear = Console.ReadLine();
                                bookinfo[2] = changebyear;
                            }
                            else if (change2 == "4")
                            {
                                Console.WriteLine("Skriv den nya mängden.");
                                string changebamount = Console.ReadLine();
                                bookinfo[3] = changebamount;
                            }
                            bookfile[choice3-1] = $"{bookinfo[0]} {bookinfo[1]} {bookinfo[2]} {bookinfo[3]} {bookinfo[4]} {bookinfo[5]}";
                            File.WriteAllLines(@"C:\Users\joakim.gullander\source\repos\Bibliotek övning\Bibliotek övning\books.txt", bookfile);
                        }
                        catch
                        {
                            Console.Clear();
                            Console.WriteLine("Välj en möjlig Bok.");
                        }



                    }
                    else if (change == "3")
                    {
                        Console.WriteLine("Vilken bok vill du ta bort?");
                        for (int i = 0; i < bookfile.Count; i++)
                            Console.WriteLine($"({i + 1}) {bookfile[i]}");
                        try
                        {
                            choice3 = int.Parse(Console.ReadLine());
                            bookfile.Remove(bookfile[choice3 - 1]);
                            File.WriteAllLines(@"C:\Users\joakim.gullander\source\repos\Bibliotek övning\Bibliotek övning\books.txt", bookfile);
                            Console.WriteLine("Bok borttagen");
                        }
                        catch
                        {
                            choice3 = bookfile.Count;
                        }
                    }
                }
                if (choice2 != "1" && choice2 != "2" && choice2 != "3")
                    login = false;
            }
            Main();
        }
        static void Member(List<string> bookfile, string[] passwordfile, int position, string[] usernamefile)
        {
            bool login = true;
            while (login)
            {

                List<int> bookcontainer = new List<int>();
                Console.WriteLine("Vad vill du göra?\n(1) Söka efter bok\n(2) Se lånade böcker \n(3) Ändra ditt lösenord\n(4) Logga ut");
                string choice1 = Console.ReadLine();
                if (choice1 == "1")
                {

                    string[][] bookinfo = new string[bookfile.Count][];

                    for (int i = 0; i < bookfile.Count; i++)
                    {
                        bookinfo[i] = bookfile[0].Split(' ');

                    }

                    Console.WriteLine("Sök");
                    string search = Console.ReadLine();
                    for (int i = 0; i < bookinfo.Length; i++)
                    {
                        if (bookfile[i].Contains(search))
                            bookcontainer.Add(i);
                    }
                    Console.WriteLine();
                    for (int i = 0; i < bookcontainer.Count; i++)
                    {
                        Console.WriteLine($"({i + 1}) {bookfile[bookcontainer[i]]}");
                    }
                    Console.WriteLine("Vilken bok vill du låna?");
                    int lån = int.Parse(Console.ReadLine());
                    string[] books = bookfile[bookcontainer[lån - 1]].Split(' ');
                    if (books[5] == "0")
                    {
                        books[5] = usernamefile[position];
                        bookfile[bookcontainer[lån - 1]] = $"{books[0]} {books[1]} {books[2]} {books[3]} {books[4]} {books[5]}";
                        File.WriteAllLines(@"C:\Users\joakim.gullander\source\repos\Bibliotek övning\Bibliotek övning\books.txt", bookfile);
                        Console.WriteLine("Bok lånad");
                    }
                    else
                    {
                        Console.WriteLine("Bok redan lånad av någon annan");
                    }

                }
                else if (choice1 == "2")
                {

                    string[][] bookinfo = new string[bookfile.Count][];
                    for (int i = 0; i < bookfile.Count; i++)
                    {
                        bookinfo[i] = bookfile[0].Split(' ');

                    }

                    string search = usernamefile[position];
                    for (int i = 0; i < bookinfo.Length; i++)
                    {
                        if (bookfile[i].Contains(search))
                            bookcontainer.Add(i);
                    }
                    for (int i = 0; i < bookcontainer.Count; i++)
                    {
                        Console.WriteLine($"({i + 1}) {bookfile[bookcontainer[i]]}");
                    }
                    Console.WriteLine("Vilken bok vill du lämna tillbaka");
                    int lån = int.Parse(Console.ReadLine());
                    if (lån > bookcontainer.Count)
                    {
                        Console.WriteLine("Boken finns inte");
                        Member(bookfile, passwordfile, position, usernamefile);
                    }
                    string[] books = bookfile[bookcontainer[lån - 1]].Split(' ');
                    
                    bookfile[bookcontainer[lån - 1]] = $"{books[0]} {books[1]} {books[2]} {books[3]} {books[4]} {0}";
                    File.WriteAllLines(@"C:\Users\joakim.gullander\source\repos\Bibliotek övning\Bibliotek övning\books.txt", bookfile);
                    Console.WriteLine("Bok återlämnad");
                

                }
                else if (choice1 == "3")
                {
                    Console.WriteLine("Skriv det nya lösenordet.");
                    string changepass = Console.ReadLine();
                    passwordfile[position] = changepass;
                    File.WriteAllText(@"C:\Users\joakim.gullander\source\repos\Bibliotek övning\Bibliotek övning\names.txt", String.Empty);
                    File.AppendAllLines(@"C:\Users\joakim.gullander\source\repos\Bibliotek övning\Bibliotek övning\names.txt", passwordfile);
                }
                else if (choice1 == "4")
                {
                    login = false;
                }





            }
            Main();
        }
    }
}

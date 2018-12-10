using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace SearchEngine
{
    /// <summary>
    /// The <see cref="Database"/> class is designed to manage multiple <see cref="Humanoid"/> instances.
    /// </summary>
    public class Database
    {
        /// <summary>
        /// Container for holding <see cref="Humanoid"/> instances.
        /// </summary>
        private List<Humanoid> Humanoids;

        /// <summary>
        /// Stores the name of the file used for the <see cref="Database"/>.
        /// </summary>
        private string DatabaseFileName;

        /// <summary>
        /// Initializes a new instance of the <see cref="Database"/> class using the specified file name.
        /// </summary>
        /// <param name="filename">The name of the file that will hold <see cref="Humanoid"/> instances or be used to load <see cref="Humanoid"/> instances into the database.</param>
        public Database(string filename)
        {
            Humanoids = new List<Humanoid>();
            DatabaseFileName = filename;
            LoadHumanoidData();
            if (Humanoids.Count == 0)
            {
                Console.WriteLine("New humanoid database has been initialized. Create a humanoid now.");
                AddHumanoid();
            }
            else
                Console.WriteLine("Humanoids successfully loaded.");
        }

        /// <summary>
        /// Adds a new <see cref="Humanoid"/> instance to this instance.
        /// </summary>
        public void AddHumanoid()
        {
            string name = "";
            ushort age = 0;

            Console.Write("What is the humanoid's name (in the format Last, First)? ");
            name = Console.ReadLine();

            Console.Write("Age? ");
            age = Convert.ToUInt16(Console.ReadLine());

            Humanoids.Add(new Humanoid(name, age));

            Humanoids.Sort();
        }

        /// <summary>
        /// Searches for <see cref="Humanoid"/> instances of this instance using a specified search phrase.
        /// </summary>
        public void SearchHumanoid()
        {
            if (Humanoids.Count == 0)
                Console.WriteLine("There are currently 0 humanoids in the database. Please try again later.");
            else
            {
                int humanoidMatches = 0;
                string searchTerm = "";
                bool matchFlag = false;
                List<Humanoid> matches = new List<Humanoid>();
                Console.WriteLine("Search for humanoids within the database. When searching for a phone number, include parentheses.");
                Console.Write("Enter your search name, age, email address, or phone number: ");
                searchTerm = Console.ReadLine();
                foreach (Humanoid human in Humanoids)
                {
                    matchFlag = false;
                    if (human.Name.ToLower().Contains(searchTerm.ToLower()))
                    {
                        matchFlag = true;
                        matches.Add(human);
                        humanoidMatches++;
                    }
                    else if (human.Email.ToString().Contains(searchTerm.ToLower()))
                    {
                        matchFlag = true;
                        matches.Add(human);
                        humanoidMatches++;
                    }
                    else if (human.Phone.Number.Contains(searchTerm.ToLower()))
                    {
                        matchFlag = true;
                        matches.Add(human);
                        humanoidMatches++;
                    }
                    if (!matchFlag)
                    {
                        try
                        {
                            ushort age = Convert.ToUInt16(searchTerm);
                            if (human.Age == age)
                            {
                                matchFlag = true;
                                matches.Add(human);
                                humanoidMatches++;
                            }
                        }
                        catch (Exception) { }
                    }
                }
                if (humanoidMatches > 0)
                {
                    Console.WriteLine(humanoidMatches + " match(es) found: ");
                    matches.Sort();
                    Console.WriteLine("Index".PadRight(7) + "Name".PadRight(20) + "Age".PadRight(5) + "Email".PadRight(35) + "Phone");
                    int index = 0;
                    foreach (Humanoid human in matches)
                    {
                        Console.WriteLine(index++.ToString().PadRight(7) + human.Name.PadRight(20)
                                        + human.Age.ToString().PadLeft(3) + "  " + human.Email.ToString().PadRight(35)
                                        + human.Phone.Number);
                    }
                }
                else
                {
                    Console.WriteLine("Sorry. No matches found according to your search.");
                }
            }
        }

        /// <summary>
        /// Sets a specified property of a <see cref="Humanoid"/> instance of this instance.
        /// </summary>
        public void ModifyHumanoid()
        {
            if (Humanoids.Count == 0)
                Console.WriteLine("There are currently 0 humanoids in the database. Please try again later.");
            else
            {
                char doneModifying = 'N';
                int index = -1;
                ShowHumanoids();
                Console.Write("What is the index of the humanoid to modify? ");
                index = Convert.ToInt32(Console.ReadLine());
                while (index < 0 || index >= Humanoids.Count)
                {
                    Console.Write("Invalid index. Please try again: ");
                    index = Convert.ToInt32(Console.ReadLine());
                }
                while (Char.ToUpper(doneModifying).Equals('N'))
                {
                    int option = 0;
                    Console.WriteLine("What number property do you want to modify: ");
                    Console.WriteLine("\t1. Name");
                    Console.WriteLine("\t2. Age");
                    Console.WriteLine("\t3. Email");
                    Console.WriteLine("\t4. Phone Number");
                    Console.Write("Your choice: ");
                    option = Convert.ToInt32(Console.ReadLine());
                    while (option < (int)HumanoidProp.NAME || option > (int)HumanoidProp.PHONE)
                    {
                        Console.Write("Property number out of range. Try again: ");
                        option = Convert.ToInt32(Console.ReadLine());
                    }
                    switch ((HumanoidProp)option)
                    {
                        case HumanoidProp.NAME:
                            Humanoids[index].SetName();
                            break;
                        case HumanoidProp.AGE:
                            Humanoids[index].SetAge();
                            break;
                        case HumanoidProp.EMAIL:
                            Humanoids[index].SetEmail();
                            break;
                        case HumanoidProp.PHONE:
                            Humanoids[index].SetPhone();
                            break;
                    }
                    Console.Write("Are you done modifying this humanoid (Y/N)? ");
                    doneModifying = Console.ReadLine()[0];
                }
            }
        }

        /// <summary>
        /// Removes a specified <see cref="Humanoid"/> from this instance.
        /// </summary>
        public void DeleteHumanoid()
        {
            if (Humanoids.Count == 0)
                Console.WriteLine("There are currently 0 humanoids in the database. Please try again later.");
            else
            {
                ShowHumanoids();
                Console.Write("Choose the index of the humanoid to delete: ");
                int index = Convert.ToInt32(Console.ReadLine());
                while (index < 0 || index >= Humanoids.Count)
                {
                    Console.Write("Index out of range. Try again: ");
                    index = Convert.ToInt32(Console.ReadLine());
                }
                Console.Write("Are you sure you want to delete " + Humanoids[index].Name + " (Y/N)? ");
                char confirmDel = Console.ReadLine()[0];
                if (Char.ToUpper(confirmDel).Equals('Y'))
                    Humanoids.RemoveAt(index);
                else
                    Console.WriteLine("Request was denied.");
            }
        }
   
        /// <summary>
        /// Shows all <see cref="Humanoid"/> instances in this instance.
        /// </summary>
        public void ShowHumanoids()
        {
            if (Humanoids.Count == 0)
                Console.WriteLine("There are currently 0 humanoids in the database. Please try again later.");
            else
            {
                Console.WriteLine("Index".PadRight(7) + "Name".PadRight(20) + "Age".PadRight(5) + "Email".PadRight(35) + "Phone");
                int index = 0;
                foreach (Humanoid human in Humanoids)
                {
                    Console.WriteLine(index++.ToString().PadRight(7) + human.Name.PadRight(20) 
                                    + human.Age.ToString().PadLeft(3) + "  " + human.Email.ToString().PadRight(35)
                                    + human.Phone.Number);
                }
            }
        }

        /// <summary>
        /// Shows the properties of a <see cref="Humanoid"/> in this instance.
        /// </summary>
        /// <param name="index">Index of <see cref="Humanoid"/> to display in this instance.</param>
        public void ShowHumanoidAt(int index)
        {
            Console.WriteLine("Name:".PadRight(8) + Humanoids[index].Name);
            Console.WriteLine("Age:".PadRight(8) + Humanoids[index].Age);
            Console.WriteLine("Email:".PadRight(8) + Humanoids[index].Email.ToString());
            Console.WriteLine("Phone:".PadRight(8) + Humanoids[index].Phone.Number);
        }

        /// <summary>
        /// Get the number of <see cref="Humanoid"/> instances in this instance.
        /// </summary>
        /// <returns>Returns the number of <see cref="Humanoid"/> instances in this instance.</returns>
        public int GetHumanoidCount() => Humanoids.Count;

        /// <summary>
        /// Loads existing <see cref="Humanoid"/> instances from a file into this instance.
        /// </summary>
        public void LoadHumanoidData()
        {
            try
            {
                StreamReader file = new StreamReader(DatabaseFileName);
                string name = "", email = "", number = "";
                ushort age = 0;
                name = file.ReadLine();
                age = Convert.ToUInt16(file.ReadLine());
                email = file.ReadLine();
                number = file.ReadLine();
                while (!file.EndOfStream)
                {
                    Humanoids.Add(new Humanoid(name, age, email, number));
                    name = file.ReadLine();
                    age = Convert.ToUInt16(file.ReadLine());
                    email = file.ReadLine();
                    number = file.ReadLine();
                }
                Humanoids.Add(new Humanoid(name, age, email, number));
                file.Close();
            }
            catch (IOException exception)
            {
                Console.WriteLine("An error has occurred: " + exception.Message);
            }
        }

        /// <summary>
        /// Writes all <see cref="Humanoid"/> instances of this instance to a file.
        /// </summary>
        public void SaveDatabaseToFile()
        {
            try
            {
                int percentComplete = 0;
                int currIndex = 0;
                StreamWriter file = new StreamWriter(DatabaseFileName);
                Console.WriteLine("Writing data to file " + DatabaseFileName + " . . .");
                foreach (Humanoid human in Humanoids)
                {
                    percentComplete = (int)((double)currIndex / GetHumanoidCount() * 100);
                    Console.WriteLine(percentComplete + "% complete . . .");
                    file.WriteLine(human.Name);
                    file.WriteLine(human.Age);
                    file.WriteLine(human.Email.ToString());
                    file.WriteLine(human.Phone.Number);
                    Thread.Sleep(1500);
                    currIndex++;
                }
                Console.WriteLine("Finished writing to file.");
                file.Close();
            }
            catch (IOException exception)
            {
                Console.WriteLine("An error occurred: " + exception.Message);
            }
            
        }
    }
}

using System;
using System.Threading;

namespace SearchEngine
{
    /// <summary>
    /// Holds the options for for mutating a <see cref="Database"/> instance.
    /// </summary>
    public enum DatabaseChoice {ADD_HUMANOID = 1, MODIFY_HUMANOID, SHOW_HUMANOIDS, SHOW_HUMANOID_AT, SEARCH_HUMANOID, DEL_HUMANOID, QUIT}

    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            int option = 0;

            string[] introduction =
            {
                "Hello and welcome to the Humanoid Database Management System,\n",
                "where you can manage your humanoid clients all in one place: here!\n",
                @"To begin, enter the name of the file where you are storing your humanoids (ex. C:\Clients.txt): "
            };

            foreach (string item in introduction)
            {
                Console.Write(item);
                Thread.Sleep(2500);
            }

            Database db = new Database(Console.ReadLine());

            while (option != (int)DatabaseChoice.QUIT)
            {
                ShowDatabaseMenu();
                option = Convert.ToInt32(Console.ReadLine());
                PerformDatabaseOption(option, db);
            }

            db.SaveDatabaseToFile();
        }

        /// <summary>
        /// 
        /// </summary>
        public static void ShowDatabaseMenu()
        {
            Console.WriteLine("Options are as follows:");
            Console.WriteLine("\t1. Add a humanoid");
            Console.WriteLine("\t2. Modify a humanoid");
            Console.WriteLine("\t3. Show all humanoids");
            Console.WriteLine("\t4. Show humanoid at index");
            Console.WriteLine("\t5. Search for a humanoid");
            Console.WriteLine("\t6. Delete a humanoid");
            Console.WriteLine("\t7. Quit");
            Console.Write("\nYour choice: ");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="option"></param>
        /// <param name="db"></param>
        public static void PerformDatabaseOption(int option, Database db)
        {
            switch ((DatabaseChoice)option)
            {
                case DatabaseChoice.ADD_HUMANOID:
                    db.AddHumanoid();
                    break;
                case DatabaseChoice.MODIFY_HUMANOID:
                    db.ModifyHumanoid();
                    break;
                case DatabaseChoice.SHOW_HUMANOIDS:
                    db.ShowHumanoids();
                    break;
                case DatabaseChoice.SHOW_HUMANOID_AT:
                    int index = -1;
                    Console.Write("What is the index of the humanoid you want (max is " + (db.GetHumanoidCount() - 1) + ")? ");
                    index = Convert.ToInt32(Console.ReadLine());
                    while (index < 0 || index >= db.GetHumanoidCount())
                    {
                        Console.Write("Invalid index. Please try again: ");
                        index = Convert.ToInt32(Console.ReadLine());
                    }
                    db.ShowHumanoidAt(index);
                    break;
                case DatabaseChoice.SEARCH_HUMANOID:
                    db.SearchHumanoid();
                    break;
                case DatabaseChoice.DEL_HUMANOID:
                    db.DeleteHumanoid();
                    break;
                case DatabaseChoice.QUIT:
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid operation. Program terminating.");
                    break;
            }
        }
    }
}

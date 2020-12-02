using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01NamesAgain
{
    class Program
    {

        public delegate void ReportingDelegateType(string mmm);

        static void reportFindingToFile(string msg)
        {
            //FIXME: unhandled ioexception
            File.AppendAllText(@"..\..\log.txt", "FOUND: "+msg+"\n");   
            // ..\ means go one level up of the working directory, normally under ..\bin\debug, use two ..\ to put it right under the projecr file
            // the sign @ used to make the special meaning character like back slash works as is, rather than ignored/escaped from the string
            // especially for the back slash, otherwise should define as ..\\
        }

        static void reportFindingScreen(string msg)
        {
            Console.WriteLine("FOUND: " + msg);
        }

        static string[] nameList = new string[5];

        static void Main(string[] args)
        {
            // delegate variable is a list of references to methods
            ReportingDelegateType report = null;

            Random random = new Random();

            // randomly going to choose one of the method to the delegate
            if (random.Next(0,2)==1)
            {
                Console.WriteLine("Adding reporting on screen");
                report += reportFindingScreen;
            }
            if (random.Next(0, 2) == 1)
            {
                Console.WriteLine("Adding reporting to file");
                report += reportFindingToFile;
            }

            //Console.WriteLine("Delegates: " + report.ToString());
            //report = reportFindingScreen;

            try
            {
                for (int i=0; i<nameList.Length; i++)
                {
                    Console.Write("Enter a name: ");
                    nameList[i] = Console.ReadLine();
                }

                Console.Write("Enter search string: ");
                string search = Console.ReadLine();
                foreach(String n in nameList)
                {
                    if (n.Contains(search))
                    {
                        string line = string.Format("{0} contains {1} string", n, search);
                        Console.WriteLine(line);
                        report?.Invoke(line);    // call only if not null
                    }
                }

                report -= reportFindingToFile;

                // String longest = "";
                String longest = nameList[0];
                foreach (String n in nameList)
                {
                    if (longest.Length < n.Length)
                    {
                        longest = n;
                    }
                }
                Console.WriteLine("The longest is " + longest);

            }
            finally
            {
                Console.WriteLine("Press any key...");
                Console.ReadKey();
            }

        }
    }
}

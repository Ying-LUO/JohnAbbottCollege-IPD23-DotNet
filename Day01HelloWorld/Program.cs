using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// everything is begin with uppercase, only variables begin with lower case
// string is lower case, since in c#, string is a primetive type as the other types as well as a object
//CLR--Common Language Runtime, like java JVM
//in C# platform, there is String(uppercase start), Int32/Int64, 
//although String alias with string, Int32 alias int, Int64 alias long, Int16 alias for short
//just different name in C#
//java type system only have signed value, like 1024 and -1024
//but C# have unsigned/signed value, like uint, but for us only use int
//KISS principle: Keep It Simple Stupid

//Modify the program to ask the user for their name and then their age
//If the age is not a valid integer value display an error message and stop the program.
//otherwise display a greeting.
//Example session:
//How is your name? Jerry
//How old are you? 33
//Hello Jerry, you are 33 y/o, nice to meet you.
//Press any key to finish.

namespace Day01HelloWorld
{
    class Program      //assemblly default, different with Java public/private
    {
        static void Main(string[] args)     

        {
            Console.WriteLine("Hello World!");

            string name;
            int age;

            Console.Write("How is your name?");
            name = Console.ReadLine();

            bool nameSuccess = !string.IsNullOrEmpty(name);

            Console.Write("How old are you?");
            //age = Console.Read();


            var ageAsString = Console.ReadLine();
            bool parseSuccess = int.TryParse(ageAsString, out age);

            if (nameSuccess && parseSuccess)
                Console.WriteLine($"Hello {name}, you are {age} y/o, nice to meet you.");
            else
                Console.WriteLine("Name cannot be empty or Age must be number!");

            Console.WriteLine("Press any key to finish");
            Console.ReadKey();
        }
    }
}

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
            // Read method only read one character

            // all exception is unchecked in .net/C#
            // the compiler will never reminder you to handle the exception
            // normal exceptions need to remember to handle, that: parse/network io/ database connection/ files

            var ageAsString = Console.ReadLine();
            bool parseSuccess = int.TryParse(ageAsString, out age);  
            //it doesn't throw exception like JAVA, but only return boolean
            // it return false when it parse failed, and the out parameter still assign value 0 to variable age
            // if it return true, the variable age will be modified
            // in java we rarely use "out" parameter which essential if you have return value, if you meet several values
            // when you parsing the value, you're parsing the value, not the object
            // when you doing out variable, it actually parsing the referrence of the object, it's from SQL 
            //System.FormatException if parse string to int failed
            // you can use try catch to catch the exception
            // try{...}
            // catch{
            //       Console.WriteLine("Error: input must be an integer");
                   //}

            // Environment.Exit(1);   --- just like JAVA System.exit(1);

            // format string, use place holder curely braces,{}, you can use it multiple times, you can put order in it {0}{1} inside, like index
            // like :
            // Console.WriteLine($"Hello {0}, you are {1} y/o, nice to meet you.", name, age};
            if (nameSuccess && parseSuccess)
                Console.WriteLine($"Hello {name}, you are {age} y/o, nice to meet you.");
            else
                Console.WriteLine("Name cannot be empty or Age must be number!");

            Console.WriteLine("Press any key to finish");
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;


namespace HelloSignConsoleTester
{
    class Program
    {
        //https://support.microsoft.com/en-us/kb/815786

        static void Main(string[] args)
        {

            Console.WriteLine("HelloWorld HelloSign");

            var sAttr = ConfigurationManager.AppSettings.Get("Key0");
            Console.WriteLine(sAttr + " Is key");



            Console.ReadKey();

        }

        static void GetConnectionStrings()
        {
    

        }
    }
}

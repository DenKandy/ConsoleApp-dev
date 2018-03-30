using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using App.Exceptions;
using App.Commands;

namespace App
{
     class Program
    {
        public delegate void Operation ( string statement );

        public static string AbsoluteAppPath                        { get; private set; }
        public static string AppDataPath                            { get; private set; }
        public static Dictionary<string, XElement> CommandHelp      { get; private set; }
        public static Dictionary<string, XElement> Accounts         { get; private set; }
        public static Dictionary<string, Operation> Commands        { get; private set; }
        public static Dictionary<string, string> CommandAlias       { get; private set; }

        static void Main ( string [] args )
        {
            AbsoluteAppPath = Directory.GetCurrentDirectory ().Substring ( 0, Directory.GetCurrentDirectory ().IndexOf ( "bin" ) );
            try
            {
                Statement statement = new Statement(" help / --syntax");
                statement.Parse ();
                Print ( ConsoleColor.Cyan, "Test is successful" );
            }
            catch ( FatalException err )
            {
                err.WriteError ();
                err.EmergencyExit ();
                
            }
            catch ( AppException err )
            {
                err.WriteLog ();
                err.Print ();
            }
            finally
            {
                Console.ReadKey ();
            }
        }

        public static void Print( ConsoleColor color, object value )
        {
            Console.ForegroundColor = color;
            Console.Write ( value );
            Console.ResetColor ();
        }
        public static void Print ( ConsoleColor color, string txt, string sym = "-> ", ConsoleColor console = ConsoleColor.White )
        {
            Print ( color: console, value: sym );

            Print ( color: color, value: txt );

            Console.WriteLine ();
        }
    }
}

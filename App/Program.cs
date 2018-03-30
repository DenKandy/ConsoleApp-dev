using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
            try
            {
                Statement statement = new Statement(" help / 'ggg: ffd, ferwf, frf; hherdg: grg , tdfb , tfgrg ;dff,gg,gt ' -F -g --pelp --PIDOR reqwegr 'rvrvrdv' ");
                statement.Parse ();
                throw new AppException ( "kk" );
            }
            catch ( FatalException err )
            {
                err.EmergencyExit ();  
            }
            catch ( AppException err )
            {
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

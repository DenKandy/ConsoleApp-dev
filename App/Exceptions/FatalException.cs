using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Exceptions
{
    public class FatalException : AppException
    {


        public FatalException ( string message, string solution = "Look at log that diside it" ) : base ( message, solution )
        {
            AppName = "Fatal Error";
        }
        /// <summary>
        /// Emergency complite work.
        /// Print error message and to wait any pressing to key.
        /// </summary>
        public void EmergencyExit ()
        {
            Program.Print ( ConsoleColor.Magenta, AppName, "" );
            Print ();
            Program.Print ( ConsoleColor.Yellow, "Please press any key to close..." );
            Console.ReadKey ();
        }
    }
}

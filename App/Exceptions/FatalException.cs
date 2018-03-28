﻿using System;
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
        public void EmergencyExit ()
        {
            Print ();
            Program.Print ( ConsoleColor.Yellow, "Please press any key to close..." );
            Console.ReadKey ();
        }
    }
}

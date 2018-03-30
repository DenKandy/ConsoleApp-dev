using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace App.Exceptions
{
    public class AppException : Exception, IAppException
    {
        public string AppName { get; set; }
        public string AppMessage { get; set; }
        public string Solution { get; set; }

        public AppException ( string message, string solution = "Look at log that solve it" ) : base ( message )
        {
            AppName = "Application Error";
            AppMessage = message;
            Solution = solution;
        }
        /// <summary>
        /// Create directory 'log' if one is absent.
        /// Write to files to path 'log/time-name.txt'
        /// </summary>
        public void WriteLog ()
        { 
            Write ( Program.AbsoluteAppPath + "log\\" );
        }
        /// <summary>
        /// Create directory 'error' if one is absent.
        /// Write to files to path 'error/time-name.txt'
        /// </summary>
        public void WriteError ()
        {
            Write ( Program.AbsoluteAppPath + "error\\" );
        }
        /// <summary>
        /// Print message about problem and its solution
        /// </summary>
        public void Print ()
        {
            Program.Print ( color: ConsoleColor.Red,    txt: AppMessage );
            Program.Print ( color: ConsoleColor.Green,  txt: Solution );
        }
        /// <summary>
        /// Create directory if one is absent and to create file also to write into its
        /// </summary>
        /// <param name="path">Path to file</param>
        void Write ( string path )
        {
            if ( !Directory.Exists ( path ) )
            {
                Directory.CreateDirectory ( path );
            }
            StreamWriter writer = new StreamWriter( File.Create( path + DateTime.Now.ToFileTime () + ".txt" ) );
            writer.Write (
                $"Name exception:   {AppName} \n" +
                $"Message:          {AppMessage} \n" +
                $"Exception:        {ToString ()} \n" +
                $"Inner exception:  {InnerException?.ToString ()} \n" +
                $"Date:             {DateTime.Now.ToString ()}"
                );
            writer.Close ();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Exceptions;
namespace App.Commands
{
    class Statement : IStatement
    { 
        public string Command                           { get; set; }
        public List<string> Parameters                    { get; set; }
        public List<string> Atributtes                  { get; set; }
        public Dictionary<string, string []> Properties { get; set; }
        public string String                            { get; set; }
        /// <summary>
        /// Create statement
        /// </summary>
        /// <param name="statement"></param>
        public Statement ( string statement )
        {
            String = statement.Trim();
            Atributtes = new List<string> ();
            Parameters = new List<string> ();
            Properties = new Dictionary<string, string []> ();
        }

        public void Parse ()
        {
            Regex regex;
            Match match;
            //get command
            Command = String.Substring ( 0, String.IndexOf ( " " ) );
            //get properties
            regex = new Regex(@"\s+('.+')\s+", RegexOptions.IgnoreCase );
            match = regex.Match ( String );
            if ( ! String.IsNullOrEmpty( match.Value ) )
            {
                ParseProperties ( match.Value );
            }
            //get parameters
            regex = new Regex ( @"\s-\w", RegexOptions.IgnoreCase );
            foreach ( Match item in regex.Matches ( String ) )
            {
                if ( !String.IsNullOrEmpty ( item.Value ) )
                {
                    Parameters.Add ( item.Value );
                }
            }
            //get attributes
            regex = new Regex ( @"\s--\w+" );
            foreach ( Match item in regex.Matches ( String ) )
            {
                if ( !String.IsNullOrEmpty ( item.Value ) )
                {
                    Atributtes.Add ( item.Value );
                }
            }

        }
        void ParseProperties ( string properties )
        {
            string [] props = properties.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries );
            foreach ( var item in props )
            {
                if ( item.IndexOf(":") != -1 )
                {
                    Properties.Add ( item.Substring ( 0, item.IndexOf ( ":" ) ), item.Split ( new char [] { ',', ':' }, StringSplitOptions.RemoveEmptyEntries ).Skip ( 1 ).ToArray() );
                }
                else
                {
                    if ( Properties.ContainsKey("null") )
                    {
                        throw new AppException ( "Command can't be contains two or more anonim properties", $"Use '{Command} --help' for get more info about command" );
                    }
                    Properties.Add ( "null", item.Split ( new char [] { ',' }, StringSplitOptions.RemoveEmptyEntries ) );
                }
            }
        }
    }
}

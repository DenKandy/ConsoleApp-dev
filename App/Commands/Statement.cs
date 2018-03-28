using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Commands
{
    class Statement : IStatement
    { 
        public string Command                           { get; set; }
        public List<char> Parameters                    { get; set; }
        public List<string> Atributtes                  { get; set; }
        public Dictionary<string, string []> Properties { get; set; }

        protected List<string> Blocks  { get; set; }
        public string String        { get; set; }
        /// <summary>
        /// Create and parse
        /// </summary>
        /// <param name="statement"></param>
        public Statement ( string statement )
        {
            String = statement.Trim();
            Atributtes = new List<string> ();
            Blocks = new List<string> ();
            Parameters = new List<char> ();
        }

        public void Parse ()
        {
            Command = String.Substring ( 0, String.IndexOf ( " " ) );
            string str = String.Substring(String.IndexOf(" "));
            Regex regex;
            Match match;
            //get properties
            regex = new Regex(@"\s+('.+')\s+", RegexOptions.IgnoreCase );
            match = regex.Match ( String );
            if ( ! String.IsNullOrEmpty( match.Value ) )
            {
                Blocks.Add ( match.Value );
                ParseProperties ( match.Value );
            }
            regex = new Regex ( @"\s-\w", RegexOptions.IgnoreCase );
            foreach ( Match item in regex.Matches ( String ) )
            {
                if ( !String.IsNullOrEmpty ( item.Value ) )
                {
                    Blocks.Add ( item.Value );
                    ParseAttribute ( item.Value );
                }
            }
            regex = new Regex ( @"\s--\w+" );
            foreach ( Match item in regex.Matches ( String ) )
            {
                if ( !String.IsNullOrEmpty ( item.Value ) )
                {
                    Blocks.Add ( item.Value );
                    ParseAttribute ( item.Value );
                }
            }

        }
        void ParseAttribute( string attributes )
        {

        }
        void ParseProperties ( string attributes )
        {

        }
        void ParseParameters ( string attributes )
        {

        }
    }
}

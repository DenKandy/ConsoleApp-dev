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

        public const char TYPE_COMMAND = '/';
        public const char TYPE_PROP_KEY = ':';
        public const char TYPE_PROP_VALUE = ';';
        public const char TYPE_PROP_DEL = ',';
        public const char TYPE_PROP_KEY_DEF = '_';

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
            regex = new Regex ( @"\s*\w+\s*/", RegexOptions.IgnoreCase );
            match = regex.Match ( String );
            if ( !String.IsNullOrEmpty ( match.Value ) )
            {
                Command = match.Value;
            }else
            {
                throw new AppException ( $"Command can't be absent '{String}'", "Use 'help / --syntax' for get how it correct " );
            }
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
            string [] props = properties.Split(new char[] { TYPE_PROP_VALUE }, StringSplitOptions.RemoveEmptyEntries );
            foreach ( var item in props )
            {
                if ( item.IndexOf( TYPE_PROP_KEY ) != -1 )
                {
                    Properties.Add ( item.Substring ( 0, item.IndexOf ( TYPE_PROP_KEY ) ), item.Split ( new char [] { TYPE_PROP_DEL, TYPE_PROP_KEY }, StringSplitOptions.RemoveEmptyEntries ).Skip ( 1 ).ToArray() );
                }
                else
                {
                    if ( Properties.ContainsKey( TYPE_PROP_KEY_DEF.ToString() ) )
                    {
                        throw new AppException ( "Command can't be contains two or more anonim properties", $"Use '{Command} --help' for get more info about command" );
                    }
                    Properties.Add ( TYPE_PROP_KEY_DEF.ToString (), item.Split ( new char [] { TYPE_PROP_DEL }, StringSplitOptions.RemoveEmptyEntries ) );
                }
            }
        }
    }
}

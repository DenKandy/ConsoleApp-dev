using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Commands
{
    interface IStatement
    {
        string Command { get; set; }

        List<char> Parameters { get; set; }

        Dictionary<string, string []> Properties { get; set; }

        string String { get; set; }

        void Parse ();
    }


}

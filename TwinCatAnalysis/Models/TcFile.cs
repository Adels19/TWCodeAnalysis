using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwinCatAnalysis.Models
{


    // .TcPOU/.TcGVL/.TcDUT — one code unit
    public class TcFile
    {

        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }

        public List<Variable> variables { get; set; } = new List<Variable>();


        public string DeclarationText { get; set; }
        public string ImplementationText { get; set; }



    }
}

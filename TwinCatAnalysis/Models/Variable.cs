using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwinCatAnalysis.Models
{
    public class Variable   
    {

        public string DefinitionSource { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string StartingValue { get; set; }
        public string Scope { get; set; }

        public int usageCount { get { return Usages.Count; } }


        //public string comment { get; set; }

        public List<VariableUsage> Usages { get; set; } = new List<VariableUsage>();

    }
    //public class MethodUsage
    //{
    //    public string Name { get; set; }
    //    public string Context { get; set; }
    //    public string Line { get; set; }

    //}
}

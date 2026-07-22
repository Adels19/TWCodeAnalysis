using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwinCatAnalysis.Models
{
    public struct Variable
    {
        public string DefinitionSource { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string StartingValue { get; set; }
        public string Scope { get; set; }


        //public string comment { get; set; }

        public List<VariableUsage> Usages { get; set; }


    }

    public class VariableUsage
    {
        public Variable Variable { get; set; }  // it stores the reference to the existing Variable
        public string UsageType{ get; set; }
        //Read
        //Write
        //MethodCall
        //FunctionArgument

        public string MethodName { get; set; }
    }
}

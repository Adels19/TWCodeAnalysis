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


        //public string comment { get; set; }

        public List<VariableUsage> Usages { get; set; } = new List<VariableUsage>();

    }



    public enum UsageType
    {
        Read,
        Write,
        ReadWrite
    }

    public enum UsageContext
    {
        Assignment,
        IfCondition,
        ElseIfCondition,
        IfBody,
        ElseBody,
        FunctionArgument,
        MethodCall,
        CaseSelector
    }

    public class VariableUsage
    {
        public Variable Variable { get; set; }  // it stores the reference to the existing Variable

        int usageCount = 0;
        public UsageType usageType { get; set; }
        public UsageContext usageContext { get; set; }

        public string LineNumber { get; set; } 


    }


    // FUNCTION BLOCK?
   

}

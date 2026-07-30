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



    public enum UsageType
    {
        Read,
        Write,
        Call
    }

    public enum UsageContext
    {
        Assignment,
        CaseCondition,
        FunctionArgument,
        FunctionalCall,
        IfCondition
    }

    public class VariableUsage
    {
        public Variable Variable { get; set; }  // it stores the reference to the existing Variable
        public UsageType usageType { get; set; }
        public UsageContext usageContext { get; set; }
        public int lineNumber { get; set; }
        public string CaseName { get; set; }


    }

    public class MethodUsage
    {
        public string Name { get; set; }
        public string Context { get; set; }
        public string Line { get; set; }

    }








}

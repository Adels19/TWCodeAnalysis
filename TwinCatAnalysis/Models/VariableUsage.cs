using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwinCatAnalysis.Models
{
    public class VariableUsage
    {
        #region Properties
        public Variable Variable { get; set; }  
        public UsageType usageType { get; set; }
        public UsageContext usageContext { get; set; }
        public int lineNumber { get; set; }
        public string CaseName { get; set; }

        #endregion  
    }

    #region Enums
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
    #endregion

}

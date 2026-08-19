using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TwinCatAnalysis.Models
{
    public class Variable   
    {
        #region Properties
        public string DefinitionSource { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string StartingValue { get; set; }
        public string Scope { get; set; }

        public int usageCount { get { return Usages.Count; } }


        // instance
        public bool isInstance { get; set; }
        public string InstanceDefinitionSource { get; set; }


        public List<VariableUsage> Usages { get; set; } = new List<VariableUsage>();

        #endregion
    }
}

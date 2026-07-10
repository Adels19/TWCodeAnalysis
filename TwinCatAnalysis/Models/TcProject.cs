using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwinCatAnalysis.Models
{
    public class TcProject
    {

        public string ProjectPath { get; set; }
        public string ProjectName { get; set; }

        public List<TcPlcProject> Projects { get; set; } = new List<TcPlcProject>();

    }
}

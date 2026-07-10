using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwinCatAnalysis.Models
{
    public class TcSolution
    {

        public string SolutionPath { get; set; }
        public List<TcProject> projects { get; set; }

        public TcSolution() 
        {
            projects = new List<TcProject>();
        }

    }
}

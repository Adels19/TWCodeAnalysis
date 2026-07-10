using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinCatAnalysis.Models;

namespace TwinCatAnalysis.Core
{
    public class ProjectLoader
    {
       
        public TcSolution Load(string path)
        {

            TcSolution solution = new TcSolution();

            solution.FilePath = path;
            solution.Load();

            return solution;

        }
       

    }
}

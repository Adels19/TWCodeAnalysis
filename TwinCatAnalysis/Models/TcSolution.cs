using Microsoft.Build.Construction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TwinCatAnalysis.Models
{

    // .sln — top-level entry point, has a path, list of TcProjects
    public class TcSolution
    {

        public string FilePath { get; set; }
        public List<TcProject> Projects { get; set; }  // stores the projects, for example Motion.tsproj, HMI.tsproj..

        public TcSolution() 
        {
            Projects = new List<TcProject>();
        }

        public void Load()  
        {
            var dir = Path.GetDirectoryName(FilePath);  // C:\\plc_project\\CNCPlasma\\CNCPlasma.sln"

            var x = SolutionFile.Parse(FilePath).ProjectsByGuid
                .Select(s => s.Value.AbsolutePath).Where(g => g.EndsWith(".tsproj") || g.EndsWith(".tspproj")).ToList();

            foreach (var tsprojPath in x)  
            {
                var project = new TcProject { FilePath = tsprojPath };  // "C:\\plc_project\\CNCPlasma\\CNCPlasma\\CNCPlasma.tsproj"
                project.Load(); 
                Projects.Add(project);  
            }
        }

        


        

    }
}

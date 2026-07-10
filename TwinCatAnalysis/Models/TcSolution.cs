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
            var dir = Path.GetDirectoryName(FilePath);  // the dir where the solution .sln is, not filename


            foreach (var tsprojPath in Directory.EnumerateFiles(dir, "*.tsproj", SearchOption.AllDirectories))  
            {
                var project = new TcProject { FilePath = tsprojPath };
                project.Load(); // let the .tsproj load everything inside itself (TcProject -> .plcproj)
                Projects.Add(project);  // adding in a list
            }
        }

        

        //string[] directories = Directory.GetDirectories(SolutionPath);

        

    }
}

using Microsoft.Build.Construction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TwinCatAnalysis.Models
{

    // .sln — top-level entry point, has a path, list of TcProjects
    public class TcSolution
    {

        #region Properties
        public string FilePath { get; set; }
        public List<TcProject> Projects { get; set; }  // example Motion.tsproj, HMI.tsproj..
        #endregion

        #region Constructor
        public TcSolution() 
        {
            Projects = new List<TcProject>();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Loads all TwinCAT projects from the solution file
        /// and initializes their PLC project data.
        /// </summary>
        public void Load()  
        {
            var projectPaths = SolutionFile.Parse(FilePath).ProjectsByGuid
                .Select(s => s.Value.AbsolutePath).Where(g => g.EndsWith(".tsproj") || g.EndsWith(".tspproj")).ToList();

            foreach (var tsprojPath in projectPaths)  
            {
                var project = new TcProject { FilePath = tsprojPath };  // "C:\\plc_project\\CNCPlasma\\CNCPlasma\\CNCPlasma.tsproj"
                project.Load(); 
                Projects.Add(project);  
            }
        }

        /// <summary>
        /// Collects and returns all variables from all PLC files
        /// across all projects in the solution.
        /// </summary>
        public List<Variable> GetAllVariables()
        {
            List<Variable> variables = new List<Variable>();

            foreach (var project in Projects)
            {
                foreach(var file in project.PlcProjects.AllFiles)
                {
                    foreach(var variable in file.Variables)    
                    {
                        variables.Add(variable);
                    }
                }
            }
            return variables;
        }

        #endregion
    }
}

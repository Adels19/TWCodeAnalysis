using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwinCatAnalysis.Models
{

    // .tsproj — has a path, list of TcPlcProjects
    public class TcProject
    {

        #region Properties
        public string FilePath { get; set; } 
        public TcPlcProject  PlcProjects { get; set; } = new TcPlcProject();
        #endregion

        #region Constructor
        public TcProject()
        {
            PlcProjects = new TcPlcProject();     
        }
        #endregion


        #region Methods
        /// <summary> 
        /// Loads the PLC project from the TwinCAT project directory. 
        /// </summary>
        public void Load()
        {
            var directoryPath = Path.GetDirectoryName(FilePath);  // "C:\\plc_project\\CNCPlasma\\CNCPlasma"


            foreach (var plcProjectPath in Directory.EnumerateFiles(directoryPath, "*.plcproj", SearchOption.AllDirectories))
            {
                var plcProject = new TcPlcProject 
                {
                    FilePath = plcProjectPath
                };  

                plcProject.Load();

                PlcProjects = plcProject;

                break;
            }
        }
        #endregion
    }
}

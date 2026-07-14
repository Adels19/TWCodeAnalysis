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

        public string name { get; set; } 
        public string FilePath { get; set; } 
        public TcPlcProject  PlcProjects { get; set; } = new TcPlcProject();
        // {TwinCatAnalysis.Models.TcPlcProject}    

        public TcProject()
        {
            PlcProjects = new TcPlcProject();     
        }



        public void Load()
        {
            var dir = Path.GetDirectoryName(FilePath);
            

            foreach(var plcprojPath in Directory.EnumerateFiles(dir, "*.plcproj", SearchOption.AllDirectories))
            {
                var plc = new TcPlcProject { FilePath = plcprojPath };
                plc.Load();
                PlcProjects = plc;
                break;
            }

        }



    }
}

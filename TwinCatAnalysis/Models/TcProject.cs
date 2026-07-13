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
        public List<TcPlcProject>  PlcProjects { get; set; } = new List<TcPlcProject>();
        // {TwinCatAnalysis.Models.TcPlcProject}    

        public TcProject()
        {
            PlcProjects = new List<TcPlcProject>();     
        }



        public void Load()
        {
            var dir = Path.GetDirectoryName(FilePath);
            

            foreach(var plcprojPath in Directory.EnumerateFiles(dir, "*.plcproj", SearchOption.AllDirectories))
            {
                var plc = new TcPlcProject { FilePath = plcprojPath };
                plc.Load();
                PlcProjects.Add(plc);
            }

        }



    }
}

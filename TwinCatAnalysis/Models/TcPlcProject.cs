using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwinCatAnalysis.Models
{

    // .plcproj — has a path, list of TcFiles (POU/GVL/DUT)
    public class TcPlcProject
    {

        public string name { get; set; }    
        public string FilePath { get; set; }   
        

        public List<TcFile> GvlFiles { get; set; } = new List<TcFile>();
        public List<TcFile> PouFiles { get; set; } = new List<TcFile>();
        public List<TcFile> DutFiles { get; set; } = new List<TcFile>();


        public TcPlcProject() 
        {
            GvlFiles = new List<TcFile>();
            PouFiles = new List<TcFile>();
            DutFiles = new List<TcFile>();
        }

        public void Load()
        {
          

        }



    }
}

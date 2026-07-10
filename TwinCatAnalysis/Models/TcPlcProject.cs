using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwinCatAnalysis.Models
{
    public class TcPlcProject
    {

        public string Name { get; set; }
        public string FilePath { get; set; }

        public List<TcFile> GvlFiles { get; set; } = new List<TcFile>();
        public List<TcFile> PouFiles { get; set; } = new List<TcFile>();
        public List<TcFile> DutFiles { get; set; } = new List<TcFile>();

    }
}

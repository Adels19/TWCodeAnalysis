using System;
using System.Collections.Generic;
using System.IO;
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
        

        public List<TcFile> allFiles { get; set; } = new List<TcFile>();
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
          
            var dir = Path.GetDirectoryName(FilePath);


            foreach(var  singleFile in Directory.EnumerateFiles(dir, "*", SearchOption.AllDirectories))
            {
                string extension = Path.GetExtension(singleFile);
                //string content = File.ReadAllText(singleFile);
                string FileName = Path.GetFileName(singleFile);

                TcFile file = new TcFile { FilePath = singleFile, FileName=FileName, FileType = extension };

                if(extension == ".TcPOU")
                {
                    PouFiles.Add(file);
                    file.LoadXML();

                }
                else if (extension == ".TcDUT")
                {
                    DutFiles.Add(file);
                }else if (extension == ".TcGVL")
                {
                    GvlFiles.Add(file);
                   
                }

            }

            //foreach (var singleFile in Directory.EnumerateFiles(dir, "*.TcPOU",SearchOption.AllDirectories))
            //{
            //    var pou = new TcFile {FilePath = singleFile};
            //    //pou.Load();
            //    PouFiles.Add(pou);

            //}

            //foreach (var singleFile in Directory.EnumerateFiles(dir, "*.TcDUT", SearchOption.AllDirectories))
            //{
            //    var dut = new TcFile { FilePath = singleFile };
            //    DutFiles.Add(dut);

            //}
            //foreach (var singleFile in Directory.EnumerateFiles(dir, "*.TcGVL", SearchOption.AllDirectories))
            //{
            //    var gvl = new TcFile { FilePath = singleFile };
            //    GvlFiles.Add(gvl);

            //}



        }

        


    }
}

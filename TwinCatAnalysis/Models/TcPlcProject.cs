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


            //foreach(var  singleFile in Directory.EnumerateFiles(dir, "*", SearchOption.AllDirectories))
            //{
            //    string extension = Path.GetExtension(singleFile);
            //    //string content = File.ReadAllText(singleFile);
            //    string FileName = Path.GetFileName(singleFile);


            //    if (extension == ".TcPOU")
            //    {
            //        TcFile file = new TcFile { FilePath = singleFile, FileName = FileName, FileType = extension };
            //        PouFiles.Add(file);
            //        file.LoadXML();

            //    }
            //    else if (extension == ".TcDUT")
            //    {
            //        TcFile file = new TcFile { FilePath = singleFile, FileName = FileName, FileType = extension };
            //        DutFiles.Add(file);
            //        //file.LoadXML();

            //    }
            //    else if (extension == ".TcGVL")
            //    {
            //        TcFile file = new TcFile { FilePath = singleFile, FileName = FileName, FileType = extension };
            //        GvlFiles.Add(file);
                   
            //    }

            //}

            
            foreach (var singleFile in Directory.EnumerateFiles(dir, "*.TcPOU", SearchOption.AllDirectories))
            {
                string extension = Path.GetExtension(singleFile);
                //string content = File.ReadAllText(singleFile);
                string FileName = Path.GetFileName(singleFile);

                TcFile file = new TcFile { FilePath = singleFile, FileName = FileName, FileType = extension };

                //pou.Load();
                file.LoadXML();
                PouFiles.Add(file);

            }

            foreach (var singleFile in Directory.EnumerateFiles(dir, "*.TcDUT", SearchOption.AllDirectories))
            {
                string extension = Path.GetExtension(singleFile);
                string FileName = Path.GetFileName(singleFile);

                TcFile file = new TcFile { FilePath = singleFile, FileName = FileName, FileType = extension };

                DutFiles.Add(file);

            }
            foreach (var singleFile in Directory.EnumerateFiles(dir, "*.TcGVL", SearchOption.AllDirectories))
            {
                string extension = Path.GetExtension(singleFile);
                string FileName = Path.GetFileName(singleFile);

                TcFile file = new TcFile { FilePath = singleFile, FileName = FileName, FileType = extension }; 

                GvlFiles.Add(file);

            }



        }

        


    }
}

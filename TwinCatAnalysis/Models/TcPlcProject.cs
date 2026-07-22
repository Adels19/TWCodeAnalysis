using Microsoft.Build.Framework.XamlTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TwinCatAnalysis.Models
{

    // .plcproj — has a path, list of TcFiles (POU/GVL/DUT)
    public class TcPlcProject
    {

        public string name { get; set; }    
        public string FilePath { get; set; }    // "C:\\plc_project\\CNCPlasma\\CNCPlasma\\CNCPlasma\\CNCPlasma.plcproj" -> Where is the PLC project file itself?
        public List<TcFile> allFiles { get; set; } = new List<TcFile>();



        public XDocument doc { get; set; }



        public TcPlcProject() 
        {
            allFiles = new List<TcFile>();
          
        }
        public void Load()
        {

            var projectDirectory = Path.GetDirectoryName(FilePath);

            doc = XDocument.Load(FilePath);
            var CompileParts = doc.Descendants().Where(x => x.Name.LocalName == "Compile");

            foreach (var x in CompileParts)
            {

                var hasInclude = x.Attribute("Include");    // {Include="DUTs\ST_Axis.TcDUT"}

                if (hasInclude != null)
                {
                    
                    string includePath = hasInclude.Value; // "DUTs\\ST_Axis.TcDUT"

                    string FileName = Path.GetFileName(includePath);    // "ST_Axis.TcDUT"
                    string FileExtension = Path.GetExtension(includePath);  // .TcDUT
                    string fullFilePath = Path.Combine(projectDirectory, includePath);  
                    // absoulte path to the file, combining the current dir with the file we found

                    TcFile file = new TcFile { FilePath = fullFilePath, FileName = FileName, FileType = FileExtension };

                    file.LoadXML();
                    allFiles.Add(file);

                    //string FilePath = Path.GetFullPath(includePath);    // "C:\\repos\\TWCodeAnalysis\\TwinCatAnalysis\\bin\\Debug\\DUTs\\ST_Axis.TcDUT"

                }
            }
        }
    }
}

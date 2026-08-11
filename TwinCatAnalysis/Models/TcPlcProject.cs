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
        #region Properties
        public string FilePath { get; set; }    
        public List<TcFile> AllFiles { get; set; } = new List<TcFile>();
        public XDocument Document { get; set; }

        #endregion

        #region Constructor

        public TcPlcProject() 
        {
            AllFiles = new List<TcFile>();
          
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the PLC project file, creates TcFile objects
        /// for all included files, and loads their XML data.
        /// </summary>
        public void Load()
        {

            var projectDirectory = Path.GetDirectoryName(FilePath);

            Document = XDocument.Load(FilePath);

            var compileParts = Document.Descendants().Where(x => x.Name.LocalName == "Compile");

            // Add all included files to the list
            foreach (var x in compileParts)
            {

                var includeAttribute = x.Attribute("Include");    // {Include="DUTs\ST_Axis.TcDUT"}

                if (includeAttribute != null)
                {
                    string includePath = includeAttribute.Value; 

                    string fileName = Path.GetFileName(includePath);   
                    string fileExtension = Path.GetExtension(includePath);

                    // absoulte path to the file, combining the current dir with the file we found
                    string fullFilePath = Path.Combine(projectDirectory, includePath);  

                    var file = new TcFile { 
                        FilePath = fullFilePath, 
                        FileName = fileName, 
                        FileType = fileExtension, 
                        Project = this 
                    };


                    AllFiles.Add(file);
                }
            }

            // Load XML data for all project files
            foreach (TcFile file in AllFiles)
            {
                file.LoadXML();
            }
        }

        #endregion
    }
}

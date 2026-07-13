using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;


namespace TwinCatAnalysis.Models
{


    // .TcPOU/.TcGVL/.TcDUT — one code unit
    public class TcFile
    {

        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }

        public string FunctionBlockName { get; set; }




        public XDocument doc { get; set; }

        //public string FileContent { get; set; }

        public List<Variable> variables { get; set; } = new List<Variable>();


        public string DeclarationText { get; set; }
        public string ImplementationText { get; set; }


        public void LoadXML()
        {
            doc = XDocument.Load(FilePath);

            if(FileType == ".TcPOU")
            {
                ExtractPOU();

            }else if (FileType == ".TcDUT")
            {
                ExtractDUT();
            }
            else if(FileType == ".TcGVL")
            {
                ExtractGVL();
            }
            
        }

        public void ExtractPOU()
        {
                var declaration = doc.Descendants().FirstOrDefault(x=>x.Name.LocalName == "Declaration");
                var implementation = doc.Descendants().FirstOrDefault(x=>x.Name.LocalName == "Implementation");
                if(declaration != null)
                {
                    DeclarationText = declaration.Value;
                }
                if (implementation != null)
                {
                    ImplementationText = implementation.Value;
                }
        }

        public void ExtractDUT()
        {

        }


        public void ExtractGVL()
        {

        }

        public void AnalyzeDeclaration()
        {

            string Type;
            string Name ="";
            string Scope;
            bool Comment;

            
            

                Match match = Regex.Match(DeclarationText, @"FUNCTION_BLOCK\s+(\w+)");

            if (match.Success)
            {
                FunctionBlockName = match.Groups[1].Value;
            }

            Variable variable = new Variable();
            variable.Name = Name;
            variables.Add(variable);
        }


    }

    
}

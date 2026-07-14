using System;
using System.Collections.Generic;
using System.IO;
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

        //public string FunctionBlockName { get; set; }




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
                     AnalyzeDeclaration();
                }
                if (implementation != null)
                {
                    ImplementationText = implementation.Value;
                }

        }

        public void ExtractDUT()
        {
            //var declaration = doc.Descendants().FirstOrDefault(x => x.Name.LocalName == "Declaration");

            //if(declaration != null)
            //{
            //    DeclarationText = declaration.Value;
            //    AnalyzeDeclaration();
            //}


        }


        public void ExtractGVL()
        {

        }

        public void AnalyzeDeclaration()
        {
            //string Comment;

            // Name : Type;
            // Name : Type := Value;
            // := ? 
            // value is optional

            StringReader reader = new StringReader(DeclarationText);

            string line;
            string currentScope = "";
            string scope = "";

            while ((line = reader.ReadLine()) != null)
            {

                line = line.Trim();

                if (line.StartsWith("VAR"))
                {
                    currentScope = line;
                    scope = line;
               
                }
                else if (line.Contains("END_VAR"))
                {
                    currentScope = "";
                }

                else if (currentScope != "")
                {

                    string pattern = @"^\s*(\w+)\s*:\s*(\w+)(?:\s*:=\s*(\w+))?\s*;";
                    Match match = Regex.Match(line, pattern);
                  
                    if (match.Success)
                    {
                        Variable variable = new Variable();

                        variable.Name = match.Groups[1].Value;
                        variable.Type = match.Groups[2].Value;
                        variable.Scope = scope;
                        variable.StartingValue = match.Groups[3].Value;
                        
                        

                        variable.DefinitionSource = Path.GetFileNameWithoutExtension(FileName);
                        variables.Add(variable);

                    }
                }

                
            }




            // TODO::
            // I AnalyzeDeclaration for Dut, dali nova funkcija da zacuvuva vo nova lista ili mala izmena na veke napisanata za pou
            // (oti vo dut ima struct namesto var, drugoto e  isto



            //Variable variable = new Variable();
            //variable.Name = name;
            //variable.Type = type;

            //variable.DefinitionSource = Path.GetFileNameWithoutExtension(FileName);//bez ekstenzijata
            //variables.Add(variable);
        }


    }

    
}

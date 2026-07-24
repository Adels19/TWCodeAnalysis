using DevExpress.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static DevExpress.Utils.MVVM.Internal.ILReader;


namespace TwinCatAnalysis.Models
{


   
    public class TcFile
    {

        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }



        public XDocument doc { get; set; }

        //public string FileContent { get; set; }

        public List<Variable> variables { get; set; } = new List<Variable>();

        public string DeclarationText { get; set; }
        public string ImplementationText { get; set; }


        public void LoadXML()
        {
            doc = XDocument.Load(FilePath); // "C:\\plc_project\\CNCPlasma\\CNCPlasma\\CNCPlasma\\POUs\\FB_Blinker.TcPOU"

            if (FileType == ".TcPOU")
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
                    AnalyzeImplementation();
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
        }






        public void AnalyzeImplementation()
        {

            StringReader reader = new StringReader(ImplementationText);
            string line;

            bool insideCase = false;     // use it properly
            string currentCaseLabel = "";



            while ((line = reader.ReadLine()) != null)
            {
                line = line.Trim();

                string expression = "";
                string CasePattern = @"CASE\s*(.+?)\s*OF";
                string caseLabel = @"^\d+\s*:";


                Match match = Regex.Match(line, CasePattern);


                if (line.StartsWith("CASE") && line.EndsWith("OF"))
                {
                    expression = match.Groups[1].Value;
                    extractExpression(expression);
                    insideCase = true;
                    continue;
                }


                if (line.StartsWith("END_CASE"))
                {
                    insideCase = false;
                    continue;
                }


                Match match1 = Regex.Match(line, caseLabel);
                if (match1.Success)
                {
                    currentCaseLabel = match1.Groups[1].Value;
                }

                if (insideCase && !match1.Success)
                {

                }

                   

             }
                    
               

                

            

        }



        public void extractExpression(string expression)
        {
            // GetState(iStateFB)  group1 - GetState, group2 - iStateFB
            string isFunction = @"^\s*(\w+)\s*\((.*?)\)\s*$";
            Match match = Regex.Match(expression, isFunction);

            VariableUsage usages = new VariableUsage();

            // what kind of CASE are possible? (in my example only with variable)
            if (match.Success)
            {
                string functionName = match.Groups[1].Value;
                string arguments = match.Groups[2].Value;

                if (string.IsNullOrWhiteSpace(arguments))
                {
                    // no arguments, only funct
                }
                else
                {
                    foreach (var v in variables)
                    {
                        if (v.Name == arguments)
                        {
                            usages.usageType = UsageType.Read;
                            usages.usageContext = UsageContext.FunctionArgument;

                            v.Usages.Add(usages);
                        }
                    }
                }
            }
            else
            {
                foreach (var v in variables)
                {
                    if(v.Name == expression)
                    {
                        usages.usageType = UsageType.Read;
                        usages.usageContext = UsageContext.CaseSelector;
                        v.Usages.Add(usages);   // this is not working.. :(
                    }
                }
            }
        }

    }
    
}

// TODO::


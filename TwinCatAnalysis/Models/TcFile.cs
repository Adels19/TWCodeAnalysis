using DevExpress.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
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

            }
            else if (FileType == ".TcDUT")
            {
                ExtractDUT();
            }
            else if (FileType == ".TcGVL")
            {
                ExtractGVL();
            }

        }

        public void ExtractPOU()
        {
            var declaration = doc.Descendants().FirstOrDefault(x => x.Name.LocalName == "Declaration");
            var implementation = doc.Descendants().FirstOrDefault(x => x.Name.LocalName == "Implementation");

            if (declaration != null)
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

            //bool insideCase = false;     // use it properly
            string currentContext = "";
            string CaseName = "";
            bool insideCase = false;

            // string THEN/ELSE - to know that is from IF expression


            // .Q, .IN, .ET, .PT... ??
            //  bBlinker := NOT bBlinker;   ??


            // after then: 
            // bBlinker := TRUE/FALSE;
            // bBlinker := 10/-10;
            // iState := iLastState/ NOT iState;
            // iCount := iCount + 1;
            // left side: write, assignment, right side: read, assignment



            while ((line = reader.ReadLine()) != null)
            {
                line = line.Trim();

                string IfElseCase = "";

                // line = CASE iState OF

                // CASE detected:
                if (line.StartsWith("CASE") && line.EndsWith("OF"))
                {
                    CaseName = line;
                    extractCaseExpression(line);
                    insideCase = true;
                }
                else if (line.StartsWith("END_CASE"))
                {
                    CaseName = "";
                    insideCase = false;
                }
                

                //if (insideCase && !match1.Success)
                //{
                //    // pattern for :=
                //    // pattern for IF, ELSE, THEN, AND, OR, END_IF (maybe function outside so we could use it in CASE and outside)
                //}


                // IF, ELSIF, ELSE
                if (line.StartsWith("IF") || line.StartsWith("ELSIF"))
                {

                    if (insideCase)
                    {
                        extract_IF_Expression(line,CaseName);
                    }



                }

                
                if (line.Contains(":="))
                {
                    int parenthesisDepth = 0;

                    for (int i = 0; i < line.Length; i++)
                    {
                        char ch = line[i];

                        if (ch == '(')
                        {
                            parenthesisDepth++;
                        }
                        else if (ch == ')')
                        {
                            parenthesisDepth--;
                        }

                        if (ch == ':' && i + 1 < line.Length && line[i + 1] == '=')
                        {
                            // FunctionArgument
                            if (parenthesisDepth > 0)
                            {
                                extract_Function_Expression(line);
                            }
                            // Assignment
                            else
                            {
                                extract_Assignment_Expression(line);
                            }
                        }
                    }
                } else if (line.Contains("()"))
                {
                    // for the rest functional call without :=, toest za -> PRG_Blinker();
                    extract_Function_Expression(line);

                }

                // fbTimer(...) -> fbTimer = function call, (...) = function argument




                // WHILE detected:
                if (line.StartsWith("WHILE"))
                {

                }
                // if IF ELSE outside the CASE
                // if not CASE or IF ELSE (smth else):
                // maybe outside function for variables like bBlinker := FALSE; fbTimer2.IN := FALSE; that could work in or out the CASE, IF, so I do not have to write it many times


            }


            // TODO:: 
            // Find all schemas for the variables. Make a function that u could use many times (optimized code).




        }

        // possible functions (functionCall or functionArgument)
        // x(IN := y)
        // x()
        // x(y)
        // x(y := 10)
        private void extract_Function_Expression(string line)
        {
            throw new NotImplementedException();
        }




        // possible assignments:
        // x := y     x - write, y - read / assignment
        // x := y()   x - write/assignment, y-read/functionalCall
        // x := y(a)  x - write/assignment, y-read/functionalCall, a-read/functionArgument
        // x.a := y   x.a - write/assignment
        private void extract_Assignment_Expression(string line)
        {
            throw new NotImplementedException();
        }





        public void extract_IF_Expression(string line, string caseName)
        {
            string CaseName = caseName;
            string IfPattern = @"^(?:IF|ELSIF)\s*(.+?)\s*THEN";

            Match matchIfPattern = Regex.Match(line, IfPattern);
            string condition = matchIfPattern.Groups[1].Value;
            // "bBlinkerInput = TRUE"


            // IF fbStart.Q = TRUE AND rLength <> 0 AND rWidth <> 0 AND (iLastState = 5 OR iLastState = 0) THEN
            // ELSIF fbStart.Q = TRUE AND rLength <> 0 AND rWidth <> 0 AND bXFor = TRUE THEN
            // iState:= 30;


            if (matchIfPattern.Success)
            {
                foreach(var variable in variables)
                {
                    if (condition.Contains(variable.Name))
                    {
                        VariableUsage usages = new VariableUsage();

                        //usages.Variable = variable; // ?
                        usages.usageType = UsageType.Read;
                        usages.usageContext = UsageContext.IfCondition;

                        // IF inside Case
                        if (CaseName != "")
                        {
                            usages.CaseName = CaseName;
                        }

                        variable.Usages.Add(usages);

                    }
                }
            }
            

        }

        public void extractCaseExpression(string line)
        {

            // pr. line = CASE iStateFB OF
            // CASE iStateFB(), iStateFB()
            // GetState(iStateFB)  group1 - GetState, group2 - iStateFB

            //string isFunction = @"^\s*(\w+)\s*\((.*?)\)\s*$";

            string CasePattern = @"CASE\s*(.+?)\s*OF";
            //string CaseName = line;

            Match matchCasePattern = Regex.Match(line, CasePattern);
            string expression = matchCasePattern.Groups[1].Value;



            if (matchCasePattern.Success)
            {
                VariableUsage usages = new VariableUsage();

                foreach (var variable in variables)
                {
                    if (expression.Contains(variable.Name))
                    {
                        //usages.Variable = variable; // ?
                        usages.usageType = UsageType.Read;
                        usages.usageContext = UsageContext.CaseCondition;
                    }
                    variable.Usages.Add(usages);
                }
                usages.CaseName = line;

            }

            // what kind of CASE are possible? (in my example only with variable)

        }



    }

}



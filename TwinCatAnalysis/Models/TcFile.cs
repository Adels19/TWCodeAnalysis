using DevExpress.Drawing.Internal.Fonts.Interop;
using DevExpress.Export.Xl;
using DevExpress.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Runtime.CompilerServices;
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

        #region Properties

        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }

        public XDocument Document { get; set; }

        public List<Variable> Variables { get; set; } = new List<Variable>();

        public string DeclarationText { get; set; }
        public string ImplementationText { get; set; }

        #endregion


        #region Analysis State
        private int CurrentLineNumber { get; set; } = 1;
        private string CurrentCase { get; set; } = "";
        #endregion


        #region XML Extraction
        public void LoadXML()
        {
            Document = XDocument.Load(FilePath); // "C:\\plc_project\\CNCPlasma\\CNCPlasma\\CNCPlasma\\POUs\\FB_Blinker.TcPOU"
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
        private void ExtractPOU()
        {
            var declaration = Document.Descendants().FirstOrDefault(x => x.Name.LocalName == "Declaration");
            var implementation = Document.Descendants().FirstOrDefault(x => x.Name.LocalName == "Implementation");

            var method = Document.Descendants().FirstOrDefault(x => x.Name.LocalName == "Method");

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
            //if (method != null)
            //{
            //    var methodName = method.Attribute("Name")?.Value;
            //    Method m = new Method();
            //    m.Name = methodName;

            //    Methods.Add(m);

            //}
        }
        private void ExtractDUT()
        {
            //var declaration = doc.Descendants().FirstOrDefault(x => x.Name.LocalName == "Declaration");

            //if(declaration != null)
            //{
            //    DeclarationText = declaration.Value;
            //    AnalyzeDeclaration();
            //}
        }
        private void ExtractGVL()
        {

        }
        #endregion


       


        #region Declaration Analysis
        private void AnalyzeDeclaration()
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
                        Variables.Add(variable);
                    }
                }
            }
        }
        #endregion

        #region Implementation Analysis
        private void AnalyzeImplementation()
        {

            StringReader reader = new StringReader(ImplementationText);
            string line;

            // string THEN/ELSE - to know that is from IF expression

            while ((line = reader.ReadLine()) != null)
            {
                line = line.Trim();

                bool IsComment = line.Contains("//") || line.Contains("*)") || line.Contains("*(");

                if (line.StartsWith("CASE") && !IsComment)
                {
                    CurrentCase = line;
                    AnalyzeCaseStatement(line);
                }
                else if (line.StartsWith("END_CASE") && !IsComment)
                {
                    CurrentCase = "";
                }

                // IF, ELSIF, ELSE
                else if ((line.StartsWith("IF") || line.StartsWith("ELSIF")) && !IsComment)
                {
                    AnalyzeIfStatement(line);
                }

                // Functions, Arguments...
                else if ((line.Contains(":=") || line.Contains("=>")) && !IsComment)
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

                        if ((ch == ':' || ch == '=') && i + 1 < line.Length && (line[i + 1] == '=' || line[i + 1] == '>'))
                        {
                            // FunctionArgument
                            if (parenthesisDepth > 0)
                            {
                                // fbStop(CLK:= bStop);
                                // fbStart(bBlinker => bBlinker);
                                AnalyzeFunctionCall(line);
                                break;
                            }
                            // Assignment
                            else
                            {
                                // "fbTimer1.IN := TRUE;"

                                AnalyzeAssignment(line);
                                break;
                            }
                        }
                    }
                }
                CurrentLineNumber++;
            }
        }
        #endregion



        #region Statement Analysis

        // public void FindBiggestNumber()
        // private int _counter;

        private void AnalyzeCaseStatement(string line)
        {
            // pr. line = CASE iStateFB OF, different then CASE iState OF
            // CASE iStateFB(), iStateFB()
            // GetState(iStateFB)  group1 - GetState, group2 - iStateFB

            //string isFunction = @"^\s*(\w+)\s*\((.*?)\)\s*$";

            string CasePattern = @"CASE\s*(.+?)\s*OF";
            Match matchCasePattern = Regex.Match(line, CasePattern);
            string expression = matchCasePattern.Groups[1].Value;

            if (matchCasePattern.Success)
            {
                VariableUsage usages = new VariableUsage();
              
                    if (expression.Contains("."))
                    {
                        expression = expression.Split('.')[0];
                    }

                    Variable variable = FindVariable(expression);

                    if (variable != null)
                    {
                        usages.Variable = variable; // do i need this?
                        usages.usageType = UsageType.Read;
                        usages.usageContext = UsageContext.CaseCondition;
                        usages.CaseName = line;
                        usages.lineNumber = CurrentLineNumber;

                        variable.Usages.Add(usages);
                    }
            }
            // other types of CASE? (in my example only with variable)
        }
        private void AnalyzeIfStatement(string line)
        {
            string IfPattern = @"^(?:IF|ELSIF)\s*(.+?)\s*THEN";

            Match matchIfPattern = Regex.Match(line, IfPattern);
            string condition = matchIfPattern.Groups[1].Value;

            // IF fbStart.Q = TRUE AND rLength <> 0 AND rWidth <> 0 AND (iLastState = 5 OR iLastState = 0) THEN
            // ELSIF fbStart.Q = TRUE AND rLength <> 0 AND rWidth <> 0 AND bXFor = TRUE THEN
            // iState:= 30;

            // so we get ( fbStart.Q = TRUE AND rLength <> 0 AND rWidth <> 0 AND (iLastState = 5 OR iLastState = 0) )

            List<string> elements = new List<string>();
            string patternForElements = @"\b[A-Za-z_]\w*(?:\.[A-Za-z_]\w*)*\b";         //

            MatchCollection matchesElements = Regex.Matches(condition, patternForElements);

            foreach (Match match in matchesElements)
            {
                string element = match.Value;

                if (element.Contains("."))
                {
                    element = element.Split('.')[0];
                }

                elements.Add(element);
            }


            foreach(string element in elements)
            {
                Variable variable = FindVariable(element);

                if (variable != null)
                {
                    VariableUsage usages = new VariableUsage();
                    usages.usageType = UsageType.Read;
                    usages.usageContext = UsageContext.IfCondition;
                    usages.lineNumber = CurrentLineNumber;

                    // IF inside Case
                    if (CurrentCase != "")
                    {
                        usages.CaseName = CurrentCase;
                    }
                    variable.Usages.Add(usages);
                }

            }

            //foreach (var variable in Variables)
            //{
            //    for (int i = 0; i < listOfElements.Count; i++)
            //    {
            //        if (listOfElements[i].Contains("."))
            //        {
            //            listOfElements[i] = listOfElements[i].Split('.')[0];
            //        }

            //        VariableUsage usages = new VariableUsage();

            //        if (listOfElements[i].Contains(variable.Name))
            //        {
            //            //usages.Variable = variable; // ?
            //            usages.usageType = UsageType.Read;
            //            usages.usageContext = UsageContext.IfCondition;
            //            usages.lineNumber = CurrentLineNumber;

            //            // IF inside Case
            //            if (CurrentCase != "")
            //            {
            //                usages.CaseName = CurrentCase;
            //            }
            //            variable.Usages.Add(usages);
            //        }
            //    }
            //}
        }
        private void AnalyzeAssignment(string line)
        {

            string assignPattern = @"^\s*(.*?)\s*:=\s*(.*?)\s*;?$";

            Match match = Regex.Match(line, assignPattern);

            if (match.Success)
            {
                string leftSide = match.Groups[1].Value.Trim();
                string rightSide = match.Groups[2].Value.Trim();


                // LEFT:
                if (leftSide.Contains("."))
                {
                    leftSide = leftSide.Split('.')[0];
                }

                foreach (var variable in Variables)
                {
                    if (leftSide == variable.Name)
                    {
                        VariableUsage usages = new VariableUsage();

                        usages.Variable = variable; // do i need this?
                        usages.usageType = UsageType.Write;
                        usages.usageContext = UsageContext.Assignment;

                        usages.lineNumber = CurrentLineNumber;
                        if (CurrentCase != "")
                        {
                            usages.CaseName = CurrentCase;
                        }
                        variable.Usages.Add(usages);
                        break;
                    }
                }


                // RIGHT:
                List<string> rightSides = new List<string>();
                string assignmentPattern = @"\b[A-Za-z_]\w*(?:\.[A-Za-z_]\w*)*\b";

                MatchCollection matches = Regex.Matches(rightSide, assignmentPattern);

                foreach (Match m in matches)
                {
                    rightSides.Add(m.Value);
                }
                // for rightSide: (iCount - 1)*rWidth + (iCount - 1)*10 - MotorX.MC_ReadActualPosition.Position
                // rightSides list will be: iCount, rWidth, iCount, rWidth, MotorX.MC_ReadActualPosition.Position

                foreach (var variable in Variables)
                {
                    for (int i = 0; i < rightSides.Count; i++)
                    {
                        if (rightSides[i].Contains("."))
                        {
                            rightSides[i] = rightSides[i].Split('.')[0];
                        }

                        VariableUsage usages = new VariableUsage();

                        if (rightSides[i] == variable.Name)
                        {
                            //usages.Variable = variable; // ?
                            usages.usageType = UsageType.Read;
                            usages.usageContext = UsageContext.Assignment;
                            usages.lineNumber = CurrentLineNumber;

                            // IF inside Case
                            if (CurrentCase != "")
                            {
                                usages.CaseName = CurrentCase;
                            }
                            variable.Usages.Add(usages);
                        }
                    }
                }
            }

        }
        private void AnalyzeFunctionCall(string line)
        {

            // MotorX.MC_Power(Axis:= MotorX.Motor, Enable:= bPowerEnable, Enable_Positive:= bPowerEnable, Enable_Negative:= bPowerEnable, Override:= 100.0);
            // MotorX → FunctionalCall


            // example line:
            // MotorX.MC_Power(Axis:= MotorX.Motor, Enable:= bPowerEnable, Enable_Positive:= bPowerEnable, Enable_Negative:= bPowerEnable, Override:= 100.0);

            string pattern = @"^\s*([A-Za-z_]\w*(?:\.[A-Za-z_]\w*)*)\s*\((.*)\)\s*;?$";
            Match match = Regex.Match(line, pattern);


            if (match.Success)
            {
                string functionCall = match.Groups[1].Value;
                string arguments = match.Groups[2].Value;

                // left side CALL - example:
                // MotorX.MC_Power
                if (functionCall.Contains("."))
                {
                    functionCall = functionCall.Split('.')[0];
                }
                foreach (var variable in Variables)
                {
                    if (functionCall == variable.Name)
                    {
                        VariableUsage usages = new VariableUsage();

                        usages.Variable = variable;
                        usages.usageType = UsageType.Read;
                        usages.usageContext = UsageContext.FunctionalCall;
                        //usages.CaseName = line;
                        usages.lineNumber = CurrentLineNumber;

                        variable.Usages.Add(usages);
                        break;
                    }
                }


                // right side ARGUMENTS - example:
                // Axis:= MotorX.Motor, Enable:= bPowerEnable, Enable_Positive:= bPowerEnable, Enable_Negative:= bPowerEnable, Override:= 100.0
                List<string> rightSides = new List<string>();
                // MotorX.Motor, bPowerEnable..

                string argPattern = @"(:=|=>)\s*([^,]+)";
                MatchCollection matches = Regex.Matches(arguments, argPattern);

                foreach (Match m in matches)
                {
                    string op = m.Groups[1].Value;
                    string rightSide = m.Groups[2].Value.Trim();
                    // MotorX.Motor, bPowerEnable

                    if (rightSide.Contains("."))
                    {
                        rightSide = rightSide.Split('.')[0];
                    }

                    foreach (var variable in Variables)
                    {
                        if (rightSide == variable.Name)
                        {
                            VariableUsage usages = new VariableUsage();

                            usages.usageContext = UsageContext.FunctionArgument;
                            usages.lineNumber = CurrentLineNumber;

                            if (op == ":=")
                                usages.usageType = UsageType.Read;
                            else
                                usages.usageType = UsageType.Write;

                            if (CurrentCase != "")
                            {
                                usages.CaseName = CurrentCase;
                            }

                            variable.Usages.Add(usages);
                            break;
                        }
                    }
                }
            }
        }

        #endregion



        #region Private Variables

        #endregion


        #region Public Variables
        #endregion


        private Variable FindVariable(string name)
        {
            return Variables.FirstOrDefault(v => v.Name == name);
        }


    }
}


// TODO:
// i could make one function for the code that is repeating
// i only have case name, should i have something else to remember, like statement or idk?
// usages.Variable = variable; // yes or no?
// PRG_CNCPlasma(); -> method call, i already have class, how to handle?



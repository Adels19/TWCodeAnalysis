using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;


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
        public TcPlcProject Project { get; set; }
        public List<CodeDependency> Dependencies { get; set; } = new List<CodeDependency>();

        #endregion


        #region Nested Classes
        public class CodeDependency
        {
            public TcFile From { get; set; }
            public TcFile To { get; set; }
            public string DependencyType { get; set; }
        }

        #endregion


        #region Analysis State
        private int CurrentLineNumber { get; set; } = 1;
        private string CurrentCase { get; set; } = "";
        private bool InsideBlockComment {get; set;}

        #endregion


        #region XML Extraction

        /// <summary>
        /// Loads the XML document and extracts the relevant data
        /// based on the file type.
        /// </summary>
        public void LoadXML()
        {
            Document = XDocument.Load(FilePath); 

            if (FileType == ".TcPOU")
            {
                ExtractPOU();
            }
            //else if (FileType == ".TcDUT")
            //{
            //    ExtractDUT();
            //}
            //else if (FileType == ".TcGVL")
            //{
            //    ExtractGVL();
            //}
        }

        /// <summary>
        /// Extracts declaration and implementation sections 
        /// from a TwinCAT POU file. 
        /// </summary>
        private void ExtractPOU()
        {
            var declaration = Document
                .Descendants()
                .FirstOrDefault(x => x.Name.LocalName == "Declaration");

            var implementation = Document
                .Descendants()
                .FirstOrDefault(x => x.Name.LocalName == "Implementation");


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


        #region Line Preprocessing

        /// <summary> 
        /// Removes block and single-line comments from a line of code. 
        /// </summary> 
        /// <param name="line"> The line of code to process.</param> 
        /// <returns>The line with comments removed.</returns>
        private string RemoveComments(string line)
        {
            if (InsideBlockComment)
            {
                int end = line.IndexOf("*)");

                if (end == -1)
                    return string.Empty;

                line = line.Substring(end + 2);

                InsideBlockComment = false;
            }


            while (true)
            {
                int start = line.IndexOf("(*");

                if (start == -1)
                    break;

                int end = line.IndexOf("*)", start + 2);

                if (end == -1)
                {
                    InsideBlockComment = true;
                    line = line.Substring(0, start);
                    break;
                }

                line = line.Remove(start, end - start + 2);
            }

            int singleLineComment = line.IndexOf("//");

            if (singleLineComment >= 0)
            {
                line = line.Substring(0, singleLineComment);
            }

            return line.Trim();
        }

        #endregion


        #region Declaration Analysis

        /// <summary>
        /// Analyzes the declaration section of the file
        /// and creates variables from the declared entries.
        /// </summary>
        private void AnalyzeDeclaration()
        {
   
            var reader = new StringReader(DeclarationText);

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
                // inside scope
                else if (currentScope != "")
                {
                    string pattern = @"^\s*(\w+)\s*:\s*(\w+)(?:\s*:=\s*(\w+))?\s*;";
                    Match match = Regex.Match(line, pattern);

                    if (match.Success)
                    {
                        var variable = new Variable();

                        variable.Name = match.Groups[1].Value;
                        variable.Type = match.Groups[2].Value; 
                        variable.Scope = scope;
                        variable.StartingValue = match.Groups[3].Value;

                        // If the type matches a file name, the variable is an instance.
                        TcFile definitionFile = FindFile(variable.Type);

                        if (definitionFile != null)
                        {
                            variable.isInstance = true;
                            variable.InstanceDefinitionSource = definitionFile.FileName;
                        }

                        variable.DefinitionSource = 
                            Path.GetFileNameWithoutExtension(FileName);

                        Variables.Add(variable);
                    }
                }
            }
        }
        #endregion


        #region Implementation Analysis
        /// <summary>
        /// Analyzes the implementation section of the file
        /// and identifies variable usages and dependencies.
        /// </summary>
        private void AnalyzeImplementation()
        {
            CurrentLineNumber = 0;

            var reader = new StringReader(ImplementationText);
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                CurrentLineNumber++;
                // RemoveComments is a function that removes every type of a comment in the line
                line = RemoveComments(line);

                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                if (line.StartsWith("CASE"))
                {
                    CurrentCase = line;
                    AnalyzeCaseStatement(line);
                }
                else if (line.StartsWith("END_CASE"))
                {
                    CurrentCase = "";
                }

                // IF, ELSIF, ELSE
                else if ((line.StartsWith("IF") || line.StartsWith("ELSIF")))
                {
                    AnalyzeIfStatement(line);
                }

                // Functions or Arguments
                else if ((line.Contains(":=") || line.Contains("=>")))
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
                                // example: fbStop(CLK:= bStop);  or  fbStart(bBlinker => bBlinker);
                                AnalyzeFunctionCall(line);
                                break;
                            }
                            // Assignment
                            else
                            {
                                // example: "fbTimer1.IN := TRUE;"
                                AnalyzeAssignment(line);
                                break;
                            }
                        }
                    }
                }
                else if (CurrentCase != "" && Regex.IsMatch(line, @"^\d+\s*:")) // especially for case numbers (0:, 5:...)
                {
                    continue;
                }
                else if (line.Equals("END_IF"))
                {
                    continue;
                }
                else 
                {
                    AnalyzePoucCall(line);
                }
            }
        }


        /// <summary>
        /// Analyzes a program or function call and creates
        /// a dependency between the current file and the called file.
        /// </summary>
        private void AnalyzePoucCall(string line)
        {
            string pattern = @"^\s*([A-Za-z_]\w*)\s*\(\s*\)\s*;?$";

            Match match = Regex.Match(line, pattern);

            if (match.Success)
            {
                string name = match.Groups[1].Value;
                TcFile calledFile = FindFile(name);

                if (calledFile != null)
                {
                    var dependency = new CodeDependency();
                    dependency.From = this;
                    dependency.To = calledFile;
                    dependency.DependencyType = "ProgramCall";

                    Dependencies.Add(dependency);
                }
            }
        }

        #endregion


        #region Statement Analysis
        /// <summary>
        /// Analyzes a CASE statement and records the variable
        /// used as its condition.
        /// </summary>
        private void AnalyzeCaseStatement(string line)
        {
            string casePattern = @"CASE\s*(.+?)\s*OF";
            Match matchCasePattern = Regex.Match(line, casePattern);


            if (matchCasePattern.Success)
            {
                string expression = matchCasePattern.Groups[1].Value; // expression between CASE OF

              
                // We need only the variable, not the member
                if (expression.Contains("."))
                {
                    expression = expression.Split('.')[0];
                }

                Variable variable = FindVariable(expression);

                if (variable != null)
                {
                    var usage = new VariableUsage 
                    { 
                        Variable = variable, 
                        usageType = UsageType.Read, 
                        usageContext = UsageContext.CaseCondition, 
                        CaseName = line, 
                        lineNumber = CurrentLineNumber 
                    };

                    variable.Usages.Add(usage);
                }
            }
        }

        /// <summary>
        /// Analyzes an IF or ELSIF condition and records
        /// all variables used by the condition.
        /// </summary>
        private void AnalyzeIfStatement(string line)
        {
            string ifPattern = @"^(?:IF|ELSIF)\s*(.+?)\s*THEN";

            Match matchIfPattern = Regex.Match(line, ifPattern);

            if (matchIfPattern.Success)
            {
                string condition = matchIfPattern.Groups[1].Value;  // condition is whole condition between IF and THEN

                // IF fbStart.Q = TRUE AND rLength <> 0 AND rWidth <> 0 AND (iLastState = 5 OR iLastState = 0) THEN
                // ELSIF fbStart.Q = TRUE AND rLength <> 0 AND rWidth <> 0 AND bXFor = TRUE THEN
                // iState:= 30;

                // so we get ( fbStart.Q = TRUE AND rLength <> 0 AND rWidth <> 0 AND (iLastState = 5 OR iLastState = 0) )

                var elements = new List<string>();

                string elementPattern = @"\b[A-Za-z_]\w*(?:\.[A-Za-z_]\w*)*\b";         //

                MatchCollection matchesElements = Regex.Matches(condition, elementPattern);

                // if it is complex IF condition, we take whole line and add all elements(variables) in a list, below we handle their usage
                foreach (Match match in matchesElements)
                {
                    string element = match.Value;

                    if (element.Contains("."))  // we want only the variable, not the member
                    {
                        element = element.Split('.')[0];
                    }

                    elements.Add(element);
                }


                foreach (string element in elements)
                {
                    Variable variable = FindVariable(element);

                    if (variable != null)
                    {
                        var usage = new VariableUsage
                        {
                            Variable = variable,
                            usageType = UsageType.Read,
                            usageContext = UsageContext.IfCondition,
                            lineNumber = CurrentLineNumber,
                        };

                        // IF inside Case
                        if (CurrentCase != "")
                        {
                            usage.CaseName = CurrentCase;
                        }

                        variable.Usages.Add(usage);
                    }
                }
            }
        }

        /// <summary>
        /// Analyzes an assignment and records the variable being written
        /// and the variables being read from the right-hand side.
        /// </summary>
        private void AnalyzeAssignment(string line)
        {
            string assignmentPattern = @"^\s*(.*?)\s*:=\s*(.*?)\s*;?$";

            Match match = Regex.Match(line, assignmentPattern);

            if (match.Success)
            {
                string leftSide = match.Groups[1].Value.Trim();
                string rightSide = match.Groups[2].Value.Trim();


                // Left side
                if (leftSide.Contains("."))
                {
                    leftSide = leftSide.Split('.')[0];
                }

                Variable variable = FindVariable(leftSide);

                if (variable != null)
                {
                    var usage = new VariableUsage();

                    usage.Variable = variable;
                    usage.usageType = UsageType.Write;
                    usage.usageContext = UsageContext.Assignment;

                    usage.lineNumber = CurrentLineNumber;

                    if (CurrentCase != "")
                    {
                        usage.CaseName = CurrentCase;
                    }

                    variable.Usages.Add(usage);
                }


                // Right side
                // it could be complex, so we make list
                var rightSides = new List<string>();

                string rightSidePattern = @"\b[A-Za-z_]\w*(?:\.[A-Za-z_]\w*)*\b";

                MatchCollection matches = Regex.Matches(rightSide, rightSidePattern);

                foreach (Match matchRightSide in matches)
                {
                    rightSides.Add(matchRightSide.Value);
                }
                // for rightSide: (iCount - 1)*rWidth + (iCount - 1)*10 - MotorX.MC_ReadActualPosition.Position
                // rightSides list will be: iCount, rWidth, iCount, rWidth, MotorX.MC_ReadActualPosition.Position
                
                
                 for (int i = 0; i < rightSides.Count; i++)
                 {
                     if (rightSides[i].Contains("."))
                     {
                         rightSides[i] = rightSides[i].Split('.')[0];
                     }

                    Variable rightSideVariable = FindVariable(rightSides[i]);

                     if (rightSideVariable != null)
                     {
                        var usage = new VariableUsage();

                        usage.Variable = rightSideVariable;
                        usage.usageType = UsageType.Read;
                        usage.usageContext = UsageContext.Assignment;
                        usage.lineNumber = CurrentLineNumber;

                         if (CurrentCase != "")
                         {
                            usage.CaseName = CurrentCase;
                         }

                        rightSideVariable.Usages.Add(usage);
                     }
                 }
            }
        }

        /// <summary>
        /// Analyzes a function call and records the function variable
        /// and variables passed through its arguments.
        /// </summary>
        private void AnalyzeFunctionCall(string line)
        {

            // MotorX.MC_Power(Axis:= MotorX.Motor, Enable:= bPowerEnable, Enable_Positive:= bPowerEnable, Enable_Negative:= bPowerEnable, Override:= 100.0);
            // MotorX -> FunctionalCall, MC_Power -> Member, arguments are inside ()

            string pattern = @"^\s*([A-Za-z_]\w*(?:\.[A-Za-z_]\w*)*)\s*\((.*)\)\s*;?$";

            Match match = Regex.Match(line, pattern);


            if (match.Success)
            {
                string functionCall = match.Groups[1].Value;
                string arguments = match.Groups[2].Value;

                // Left side CALL
                if (functionCall.Contains("."))
                {
                    functionCall = functionCall.Split('.')[0];
                }

                Variable variable = FindVariable(functionCall);

                if (variable != null)
                {
                    var usage = new VariableUsage();

                    usage.Variable = variable;
                    usage.usageType = UsageType.Read;
                    usage.usageContext = UsageContext.FunctionalCall;
                    usage.lineNumber = CurrentLineNumber;

                    variable.Usages.Add(usage);
                }



                // Right side ARGUMENTS
                string argumentPattern = @"(:=|=>)\s*([^,]+)";
                MatchCollection matches = Regex.Matches(arguments, argumentPattern);

                foreach (Match argumentMatch in matches)
                {
                    string operation = argumentMatch.Groups[1].Value;
                    string rightSide = argumentMatch.Groups[2].Value.Trim();
                    // MotorX.Motor, bPowerEnable

                    if (rightSide.Contains("."))
                    {
                        rightSide = rightSide.Split('.')[0];
                    }

                    Variable argumentVariable = FindVariable(rightSide);


                    if (argumentVariable != null)
                    {
                        var usage = new VariableUsage();

                        usage.Variable = argumentVariable;
                        usage.usageContext = UsageContext.FunctionArgument;
                        usage.lineNumber = CurrentLineNumber;

                        if (operation == ":=")
                            usage.usageType = UsageType.Read;
                        else
                            usage.usageType = UsageType.Write;

                        if (CurrentCase != "")
                        {
                            usage.CaseName = CurrentCase;
                        }

                        argumentVariable.Usages.Add(usage);
                    }
                }
            }
        }

        #endregion


        #region Search Methods
        /// <summary>
        /// Finds the variable with the specified name in the current file.
        /// </summary>
        /// <param name="name">The name of the variable to find.</param>
        /// <returns>The matching variable, or null if it cannot be found.</returns>
        private Variable FindVariable(string name)
        {
            return Variables.FirstOrDefault(v => v.Name == name);
        }

        /// <summary>
        /// Finds the file that defines the specified type.
        /// </summary>
        /// <param name="type">The type or file name to search for.</param>
        /// <returns>The matching file, or null if it cannot be found.</returns>
        private TcFile FindFile(string type)
        {
            return Project.AllFiles.FirstOrDefault(f =>
                Path.GetFileNameWithoutExtension(f.FileName) == type);
        }
        #endregion

      
    }
}






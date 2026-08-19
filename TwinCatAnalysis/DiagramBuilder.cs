using DevExpress.Diagram.Core;
using DevExpress.Utils;
using DevExpress.XtraDiagram;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TwinCatAnalysis.Models;


namespace TwinCatAnalysis
{
    public class DiagramBuilder
    {
        #region Properties

        private DiagramControl diagramControl;

        private readonly Panel detailsPanel;

        private List<TcFile> currentFiles = new List<TcFile>();

        private TcFile selectedFile;

        private readonly Dictionary<TcFile, DiagramShape> fileNodes = new Dictionary<TcFile, DiagramShape>();

        private readonly HashSet<TcFile> expandedFiles = new HashSet<TcFile>();

        #endregion

        #region color palette

        private readonly Color fileColor = Color.FromArgb(52, 73, 94);

        private readonly  Color variableColor = Color.FromArgb(142, 68, 173);

        private readonly Color instanceColor = Color.FromArgb(230, 126, 34);

        private readonly Color dependencyColor = Color.FromArgb(211, 84, 0);

        #endregion

        #region Constructor
        public DiagramBuilder(DiagramControl diagramControl, Panel detailsPanel=null)
        {
            this.diagramControl = diagramControl;
            this.detailsPanel = detailsPanel;

            if(detailsPanel != null)
            {
                ClearDetailsPanel();
            }
        }
        #endregion


        #region TAB 2
        public void Build(List<Variable> variables)
        {
            
            diagramControl.Items.Clear();

            var groups = variables.GroupBy(v => v.DefinitionSource);


            // Horizontal positions
            const int fileX = 50;
            const int variableX = 600;
            const int usageStartX = 900;

            // Vertical starting position
            int y = 50;
            const int variableSpacing = 150;

            const int usageSpacing = 200;

            var lineNumbers = variables.SelectMany(v => v.Usages).Select(u => u.lineNumber).Distinct().OrderBy(line => line).ToList();

            Dictionary<int, int> lineXPositions = new Dictionary<int, int>();

            for(int i=0; i< lineNumbers.Count; i++)
            {
                lineXPositions[lineNumbers[i]] = usageStartX + i * usageSpacing;
            }


            foreach (var group in groups)
            {
                List<Variable> fileVariables = group.ToList();

                if (fileVariables.Count == 0)
                    continue;

                int firstVariableY = y + 70;
                int lastVariableY = firstVariableY + (fileVariables.Count - 1) * variableSpacing;

                int fileY = (firstVariableY + lastVariableY) / 2 - 35;

                // file node
                DiagramShape fileShape = new DiagramShape
                {
                    Content = group.Key,
                    Shape = BasicShapes.RoundedRectangle,
                    Width = 200,
                    Height = 80,
                    Position = new PointFloat(fileX, fileY)
                };

                fileShape.Appearance.BackColor = fileColor; 
                fileShape.Appearance.ForeColor = Color.White;

                fileShape.Appearance.BorderColor = Color.FromArgb(35, 50, 65);
                fileShape.Appearance.BorderSize = 2;

                fileShape.Appearance.Font = new Font("Segoe UI", 11, FontStyle.Bold);

                diagramControl.Items.Add(fileShape);


                // variables
                for (int i = 0; i < fileVariables.Count; i++)
                {
                    Variable variable = fileVariables[i];
                    int variableY = firstVariableY + i * variableSpacing;

                    Color nodeColor = variable.isInstance ? instanceColor : variableColor;
                    string content = variable.isInstance ? $"{variable.Name} : {variable.Type}" : variable.Name;

                    // variable node
                    DiagramShape variableShape = new DiagramShape
                    {
                        Content = content,
                        Shape = BasicShapes.RoundedRectangle,
                        Width = 170,
                        Height = 80,
                        Position = new PointFloat(variableX, variableY)
                    };

                    variableShape.Appearance.BackColor = nodeColor;
                    variableShape.Appearance.ForeColor = Color.White;

                    variableShape.Appearance.BorderColor = Color.FromArgb(90, 90, 90);

                    variableShape.Appearance.BorderSize = 2;

                    variableShape.Appearance.Font = new Font("Segoe UI", 10, FontStyle.Regular);

                    diagramControl.Items.Add(variableShape);

                    AddVariableConnector(fileShape, variableShape);


                    var usagesByLine = variable.Usages.GroupBy(u => u.lineNumber).OrderBy(g => g.Key);

                    DiagramShape previousUsage = null;

                    foreach (var usageGroup in usagesByLine)
                    {

                        int lineNumber = usageGroup.Key;
                        int usageX = lineXPositions[lineNumber];

                        DiagramShape usageShape = CreateUsageNode(usageGroup.ToList(), usageX, variableY);

                        diagramControl.Items.Add(usageShape);


                        if (previousUsage == null)
                        {
                            AddUsageConnector(variableShape, usageShape);
                        }
                        else
                        {
                            AddUsageConnector(previousUsage, usageShape);
                        }

                        previousUsage = usageShape;

                    }
                }
                y = lastVariableY + variableSpacing + 50;
            }
            diagramControl.FitToDrawing();
        }

        private void AddVariableConnector(DiagramShape from, DiagramShape to)
        {
            DiagramConnector connector = new DiagramConnector
                {
                    BeginItem = from,
                    EndItem = to,

                    Type = ConnectorType.Straight,

                    EndArrow = ArrowDescriptions.FilledDot
                };


            connector.Appearance.ForeColor = Color.FromArgb(120, 120, 120);

            connector.Appearance.BorderSize = 1;


            diagramControl.Items.Add(connector);
        }

        private DiagramShape CreateUsageNode(List<VariableUsage> usages, int x, int y)
        {

            Color usageColor = Color.Gray;

            bool hasRead = usages.Any(u => u.usageType == UsageType.Read);
            bool hasWrite = usages.Any(u => u.usageType == UsageType.Write);

            if (hasRead && hasWrite)
            {
                usageColor = Color.FromArgb(255, 193, 7);
                usageColor.GetBrightness();
            }
            else if (hasWrite)
            {
                usageColor = Color.FromArgb(244, 67, 54);
            }
            else if (hasRead)
            {
                usageColor = Color.FromArgb(76, 175, 80);

            }
            else
            {
                usageColor = Color.Gray;
            }

            string content = $"line: {usages[0].lineNumber}\n";

            foreach(var usage in usages)
            {
                content += $"{usage.usageType} - " + $"{usage.usageContext}\n";
            }

            content = content.TrimEnd();

            int height = 60 + usages.Count * 25;

            DiagramShape usageShape = new DiagramShape
            {
                Content = content,

                Shape = BasicShapes.RoundedRectangle,

                Width = 170,
                Height = height,

                Position = new PointFloat(x, y)
            };


            usageShape.Appearance.ForeColor = Color.FromArgb(50, 50, 50);

            usageShape.Appearance.BackColor = Color.FromArgb(245, 247, 249);

            usageShape.Appearance.BorderColor = usageColor;

            usageShape.Appearance.BorderSize = 2;

            usageShape.Appearance.Font = new Font("Segoe UI", 9, FontStyle.Regular);

            return usageShape;
        }

        private void AddUsageConnector(DiagramShape from, DiagramShape to)
        {
            DiagramConnector connector = new DiagramConnector
                {
                    BeginItem = from,
                    EndItem = to,

                    Type = ConnectorType.Straight,

                    EndArrow = ArrowDescriptions.FilledDot
                };


            connector.Appearance.ForeColor = Color.FromArgb(120,120,120);

            connector.Appearance.BorderSize = 1;


            diagramControl.Items.Add(connector);
        }

        #endregion


        #region TAB 3
        public void BuildDependencies(List<TcFile> files)
        {
            // Save the files currently displayed in the diagram.
            // We need this later when the user selects/deselects a file.
            currentFiles = files;

            diagramControl.Items.Clear();

            fileNodes.Clear();

            // create all file nodes + dependencies
            CreateFileNodes(files);
            CreateDependencyConnectors(files);

            // BuildDependencies() is also called after clicking file, so we have to check if the file still exist
            if (selectedFile != null &&
                files.Contains(selectedFile))
            {
                ShowFileDetails(selectedFile);
            }
            else
            {
                selectedFile = null;
                ClearDetailsPanel();
            }
        }

        // create file nodes
        private void CreateFileNodes(List<TcFile> files)
        {
            const int startX = 100;
            const int startY = 80;

            const int horizontalSpace = 320;
            const int verticalSpace = 150;

            const int rowsPerColumn = 4;

            int column = 0;
            int row = 0;

            foreach (TcFile file in files)
            {
                DiagramShape fileNode = CreateFileNode(file);
                fileNode.Position = new PointFloat(startX + column * horizontalSpace, startY + row * verticalSpace);

                diagramControl.Items.Add(fileNode);

                // save the connection between the actual TcFile, later: which file was clicked?
                fileNodes[file] = fileNode;

                row++;

                if (row >= rowsPerColumn)
                {
                    row = 0;
                    column++;
                }
            }
        }


        // Create a file node
        private DiagramShape CreateFileNode(TcFile file)
        {
            bool selected = selectedFile == file;

            string arrow =
                selected ? "▼ " : "▶ ";

            DiagramShape node =
                new DiagramShape
                {
                    Content = arrow + file.FileName,
                    Shape = BasicShapes.RoundedRectangle,
                    Width = 230,
                    Height = 70
                };

            node.Appearance.BackColor = fileColor;

            node.Appearance.ForeColor = Color.White;

            node.Appearance.BorderColor =
                selected
                    ? Color.FromArgb(241, 196, 15)
                    : Color.FromArgb(35, 50, 65);

            node.Appearance.BorderSize =
                selected ? 3 : 2;

            node.Appearance.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            return node;
        }

        // create dependency connectors
        private void CreateDependencyConnectors(List<TcFile> files)
        {
            foreach (TcFile file in files)
            {
                if (!fileNodes.TryGetValue(file, out DiagramShape fromNode))
                {
                    continue;
                }

                foreach (TcFile.CodeDependency dependency in file.Dependencies)
                {
                    if (!fileNodes.TryGetValue(dependency.To, out DiagramShape toNode))
                    {
                        continue;
                    }

                    AddDependencyConnector(fromNode, toNode);
                }
            }
        }


        // add dependency connector
        private void AddDependencyConnector(DiagramShape from, DiagramShape to)
        {
            DiagramConnector connector = new DiagramConnector
                {
                    BeginItem = from,
                    EndItem = to,
                    Type = ConnectorType.Curved,
                    EndArrow = ArrowDescriptions.ClosedDiamond
                };

            connector.Appearance.ForeColor = dependencyColor;

            connector.Appearance.BorderSize = 2;

            diagramControl.Items.Add(connector);
        }


        // handle mouse click
        public void HandleClick(Point location)
        {
            // which diagram item is under the mouse.
            DiagramItem item = diagramControl.CalcHitItem(location);

            if (item == null)
                return;

            foreach (var pair in fileNodes)
            {
                if (pair.Value == item)
                {
                    // pair.Key is the actual TcFile object.
                    SelectFile(pair.Key);
                    return;
                }
            }
        }


        // select / deselect file
        public void SelectFile(TcFile file)
        {
            if (selectedFile == file)
            {
                selectedFile = null;

                ClearDetailsPanel();
                // rebuild the diagram if it is deselected
                BuildDependencies(currentFiles);

                return;
            }

            selectedFile = file;

            ShowFileDetails(file);

            BuildDependencies(currentFiles);
        }


        // show selected file details
        private void ShowFileDetails(TcFile file)
        {
            detailsPanel.Controls.Clear();

            detailsPanel.AutoScroll = true;

            // Main title
            Label title = new Label();

            title.Text = "Selected: " + file.FileName;

            title.Font =new Font("Segoe UI", 11, FontStyle.Bold);

            title.AutoSize = true;

            title.Location = new Point(15, 10);

            detailsPanel.Controls.Add(title);


            // counters:
            int instanceCount = file.Variables.Count(v => v.isInstance);
            int variableCount =file.Variables.Count(v => !v.isInstance);

            
            // instances:
            Label instancesTitle = new Label();

            instancesTitle.Text = $"Instances ({instanceCount})";

            instancesTitle.Font =new Font("Segoe UI", 9, FontStyle.Bold);

            instancesTitle.AutoSize = true;

            instancesTitle.Location = new Point(15, 40);

            detailsPanel.Controls.Add(instancesTitle);

            int instanceY = 65;

            foreach (Variable variable in file.Variables.Where(v => v.isInstance))
            {
                Label label = CreateVariableLabel(variable, instanceColor);

                label.Location =new Point(25, instanceY);

                detailsPanel.Controls.Add(label);

                instanceY += 25;
            }


            // variables:
            int variableStartX = 300;

            Label variablesTitle = new Label();

            variablesTitle.Text = $"Variables ({variableCount})";

            variablesTitle.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            variablesTitle.AutoSize = true;

            variablesTitle.Location = new Point(variableStartX, 40);

            detailsPanel.Controls.Add(variablesTitle);

            int variableY = 65;

            foreach (Variable variable in file.Variables.Where(v => !v.isInstance))
            {
                Label label = CreateVariableLabel(variable, variableColor);

                label.Location = new Point(variableStartX + 10, variableY);

                detailsPanel.Controls.Add(label);

                variableY += 25;
            }

            detailsPanel.AutoScrollMinSize = new Size(0, Math.Max(instanceY, variableY) + 20);
        }


        // create variable label:
        private Label CreateVariableLabel(Variable variable, Color color)
        {
            Label label = new Label();

            label.Text = $"{variable.Name} : {variable.Type}";

            label.AutoSize = true;

            label.Font = new Font("Segoe UI", 9, FontStyle.Regular);

            label.ForeColor = color;

            return label;
        }


        // clear details panel
        private void ClearDetailsPanel()
        {
            detailsPanel.Controls.Clear();

            Label label = new Label();

            label.Text = "Select a file in the diagram";

            label.Font = new Font("Segoe UI", 10, FontStyle.Italic);

            label.ForeColor = Color.Gray;

            label.AutoSize = true;

            label.Location = new Point(15, 15);

            detailsPanel.Controls.Add(label);
        }
        #endregion

    }
}

using DevExpress.Diagram.Core;
using DevExpress.Utils;
using DevExpress.XtraDiagram;
using DevExpress.XtraEditors.Filtering;
using DevExpress.XtraGrid.Views.Layout;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TwinCatAnalysis.Models;
using static DevExpress.Data.Mask.Internal.RegExMaskMath.CloudOfInts;



namespace TwinCatAnalysis
{
    public class DiagramBuilder
    {
        private DiagramControl diagramControl;

        private List<TcFile> currentFiles;

        // connects data object to its visual object
        private Dictionary<TcFile, DiagramShape> fileNodes = new Dictionary<TcFile, DiagramShape>();

        // stores currently expanded files
        private HashSet<TcFile> expandedFiles = new HashSet<TcFile>();

        public DiagramBuilder(DiagramControl diagramControl)
        {
            this.diagramControl = diagramControl;
        }

        public void Build(List<Variable> variables)
        {
            
            diagramControl.Items.Clear();

            var groups = variables.GroupBy(v => v.DefinitionSource);

            
            int x = 50;
            int y = 50;


            foreach (var group in groups)
            {
                DiagramShape fileShape = new DiagramShape();

                fileShape.Shape = BasicFlowchartShapes.Database;
                fileShape.Content = group.Key;

                fileShape.Appearance.BackColor = Color.FromArgb(52, 73, 94);  
                fileShape.Appearance.ForeColor = Color.White;

                fileShape.Appearance.BorderColor = Color.FromArgb(35, 50, 65);
                fileShape.Appearance.BorderSize = 2;

                fileShape.Appearance.Font =
                    new Font("Segoe UI", 11, FontStyle.Bold);

                fileShape.Position = new PointFloat(x, y);
                fileShape.Width = 180;
                fileShape.Height = 50;

                diagramControl.Items.Add(fileShape);

                y += 70;


                foreach (var variable in group)
                {
                    DiagramShape varShape = new DiagramShape(); 

                    varShape.Content = variable.Name;
                    varShape.Width = 150;
                    varShape.Height = 50;
                    varShape.Position = new PointFloat(300, y);
                    varShape.Shape = BasicShapes.Ellipse;

                    varShape.Appearance.BackColor = Color.White;
                    varShape.Appearance.ForeColor = Color.Black;

                    varShape.Appearance.BorderColor = Color.SteelBlue;

                    varShape.Appearance.BorderSize = 2;

                    varShape.Appearance.Font =
                        new Font("Segoe UI", 10, FontStyle.Bold);

                    diagramControl.Items.Add(varShape);


                    DiagramConnector fileConnector = new DiagramConnector();

                    fileConnector.BeginItem = fileShape;
                    fileConnector.EndItem = varShape;


                    diagramControl.Items.Add(fileConnector);
                    

                    int usageX = 550;

                    foreach (var usage in variable.Usages)
                    {
                        DiagramShape usageShape = new DiagramShape();

                        usageShape.Content =
                            $"{usage.usageType}\n" +
                            $"{usage.usageContext}\n" +
                            "line: " +
                            $"{usage.lineNumber}";

                        usageShape.Width = 170;
                        usageShape.Height = 80;
                        usageShape.Shape = BasicFlowchartShapes.Process;



                        switch (usage.usageType)
                        {
                            case UsageType.Read:
                                usageShape.Appearance.BackColor = Color.FromArgb(76, 175, 80);
                                usageShape.Appearance.ForeColor = Color.White;
                                break;

                            case UsageType.Write:
                                usageShape.Appearance.BackColor = Color.FromArgb(244, 67, 54);
                                usageShape.Appearance.ForeColor = Color.White;
                                break;

                            case UsageType.Call:
                                usageShape.Appearance.BackColor = Color.FromArgb(33, 150, 243);
                                usageShape.Appearance.ForeColor = Color.White;
                                break;
                        }

                        usageShape.Position = new PointFloat(usageX, y);
                        usageShape.Appearance.BorderSize = 1;


                        diagramControl.Items.Add(usageShape);

                        DiagramConnector usageConnector = new DiagramConnector();

                        usageConnector.BeginItem = varShape;
                        usageConnector.EndItem = usageShape;
                        usageConnector.EndArrow = ArrowDescriptions.FilledDot;


                        diagramControl.Items.Add(usageConnector);

                        usageX += 220;
                    }

                    y += 150;
                }

            }
            diagramControl.FitToDrawing();
        }

        public void BuildDependencies(List<TcFile> files)
        {
            currentFiles = files;

            diagramControl.Items.Clear();

            fileNodes.Clear();

            var variableNodes = new Dictionary<Variable, DiagramShape>();
            var definitionNodes = new Dictionary<TcFile, DiagramShape>();

            // Layout
            int fileX = 50;
            int variableX = 1400;
            int definitionX = 2600;

            int startY = 50;
            int fileSpacing = 100;
            int variableSpacing = 80;

            // Colors
            Color fileColor = Color.FromArgb(52, 73, 94);
            Color variableColor = Color.FromArgb(142, 68, 173);
            Color instanceColor = Color.FromArgb(230, 126, 34);
            Color definitionColor = Color.FromArgb(39, 125, 161);


            // FILES
            int fileY = startY;

            foreach (TcFile file in files)
            {
                // is this file currently expanded?
                bool expanded = expandedFiles.Contains(file);
                //string text = expanded ? $"{file.FileName}" : $"{file.FileName}";

                var node = CreateNode(file.FileName, fileColor, 280, 70);

                node.Position = new PointFloat(fileX, fileY);

                diagramControl.Items.Add(node);

                fileNodes[file] = node;

                fileY += fileSpacing;
            }


            // VARIABLES
            foreach (TcFile file in files)
            {
                // do not create variables if file is collapsed
                if (!expandedFiles.Contains(file))
                    continue;
                // does this file exist as a key in fileNodes
                if (!fileNodes.TryGetValue(file, out var fileNode))
                    continue;

                int variableY = (int)fileNode.Position.Y;

                foreach (Variable variable in file.Variables)
                {
                    Color color = variable.isInstance ? instanceColor : variableColor;

                    var node = CreateNode($"{variable.Name} : {variable.Type}", color, 220, 55);

                    node.Position = new PointFloat(variableX, variableY);

                    diagramControl.Items.Add(node);

                    variableNodes[variable] = node;

                    // File -> Variable
                    AddConnector(fileNode, node);

                    variableY += variableSpacing;
                }
            }


            // DEFINITION SOURCES
            var definitionFiles = files
                .SelectMany(f => f.Variables)
                .Where(v =>
                    v.isInstance &&
                    !string.IsNullOrEmpty(v.InstanceDefinitionSource))
                .Select(v =>
                    files.FirstOrDefault(f =>
                        f.FileName == v.InstanceDefinitionSource))
                .Where(f => f != null)
                .Distinct()
                .ToList();

            int definitionY = startY;

            foreach (TcFile file in definitionFiles)
            {
                // Only show definition if at least one
                // file using it is expanded
                bool shouldShow = files.Any(f =>
                    expandedFiles.Contains(f) &&
                    f.Variables.Any(v =>
                        v.isInstance &&
                        v.InstanceDefinitionSource == file.FileName));

                if (!shouldShow)
                    continue;

                var node = CreateNode(file.FileName, definitionColor, 280, 70);

                node.Position = new PointFloat(definitionX, definitionY);

                diagramControl.Items.Add(node);

                definitionNodes[file] = node;

                definitionY += fileSpacing;
            }


            // INSTANCE -> DEFINITION SOURCE
            foreach (TcFile file in files)
            {
                if (!expandedFiles.Contains(file))
                    continue;

                foreach (Variable variable in file.Variables)
                {
                    if (!variable.isInstance)
                        continue;

                    if (string.IsNullOrEmpty(variable.InstanceDefinitionSource))
                        continue;

                    if (!variableNodes.TryGetValue(variable, out var variableNode))
                        continue;

                    TcFile definitionFile = files.FirstOrDefault(f =>
                        f.FileName == variable.InstanceDefinitionSource);

                    if (definitionFile == null)
                        continue;

                    if (!definitionNodes.TryGetValue(
                        definitionFile,
                        out var definitionNode))
                        continue;

                    AddConnector(variableNode, definitionNode, ArrowDescriptions.ClosedDiamond);
                }
            }

            // FILE DEPENDENCIES
            foreach (TcFile file in files)
            {
                foreach (TcFile.CodeDependency dependency in file.Dependencies)
                {
                    if (!fileNodes.TryGetValue(
                        dependency.From,
                        out var fromNode))
                        continue;

                    if (!fileNodes.TryGetValue(
                        dependency.To,
                        out var toNode))
                        continue;

                    AddConnector(fromNode, toNode, ArrowDescriptions.FilledDoubleArrow);
                }
            }

            // LAYOUT
            diagramControl.FitToDrawing();
        }

        private DiagramShape CreateNode(string text, Color color, int width, int height)
        {
            var node = new DiagramShape
            {
                Content = text,
                Shape = BasicShapes.RoundedRectangle,
                Width = width,
                Height = height
            };

            node.Appearance.BackColor = color;
            node.Appearance.ForeColor = Color.White;
            node.Appearance.BorderSize = 2;
            node.Appearance.Font = new Font(
                "Segoe UI",
                9F,
                FontStyle.Bold);

            return node;
        }

        private void AddConnector(DiagramItem begin, DiagramItem end, ArrowDescription endArrow = null)
        {
            var connector = new DiagramConnector
            {
                BeginItem = begin,
                EndItem = end,
                Type = ConnectorType.Curved,
                EndArrow = endArrow
            };

            diagramControl.Items.Add(connector);
        }

        public void HandleClick(Point location)
        {
            DiagramItem item =
                diagramControl.CalcHitItem(location);

            if (item == null)
                return;

            foreach (var pair in fileNodes)
            {
                if (pair.Value == item)
                {
                    ToggleFile(pair.Key);
                    return;
                }
            }
        }
        public void ToggleFile(TcFile file)
        {
            if (expandedFiles.Contains(file))
                expandedFiles.Remove(file);
            else
                expandedFiles.Add(file);

            BuildDependencies(currentFiles);
        }
    }
}

// TODO::
// expand the given file, instnace definition source more good looking
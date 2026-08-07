using DevExpress.Diagram.Core;
using DevExpress.Utils;
using DevExpress.XtraDiagram;
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

        public DiagramBuilder(DiagramControl diagramControl)
        {
            this.diagramControl = diagramControl;
        }


        public void Build(List<Variable> variables)
        {
            diagramControl.Items.Clear();

            int x = 50;
            int y = 50;

            foreach (var variable in variables)
            {
                DiagramShape shape = new DiagramShape();

                shape.Content = variable.Name;
                shape.Position = new PointFloat(x, y);
                shape.Width = 150;
                shape.Height = 50;

                diagramControl.Items.Add(shape);

                y += 70;
            }

            //diagramControl.FitToDrawing();
        }

       // TODO:
       // MAKE GOOD LOOKING DIAGRAM CONTROL, especially the properties - margins, space etc..

        public void BuildDependencies(List<Variable> variables)
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
            //internal void BuildFile(string definitionSource, List<Variable> variables)
            //{
            //    diagramControl.Items.Clear();

            //    DiagramContainer container = new DiagramContainer();

            //    container.Header = definitionSource;
            //    container.ShowHeader = true;

            //    container.Position = new PointFloat(50, 50);
            //    container.Width = 500;

            //    diagramControl.Items.Add(container);

            //    var fileVariables = variables
            //        .Where(v => v.DefinitionSource == definitionSource)
            //        .ToList();

            //    int y = 100;

            //    container.Height = 120 + (fileVariables.Count * 70);

            //    diagramControl.Items.Add(container);

            //    foreach (var variable in fileVariables)
            //    {
            //        DiagramShape shape = new DiagramShape();

            //        shape.Content = variable.Name;

            //        shape.Position = new PointFloat(100, y);

            //        shape.Width = 150;
            //        shape.Height = 50;

            //        container.Items.Add(shape);

            //        y += 70;
            //    }
            //    diagramControl.FitToDrawing();
            //}

        }
    }
}
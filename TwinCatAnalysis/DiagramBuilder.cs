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


        internal void BuildFile(string definitionSource, List<Variable> variables)
        {
            diagramControl.Items.Clear();

            DiagramContainer container = new DiagramContainer();

            container.Header = definitionSource;
            container.ShowHeader = true;

            container.Position = new PointFloat(50, 50);
            container.Width = 500;

            diagramControl.Items.Add(container);

            var fileVariables = variables
                .Where(v => v.DefinitionSource == definitionSource)
                .ToList();

            int y = 100;

            container.Height = 120 + (fileVariables.Count * 70);

            diagramControl.Items.Add(container);

            foreach (var variable in fileVariables)
            {
                DiagramShape shape = new DiagramShape();

                shape.Content = variable.Name;

                shape.Position = new PointFloat(100, y);

                shape.Width = 150;
                shape.Height = 50;

                container.Items.Add(shape);

                y += 70;
            }
            diagramControl.FitToDrawing();
        }

        //internal void BuildFile(string definitionSource)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
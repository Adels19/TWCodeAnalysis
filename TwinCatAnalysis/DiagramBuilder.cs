using DevExpress.XtraDiagram;
using System.Collections.Generic;
using System.Drawing;
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

            foreach (var variable in variables)
            {
                DiagramShape shape = new DiagramShape();

                shape.DataContext = variable.Name;
                shape.Size = new SizeF(100, 50);

                diagramControl.Items.Add(shape);

            }

        }
    }
}
using DevExpress.Utils;
using DevExpress.XtraDiagram;
using DevExpress.XtraVerticalGrid.Events;
using Microsoft.Build.Construction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwinCatAnalysis.Core;
using TwinCatAnalysis.Models;


namespace TwinCatAnalysis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            gridControl1.DataSource = UiVariables;

            diagramBuilder = new DiagramBuilder(diagramControl);
        }

        // we save here the path so later we will set and send to other classes

        public string projectPath = "";
        public List<Variable> UiVariables { get; set; } = new List<Variable>();

        private DiagramBuilder diagramBuilder;

        private void btnOpenProject_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            
            var res = openFileDialog.ShowDialog();

            if (res == DialogResult.OK)
            {
                projectPath = openFileDialog.FileName;

                ProjectLoader loader = new ProjectLoader();

                TcSolution solution = loader.Load(projectPath);

                UiVariables = solution.GetAllVariables();

                gridControl1.DataSource = UiVariables;

                // gridView1 features
                gridView1.Columns["Name"].Caption = "Variable Name";
                gridView1.Columns["Type"].Caption = "Data Type";
                gridView1.Columns["Scope"].Caption = "Variable Scope";

                gridView1.OptionsView.ShowAutoFilterRow = true;
                gridView1.OptionsView.ShowGroupPanel = true;


                //tabNavigationPage1.PageText = "Variables";
                //tabNavigationPage2.PageText = "Dependency Graph";

                //diagramBuilder.BuildFile();

            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Variable selectedVariable = gridView1.GetFocusedRow() as Variable;

            if (selectedVariable != null)
            {
                diagramBuilder.BuildFile(selectedVariable.DefinitionSource, UiVariables);
            }
        }
    }
}

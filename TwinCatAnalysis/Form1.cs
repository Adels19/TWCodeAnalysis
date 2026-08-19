using System;
using System.Collections.Generic;
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

            dependencyDiagramBuilder = new DiagramBuilder(diagramControl1, panelFileDetails);

            diagramControl1.MouseClick += DiagramControl_MouseClick;
        }

        public string projectPath = "";
        public List<Variable> UiVariables { get; set; } = new List<Variable>();

        private DiagramBuilder diagramBuilder;
        private DiagramBuilder dependencyDiagramBuilder;
        private TcSolution solution;

        private void btnOpenProject_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            
            var res = openFileDialog.ShowDialog();

            if (res == DialogResult.OK)
            {
                projectPath = openFileDialog.FileName;      // CNCPlasma.sln 

                ProjectLoader loader = new ProjectLoader();

                solution = loader.Load(projectPath);

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
                diagramBuilder.Build(UiVariables);

                //dependencyDiagramBuilder.BuildDependencies(solution.Projects[0].PlcProjects.AllFiles);
            }
        }


        private void DiagramControl_MouseClick(object sender, MouseEventArgs e)
        {
            dependencyDiagramBuilder.HandleClick(e.Location);
        }

    }
}

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

        }

        // we save here the path so later we will set and send to other classes

        public string projectPath = "";

        public List<Variable> UiVariables { get; set; } = new List<Variable>();


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
                //gridView1.OptionsView.ShowGroupPanel = true;
            }


            // TODO::
            // learn more about DevExpress GridControl
            // handle this situation better, refresh constantly


            //gridControl1.Refresh();



        }
    }
}

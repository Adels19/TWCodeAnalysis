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

        public List<Variable> UiVariables= new List<Variable>();


        private void btnOpenProject_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "Select a TwinCAT Project |*.plcproj";
            //openFileDialog.InitialDirectory = @"C:\";
            var res = openFileDialog.ShowDialog();

            if (res == DialogResult.OK)
            {
                projectPath = openFileDialog.FileName;

               ProjectLoader loader = new ProjectLoader();

                TcSolution solution = loader.Load(projectPath);

                UiVariables = solution.GetAllVariables();
                gridControl1.DataSource = UiVariables;
            }


            //gridControl1.Refresh();


            //TcSolution solution = new TcSolution();
            //solution.FilePath = projectPath;
            //solution.Load();

        }
    }
}

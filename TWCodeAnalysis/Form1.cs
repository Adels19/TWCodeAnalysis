using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TWCodeAnalysis
{
    public partial class Form1 : Form   // partial - form1.css + fprm1.designer.css
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sende, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();

            var results = dialog.FileName;

            if (results.EndsWith(".exe"))
            {
                MessageBox.Show("Valid EXE file");
            }

            //Match res = Regex.Match(results, "[.exe]");


        }
        
    }

    public class TcProject
    {
        public void LoadPlcProject(string path)
        {
            //// logic

            //data = //new stuff'
        }

        private List<string> data;
    }
}

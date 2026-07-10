using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;



namespace TWCodeAnalysis
{
    public partial class Learning : Form
    {

        private string currentFile;
        //private string newFile;
        public Learning()
        {
            InitializeComponent();

        }


        private void open_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();   

            //FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            var res = openFileDialog.ShowDialog();      // returns ok or !ok

            if (res == DialogResult.OK)
            {
                currentFile = openFileDialog.FileName;      // stored  C:\Documents\file.xml
                LoadTitles();
            }
        }

        private void LoadTitles()
        {
            functions fn = new functions(); 

            List<string> titles = fn.ReadXml(currentFile);  // called the function of the class, because it returns list, we made list and send xml
            // also now is connected with the function the ListBox action

            view_items.Items.Clear();


            //foreach (string title in titles)    // one by one
            //{
            //    view_items.Items.Add(title);    // adding in the form
            //}
            
            //or:
            
            //refresh
            view_items.DataSource = null;   // Optional: clears previous binding
            view_items.DataSource = titles;

        }

       

        // rich text box for better representation
        private void titles_SelectedIndexChanged(object sender, EventArgs e)
        {
            functions fn = new functions(); 

            string details = fn.GetDetails(currentFile, view_items.SelectedIndex);  // sending the filename and index of selected item to function

            richTextBox1.Text = details;    // puts on the screen the result
                  
        }

    }
}




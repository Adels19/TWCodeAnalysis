using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace XmlPractice2
{
    public partial class Form1 : Form

    {


        public List<Person> People { get; set; }


        public Form1()
        {
            People = GetPeople();
            InitializeComponent();
            
        }

        private List<Person> GetPeople()
        {
            var list = new List<Person>();
            list.Add(new Person()
            {
                PersonId = 1,
                Name = "Person 1",
                Surname = "Surname 1",
                Profession = "Profession 1"
            });
            list.Add(new Person()
            {
                PersonId = 2,
                Name = "Person 2",
                Surname = "Surname 2",
                Profession = "Profession 2"
            });
            list.Add(new Person()
            {
                PersonId = 3,
                Name = "Person 3",
                Surname = "Surname 3",
                Profession = "Profession 3"
            });

            return list;
        }


        // default, initializers
        private void Form1_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("welcome");
            lblName.Text = "Name: ";
            lblAge.Text = "Age: ";
            lblSelection.Visible = false;

            string[] vehicle = { "Car", "MC", "Bus", "Bike" };
            comboVehicle.Items.AddRange(vehicle);

            lblParking.Visible = false;
            gbPark.Enabled = false;

            tabControlParkingSystem.SelectedTab = tabPagePark;

            lblshowInfo.Visible = false;
            lblBookInfo.Visible = false;


            var people = this.People;
            dataGridView1.DataSource = people;
            dataGridView1.Columns["PersonId"].Visible = false;


        }


        string fileName = "";

        private List<string> titles = new List<string>();

        private void btn_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();

            var res = ofd.ShowDialog();

            if (res == DialogResult.OK)
            {
                fileName = ofd.FileName;

                loadTitles();

            }

        }

        public void loadTitles()
        {
            Functions fn = new Functions();

            titles = fn.GetTitles(fileName);

            //list.Items.Clear();

            //// refresh
            //list.DataSource = null;
            list.DataSource = titles;

        }

        


        private void calculate_Click(object sender, EventArgs e)
        {
            button1.Text = 10.ToString();
            button2.Text = 6.ToString();

        }

        private void display_info_Click(object sender, EventArgs e)
        {
            lblName.Text = "Name: " + "Adela";
            lblAge.Text = "Age: " + "22";

            error.Visible = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            string outputMessage = "";

            // read input from the text box
            string name = tbxName.Text;

            if (!string.IsNullOrEmpty(name))
            {
                outputMessage = $"Information received!\n" + $"{name}";
                lblSubmittedInformation.Text = outputMessage;
                lblSubmittedInformation.Visible = true;
            }
            else
            {
                MessageBox.Show("Invalid Name");
            }



        }

        private void RadioButtonSelection_CheckedChanged(object sender, EventArgs e)
        {
            if (radioVisa.Checked)
            {
                lblSelection.Text = radioVisa.Text;
            } else if (radioCash.Checked)
            {
                lblSelection.Text = radioCash.Text;
            }
            lblSelection.Visible = true;
        }

        private void btnPark_Click(object sender, EventArgs e)
        {
            string regNumber = tgRegNum.Text;

            bool regNumbOk = !string.IsNullOrEmpty(regNumber);


            if (comboVehicle.SelectedItem != null && regNumbOk)
            {
                string type = comboVehicle.SelectedItem.ToString();

                lblParking.Text = regNumber + " -- " + type;
                lblParking.Visible = true;


            }


        }

        private void btnPayParking_Click(object sender, EventArgs e)
        {
            gbPark.Enabled = true;
        }

        private void list_SelectedIndexChanged(object sender, EventArgs e)
        {

            Functions fn = new Functions();

            string details = fn.GetDetails(fileName, list.SelectedIndex);

            lblshowInfo.Text = details;

            lblshowInfo.Visible = true;
            lblBookInfo.Visible = true;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var selectedPerson = dataGridView1.SelectedRows[0].DataBoundItem as Person;
                textBoxPersonId.Text = selectedPerson.PersonId.ToString();
                textBoxFirstName.Text = selectedPerson.Name.ToString();
                textBoxLastName.Text = selectedPerson.Surname.ToString();
                textBoxProfession.Text = selectedPerson.Profession.ToString();


            }catch(Exception ex)
            {
                MessageBox.Show("Some error occured: " + ex.Message + " - " + ex.Source);
            }
        }
    }
}

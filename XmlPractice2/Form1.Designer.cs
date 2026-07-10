namespace XmlPractice2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn = new System.Windows.Forms.Button();
            this.list = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.display_info = new System.Windows.Forms.Button();
            this.lblUserInformation = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.error = new System.Windows.Forms.Label();
            this.tbxName = new System.Windows.Forms.TextBox();
            this.name_lbl = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.lblSubmittedInformation = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.calculate = new System.Windows.Forms.Button();
            this.radioVisa = new System.Windows.Forms.RadioButton();
            this.radioCash = new System.Windows.Forms.RadioButton();
            this.lblPayment = new System.Windows.Forms.Label();
            this.lblSelection = new System.Windows.Forms.Label();
            this.comboVehicle = new System.Windows.Forms.ComboBox();
            this.tgRegNum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPark = new System.Windows.Forms.Button();
            this.lblParking = new System.Windows.Forms.Label();
            this.gbPark = new System.Windows.Forms.GroupBox();
            this.btnPayParking = new System.Windows.Forms.Button();
            this.tabControlParkingSystem = new System.Windows.Forms.TabControl();
            this.tabPagePay = new System.Windows.Forms.TabPage();
            this.tabPagePark = new System.Windows.Forms.TabPage();
            this.lblshowInfo = new System.Windows.Forms.Label();
            this.lblBookInfo = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.PersonId = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LastName = new System.Windows.Forms.Label();
            this.textBoxPersonId = new System.Windows.Forms.TextBox();
            this.textBoxFirstName = new System.Windows.Forms.TextBox();
            this.textBoxLastName = new System.Windows.Forms.TextBox();
            this.textBoxProfession = new System.Windows.Forms.TextBox();
            this.Proffesion = new System.Windows.Forms.Label();
            this.gbPark.SuspendLayout();
            this.tabControlParkingSystem.SuspendLayout();
            this.tabPagePay.SuspendLayout();
            this.tabPagePark.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn
            // 
            this.btn.Location = new System.Drawing.Point(16, 482);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(107, 37);
            this.btn.TabIndex = 0;
            this.btn.Text = "Open";
            this.btn.UseVisualStyleBackColor = true;
            this.btn.Click += new System.EventHandler(this.btn_Click);
            // 
            // list
            // 
            this.list.FormattingEnabled = true;
            this.list.Location = new System.Drawing.Point(16, 534);
            this.list.Name = "list";
            this.list.Size = new System.Drawing.Size(107, 173);
            this.list.TabIndex = 1;
            this.list.SelectedIndexChanged += new System.EventHandler(this.list_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(26, 83);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "0";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(107, 83);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "0";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // display_info
            // 
            this.display_info.Location = new System.Drawing.Point(22, 161);
            this.display_info.Name = "display_info";
            this.display_info.Size = new System.Drawing.Size(170, 23);
            this.display_info.TabIndex = 5;
            this.display_info.Text = "display information";
            this.display_info.UseVisualStyleBackColor = true;
            this.display_info.Click += new System.EventHandler(this.display_info_Click);
            // 
            // lblUserInformation
            // 
            this.lblUserInformation.AutoSize = true;
            this.lblUserInformation.Location = new System.Drawing.Point(33, 187);
            this.lblUserInformation.Name = "lblUserInformation";
            this.lblUserInformation.Size = new System.Drawing.Size(159, 13);
            this.lblUserInformation.TabIndex = 6;
            this.lblUserInformation.Text = "Click the button to show the info";
            // 
            // lblName
            // 
            this.lblName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblName.Location = new System.Drawing.Point(52, 232);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(121, 19);
            this.lblName.TabIndex = 7;
            this.lblName.Text = "label1";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAge
            // 
            this.lblAge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAge.Location = new System.Drawing.Point(52, 263);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(121, 17);
            this.lblAge.TabIndex = 8;
            this.lblAge.Text = "label2";
            this.lblAge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // error
            // 
            this.error.BackColor = System.Drawing.Color.Firebrick;
            this.error.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.error.Location = new System.Drawing.Point(52, 297);
            this.error.Name = "error";
            this.error.Size = new System.Drawing.Size(121, 29);
            this.error.TabIndex = 9;
            this.error.Text = "Error in program.";
            this.error.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.error.Visible = false;
            // 
            // tbxName
            // 
            this.tbxName.Location = new System.Drawing.Point(293, 80);
            this.tbxName.MaxLength = 30;
            this.tbxName.Name = "tbxName";
            this.tbxName.Size = new System.Drawing.Size(199, 20);
            this.tbxName.TabIndex = 10;
            // 
            // name_lbl
            // 
            this.name_lbl.AutoSize = true;
            this.name_lbl.Location = new System.Drawing.Point(290, 55);
            this.name_lbl.Name = "name_lbl";
            this.name_lbl.Size = new System.Drawing.Size(38, 13);
            this.name_lbl.TabIndex = 11;
            this.name_lbl.Text = "Name:";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(354, 106);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 12;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // lblSubmittedInformation
            // 
            this.lblSubmittedInformation.Location = new System.Drawing.Point(292, 150);
            this.lblSubmittedInformation.Name = "lblSubmittedInformation";
            this.lblSubmittedInformation.Size = new System.Drawing.Size(200, 36);
            this.lblSubmittedInformation.TabIndex = 13;
            this.lblSubmittedInformation.Text = "label1";
            this.lblSubmittedInformation.Visible = false;
            // 
            // panel1
            // 
            this.panel1.AccessibleDescription = "";
            this.panel1.AccessibleName = "";
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1058, 27);
            this.panel1.TabIndex = 14;
            // 
            // calculate
            // 
            this.calculate.Location = new System.Drawing.Point(66, 50);
            this.calculate.Name = "calculate";
            this.calculate.Size = new System.Drawing.Size(75, 23);
            this.calculate.TabIndex = 4;
            this.calculate.Text = "calculate";
            this.calculate.UseVisualStyleBackColor = true;
            this.calculate.Click += new System.EventHandler(this.calculate_Click);
            // 
            // radioVisa
            // 
            this.radioVisa.AutoSize = true;
            this.radioVisa.Location = new System.Drawing.Point(364, 260);
            this.radioVisa.Name = "radioVisa";
            this.radioVisa.Size = new System.Drawing.Size(45, 17);
            this.radioVisa.TabIndex = 15;
            this.radioVisa.TabStop = true;
            this.radioVisa.Text = "Visa";
            this.radioVisa.UseVisualStyleBackColor = true;
            this.radioVisa.CheckedChanged += new System.EventHandler(this.RadioButtonSelection_CheckedChanged);
            // 
            // radioCash
            // 
            this.radioCash.AutoSize = true;
            this.radioCash.Location = new System.Drawing.Point(432, 259);
            this.radioCash.Name = "radioCash";
            this.radioCash.Size = new System.Drawing.Size(49, 17);
            this.radioCash.TabIndex = 16;
            this.radioCash.TabStop = true;
            this.radioCash.Text = "Cash";
            this.radioCash.UseVisualStyleBackColor = true;
            this.radioCash.CheckedChanged += new System.EventHandler(this.RadioButtonSelection_CheckedChanged);
            // 
            // lblPayment
            // 
            this.lblPayment.AutoSize = true;
            this.lblPayment.Location = new System.Drawing.Point(361, 235);
            this.lblPayment.Name = "lblPayment";
            this.lblPayment.Size = new System.Drawing.Size(89, 13);
            this.lblPayment.TabIndex = 17;
            this.lblPayment.Text = "Choose payment:";
            // 
            // lblSelection
            // 
            this.lblSelection.AutoSize = true;
            this.lblSelection.Location = new System.Drawing.Point(391, 289);
            this.lblSelection.Name = "lblSelection";
            this.lblSelection.Size = new System.Drawing.Size(49, 13);
            this.lblSelection.TabIndex = 18;
            this.lblSelection.Text = "selection";
            // 
            // comboVehicle
            // 
            this.comboVehicle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboVehicle.FormattingEnabled = true;
            this.comboVehicle.Location = new System.Drawing.Point(54, 71);
            this.comboVehicle.Name = "comboVehicle";
            this.comboVehicle.Size = new System.Drawing.Size(155, 21);
            this.comboVehicle.TabIndex = 19;
            // 
            // tgRegNum
            // 
            this.tgRegNum.Location = new System.Drawing.Point(109, 44);
            this.tgRegNum.Name = "tgRegNum";
            this.tgRegNum.Size = new System.Drawing.Size(100, 20);
            this.tgRegNum.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Reg Num";
            // 
            // btnPark
            // 
            this.btnPark.Location = new System.Drawing.Point(96, 102);
            this.btnPark.Name = "btnPark";
            this.btnPark.Size = new System.Drawing.Size(75, 23);
            this.btnPark.TabIndex = 22;
            this.btnPark.Text = "park";
            this.btnPark.UseVisualStyleBackColor = true;
            this.btnPark.Click += new System.EventHandler(this.btnPark_Click);
            // 
            // lblParking
            // 
            this.lblParking.AutoSize = true;
            this.lblParking.Location = new System.Drawing.Point(51, 138);
            this.lblParking.Name = "lblParking";
            this.lblParking.Size = new System.Drawing.Size(35, 13);
            this.lblParking.TabIndex = 23;
            this.lblParking.Text = "label2";
            // 
            // gbPark
            // 
            this.gbPark.Controls.Add(this.tgRegNum);
            this.gbPark.Controls.Add(this.lblParking);
            this.gbPark.Controls.Add(this.comboVehicle);
            this.gbPark.Controls.Add(this.btnPark);
            this.gbPark.Controls.Add(this.label1);
            this.gbPark.Location = new System.Drawing.Point(3, 0);
            this.gbPark.Name = "gbPark";
            this.gbPark.Size = new System.Drawing.Size(314, 182);
            this.gbPark.TabIndex = 24;
            this.gbPark.TabStop = false;
            this.gbPark.Text = "park you vehicle";
            // 
            // btnPayParking
            // 
            this.btnPayParking.Location = new System.Drawing.Point(70, 66);
            this.btnPayParking.Name = "btnPayParking";
            this.btnPayParking.Size = new System.Drawing.Size(148, 42);
            this.btnPayParking.TabIndex = 25;
            this.btnPayParking.Text = "Pay parking $10";
            this.btnPayParking.UseVisualStyleBackColor = true;
            this.btnPayParking.Click += new System.EventHandler(this.btnPayParking_Click);
            // 
            // tabControlParkingSystem
            // 
            this.tabControlParkingSystem.Controls.Add(this.tabPagePay);
            this.tabControlParkingSystem.Controls.Add(this.tabPagePark);
            this.tabControlParkingSystem.Location = new System.Drawing.Point(589, 58);
            this.tabControlParkingSystem.Name = "tabControlParkingSystem";
            this.tabControlParkingSystem.SelectedIndex = 0;
            this.tabControlParkingSystem.Size = new System.Drawing.Size(291, 208);
            this.tabControlParkingSystem.TabIndex = 26;
            // 
            // tabPagePay
            // 
            this.tabPagePay.Controls.Add(this.btnPayParking);
            this.tabPagePay.Location = new System.Drawing.Point(4, 22);
            this.tabPagePay.Name = "tabPagePay";
            this.tabPagePay.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePay.Size = new System.Drawing.Size(283, 182);
            this.tabPagePay.TabIndex = 0;
            this.tabPagePay.Text = "Pay";
            this.tabPagePay.UseVisualStyleBackColor = true;
            // 
            // tabPagePark
            // 
            this.tabPagePark.Controls.Add(this.gbPark);
            this.tabPagePark.Location = new System.Drawing.Point(4, 22);
            this.tabPagePark.Name = "tabPagePark";
            this.tabPagePark.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePark.Size = new System.Drawing.Size(283, 182);
            this.tabPagePark.TabIndex = 1;
            this.tabPagePark.Text = "Park";
            this.tabPagePark.UseVisualStyleBackColor = true;
            // 
            // lblshowInfo
            // 
            this.lblshowInfo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblshowInfo.Location = new System.Drawing.Point(146, 534);
            this.lblshowInfo.Name = "lblshowInfo";
            this.lblshowInfo.Padding = new System.Windows.Forms.Padding(5);
            this.lblshowInfo.Size = new System.Drawing.Size(248, 173);
            this.lblshowInfo.TabIndex = 28;
            this.lblshowInfo.Text = "label2";
            this.lblshowInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBookInfo
            // 
            this.lblBookInfo.Location = new System.Drawing.Point(146, 482);
            this.lblBookInfo.Name = "lblBookInfo";
            this.lblBookInfo.Size = new System.Drawing.Size(248, 37);
            this.lblBookInfo.TabIndex = 29;
            this.lblBookInfo.Text = "Book Info:";
            this.lblBookInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(589, 567);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(355, 140);
            this.dataGridView1.TabIndex = 30;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // PersonId
            // 
            this.PersonId.AutoSize = true;
            this.PersonId.Location = new System.Drawing.Point(587, 484);
            this.PersonId.Name = "PersonId";
            this.PersonId.Size = new System.Drawing.Size(49, 13);
            this.PersonId.TabIndex = 31;
            this.PersonId.Text = "PersonId";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(730, 484);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "First name:";
            // 
            // LastName
            // 
            this.LastName.AutoSize = true;
            this.LastName.Location = new System.Drawing.Point(865, 484);
            this.LastName.Name = "LastName";
            this.LastName.Size = new System.Drawing.Size(59, 13);
            this.LastName.TabIndex = 33;
            this.LastName.Text = "Last name:";
            // 
            // textBoxPersonId
            // 
            this.textBoxPersonId.Location = new System.Drawing.Point(589, 500);
            this.textBoxPersonId.Name = "textBoxPersonId";
            this.textBoxPersonId.Size = new System.Drawing.Size(83, 20);
            this.textBoxPersonId.TabIndex = 34;
            // 
            // textBoxFirstName
            // 
            this.textBoxFirstName.Location = new System.Drawing.Point(732, 500);
            this.textBoxFirstName.Name = "textBoxFirstName";
            this.textBoxFirstName.Size = new System.Drawing.Size(80, 20);
            this.textBoxFirstName.TabIndex = 35;
            // 
            // textBoxLastName
            // 
            this.textBoxLastName.Location = new System.Drawing.Point(867, 500);
            this.textBoxLastName.Name = "textBoxLastName";
            this.textBoxLastName.Size = new System.Drawing.Size(77, 20);
            this.textBoxLastName.TabIndex = 36;
            // 
            // textBoxProfession
            // 
            this.textBoxProfession.Location = new System.Drawing.Point(663, 536);
            this.textBoxProfession.Name = "textBoxProfession";
            this.textBoxProfession.Size = new System.Drawing.Size(281, 20);
            this.textBoxProfession.TabIndex = 38;
            // 
            // Proffesion
            // 
            this.Proffesion.AutoSize = true;
            this.Proffesion.Location = new System.Drawing.Point(599, 539);
            this.Proffesion.Name = "Proffesion";
            this.Proffesion.Size = new System.Drawing.Size(59, 13);
            this.Proffesion.TabIndex = 37;
            this.Proffesion.Text = "Profession:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 817);
            this.Controls.Add(this.textBoxProfession);
            this.Controls.Add(this.Proffesion);
            this.Controls.Add(this.textBoxLastName);
            this.Controls.Add(this.textBoxFirstName);
            this.Controls.Add(this.textBoxPersonId);
            this.Controls.Add(this.LastName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PersonId);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblBookInfo);
            this.Controls.Add(this.lblshowInfo);
            this.Controls.Add(this.tabControlParkingSystem);
            this.Controls.Add(this.lblSelection);
            this.Controls.Add(this.lblPayment);
            this.Controls.Add(this.radioCash);
            this.Controls.Add(this.radioVisa);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblSubmittedInformation);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.name_lbl);
            this.Controls.Add(this.tbxName);
            this.Controls.Add(this.error);
            this.Controls.Add(this.lblAge);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblUserInformation);
            this.Controls.Add(this.display_info);
            this.Controls.Add(this.calculate);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.list);
            this.Controls.Add(this.btn);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbPark.ResumeLayout(false);
            this.gbPark.PerformLayout();
            this.tabControlParkingSystem.ResumeLayout(false);
            this.tabPagePay.ResumeLayout(false);
            this.tabPagePark.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn;
        private System.Windows.Forms.ListBox list;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button display_info;
        private System.Windows.Forms.Label lblUserInformation;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label error;
        private System.Windows.Forms.TextBox tbxName;
        private System.Windows.Forms.Label name_lbl;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label lblSubmittedInformation;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button calculate;
        private System.Windows.Forms.RadioButton radioVisa;
        private System.Windows.Forms.RadioButton radioCash;
        private System.Windows.Forms.Label lblPayment;
        private System.Windows.Forms.Label lblSelection;
        private System.Windows.Forms.ComboBox comboVehicle;
        private System.Windows.Forms.TextBox tgRegNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPark;
        private System.Windows.Forms.Label lblParking;
        private System.Windows.Forms.GroupBox gbPark;
        private System.Windows.Forms.Button btnPayParking;
        private System.Windows.Forms.TabControl tabControlParkingSystem;
        private System.Windows.Forms.TabPage tabPagePay;
        private System.Windows.Forms.TabPage tabPagePark;
        private System.Windows.Forms.Label lblshowInfo;
        private System.Windows.Forms.Label lblBookInfo;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label PersonId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LastName;
        private System.Windows.Forms.TextBox textBoxPersonId;
        private System.Windows.Forms.TextBox textBoxFirstName;
        private System.Windows.Forms.TextBox textBoxLastName;
        private System.Windows.Forms.TextBox textBoxProfession;
        private System.Windows.Forms.Label Proffesion;
    }
}


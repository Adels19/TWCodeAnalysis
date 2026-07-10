using System.Drawing;

namespace TWCodeAnalysis
{
    partial class Learning
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
            this.components = new System.ComponentModel.Container();
            this.read_btn = new System.Windows.Forms.Button();
            this.button = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel = new System.Windows.Forms.Panel();
            this.dashboard = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.title_box = new System.Windows.Forms.ListBox();
            this.open = new System.Windows.Forms.Button();
            this.view_items = new System.Windows.Forms.ListBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // read_btn
            // 
            this.read_btn.Location = new System.Drawing.Point(0, 0);
            this.read_btn.Name = "read_btn";
            this.read_btn.Size = new System.Drawing.Size(75, 23);
            this.read_btn.TabIndex = 0;
            // 
            // button
            // 
            this.button.Location = new System.Drawing.Point(0, 0);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(75, 23);
            this.button.TabIndex = 0;
            // 
            // panel
            // 
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(200, 100);
            this.panel.TabIndex = 0;
            // 
            // dashboard
            // 
            this.dashboard.Location = new System.Drawing.Point(0, 0);
            this.dashboard.Name = "dashboard";
            this.dashboard.Size = new System.Drawing.Size(100, 20);
            this.dashboard.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // title_box
            // 
            this.title_box.Location = new System.Drawing.Point(0, 0);
            this.title_box.Name = "title_box";
            this.title_box.Size = new System.Drawing.Size(120, 96);
            this.title_box.TabIndex = 0;
            // 
            // open
            // 
            this.open.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.open.BackColor = System.Drawing.Color.Navy;
            this.open.Cursor = System.Windows.Forms.Cursors.Hand;
            this.open.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.open.FlatAppearance.BorderSize = 51;
            this.open.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.open.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.open.Location = new System.Drawing.Point(35, 80);
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(174, 32);
            this.open.TabIndex = 1;
            this.open.Text = "Choose File";
            this.open.UseVisualStyleBackColor = false;
            this.open.Click += new System.EventHandler(this.open_Click);
            // 
            // view_items
            // 
            this.view_items.Font = new System.Drawing.Font("SansSerif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.view_items.FormattingEnabled = true;
            this.view_items.HorizontalScrollbar = true;
            this.view_items.ItemHeight = 22;
            this.view_items.Location = new System.Drawing.Point(35, 162);
            this.view_items.Name = "view_items";
            this.view_items.ScrollAlwaysVisible = true;
            this.view_items.Size = new System.Drawing.Size(217, 268);
            this.view_items.TabIndex = 2;
            this.view_items.SelectedIndexChanged += new System.EventHandler(this.titles_SelectedIndexChanged);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBox1.HideSelection = false;
            this.richTextBox1.Location = new System.Drawing.Point(282, 162);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox1.Size = new System.Drawing.Size(275, 268);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // Learning
            // 
            this.AcceptButton = this.button;
            this.ClientSize = new System.Drawing.Size(637, 534);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.view_items);
            this.Controls.Add(this.open);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "Learning";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button read_btn;
        private System.Windows.Forms.Button button;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox dashboard;
        private System.Windows.Forms.ListBox title_box;
        private System.Windows.Forms.Button open;
        private System.Windows.Forms.ListBox view_items;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}
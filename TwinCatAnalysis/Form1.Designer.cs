namespace TwinCatAnalysis
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
            this.components = new System.ComponentModel.Container();
            this.btnOpenProject = new System.Windows.Forms.Button();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabPane1 = new DevExpress.XtraBars.Navigation.TabPane();
            this.tabNavigationPage1 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.tabNavigationPage2 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.diagramControl = new DevExpress.XtraDiagram.DiagramControl();
            this.barAndDockingController1 = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.applicationMenu1 = new DevExpress.XtraBars.Ribbon.ApplicationMenu(this.components);
            this.diagramBarController1 = new DevExpress.XtraDiagram.Bars.DiagramBarController(this.components);
            this.PrintMenuPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.ExportAsPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.ImageToolsBringToFrontContainerPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.ImageToolsSendToBackContainerPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.ToolsContainerPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.BringToFrontPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.SendToBackPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPane1)).BeginInit();
            this.tabPane1.SuspendLayout();
            this.tabNavigationPage1.SuspendLayout();
            this.tabNavigationPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.diagramControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diagramBarController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrintMenuPopupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExportAsPopupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageToolsBringToFrontContainerPopupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageToolsSendToBackContainerPopupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToolsContainerPopupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BringToFrontPopupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SendToBackPopupMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpenProject
            // 
            this.btnOpenProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenProject.Location = new System.Drawing.Point(9, 627);
            this.btnOpenProject.Name = "btnOpenProject";
            this.btnOpenProject.Size = new System.Drawing.Size(94, 32);
            this.btnOpenProject.TabIndex = 0;
            this.btnOpenProject.Text = "Open Project";
            this.btnOpenProject.UseVisualStyleBackColor = true;
            this.btnOpenProject.Click += new System.EventHandler(this.btnOpenProject_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(691, 528);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gridView1.DetailHeight = 284;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsEditForm.PopupEditFormWidth = 600;
            this.gridView1.RowHeight = 30;
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            // 
            // tabPane1
            // 
            this.tabPane1.Controls.Add(this.tabNavigationPage1);
            this.tabPane1.Controls.Add(this.tabNavigationPage2);
            this.tabPane1.Location = new System.Drawing.Point(9, 12);
            this.tabPane1.Name = "tabPane1";
            this.tabPane1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.tabNavigationPage1,
            this.tabNavigationPage2});
            this.tabPane1.RegularSize = new System.Drawing.Size(691, 561);
            this.tabPane1.SelectedPage = this.tabNavigationPage1;
            this.tabPane1.Size = new System.Drawing.Size(691, 561);
            this.tabPane1.TabIndex = 2;
            this.tabPane1.Text = "tabPane1";
            // 
            // tabNavigationPage1
            // 
            this.tabNavigationPage1.Caption = "Variables";
            this.tabNavigationPage1.Controls.Add(this.gridControl1);
            this.tabNavigationPage1.Name = "tabNavigationPage1";
            this.tabNavigationPage1.Size = new System.Drawing.Size(691, 528);
            // 
            // tabNavigationPage2
            // 
            this.tabNavigationPage2.Caption = "Graph";
            this.tabNavigationPage2.Controls.Add(this.diagramControl);
            this.tabNavigationPage2.Name = "tabNavigationPage2";
            this.tabNavigationPage2.Size = new System.Drawing.Size(691, 528);
            // 
            // diagramControl
            // 
            this.diagramControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diagramControl.Location = new System.Drawing.Point(0, 0);
            this.diagramControl.Name = "diagramControl";
            this.diagramControl.OptionsBehavior.SelectedStencils = new DevExpress.Diagram.Core.StencilCollection(new string[] {
            "BasicShapes",
            "BasicFlowchartShapes"});
            this.diagramControl.OptionsView.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.diagramControl.Size = new System.Drawing.Size(691, 528);
            this.diagramControl.TabIndex = 0;
            this.diagramControl.Text = "diagramControl1";
            // 
            // applicationMenu1
            // 
            this.applicationMenu1.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.LargeImagesTextDescription;
            this.applicationMenu1.MinWidth = 350;
            this.applicationMenu1.Name = "applicationMenu1";
            // 
            // diagramBarController1
            // 
            this.diagramBarController1.Control = this.diagramControl;
            // 
            // PrintMenuPopupMenu
            // 
            this.PrintMenuPopupMenu.Name = "PrintMenuPopupMenu";
            // 
            // ExportAsPopupMenu
            // 
            this.ExportAsPopupMenu.Name = "ExportAsPopupMenu";
            // 
            // ImageToolsBringToFrontContainerPopupMenu
            // 
            this.ImageToolsBringToFrontContainerPopupMenu.Name = "ImageToolsBringToFrontContainerPopupMenu";
            // 
            // ImageToolsSendToBackContainerPopupMenu
            // 
            this.ImageToolsSendToBackContainerPopupMenu.Name = "ImageToolsSendToBackContainerPopupMenu";
            // 
            // ToolsContainerPopupMenu
            // 
            this.ToolsContainerPopupMenu.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.SmallImagesText;
            this.ToolsContainerPopupMenu.Name = "ToolsContainerPopupMenu";
            // 
            // BringToFrontPopupMenu
            // 
            this.BringToFrontPopupMenu.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.SmallImagesText;
            this.BringToFrontPopupMenu.Name = "BringToFrontPopupMenu";
            // 
            // SendToBackPopupMenu
            // 
            this.SendToBackPopupMenu.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.SmallImagesText;
            this.SendToBackPopupMenu.Name = "SendToBackPopupMenu";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 660);
            this.Controls.Add(this.tabPane1);
            this.Controls.Add(this.btnOpenProject);
            this.Name = "Form1";
            this.Text = "TwinCatCodeAnalyzer";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPane1)).EndInit();
            this.tabPane1.ResumeLayout(false);
            this.tabNavigationPage1.ResumeLayout(false);
            this.tabNavigationPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.diagramControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diagramBarController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrintMenuPopupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExportAsPopupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageToolsBringToFrontContainerPopupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageToolsSendToBackContainerPopupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToolsContainerPopupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BringToFrontPopupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SendToBackPopupMenu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenProject;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraBars.Navigation.TabPane tabPane1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage2;
        private DevExpress.XtraDiagram.DiagramControl diagramControl;
        private DevExpress.XtraBars.Ribbon.ApplicationMenu applicationMenu1;
        private DevExpress.XtraBars.PopupMenu PrintMenuPopupMenu;
        private DevExpress.XtraBars.PopupMenu ExportAsPopupMenu;
        private DevExpress.XtraBars.PopupMenu ImageToolsBringToFrontContainerPopupMenu;
        private DevExpress.XtraBars.PopupMenu ImageToolsSendToBackContainerPopupMenu;
        private DevExpress.XtraBars.PopupMenu ToolsContainerPopupMenu;
        private DevExpress.XtraBars.PopupMenu BringToFrontPopupMenu;
        private DevExpress.XtraBars.PopupMenu SendToBackPopupMenu;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController1;
        private DevExpress.XtraDiagram.Bars.DiagramBarController diagramBarController1;
    }
}


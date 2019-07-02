namespace Practice7
{
    partial class MainWinForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWinForm));
            this.labelRecentlyProjectText = new System.Windows.Forms.Label();
            this.buttonNewProject = new System.Windows.Forms.Button();
            this.buttonOpenProject = new System.Windows.Forms.Button();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.fileExplorer = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanelRecentlyProjects = new System.Windows.Forms.TableLayoutPanel();
            this.panelHelp = new System.Windows.Forms.Panel();
            this.panelSetting = new System.Windows.Forms.Panel();
            this.labelNewProject = new System.Windows.Forms.Label();
            this.labelOpenProject = new System.Windows.Forms.Label();
            this.labelSetting = new System.Windows.Forms.Label();
            this.labelHelp = new System.Windows.Forms.Label();
            this.createNewProjectFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.panelSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelRecentlyProjectText
            // 
            this.labelRecentlyProjectText.AutoSize = true;
            this.labelRecentlyProjectText.BackColor = System.Drawing.SystemColors.Control;
            this.labelRecentlyProjectText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRecentlyProjectText.Location = new System.Drawing.Point(8, 113);
            this.labelRecentlyProjectText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRecentlyProjectText.Name = "labelRecentlyProjectText";
            this.labelRecentlyProjectText.Size = new System.Drawing.Size(169, 17);
            this.labelRecentlyProjectText.TabIndex = 0;
            this.labelRecentlyProjectText.Text = "Recently opened projects";
            // 
            // buttonNewProject
            // 
            this.buttonNewProject.BackColor = System.Drawing.Color.Transparent;
            this.buttonNewProject.BackgroundImage = global::Practice7.Properties.Resources.NewProject;
            this.buttonNewProject.FlatAppearance.BorderSize = 0;
            this.buttonNewProject.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonNewProject.Location = new System.Drawing.Point(449, 154);
            this.buttonNewProject.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNewProject.Name = "buttonNewProject";
            this.buttonNewProject.Size = new System.Drawing.Size(100, 76);
            this.buttonNewProject.TabIndex = 1;
            this.buttonNewProject.UseVisualStyleBackColor = false;
            this.buttonNewProject.Click += new System.EventHandler(this.newProject_Click);
            // 
            // buttonOpenProject
            // 
            this.buttonOpenProject.BackColor = System.Drawing.Color.Transparent;
            this.buttonOpenProject.BackgroundImage = global::Practice7.Properties.Resources.OpenProject;
            this.buttonOpenProject.FlatAppearance.BorderSize = 0;
            this.buttonOpenProject.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonOpenProject.Location = new System.Drawing.Point(601, 154);
            this.buttonOpenProject.Margin = new System.Windows.Forms.Padding(2);
            this.buttonOpenProject.Name = "buttonOpenProject";
            this.buttonOpenProject.Size = new System.Drawing.Size(74, 76);
            this.buttonOpenProject.TabIndex = 2;
            this.buttonOpenProject.UseVisualStyleBackColor = false;
            this.buttonOpenProject.Click += new System.EventHandler(this.openProject_Click);
            // 
            // buttonSettings
            // 
            this.buttonSettings.BackColor = System.Drawing.Color.Transparent;
            this.buttonSettings.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonSettings.Location = new System.Drawing.Point(449, 268);
            this.buttonSettings.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(100, 80);
            this.buttonSettings.TabIndex = 3;
            this.buttonSettings.Text = "Settings";
            this.buttonSettings.UseVisualStyleBackColor = false;
            this.buttonSettings.Click += new System.EventHandler(this.settings_Click);
            // 
            // buttonHelp
            // 
            this.buttonHelp.BackColor = System.Drawing.Color.Transparent;
            this.buttonHelp.BackgroundImage = global::Practice7.Properties.Resources.Help;
            this.buttonHelp.FlatAppearance.BorderSize = 0;
            this.buttonHelp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonHelp.Location = new System.Drawing.Point(612, 268);
            this.buttonHelp.Margin = new System.Windows.Forms.Padding(2);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(53, 80);
            this.buttonHelp.TabIndex = 4;
            this.buttonHelp.UseVisualStyleBackColor = false;
            this.buttonHelp.Click += new System.EventHandler(this.help_Click);
            // 
            // fileExplorer
            // 
            this.fileExplorer.FileOk += new System.ComponentModel.CancelEventHandler(this.exploradorArchivo_FileOk);
            // 
            // tableLayoutPanelRecentlyProjects
            // 
            this.tableLayoutPanelRecentlyProjects.ColumnCount = 1;
            this.tableLayoutPanelRecentlyProjects.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelRecentlyProjects.Location = new System.Drawing.Point(11, 132);
            this.tableLayoutPanelRecentlyProjects.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanelRecentlyProjects.Name = "tableLayoutPanelRecentlyProjects";
            this.tableLayoutPanelRecentlyProjects.RowCount = 10;
            this.tableLayoutPanelRecentlyProjects.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanelRecentlyProjects.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanelRecentlyProjects.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanelRecentlyProjects.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanelRecentlyProjects.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanelRecentlyProjects.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanelRecentlyProjects.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanelRecentlyProjects.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanelRecentlyProjects.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanelRecentlyProjects.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanelRecentlyProjects.Size = new System.Drawing.Size(377, 250);
            this.tableLayoutPanelRecentlyProjects.TabIndex = 6;
            // 
            // panelHelp
            // 
            this.panelHelp.Location = new System.Drawing.Point(188, 25);
            this.panelHelp.Margin = new System.Windows.Forms.Padding(2);
            this.panelHelp.Name = "panelHelp";
            this.panelHelp.Size = new System.Drawing.Size(203, 125);
            this.panelHelp.TabIndex = 0;
            this.panelHelp.Visible = false;
            // 
            // panelSetting
            // 
            this.panelSetting.Controls.Add(this.panelHelp);
            this.panelSetting.Location = new System.Drawing.Point(72, 49);
            this.panelSetting.Margin = new System.Windows.Forms.Padding(2);
            this.panelSetting.Name = "panelSetting";
            this.panelSetting.Size = new System.Drawing.Size(251, 191);
            this.panelSetting.TabIndex = 5;
            this.panelSetting.Visible = false;
            // 
            // labelNewProject
            // 
            this.labelNewProject.AutoSize = true;
            this.labelNewProject.BackColor = System.Drawing.Color.Transparent;
            this.labelNewProject.ForeColor = System.Drawing.SystemColors.Control;
            this.labelNewProject.Location = new System.Drawing.Point(469, 242);
            this.labelNewProject.Name = "labelNewProject";
            this.labelNewProject.Size = new System.Drawing.Size(64, 13);
            this.labelNewProject.TabIndex = 7;
            this.labelNewProject.Text = "New project";
            // 
            // labelOpenProject
            // 
            this.labelOpenProject.AutoSize = true;
            this.labelOpenProject.BackColor = System.Drawing.Color.Transparent;
            this.labelOpenProject.ForeColor = System.Drawing.SystemColors.Control;
            this.labelOpenProject.Location = new System.Drawing.Point(607, 242);
            this.labelOpenProject.Name = "labelOpenProject";
            this.labelOpenProject.Size = new System.Drawing.Size(68, 13);
            this.labelOpenProject.TabIndex = 8;
            this.labelOpenProject.Text = "Open project";
            // 
            // labelSetting
            // 
            this.labelSetting.AutoSize = true;
            this.labelSetting.BackColor = System.Drawing.Color.Transparent;
            this.labelSetting.ForeColor = System.Drawing.SystemColors.Control;
            this.labelSetting.Location = new System.Drawing.Point(483, 350);
            this.labelSetting.Name = "labelSetting";
            this.labelSetting.Size = new System.Drawing.Size(40, 13);
            this.labelSetting.TabIndex = 9;
            this.labelSetting.Text = "Setting";
            // 
            // labelHelp
            // 
            this.labelHelp.AutoSize = true;
            this.labelHelp.BackColor = System.Drawing.Color.Transparent;
            this.labelHelp.ForeColor = System.Drawing.SystemColors.Control;
            this.labelHelp.Location = new System.Drawing.Point(624, 350);
            this.labelHelp.Name = "labelHelp";
            this.labelHelp.Size = new System.Drawing.Size(29, 13);
            this.labelHelp.TabIndex = 10;
            this.labelHelp.Text = "Help";
            // 
            // createNewProjectFileDialog
            // 
            this.createNewProjectFileDialog.Filter = "AZOR | ";
            // 
            // MainWinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(784, 411);
            this.Controls.Add(this.labelHelp);
            this.Controls.Add(this.labelSetting);
            this.Controls.Add(this.labelOpenProject);
            this.Controls.Add(this.labelNewProject);
            this.Controls.Add(this.tableLayoutPanelRecentlyProjects);
            this.Controls.Add(this.panelSetting);
            this.Controls.Add(this.buttonHelp);
            this.Controls.Add(this.buttonSettings);
            this.Controls.Add(this.buttonOpenProject);
            this.Controls.Add(this.buttonNewProject);
            this.Controls.Add(this.labelRecentlyProjectText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainWinForm";
            this.Text = "MainWinForm";
            this.Load += new System.EventHandler(this.mainWinFormLoad_Load);
            this.panelSetting.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelRecentlyProjectText;
        private System.Windows.Forms.Button buttonNewProject;
        private System.Windows.Forms.Button buttonOpenProject;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Button buttonHelp;
        private System.Windows.Forms.OpenFileDialog fileExplorer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelRecentlyProjects;
        private System.Windows.Forms.Panel panelHelp;
        private System.Windows.Forms.Panel panelSetting;
        private System.Windows.Forms.Label labelNewProject;
        private System.Windows.Forms.Label labelOpenProject;
        private System.Windows.Forms.Label labelSetting;
        private System.Windows.Forms.Label labelHelp;
        private System.Windows.Forms.SaveFileDialog createNewProjectFileDialog;
    }
}

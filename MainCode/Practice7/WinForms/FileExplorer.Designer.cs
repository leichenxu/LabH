namespace Practice7
{
    partial class FileExplorer
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
            this.buttonMoveBackward = new System.Windows.Forms.Button();
            this.buttonMoveForward = new System.Windows.Forms.Button();
            this.buttonOpenFileExplorer = new System.Windows.Forms.Button();
            this.labelPathText = new System.Windows.Forms.Label();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.webBrowserFiles = new System.Windows.Forms.WebBrowser();
            this.buttonCreateProject = new System.Windows.Forms.Button();
            this.textBoxNewFileName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonMoveBackward
            // 
            this.buttonMoveBackward.Location = new System.Drawing.Point(8, 8);
            this.buttonMoveBackward.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonMoveBackward.Name = "buttonMoveBackward";
            this.buttonMoveBackward.Size = new System.Drawing.Size(50, 23);
            this.buttonMoveBackward.TabIndex = 0;
            this.buttonMoveBackward.Text = "<<";
            this.buttonMoveBackward.UseVisualStyleBackColor = true;
            this.buttonMoveBackward.Click += new System.EventHandler(this.backward_Click);
            // 
            // buttonMoveForward
            // 
            this.buttonMoveForward.Location = new System.Drawing.Point(62, 8);
            this.buttonMoveForward.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonMoveForward.Name = "buttonMoveForward";
            this.buttonMoveForward.Size = new System.Drawing.Size(50, 23);
            this.buttonMoveForward.TabIndex = 1;
            this.buttonMoveForward.Text = ">>";
            this.buttonMoveForward.UseVisualStyleBackColor = true;
            this.buttonMoveForward.Click += new System.EventHandler(this.adelante_Click);
            // 
            // buttonOpenFileExplorer
            // 
            this.buttonOpenFileExplorer.Location = new System.Drawing.Point(529, 8);
            this.buttonOpenFileExplorer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonOpenFileExplorer.Name = "buttonOpenFileExplorer";
            this.buttonOpenFileExplorer.Size = new System.Drawing.Size(69, 23);
            this.buttonOpenFileExplorer.TabIndex = 2;
            this.buttonOpenFileExplorer.Text = "Abrir ruta";
            this.buttonOpenFileExplorer.UseVisualStyleBackColor = true;
            this.buttonOpenFileExplorer.Click += new System.EventHandler(this.openFileExplorer_Click);
            // 
            // labelPathText
            // 
            this.labelPathText.AutoSize = true;
            this.labelPathText.Location = new System.Drawing.Point(116, 13);
            this.labelPathText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPathText.Name = "labelPathText";
            this.labelPathText.Size = new System.Drawing.Size(55, 13);
            this.labelPathText.TabIndex = 3;
            this.labelPathText.Text = "Direccion:";
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(173, 11);
            this.textBoxPath.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(353, 20);
            this.textBoxPath.TabIndex = 4;
            this.textBoxPath.TextChanged += new System.EventHandler(this.path_TextChanged);
            // 
            // webBrowserFiles
            // 
            this.webBrowserFiles.Location = new System.Drawing.Point(13, 40);
            this.webBrowserFiles.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.webBrowserFiles.MinimumSize = new System.Drawing.Size(13, 13);
            this.webBrowserFiles.Name = "webBrowserFiles";
            this.webBrowserFiles.Size = new System.Drawing.Size(584, 230);
            this.webBrowserFiles.TabIndex = 5;
            this.webBrowserFiles.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
            // 
            // buttonCreateProject
            // 
            this.buttonCreateProject.Location = new System.Drawing.Point(429, 296);
            this.buttonCreateProject.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonCreateProject.Name = "buttonCreateProject";
            this.buttonCreateProject.Size = new System.Drawing.Size(169, 25);
            this.buttonCreateProject.TabIndex = 6;
            this.buttonCreateProject.Text = "Crear";
            this.buttonCreateProject.UseVisualStyleBackColor = true;
            this.buttonCreateProject.Click += new System.EventHandler(this.createProject_Click);
            // 
            // textBoxNewFileName
            // 
            this.textBoxNewFileName.Location = new System.Drawing.Point(25, 300);
            this.textBoxNewFileName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxNewFileName.Name = "textBoxNewFileName";
            this.textBoxNewFileName.Size = new System.Drawing.Size(385, 20);
            this.textBoxNewFileName.TabIndex = 7;
            // 
            // FileExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 339);
            this.Controls.Add(this.textBoxNewFileName);
            this.Controls.Add(this.buttonCreateProject);
            this.Controls.Add(this.webBrowserFiles);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.labelPathText);
            this.Controls.Add(this.buttonOpenFileExplorer);
            this.Controls.Add(this.buttonMoveForward);
            this.Controls.Add(this.buttonMoveBackward);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FileExplorer";
            this.Text = "FileExplorer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonMoveBackward;
        private System.Windows.Forms.Button buttonMoveForward;
        private System.Windows.Forms.Button buttonOpenFileExplorer;
        private System.Windows.Forms.Label labelPathText;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.WebBrowser webBrowserFiles;
        private System.Windows.Forms.Button buttonCreateProject;
        private System.Windows.Forms.TextBox textBoxNewFileName;
    }
}
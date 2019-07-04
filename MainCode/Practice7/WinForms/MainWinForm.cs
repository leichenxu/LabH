using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Threading;

namespace Practice7
{

    public partial class MainWinForm : Form
    {
        /// <summary>
        /// Save the path of the new project.
        /// </summary>
        private string newProjectPath;
        /// <summary>
        /// New project extention.
        /// </summary>
        private string extension = ".xxx";
        /// <summary>
        /// Can open new win form or not boolean.
        /// </summary>
        private bool openWinForm = false;
        /// <summary>
        /// General use.
        /// </summary>
        private System.Windows.Forms.ToolTip tip;
        /// <summary>
        /// Store the project start string.
        /// </summary>
        private string projectStartPath;
        ///// <summary>
        ///// FileExplorer for create projects.
        ///// </summary>
        //private Form createProjectsFileExplorer;
        /// <summary>
        /// The extention to create.
        /// </summary>
        string extention = ".xxx";
        /// <summary>
        /// Path for read recently documents.
        /// </summary>
        string recentlyOpenedProjectFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        /// <summary>
        /// Store the recently opened projects in arrayList.
        /// </summary>
        ArrayList arrayListRecentlyOpennedProjects = new ArrayList();
        /// <summary>
        /// Setting winform.
        /// </summary>
        private WinFormSetting settingForm;
        /// <summary>
        /// Class constructor.
        /// </summary>
        public MainWinForm(WinFormSetting settingForm)
        {
            InitializeComponent();
            //cannot maximize window size
            this.MaximizeBox = false;
            //path recently openend project
            recentlyOpenedProjectFilePath += @"\AZOR\lstpjs";
            //center the winform
            this.CenterToScreen();

            //make transparent the colours
            this.labelRecentlyProjectText.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelRecentlyProjects.BackColor = System.Drawing.Color.Transparent;
            //save it
            this.settingForm = settingForm;

            //add extention filter
            this.fileExplorer.Filter = "AZOR | *" + extension;
            //add event
            this.FormClosed += checkOpenNewWindow;
        }
        /// <summary>
        /// Check open or not new window, if yes open it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkOpenNewWindow(object sender, EventArgs e)
        {
            if (openWinForm)
            {
                //start new thread
                Thread th = new Thread(openCreatedProject);
                th.SetApartmentState(ApartmentState.STA);
                settingForm.CanShow = false;
                th.Start();
            }
        }
        /// <summary>
        /// When winform opened load the recently openned projects.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainWinFormLoad_Load(object sender, EventArgs e)
        {
            showRecentlyOpennedProjects();
        }

        /// <summary>
        /// Read all the lines in the file, and write it.
        /// </summary>
        private void showRecentlyOpennedProjects()
        {
            //count row in another variable
            int rowCount = 0;
            string newFileContent = "";
            //save all the string in a buffer
            string[] bufferString = File.ReadAllLines(this.recentlyOpenedProjectFilePath);
            //read the buffer
            for (int i = 0; i < bufferString.Count(); i++)
            {
                //check
                if (File.Exists(bufferString[i]))
                {
                    if (i != bufferString.Count() - 1)
                        //br
                        newFileContent += bufferString[i] + "\n";
                    else
                        //not br
                        newFileContent += bufferString[i];
                    //create new label
                    System.Windows.Forms.LinkLabel label = new System.Windows.Forms.LinkLabel();
                    //adjust                                
                    label.AutoSize = true;
                    label.BackColor = System.Drawing.Color.Transparent;
                    label.LinkColor = System.Drawing.Color.White;
                    label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    label.Name = "labelProjects" + i.ToString();
                    label.Size = new System.Drawing.Size(100, 25);
                    label.AccessibleDescription = bufferString[i];
                    label.Text = Path.GetFileName(Path.GetDirectoryName(bufferString[i]));
                    label.Click += new System.EventHandler(label_Click);
                    tableLayoutPanelRecentlyProjects.Controls.Add(label, 0, rowCount);
                    rowCount++;
                    //add tooltip
                    tip = new System.Windows.Forms.ToolTip();
                    tip.SetToolTip(label, bufferString[i]);
                    label.AccessibleDefaultActionDescription = bufferString[i];
                }
            }
            //save it
            StreamWriter sw = File.CreateText(this.recentlyOpenedProjectFilePath);
            sw.Write(newFileContent);
            //close it
            sw.Close();
        }

        /// <summary>
        /// Event when recently project is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_Click(object sender, EventArgs e)
        {
            //if exception show it
            try
            {
                System.Windows.Forms.Label aux = (System.Windows.Forms.Label)sender;
                MessageBox.Show("Se ha seleccionado el proyecto " + aux.Text);
                projectStartPath = Path.GetDirectoryName(aux.AccessibleDefaultActionDescription);
                //close it
                openWinForm = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        /// <summary>
        /// Event when new project is clicked, open the file explorer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newProject_Click(object sender, EventArgs e)
        {
            //createProjectsFileExplorer = new FileExplorer(this, extention,settingForm);
            ////show it
            //createProjectsFileExplorer.ShowDialog();

            //SHOW IT
            if (createNewProjectFileDialog.ShowDialog() == DialogResult.OK)
            {

                //get the path
                newProjectPath = createNewProjectFileDialog.FileName;
                //files and directories for crete
                Directory.CreateDirectory(newProjectPath);
                Directory.CreateDirectory(newProjectPath + "\\Sources");
                Directory.CreateDirectory(newProjectPath + "\\Sources\\data");
                Directory.CreateDirectory(newProjectPath + "\\Sources\\temp");
                Directory.CreateDirectory(newProjectPath + "\\Sources\\videos");
                Directory.CreateDirectory(newProjectPath + "\\Sources\\exported");
                File.Create(newProjectPath + "\\azor" + extension);

                //save it in recently
                string[] fileToAdd = { newProjectPath + "\\azor" + extension };
                fileToAdd = fileToAdd.Union(File.ReadAllLines(recentlyOpenedProjectFilePath)).ToArray();

                //write all lines in table
                File.WriteAllLines(recentlyOpenedProjectFilePath, fileToAdd);


                //close the mainform
                this.Close();
                //start new thread
                Thread th = new Thread(opennewform);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
        }
        /// <summary>
        /// Open new form, run it.
        /// </summary>
        private void opennewform()
        {
            Application.Run(new WinFormWithVideoPlayer(newProjectPath, settingForm));
        }
        /// <summary>
        /// Open the project with the extension.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openProject_Click(object sender, EventArgs e)
        {
            //show file explorer for open project
            this.fileExplorer.ShowDialog();
        }

        /// <summary>
        /// When projects is selected and have the right extension, "open" it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exploradorArchivo_FileOk(object sender, CancelEventArgs e)
        {
            //if wrong extention notify it
            if (!Path.GetExtension(this.fileExplorer.FileName).Equals(extention))
            {
                MessageBox.Show("Formato invalido");
                //do not close after this
                e.Cancel = true;
            }
            else
            {
                //string[] aux = File.ReadAllLines(recentlyOpenedProjectFilePath);

                //get the file name
                string[] fileToAdd = { this.fileExplorer.FileName };
                fileToAdd = fileToAdd.Union(File.ReadAllLines(recentlyOpenedProjectFilePath)).ToArray();

                //string aniadir = this.exploradorArchivo.FileName+"\n"+File.ReadAllText(recentlyOpenedProjectFilePath);   

                //write all lines in table
                File.WriteAllLines(recentlyOpenedProjectFilePath, fileToAdd);
                //if more than 5 then delete one
                if (fileToAdd.Count() == 6)
                {
                    File.WriteAllLines(recentlyOpenedProjectFilePath, File.ReadAllLines(recentlyOpenedProjectFilePath).
                        Take(File.ReadAllLines(recentlyOpenedProjectFilePath).Length - 1));
                }
                //rewrite all in table
                for (int i = 0; i < arrayListRecentlyOpennedProjects.Capacity; i++)
                {
                    System.Windows.Forms.Label label = (System.Windows.Forms.Label)arrayListRecentlyOpennedProjects[i];
                    this.tableLayoutPanelRecentlyProjects.Controls.Remove(label);
                }
                //clear the table
                this.tableLayoutPanelRecentlyProjects.Controls.Clear();
                showRecentlyOpennedProjects();
                //save the name
                projectStartPath = Path.GetDirectoryName(this.fileExplorer.FileName);

                //close myself,turn to true
                openWinForm = true;
                this.Close();
            }
        }

        /// <summary>
        /// Open new form with location, run it.
        /// </summary>
        private void openCreatedProject()
        {
            Application.Run(new WinFormWithVideoPlayer(projectStartPath, settingForm));
        }


        /// <summary>
        /// Show and not show panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settings_Click(object sender, EventArgs e)
        {
            this.settingForm.ShowDialog();

        }

        /// <summary>
        /// Show and not show panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void help_Click(object sender, EventArgs e)
        {

            if (this.panelHelp.Visible)
            {
                this.panelHelp.Visible = false;
            }
            else
            {
                this.panelHelp.Visible = true;
            }
        }


    }
}


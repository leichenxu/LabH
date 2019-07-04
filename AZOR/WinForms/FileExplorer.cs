using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
namespace AZOR
{
    public partial class FileExplorer : Form
    {
        /// <summary>
        /// Setting form.
        /// </summary>
        private WinFormSetting settingForm;
        /// <summary>
        /// The start path to show in webbrowser.
        /// </summary>
        private string webBrowserStartPath = "C:\\";
        /// <summary>
        /// The extension received to create project.
        /// </summary>
        private string extension;
        /// <summary>
        /// Path where the project will store video.
        /// </summary>
        private string projectStorePath;
        /// <summary>
        /// Store the mainForm for close it when project is created.
        /// </summary>
        private Form mainForm;
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="mainForm"></param>
        /// <param name="extension"></param>
        public FileExplorer(Form mainForm, string extension, WinFormSetting settingForm)
        {
            //activate key preview
            KeyPreview = true;
            InitializeComponent();
            //start the webbrowser in the path
            this.webBrowserFiles.Url = new Uri(webBrowserStartPath);
            //save these variables
            this.mainForm = mainForm;
            this.extension = extension;
            this.KeyPress += enterNewProjectName;
            //save setting
            this.settingForm = settingForm;
        }
        /// <summary>
        /// Take the name and create the project.
        /// </summary>
        private void enterNewProjectName(object sender, KeyPressEventArgs k)
        {
            if (k.KeyChar == (char)Keys.Return)
            {
                if (textBoxNewFileName.Text != null)
                {
                    this.createProject_Click(sender, k);
                }
            }
        }
        /// <summary>
        /// Open file explorer for select better the path.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileExplorer_Click(object sender, EventArgs e)
        {
            //using folderbrowerdialog to show the file explorer
            using (FolderBrowserDialog fbd = new FolderBrowserDialog()
            {
                Description = "Select your path"
            })
            {
                //disable the new folder button
                fbd.ShowNewFolderButton = false;
                //when path selected show in webbrownser
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    webBrowserFiles.Url = new Uri(fbd.SelectedPath);
                    this.textBoxPath.Text = new Uri(this.webBrowserFiles.Url.ToString()).LocalPath.ToString();
                    //change textbox, when is change change the webbrowser content
                    webBrowserStartPath = this.textBoxPath.Text;
                }
            }
        }

        /// <summary>
        /// Go backward when is cliked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backward_Click(object sender, EventArgs e)
        {
            //if can go back backward
            if (webBrowserFiles.CanGoBack)
                webBrowserFiles.GoBack();
        }

        /// <summary>
        /// Go forward when is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void adelante_Click(object sender, EventArgs e)
        {
            //if can go forward go forward
            if (webBrowserFiles.CanGoForward)
                webBrowserFiles.GoForward();
        }

        /// <summary>
        /// When textBoxPath context change, change the webbrowser content.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void path_TextChanged(object sender, EventArgs e)
        {
            //if directory exist change it
            if (Directory.Exists(this.textBoxPath.Text))
            {
                webBrowserFiles.Url = new Uri(this.textBoxPath.Text);
                webBrowserStartPath = this.textBoxPath.Text;
            }
        }
        /// <summary>
        /// Create the projects if is possible, if not notify.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createProject_Click(object sender, EventArgs e)
        {
            //check webbrowser not null
            if (this.webBrowserFiles.Url != null)
            {
                //if not exist create project with all files and close this form and show new window
                if (!Directory.Exists(webBrowserStartPath + "\\" + this.textBoxNewFileName.Text))
                {
                    //get the path
                    string pathAux = new Uri(this.webBrowserFiles.Url.ToString()).LocalPath + "\\" + this.textBoxNewFileName.Text;

                    //files and directories for crete
                    Directory.CreateDirectory(pathAux);
                    Directory.CreateDirectory(pathAux + "\\Sources");
                    File.Create(pathAux + "\\azor" + extension);
                    Directory.CreateDirectory(pathAux + "\\Sources\\temp");
                    Directory.CreateDirectory(pathAux + "\\Sources\\videos");
                    Directory.CreateDirectory(pathAux + "\\Sources\\exported");
                    this.projectStorePath = pathAux;

                    //close the mainform
                    this.mainForm.Close();
                    this.Close();
                    //start new thread
                    Thread th = new Thread(opennewform);
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                }
                else
                {
                    //notify
                    MessageBox.Show("Ya existe");
                }
            }
        }

        /// <summary>
        /// Open new form, run it.
        /// </summary>
        private void opennewform()
        {
            Application.Run(new WinFormWithVideoPlayer(projectStorePath, settingForm));
        }

        /// <summary>
        /// When browser change, textbox change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //change the textbox for have current path
            this.textBoxPath.Text = new Uri(this.webBrowserFiles.Url.ToString()).LocalPath.ToString();
        }


    }

}


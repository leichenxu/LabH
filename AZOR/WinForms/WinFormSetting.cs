using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Mpv.NET.Player;
using Newtonsoft.Json;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
using System.Text;

namespace AZOR
{
    public partial class WinFormSetting : Form
    {
        /// <summary>
        /// Azor setting file extension.
        /// </summary>
        private static readonly string AZORSettingExtension = ".azorSetting";
        /// <summary>
        /// Add all the label, for easy run.
        /// </summary>
        private ArrayList allInformation = new ArrayList();
        /// <summary>
        /// If available, then can be openned.
        /// </summary>
        private bool canShow = true;
        /// <summary>
        /// For write in the same setting when the name is the same.
        /// </summary>
        private Dictionary<String, Setting> mapSetting = new Dictionary<string, Setting>();
        /// <summary>
        /// Store the last index, verify openned or not, setted or not.
        /// </summary>
        private static int lastIndex = -1;
        /// <summary>
        /// List of settings.
        /// </summary>
        private List<Setting> settings;
        /// <summary>
        /// Store 4 secondary url.
        /// </summary>
        private string[] secondaryUrl = new string[4];
        /// <summary>
        /// Path json file.
        /// </summary>
        private string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\AZOR\\setting" + AZORSettingExtension;
        /// <summary>
        /// Store 4 main url.
        /// </summary>
        private string[] mainUrl = new string[4];
        /// <summary>
        /// String message avaiable.
        /// </summary>
        private static string Available = "Valido";
        /// <summary>
        /// String message no avaiable.
        /// </summary>
        private static string noAvailable = "No valido";
        /// <summary>
        /// Player for previsualize.
        /// </summary>
        private MpvPlayer mpv;
        /// <summary>
        /// FFMPEG location.
        /// </summary>
        private static string ffmpegtool;// = @"C:\Users\Developer Lee\source\repos\librerias\ffmpeg-4.1.3-win64-static\bin\ffmpeg.exe";
        public WinFormSetting()
        {
            InitializeComponent();
            myOwnInitialize();
            //key input
            textBoxNewSettingName.KeyPress += enterNewProjectName;

            //find ffmpeg
            ffmpegtool = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\ffmpeg.exe";
        }
        /// <summary>
        /// Take the name and create the project.
        /// </summary>
        private void enterNewProjectName(object sender, KeyPressEventArgs k)
        {

            if (k.KeyChar == (char)Keys.Return)
            {
                if (!textBoxNewSettingName.Text.Equals(""))
                {
                    this.buttonAccept_Click(sender, k);
                }
            }
        }
        /// <summary>
        /// Show it in center.
        /// </summary>
        public void centerShow()
        {
            //center it
            this.CenterToScreen();
            this.ShowDialog();
        }
        /// <summary>
        /// Hide from alt+tab.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x80;
                return cp;
            }
        }

        public string[] SecondaryUrl { get => secondaryUrl; set => secondaryUrl = value; }
        public string[] MainUrl { get => mainUrl; set => mainUrl = value; }
        public static int LastIndex { get => lastIndex; set => lastIndex = value; }
        public ComboBox ComboBoxSetting { get => comboBoxSetting; set => comboBoxSetting = value; }
        public bool CanShow { get => canShow; set => canShow = value; }

        /// <summary>
        /// My own initialize component.
        /// </summary>
        private void myOwnInitialize()
        {
            // mpv.MediaResumed += setAvailable;
            //set the text in bot for align the textbox
            //label main and label secondary
            labelMain1.TextAlign = ContentAlignment.BottomCenter;
            labelMain2.TextAlign = ContentAlignment.BottomCenter;
            labelMain3.TextAlign = ContentAlignment.BottomCenter;
            labelMain4.TextAlign = ContentAlignment.BottomCenter;

            labelSecondary1.TextAlign = ContentAlignment.BottomCenter;
            labelSecondary2.TextAlign = ContentAlignment.BottomCenter;
            labelSecondary3.TextAlign = ContentAlignment.BottomCenter;
            labelSecondary4.TextAlign = ContentAlignment.BottomCenter;

            this.VisibleChanged += notShowWriteInTextBox;
            //add the button event, all main button do the same thing
            buttonMain1.Click += buttonMainClickEvent;
            buttonMain2.Click += buttonMainClickEvent;
            buttonMain3.Click += buttonMainClickEvent;
            buttonMain4.Click += buttonMainClickEvent;

            //add text change event, main
            textBoxMain1.TextChanged += textBoxMain_TextChanged;
            textBoxMain2.TextChanged += textBoxMain_TextChanged;
            textBoxMain3.TextChanged += textBoxMain_TextChanged;
            textBoxMain4.TextChanged += textBoxMain_TextChanged;

            //add text change event, url
            textBoxSecondary1.TextChanged += textBoxSecondary_TextChanged;
            textBoxSecondary2.TextChanged += textBoxSecondary_TextChanged;
            textBoxSecondary3.TextChanged += textBoxSecondary_TextChanged;
            textBoxSecondary4.TextChanged += textBoxSecondary_TextChanged;

            //store last index
            this.FormClosed += storeIndexClean;

            //check document
            if (File.Exists(documentsPath) && !File.ReadAllText(documentsPath).Equals(""))
            {
                //save setting if have content
                settings= JsonConvert.DeserializeObject<List<Setting>>
                    (AES128.Decrypt(File.ReadAllText(documentsPath)));
                
                //if empty
                if (settings == null)
                {
                    //create new one
                    settings = new List<Setting>();
                }
                //foreach and add to textbox
                foreach (Setting s in settings)
                {
                    comboBoxSetting.Items.Add(s);
                    mapSetting.Add(s.Name, s);
                }
                //select when have more than 1 object
                if (settings.Count != 0)
                {
                    //select the first
                    comboBoxSetting.SelectedIndex = 0;
                    lastIndex = 0;
                }

            }
            else
            {
                settings = new List<Setting>();
            }
            //add the label 
            allInformation.Add(labelNameVideo);
            allInformation.Add(labelResolution);
            allInformation.Add(labelBitRate);
            allInformation.Add(labelFPS);
        }
        /// <summary>
        /// Store the last index in the combobox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void storeIndexClean(object sender, EventArgs e)
        {
            if (!comboBoxSetting.Text.Equals(""))
            {
                LastIndex = comboBoxSetting.SelectedIndex;
            }
            //clean mpv
            this.mpv.Dispose();

            //reset text
            labelNameVideo.ResetText();
            labelResolution.ResetText();
            labelAvailable.ResetText();
            labelFPS.ResetText();
            labelBitRate.ResetText();
        }

        /// <summary>
        /// Set text to available
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setAvailable(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                SetAvailableText s = new SetAvailableText(setAvailable);
                this.Invoke(s, new object[] { sender, e });
            }
            else
                this.labelAvailable.Text = Available;
        }
        private delegate void SetAvailableText(object sender, EventArgs e);
        /// <summary>
        /// When the main text is valid, show the information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMainClickEvent(object sender, EventArgs e)
        {
            mpv.Stop();
            Button b = sender as Button;
            string url = null;
            //check all
            if (b == buttonMain1)
            {
                url = textBoxMain1.Text;
                MainUrl[0] = url;
            }
            else if (b == buttonMain2)
            {
                url = textBoxMain2.Text;
                MainUrl[1] = url;
            }
            else if (b == buttonMain3)
            {
                url = textBoxMain3.Text;
                MainUrl[2] = url;
            }
            else if (b == buttonMain4)
            {
                url = textBoxMain4.Text;
                MainUrl[3] = url;
            }
            if (url != null && !url.Equals(""))
            {
                //store the info
                string info = checkUrl(url);
                if (info != null)
                {
                    showInformation(url, info);
                }
                else
                {
                    labelAvailable.Text = noAvailable;
                    labelNameVideo.ResetText();
                    labelResolution.ResetText();
                    labelFPS.ResetText();
                    labelBitRate.ResetText();
                }
            }
            else
            {
                MessageBox.Show("Url invalida");
            }
        }
        /// <summary>
        /// Show the information with the url.
        /// </summary>
        /// <param name="url"></param>
        private void showInformation(string url, string info)
        {
            try
            {
                //play the video
                this.mpv.AutoPlay = true;
                this.mpv.Load(url, null);
                //show the info, 0 widht X height, 2 name
                string[] information = info.Split(';');
                this.labelAvailable.Text = Available;
                for (int i = 0; i < allInformation.Count; i++)
                {
                    //add the head
                    switch (i)
                    {
                        case 0:
                            (allInformation[i] as Label).Text = "Name:";
                            break;
                        case 1:
                            (allInformation[i] as Label).Text = "Resolution";
                            break;
                        case 2:
                            (allInformation[i] as Label).Text = "Bit rate:";
                            break;
                        case 3:
                            (allInformation[i] as Label).Text = "FPS:";
                            break;
                    }
                    //add the text
                    if (information.Length - 1 >= i && !information[i].Equals(""))
                    {
                        (allInformation[i] as Label).Text += information[i];
                    }
                    else
                    {
                        (allInformation[i] as Label).Text += "No info";
                    }
                }

            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }
        /// <summary>
        /// Check the url, in case of http,https,rstp if is available or not.
        /// </summary>
        /// <param name="url"></param>
        private string checkUrl(string url)
        {
            try
            {
                Uri uri = new Uri(url);
                if (uri.Scheme.Equals("rtsp"))
                {
                    string info = GetVideoInfo(url);
                    if (!info.Equals(""))
                        return info;
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }
        public string GetVideoInfo(string input)
        {
            string info = "";
            //  set up the parameters for video info.
            string @params = "-stimeout -1 -rtsp_transport tcp " +
                "-rtsp_flags prefer_tcp -i " + input;
            string output = Run(ffmpegtool, @params);
            string resolutionRegex = "(\\d+)x(\\d+)";
            string nameRegex = "(title\\s*:\\s*)(.+)";
            Match nameInfo = getInfoFromOutput(output, nameRegex);
            if (nameInfo.Success)
            {
                //get name
                info += nameInfo.Groups[2].Value;
            }
            //match the info then check it
            Match reInfo = getInfoFromOutput(output, resolutionRegex);
            if (reInfo.Success)
            {
                //get resolution info
                int width = 0; int height = 0;
                int.TryParse(reInfo.Groups[1].Value, out width);
                int.TryParse(reInfo.Groups[2].Value, out height);
                info += ";" + width + " x" + height;
            }

            //get bit rate
            string bitRateRegex = "(bitrate\\s*:\\s*)(.+)";
            Match bitRateInfo = getInfoFromOutput(output, bitRateRegex);
            if (bitRateInfo.Success)
            {
                //get rate and add it
                info += ";" + bitRateInfo.Groups[2].Value;
            }
            //get fps
            string fpsRegex = "(,\\s*)(\\d{2,3})(\\s*fps\\s*,)";
            Match fpsInfo = getInfoFromOutput(output, fpsRegex);
            if (bitRateInfo.Success)
            {
                //get name
                info += ";" + fpsInfo.Groups[2].Value;
            }

            return info;
        }

        private Match getInfoFromOutput(string output, string regex)
        {
            //get the video format
            var re = new Regex(regex);/*("(\\d{2,3})x(\\d{2,3})");*/
            Match info = re.Match(output);
            return info;
        }
        /// <summary>
        /// Run ffmpeg for get rtsp information.
        /// </summary>
        /// <param name="process"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static string Run(string process/*ffmpegFile*/, string parameters)
        {
            //check ffmpeg
            if (!File.Exists(process))
                throw new Exception(string.Format("Cannot find {0}.", process));

            //  Create a process info.
            ProcessStartInfo oInfo = new ProcessStartInfo();
            oInfo.FileName = process;
            oInfo.Arguments = parameters;
            oInfo.UseShellExecute = false;
            oInfo.CreateNoWindow = true;
            oInfo.RedirectStandardOutput = true;
            oInfo.RedirectStandardError = true;
            //  Create the output and streamreader to get the output.
            string output = null;
            StreamReader outputStream = null;

            //  Try the process.
            try
            {
                //  Run the process.
                Process proc = System.Diagnostics.Process.Start(oInfo);
                //Stopwatch stopwatch = new Stopwatch();
                //stopwatch.Start();
                //while (!proc.HasExited)
                //{
                //    if (stopwatch.ElapsedMilliseconds > 10000)
                //    {
                //        proc.Kill();
                //        return "";
                //    }
                //}


                //read it
                outputStream = proc.StandardError;
                output = outputStream.ReadToEnd();
                proc.Close();
            }
            catch (Exception ex)
            {
                output = ex.Message;
            }
            finally
            {
                //  Close out the streamreader.
                if (outputStream != null)
                    outputStream.Close();
            }
            return output;
        }
        /// <summary>
        /// Select anothe label.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notShowWriteInTextBox(object sender, EventArgs e)
        {
            //for not show writting in textbox
            this.labelMain1.Select();
        }
        /// <summary>
        /// When open load the settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WinFormSetting_Load(object sender, EventArgs e)
        {
            //create new one
            this.mpv = new MpvPlayer(this.pictureBoxPrevisualize.Handle);
            //if have not showed before, create it, else show it with last index
            if (LastIndex != -1)
            {
                comboBoxSetting.SelectedItem = LastIndex;
            }
        }

        /// <summary>
        /// When clicked store in json the actual setting.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveSetting_Click(object sender, EventArgs e)
        {
            //clean it
            textBoxNewSettingName.ResetText();
            //show it
            panelNewSettingName.Show();
            //select it
            textBoxNewSettingName.Select();
        }
        /// <summary>
        /// When text change store the url.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxMain_TextChanged(object sender, EventArgs e)
        {
            TextBox t = sender as TextBox;
            //check all and save it
            if (t == textBoxMain1)
            {
                this.MainUrl[0] = t.Text;
            }
            else if (t == textBoxMain2)
            {
                this.MainUrl[1] = t.Text;

            }
            else if (t == textBoxMain3)
            {
                this.MainUrl[2] = t.Text;

            }
            else if (t == textBoxMain4)
            {
                this.MainUrl[3] = t.Text;
            }
        }
        /// <summary>
        /// When text change store the url.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxSecondary_TextChanged(object sender, EventArgs e)
        {
            TextBox t = sender as TextBox;
            //check all and save it
            if (t == textBoxSecondary1)
            {
                this.SecondaryUrl[0] = t.Text;
            }
            else if (t == textBoxSecondary2)
            {
                this.SecondaryUrl[1] = t.Text;

            }
            else if (t == textBoxSecondary3)
            {
                this.SecondaryUrl[2] = t.Text;

            }
            else if (t == textBoxSecondary4)
            {
                this.SecondaryUrl[3] = t.Text;
            }
        }

        /// <summary>
        /// When change, rewrite.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxSetting_SelectedIndexChanged(object sender, EventArgs e)
        {
            //take the item then casting
            Setting s = comboBoxSetting.Items[comboBoxSetting.SelectedIndex] as Setting;
            //rewrite it
            rewriteAll(s);
        }

        /// <summary>
        /// Rewrite all textbox with setting given.
        /// </summary>
        /// <param name="setting"></param>
        private void rewriteAll(Setting setting)
        {
            //run all and rewrite if have text, else set empty "" text
            for (int i = 0; i < setting.MainUrl.Length; i++)
            {
                if (setting.MainUrl[i] != null)
                {
                    this.textBoxMainSetText(i, setting.MainUrl[i]);
                }
                else
                {
                    this.textBoxMainSetText(i, "");
                }
                if (setting.SecondaryUrl[i] != null)
                {
                    this.textBoxSecondarySetText(i, setting.SecondaryUrl[i]);
                }
                else
                {
                    this.textBoxSecondarySetText(i, "");
                }
            }
        }
        /// <summary>
        /// Set text with the number to the texbox.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="text"></param>
        private void textBoxMainSetText(int i, string text)
        {
            switch (i)
            {
                case 0:
                    textBoxMain1.Text = text;
                    break;
                case 1:
                    textBoxMain2.Text = text;
                    break;
                case 2:
                    textBoxMain3.Text = text;
                    break;
                case 3:
                    textBoxMain4.Text = text;
                    break;
            }
        }
        /// <summary>
        /// Set text with the number to the texbox.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="text"></param>
        private void textBoxSecondarySetText(int i, string text)
        {
            switch (i)
            {
                case 0:
                    textBoxSecondary1.Text = text;
                    break;
                case 1:
                    textBoxSecondary2.Text = text;
                    break;
                case 2:
                    textBoxSecondary3.Text = text;
                    break;
                case 3:
                    textBoxSecondary4.Text = text;
                    break;
            }
        }
        /// <summary>
        /// Save the json file with the last index first
        /// </summary>
        public void SaveJsonWithTheLastIndexFirst()
        {
            //new list for store
            List<Setting> settings = new List<Setting>();
            //check have index or not
            if (LastIndex != -1)
                //add the last first
                settings.Add(mapSetting[(comboBoxSetting.Items[LastIndex] as Setting).Name]);
            //add all, but last index not
            for (int i = 0; i < ComboBoxSetting.Items.Count; i++)
            {
                if (i != LastIndex)
                {
                    settings.Add(mapSetting[(comboBoxSetting.Items[i] as Setting).Name]);
                }
            }
            //save it
            Encrypt(settings);
        }

        /// <summary>
        /// Close the panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            panelNewSettingName.Visible = false;
        }

        /// <summary>
        /// Create new setting or modify with the name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAccept_Click(object sender, EventArgs e)
        {
            Setting s = null;
            if (!textBoxNewSettingName.Text.Equals(""))
                if (!mapSetting.ContainsKey(textBoxNewSettingName.Text))
                {
                    //create
                    s = new Setting(textBoxNewSettingName.Text, this.MainUrl.Clone() as string[], this.SecondaryUrl.Clone() as string[]);
                    //add it
                    settings.Add(s);
                    mapSetting.Add(textBoxNewSettingName.Text, s);
                    //add to combobox
                    comboBoxSetting.Items.Add(s);
                }
                else
                {
                    //new setting
                    s = mapSetting[textBoxNewSettingName.Text];
                    s.MainUrl = this.mainUrl.Clone() as string[];
                    s.SecondaryUrl = this.secondaryUrl.Clone() as string[];
                }
            //encrypt it
            Encrypt(settings);

            //close it
            panelNewSettingName.Visible = false;

            if (s != null)
                //move index
                comboBoxSetting.SelectedIndex = comboBoxSetting.Items.IndexOf(s);

        }

        /// <summary>
        /// Encrypt the content and write it.
        /// </summary>
        /// <param name="settings"></param>
        private void Encrypt(List<Setting> settings)
        {
            //save list every time when save clicked
            StreamWriter sw = File.CreateText(documentsPath);
            sw.Write(AES128.Encrypt(JsonConvert.SerializeObject(settings)));
            sw.Close();            
        }
    }
}

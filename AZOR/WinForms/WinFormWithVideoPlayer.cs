using System;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using CircularProgressBar;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace AZOR
{
    public partial class WinFormWithVideoPlayer : Form
    {
        #region Variables 
        /// <summary>
        /// Check video is reverse playing or not
        /// </summary>
        private bool reversePlaying = false;
        /// <summary>
        /// When clip save, the name.
        /// </summary>
        private readonly string clipFileName = "clip.azorClipFile";
        /// <summary>
        /// When tag save, the name.
        /// </summary>
        private readonly string tagFileName = "tag.azorTagFile";
        /// <summary>
        /// Event for control video loads
        /// </summary>
        private ManualResetEvent manualResetEventMpvUnlock = new ManualResetEvent(false);
        /// <summary>
        /// Set to true when is closed, and need to wait convert video.
        /// </summary>
        private bool WasClosed = false;
        /// <summary>
        /// Default clip name when press i.
        /// </summary>
        private string defaultClipName = "DefaultClip ";
        /// <summary>
        /// Save all clips here as value.
        /// </summary>
        Dictionary<CheckBox, Clip> mapAllClip = new Dictionary<CheckBox, Clip>();
        /// <summary>
        /// String store close program message.
        /// </summary>
        private string CloseProgramMessage = "¿Estas seguro de cerrar el programa?";
        /// <summary>
        /// String close program dialog name.
        /// </summary>
        private string CloseProgramDialogName = "Cerrar programa";
        /// <summary>
        /// Save the playing signal.
        /// </summary>
        private int playingSignal = 0;
        /// <summary>
        /// Global message for not creating all time.
        /// </summary>
        private Message GlobalMessage = new Message();
        /// <summary>
        /// Check video is loaded or not.
        /// </summary>
        private bool loaded = false;
        /// <summary>
        /// Is the same to signal wait in c.
        /// </summary>
        private ManualResetEvent ManualResetEventMedia = new ManualResetEvent(false);
        /// <summary>
        /// Sync winform.
        /// </summary>
        private WinFormSync WinFormSync;
        /// <summary>
        /// Analysys window.
        /// </summary>
        private WinFormAnalysis winFormAnalysis;
        /// <summary>
        /// Store the process with control.
        /// </summary>
        private Dictionary<Process, Control> convertProcessMap = new Dictionary<Process, Control>();
        /// <summary>
        /// Can invoke or not.
        /// </summary>
        private bool CanInvoke = true;
        /// <summary>
        /// Store the url to set camera change.
        /// </summary>
        private ArrayList changeCameraUrl = new ArrayList();
        /// <summary>
        /// Store all video names.
        /// </summary>
        private ArrayList videoNames = new ArrayList();
        /// <summary>
        /// Boolean for check convert video or not.
        /// </summary>
        private bool convertVideo = false;
        /// <summary>
        /// If position was in zero.
        /// </summary>
        private bool inZero = false;
        /// <summary>
        /// When change camera true.
        /// </summary>
        private Boolean changeCameraBool = false;
        /// <summary>
        /// Store the animation in the map for delete.
        /// </summary>
        private Dictionary<Button, CircularProgressBar.CircularProgressBar> mapAnimation = new Dictionary<Button,
            CircularProgressBar.CircularProgressBar>();
        /// <summary>
        /// Store the first timespan.
        /// </summary>
        private Dictionary<Button, TimeSpan> manualTagMap = new Dictionary<Button, TimeSpan>();
        /// <summary>
        /// Panel that allow to create new tag.
        /// </summary>
        private PanelTagForm panelTag = new PanelTagForm();
        /// <summary>
        /// Size of round in tablelayoutclipspanel.
        /// </summary>
        private Size roundSize = new Size(8, 15);

        /// <summary>
        /// Count the clips when i is pressed;
        /// </summary>
        private int clipCount = 1;
        /// <summary>
        /// Declare tooltip for multiuse and not create all time when need.
        /// </summary>
        private System.Windows.Forms.ToolTip tip = new System.Windows.Forms.ToolTip();
        /// <summary>
        /// Store all the clip name for show.
        /// </summary>
        private Dictionary<Label, String> mapFullClipName = new Dictionary<Label, string>();
        /// <summary>
        /// Maximun clip name length.
        /// </summary>
        private static readonly int LIMIT_CLIP_NAME = 14;
        /// <summary>
        /// Store project path.
        /// </summary>
        private string path;
        /// <summary>
        /// Save all !!!!checkedBox, for fastly foreach.
        /// </summary>
        private ArrayList checkedClipListBox = new ArrayList();
        /// <summary>
        /// One button have one tag.
        /// </summary>
        private Dictionary<Button, Tag> tagMap = new Dictionary<Button, Tag>();
        /// <summary>
        /// Current winform widht.
        /// </summary>
        private float currentWidht;
        /// <summary>
        /// Current winform height.
        /// </summary>
        private float currentHeight;
        /// <summary>
        /// Stopwatch for show the time recording.
        /// </summary>
        private Stopwatch stopwatchVideoRecording = new Stopwatch();
        /// <summary>
        /// Timelabel size
        /// </summary>
        private Size labelTimeSize = new Size(127, 24);
        /// <summary>
        /// Store the control when mouse right click on time.
        /// </summary>
        private Control lastRowClicked;
        /// <summary>
        /// Store the label for enable=false when camera change.
        /// </summary>
        private Label labelForDesactivate;
        /// <summary>
        /// Count videos for create.
        /// </summary>
        private int numeroVideos = 4;
        /// <summary>
        /// Keys used for change camera and store the time.
        /// </summary>
        #region list of keys
        private static Keys playCameraOne = Keys.D1;
        private static Keys playCameraTwo = Keys.D2;
        private static Keys playCameraThree = Keys.D3;
        private static Keys playCameraFour = Keys.D4;
        private static Keys storeCurrentTime = Keys.I;
        #endregion        
        /// <summary>
        /// FFMPEGTOOL path in current pc, change if is in another path.
        /// </summary>
        private static string ffmpegtool;//= @"C:\Users\Developer Lee\source\repos\librerias\ffmpeg-4.1.3-win64-static\bin\ffmpeg.exe";

        /// <summary>
        /// Path received to store videos.
        /// </summary>
        private string pathStoreVideo;
        /// <summary>
        /// Use mpvPersonalized for display videos.
        /// </summary>
        private MpvPersonalized mpvPersonalized;
        /// <summary>
        /// Save in string the video that are playing.
        /// </summary>
        private string pathVideoPlaying = null;
        /// <summary>
        /// Store the timespan when change camera, reload video, etc.
        /// </summary>
        private TimeSpan currentTimeSpan;
        /// <summary>
        /// Store all the process opened for stream/download videos.
        /// </summary>
        private ArrayList arrayListProcessForDownloadVideo = new ArrayList();
        /// <summary>
        /// Directory for store tag.
        /// </summary>
        private String pathStoreTagAndClips;
        /// <summary>
        /// Setting form.
        /// </summary>
        private WinFormSetting settingForm;

        public ArrayList ChangeCameraUrl { get => changeCameraUrl; set => changeCameraUrl = value; }
        public TimeSpan CurrentTimeSpan { get => currentTimeSpan; set => currentTimeSpan = value; }
        public bool InZero { get => inZero; set => inZero = value; }
        public MpvPersonalized MpvPersonalized { get => mpvPersonalized; set => mpvPersonalized = value; }
        public ManualResetEvent ManualResetEventMpvUnlock { get => manualResetEventMpvUnlock; set => manualResetEventMpvUnlock = value; }
        #endregion

        /// <summary>
        /// Class constructor, initialize comomponents and ajust same variables.
        /// </summary>
        /// <param name="pathStoreVideos">Path received to store videos.</param>
        public WinFormWithVideoPlayer(string path, WinFormSetting settingForm)
        {
            InitializeComponent();
            //set win name as the same to the project name
            this.Text = Path.GetFileName(path);
            //store the path
            this.path = path;
            pathStoreVideo = path + "\\Sources\\temp";
            pathStoreTagAndClips = path + "\\Sources";
            myOwnInitializeComponent();
            //save settings
            this.settingForm = settingForm;
            //if can show, show it,else close functions
            if (!settingForm.CanShow)
            {
                openProjectSet();
                buttonAnalysis.Enabled = true;
                this.buttonSync.Enabled = true;
            }
            else
            {
                convertVideo = true;
            }
        }
        /// <summary>
        /// If the project is openned, set id.
        /// </summary>
        private void openProjectSet()
        {
            //set button enable to false
            this.buttonSetting.Enabled = false;
            this.buttonRecordVideo.Enabled = false;
            //set the button image
            this.buttonRecordVideo.Image = Properties.Resources.stopButton;
            //get all files in videos
            DirectoryInfo videoDirectory = new DirectoryInfo(this.path + "\\Sources\\videos");
            foreach (var file in videoDirectory.GetFiles())
            {
                //only store 4
                if (ChangeCameraUrl.Count <= 4)
                    ChangeCameraUrl.Add(file.FullName);
            }
            //if have video, play the first
            if (this.ChangeCameraUrl.Count > 0)
            {
                //store the path playing
                this.pathVideoPlaying = ChangeCameraUrl[0] as string;
                //when can see show the time
                this.Shown += new EventHandler(SetRecordTime);
                //call it for add all functions
                timerEndProgressBar_Tick(null, null);
                //stop the reload timer
                timerReload.Stop();
            }

        }
        /// <summary>
        /// Set the record time, then delete myself.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetRecordTime(object sender, EventArgs e)
        {
            //thread safe call
            if (this.labelRecordTime.InvokeRequired)
            {
                //same parameters
                setTextBotTime s = new setTextBotTime(SetRecordTime);
                if (CanInvoke)
                    this.Invoke(s, new object[] { sender, e });
            }
            else
            {
                //set record time
                this.labelRecordTime.Text = this.mpvPersonalized.MpvPlayer.Duration.ToString(@"hh\:mm\:ss");
                mpvPersonalized.MpvPlayer.MediaLoaded -= SetRecordTime;
            }
        }
        /// <summary>
        /// My own component initialize.
        /// Adjust the components.
        /// </summary>
        private void myOwnInitializeComponent()
        {
            //scroll time ajust
            tableLayoutPanelClips.AutoSize = true;
            tableLayoutPanelClips.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanelClips.AutoScroll = false;

            tableLayoutPanelTags.AutoSize = true;
            tableLayoutPanelTags.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanelTags.AutoScroll = false;

            panelClipsSaved.AutoScroll = true;
            panelClipsSaved.AutoSize = false;
            tableLayoutPanelClips.Padding = new Padding(0, 0, SystemInformation.VerticalScrollBarWidth, 0);

            //allow threadcall (can modify variables, call functions, etc)
            //Control.CheckForIllegalCrossThreadCalls = false;
            //activate keyinput
            KeyPreview = true;
            //form closing event
            this.FormClosing += closingEvent;
            //create the mpv and ajust some variables
            createMPVAndAjust();

            //transparent components
            this.mpvPictureBox.BackColor = System.Drawing.Color.Transparent;

            //add functions
            panelClipsSaved.Click += rightClickOnClipPanel;

            //adjust paneltag
            panelTag.VisibleChanged += createOrNotButtonTag;

            //enable controls when progress bar close
            labelProgressBarLoading.VisibleChanged += EnableControls;
            //for the same state, when the main is paused pause the analysis
            this.buttonPlay.VisibleChanged += SetAnalysisVideoState;

            //create analysis view
            winFormAnalysis = new WinFormAnalysis(mpvPersonalized, this);

            //create sync view
            WinFormSync = new WinFormSync(this);

            //Load tags when openned
            LoadTagFromDefaultPath();
            //load clips when openned
            LoadClipsInDefaultPath();

            //find ffmpeg
            ffmpegtool = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\ffmpeg.exe";

            //add more key events
            this.KeyPress += KeyPressAddSpeedFunctions;//more speed
            this.KeyPress += KeyPressReverseSpeedFunctions;
        }
        /// <summary>
        /// Reverse play simulation changing time span.
        /// </summary>
        private void KeyPressReverseSpeedFunctions(object sender, KeyPressEventArgs k)
        {
            int interval = -1;
            //if x pressed, speed -2
            if (k.KeyChar == 'x' || k.KeyChar == 'X')
            {
                interval = 250;
            }
            //speed X-4
            else if (k.KeyChar == 'z' || k.KeyChar == 'Z')
            {
                interval = 125;
            }
            //speed X-8
            else if (k.KeyChar == 'k' || k.KeyChar == 'K')
            {
                interval = 72;
            }
            //speed x-16
            else if (k.KeyChar == 'j' || k.KeyChar == 'J')
            {
                interval = 36;
            }
            //speed X-32
            else if (k.KeyChar == 'g' || k.KeyChar == 'G')
            {
                interval = 18;
            }
            //speed x-64
            else if (k.KeyChar == 'd' || k.KeyChar == 'D')
            {
                interval = 9;
            }
            //speed x-128
            else if (k.KeyChar == 'e' || k.KeyChar == 'E')
            {
                interval = 1;
            }
            if (interval != -1)
            {
                //stop the timer
                timerReversePlay.Stop();
                //new interval
                timerReversePlay.Interval = interval;
                //run it again
                AfterReverseKeyPressed();
            }
        }

        /// <summary>
        /// General code after reverse key is pressed.
        /// </summary>
        private void AfterReverseKeyPressed()
        {

            //set true
            reversePlaying = true;
            timerReversePlay.Start();
            mpvPersonalized.MpvPlayer.Pause();
            buttonPause.Visible = true;
            buttonPlay.Visible = false;
        }
        /// <summary>
        /// Add more functions to mpv, more speed play back,slower,etc.
        /// </summary>
        private void KeyPressAddSpeedFunctions(object sender, KeyPressEventArgs k)
        {
            double speed = -1;
            //if ¡ pressed, speed 0.25
            if (k.KeyChar == '¡')
            {
                speed = 0.25;
            }
            //speed x0.5
            else if (k.KeyChar == '\'')
            {
                speed = 0.5;
            }
            //speed x0.75
            else if (k.KeyChar == '0')
            {
                speed = 0.75;
            }
            //speed x2
            else if (k.KeyChar == 'c' || k.KeyChar == 'C')
            {
                speed = 2;
            }
            //speed x4
            else if (k.KeyChar == 'v' || k.KeyChar == 'V')
            {
                speed = 4;
            }
            //speed x8
            else if (k.KeyChar == 'b' || k.KeyChar == 'B')
            {
                speed = 8;
            }
            //speed x16
            else if (k.KeyChar == 'n' || k.KeyChar == 'N')
            {
                speed = 16;
            }
            //speed x32
            else if (k.KeyChar == ',')
            {
                speed = 32;
            }
            //speed x64
            else if (k.KeyChar == '.')
            {
                speed = 64;
            }
            //speed x100
            else if (k.KeyChar == '-')
            {
                speed = 100;
            }
            if (speed != -1)
            {
                //check reverse play
                if (reversePlaying)
                {
                    //if was playing
                    if (!buttonPlay.Visible)
                        //play it
                        MpvPersonalized.MpvPlayer.Resume();
                }
                //stop the timer
                timerReversePlay.Stop();
                //set the time
                mpvPersonalized.MpvPlayer.Speed = speed;
                //set to false
                reversePlaying = false;
            }
        }
        /// <summary>
        /// Set the state for analysis video.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetAnalysisVideoState(object sender, EventArgs e)
        {
            lock (this.buttonPlay)
                //if button play is visible, mean video is paused.
                if (this.buttonPlay.Visible)
                {
                    SetAnalysisStateWithBoolean(false);
                }
                else
                {
                    SetAnalysisStateWithBoolean(true);
                }
        }
        /// <summary>
        /// With the boolean, reloadone, two or four, set the state.
        /// </summary>
        /// <param name="state">True for play, false for pause.</param>        
        private void SetAnalysisStateWithBoolean(bool state)
        {
            /*check set state for one, two or four video*/
            if (winFormAnalysis.ReloadOne)
            {
                if (state)
                    winFormAnalysis.MpvPersonalized.MpvPlayer.Resume();
                else
                    winFormAnalysis.MpvPersonalized.MpvPlayer.Pause();
            }
            else if (winFormAnalysis.ReloadTwo)
            {
                if (state)
                    foreach (MpvPersonalized mpv in winFormAnalysis.TwoMainMpv)
                    {
                        mpv.MpvPlayer.Resume();
                    }
                else
                    foreach (MpvPersonalized mpv in winFormAnalysis.TwoMainMpv)
                    {
                        mpv.MpvPlayer.Pause();
                    }
            }
            else if (winFormAnalysis.ReloadFour)
            {
                if (state)
                    foreach (MpvPersonalized mpv in winFormAnalysis.FourMpv)
                    {
                        mpv.MpvPlayer.Resume();
                    }
                else
                    foreach (MpvPersonalized mpv in winFormAnalysis.FourMpv)
                    {
                        mpv.MpvPlayer.Pause();
                    }
            }
        }
        /// <summary>
        /// Enable controls when visible change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnableControls(object sender, EventArgs e)
        {
            if (!progressBarLoadingVideo.Visible)
            {
                //when progress bar finish, enable it
                buttonAnalysis.Enabled = true;
                this.buttonSync.Enabled = true;
            }
        }
        /// <summary>
        /// Crete or not the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createOrNotButtonTag(object sender, EventArgs e)
        {
            //when panel tag visible pause the layout
            if (panelTag.Visible)
            {
                this.SuspendLayout();
            }
            else
            {
                this.ResumeLayout();
            }
            //check have information or not for create tag button
            if (panelTag.HaveInformation)
            {
                createAutomaticOrManualTagButton(panelTag.NewTagInfo);
            }
        }
        /// <summary>
        /// Create the automatic button and return it for use if is necesary.
        /// </summary>
        private Button createAutomaticOrManualTagButton(Tag tag)
        {
            //create the button
            Button tagButton = createButtonTag(tag);
            //add to tag
            AddTag(tag, tagButton);
            //scroll it
            panelTags.ScrollControlIntoView(tagButton);
            //create=>information used=>not have information now
            panelTag.HaveInformation = false;
            //return it
            return tagButton;
        }
        /// <summary>
        /// Delete functions when the form is closing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closingEvent(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show(CloseProgramMessage, CloseProgramDialogName,
                MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                //set to true
                WasClosed = true;
                //save tag in default path
                this.SaveTagInDefaultPath();
                //save all the clips
                SaveClipsInDefaultPath();
                //after kill all convert video
                if (convertVideo)
                {
                    //same things
                    this.stopButtonPressedEvent(null, null);
                    //send to back
                    mpvPictureBox.SendToBack();
                }
                this.FormClosing -= closingEvent;
                lock (convertProcessMap)
                    //if have process
                    if (convertProcessMap.Count > 0)
                        //dont close, wait complete                    
                        (e as FormClosingEventArgs).Cancel = true;
            }
            else
            {
                //Do nothing
                (e as FormClosingEventArgs).Cancel = true;
            }
            //save json
            settingForm.SaveJsonWithTheLastIndexFirst();
            //no invoke more
            CanInvoke = false;

        }
        /// <summary>
        /// Save all clips in default path.
        /// </summary>
        private void SaveClipsInDefaultPath()
        {
            //save it
            StreamWriter sw = File.CreateText(pathStoreTagAndClips + "\\data\\" + clipFileName);
            //encrypt it and save it
            sw.Write(AES128.Encrypt(JsonConvert.SerializeObject(mapAllClip.Values)));
            //close it
            sw.Close();
            //save default clip count
            sw = File.CreateText(pathStoreTagAndClips + "\\data\\DefaultClipCount.txt");
            sw.Write(clipCount);
            //close it
            sw.Close();
        }
        /// <summary>
        /// Load clips from default path.
        /// </summary>
        private void LoadClipsInDefaultPath()
        {
            //if exist clips load it
            if (File.Exists(pathStoreTagAndClips + "\\data\\" + clipFileName))
            {
                List<Clip> listClips = JsonConvert.DeserializeObject<List<Clip>>
                    (AES128.Decrypt(File.ReadAllText(pathStoreTagAndClips + "\\data\\" + clipFileName)));
                foreach (Clip clip in listClips)
                {
                    storeInTableLayoutPanelClips(clip);
                }
            }
            //load default clip count
            //if exist clips load it
            if (File.Exists(pathStoreTagAndClips + "\\data\\DefaultClipCount.txt"))
            {
                //open it
                TextReader reader = File.OpenText(pathStoreTagAndClips + "\\data\\DefaultClipCount.txt");
                //read it
                clipCount = int.Parse(reader.ReadLine());
                //close it
                reader.Close();
            }
        }
        /// <summary>
        /// Create the mpv and ajust.
        /// </summary>
        private void createMPVAndAjust()
        {
            mpvPersonalized = new MpvPersonalized(this.mpvPictureBox.Handle);
            mpvPersonalized.MpvPlayer.AutoPlay = true;
            mpvPersonalized.MpvPlayer.MediaLoaded += setMediaPlayerTime;
            mpvPersonalized.MpvPlayer.KeepOpen = Mpv.NET.Player.KeepOpen.Yes;
            mpvPersonalized.MpvPlayer.PositionChanged += storeTimeChangeEvent;
            //add the event first time showed
            this.Shown += AddEventFirstTime;
        }
        /// <summary>
        /// Add event when first time showed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddEventFirstTime(object sender, EventArgs e)
        {
            mpvPersonalized.MpvPlayer.PositionChanged += ShowTimeChange;
        }
        /// <summary>
        /// Store time key is pressed, store the time and and ajust.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="keyEventArgs"></param>
        private void storeTimeEvent(object sender, KeyEventArgs keyEventArgs)
        {
            //check if have mdia loaded
            if (this.mpvPersonalized.MpvPlayer.IsMediaLoaded && !panelTag.Visible)
            {
                //check the keycode
                if (keyEventArgs.KeyCode == storeCurrentTime)
                {
                    //create new clip
                    Clip clip = new Clip(mpvPersonalized.MpvPlayer.Position.ToString(@"hh\:mm\:ss\.ff"),
                        mpvPersonalized.MpvPlayer.Position.ToString(@"hh\:mm\:ss\.ff"), defaultClipName + clipCount);
                    //count plus one
                    clipCount++;

                    //save color
                    clip.MyColor = Color.Black;
                    //store it
                    storeInTableLayoutPanelClips(clip);
                }
            }
        }
        /// <summary>
        /// Create the time label with the time span, hh:mm:ss ms.
        /// </summary>
        /// <returns>Return the label created.</returns>
        private Label createTimeLabel(string time)
        {
            Label newLabelTime = new Label();
            //cannot autosize, for make same size in all
            newLabelTime.AutoSize = false;
            //the same size all
            newLabelTime.Size = labelTimeSize;
            //set the text (time)
            newLabelTime.Text = mpvPersonalized.MpvPlayer.Position.ToString(@"hh\:mm\:ss\.ff");
            //add the right click function
            newLabelTime.Click += clickOnTimeLabel;
            //set time
            newLabelTime.Text = time;
            return newLabelTime;
        }
        /// <summary>
        /// Add the label to the table for show.
        /// </summary>
        /// <param name="labelToAdd">Label to add.</param>
        private void addTimeToTable(Label labelToAdd)
        {
            //adjust the label
            labelToAdd.Dock = DockStyle.Fill;
            labelToAdd.Margin = new Padding(0);
            //adjust the table
            this.tableLayoutPanelClips.RowCount++;
            this.tableLayoutPanelClips.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));
            tableLayoutPanelClips.Controls.Add(labelToAdd);

            panelClipsSaved.ScrollControlIntoView(labelToAdd);
        }
        /// <summary>
        /// Reload the video when is called.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void reloadVideo(object sender, EventArgs eventArgs)
        {
            //lock the mpv
            lock (mpvPersonalized.MpvPlayer.MpvLock)
            {
                //this two lines for now set text when video is reloading
                this.mpvPersonalized.MpvPlayer.MediaPaused -= videoPaused;
                this.mpvPersonalized.MpvPlayer.MediaResumed -= videoResumed;
                //add the new video
                this.addVideo();
            }
            //mpv unlock
            manualResetEventMpvUnlock.Set();
        }
        /// <summary>
        /// Set the image for playorpause button when paused
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void videoPaused(object sender, EventArgs eventArgs)
        {
            //check reverse play
            if (!reversePlaying)
                //thread safe call
                if (labelTimePlaying.InvokeRequired)
                {
                    //create the delegate for invoke
                    buttonPlayOrPauseShow b = new buttonPlayOrPauseShow(videoPaused);
                    //check can invoke or not
                    if (!this.IsDisposed && !mpvPersonalized.MpvPlayer.IsPlaying && CanInvoke)
                        this.Invoke(b, new object[] { sender, eventArgs });
                }
                else
                {
                    //show the button
                    buttonPlay.Show();
                    buttonPause.Visible = false;
                }
        }
        private delegate void buttonPlayOrPauseShow(object sender, EventArgs eventArgs);
        /// <summary>
        /// Set the image for playorpause button when paused
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void videoResumed(object sender, EventArgs eventArgs)
        {
            //check reverse play
            if (!reversePlaying)
                //thread safe call, its the same(videoPaused())
                if (labelTimePlaying.InvokeRequired)
                {
                    buttonPlayOrPauseShow b = new buttonPlayOrPauseShow(videoResumed);
                    if (!this.IsDisposed && mpvPersonalized.MpvPlayer.IsPlaying && CanInvoke)
                        this.Invoke(b, new object[] { sender, eventArgs });
                }
                else
                {
                    buttonPlay.Visible = false;
                    buttonPause.Show();
                }

        }
        /// <summary>
        /// Clean media player entries and add the video.
        /// </summary>
        private void addVideo()
        {
            //add the new video
            this.LoadNewVideo(this.mpvPersonalized);

            //add event
            this.mpvPersonalized.MpvPlayer.MediaPaused += videoPaused;
            this.mpvPersonalized.MpvPlayer.MediaResumed += videoResumed;

        }
        /// <summary>
        /// Load the new video.
        /// </summary>
        private void LoadNewVideo(MpvPersonalized mpv)
        {
            //enter zone
            Monitor.Enter(mpv.MpvPlayer.MpvLock);
            //wait event
            loaded = false;
            if (mpv.MpvPlayer.IsMediaLoaded || mpv != this.MpvPersonalized)
                loaded = true;
            mpv.PlayingMedia = pathVideoPlaying;
            //add the path again
            mpv.MpvPlayer.Load(pathVideoPlaying, currentTimeSpan.ToString());
            //check if loaded or not, is yes "unlock"
            if (!loaded && File.Exists(pathVideoPlaying))
            {
                //leave it
                Monitor.Exit(mpv.MpvPlayer.MpvLock);
                //wait signal
                ManualResetEventMedia.WaitOne();
                //enter
                Monitor.Enter(mpv.MpvPlayer.MpvLock);
            }
            //leave it
            Monitor.Exit(mpv.MpvPlayer.MpvLock);
        }
        /// <summary>
        /// Load the new video in analysis.
        /// </summary>
        private void LoadNewVideoAnalysis(MpvPersonalized mpv, string videoPath, int i)
        {
            //lock it
            Monitor.Enter(mpv.MpvPlayer.MpvLock);

            //set false for wait event
            winFormAnalysis.MapLoaded[mpv.MpvPlayer] = false;
            //if media alredy loaded then not wait event
            if (mpv.MpvPlayer.IsMediaLoaded)
                winFormAnalysis.MapLoaded[mpv.MpvPlayer] = true;
            //video path
            mpv.PlayingMedia = videoPath;
            //add the path again
            //check sync
            if (WinFormSync.TimeReference == -1)
                mpv.MpvPlayer.Load(mpv.PlayingMedia, currentTimeSpan.ToString());
            else
                //set time load
                mpv.MpvPlayer.Load(mpv.PlayingMedia, currentTimeSpan.Subtract(WinFormSync.MpvTimeSpan[playingSignal])
                        .Add(WinFormSync.MpvTimeSpan[i]).ToString());
            //if not loaded wait the event
            if (!winFormAnalysis.MapLoaded[mpv.MpvPlayer] && !mpv.MpvPlayer.IsMediaLoaded && File.Exists(mpv.PlayingMedia))
            {
                //leave it
                Monitor.Exit(mpv.MpvPlayer.MpvLock);
                Console.WriteLine("video " + mpv.PlayingMedia);
                winFormAnalysis.MapLoadedEvent[mpv.MpvPlayer].WaitOne();
                //try enter
                bool enter = Monitor.TryEnter(mpv.MpvPlayer.MpvLock, 1000);
                //try it till enter
                while (!enter)
                {
                    enter = Monitor.TryEnter(mpv.MpvPlayer.MpvLock, 1000);
                }
            }
            //pause or play
            if (this.buttonPlay.Visible)
                mpv.MpvPlayer.Pause();
            else
                mpv.MpvPlayer.Resume();
            Monitor.Exit(mpv.MpvPlayer.MpvLock);

        }
        /// <summary>
        /// Change the camera if the key is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="k"></param>
        private void changeCameraEvent(object sender, KeyEventArgs k)
        {
            //check video is playing,not writting
            if (pathVideoPlaying != null && !panelTag.Visible)
            {
                //check in all is not the same camera
                if (k.KeyCode == playCameraOne && !this.pathVideoPlaying.Equals(ChangeCameraUrl[0]))
                {
                    ajustCameraButton(labelSignal1, k);
                }
                else if (playCameraTwo == k.KeyCode && !this.pathVideoPlaying.Equals(ChangeCameraUrl[1]))
                {
                    ajustCameraButton(labelSignal2, k);
                }
                else if (playCameraThree == k.KeyCode && !this.pathVideoPlaying.Equals(ChangeCameraUrl[2]))
                {
                    ajustCameraButton(labelSignal3, k);
                }
                else if (playCameraFour == k.KeyCode && !this.pathVideoPlaying.Equals(ChangeCameraUrl[3]))
                {
                    ajustCameraButton(labelSignal4, k);
                }
            }
        }
        /// <summary>
        /// Ajust the button for camera change when is invoked.
        /// </summary>
        private void ajustCameraButton(Label label, KeyEventArgs k)
        {
            //set enable the last button that enabled=false
            labelForDesactivate.Enabled = true;
            //change the path and change the camera
            /*Set the new playing path, set the signal number, check if have to sync or not.
             */
            if (label == labelSignal1)
            {
                this.pathVideoPlaying = changeCameraUrl[0] as string;
                CheckTimeWithSync(0);
                playingSignal = 0;
            }
            else if (label == labelSignal2)
            {
                this.pathVideoPlaying = changeCameraUrl[1] as string;
                CheckTimeWithSync(1);
                playingSignal = 1;
            }
            else if (label == labelSignal3)
            {
                this.pathVideoPlaying = changeCameraUrl[2] as string;
                CheckTimeWithSync(2);
                playingSignal = 2;
            }
            else if (label == labelSignal4)
            {
                this.pathVideoPlaying = changeCameraUrl[3] as string;
                CheckTimeWithSync(3);
                playingSignal = 3;
            }
            changeCamera();
            //set enabled the button
            label.Enabled = false;
            //store current button
            labelForDesactivate = label;
        }
        /// <summary>
        /// Set the new current timespan with sync.
        /// </summary>
        /// <param name="i"></param>
        private void CheckTimeWithSync(int i)
        {
            //lock for safe
            lock (WinFormSync.MpvTimeSpan)
                //check if have save time or not
                if (WinFormSync.TimeReference != -1)
                {
                    //change the time
                    CurrentTimeSpan = currentTimeSpan.Subtract(WinFormSync.MpvTimeSpan[playingSignal])
                        .Add(WinFormSync.MpvTimeSpan[i]);
                }
        }
        /// <summary>
        /// Change the camera with the video path.
        /// </summary>
        private void changeCamera()
        {
            //lock it
            lock (mpvPersonalized.MpvPlayer.MpvLock)
            {
                changeCameraBool = true;
                //set autoplay
                setPauseOrPlayWhenRelaod();
                //if (mpvPersonalized.MpvPlayer.Position != TimeSpan.Zero)
                //    //when change camera store current time span, and then when change set it.
                //    this.CurrentTimeSpan = mpvPersonalized.MpvPlayer.Position;
                //stop the current video
                this.mpvPersonalized.MpvPlayer.Stop();
                //add video
                this.addVideo();
                changeCameraBool = false;
                //if reload one stop it
                if (winFormAnalysis.ReloadOne)
                    winFormAnalysis.MpvPersonalized.MpvPlayer.Stop();
                //set time
                SetAnalysisTime();
            }
            //mpv unlock
            manualResetEventMpvUnlock.Set();
        }
        /// <summary>
        /// Set play or pause when the video is reload.
        /// </summary>
        private void setPauseOrPlayWhenRelaod()
        {
            MpvSetPauseOrPlay(this.mpvPersonalized);
            ////check analysis is open or not
            if (winFormAnalysis.ReloadOne)
            {
                MpvSetPauseOrPlay(winFormAnalysis.MpvPersonalized);
            }
            else if (winFormAnalysis.ReloadFour)
            {
                foreach (MpvPersonalized mpv in winFormAnalysis.FourMpv)
                    MpvSetPauseOrPlay(mpv);
            }
            else if (winFormAnalysis.ReloadTwo)
                foreach (MpvPersonalized mpv in winFormAnalysis.TwoMainMpv)
                    MpvSetPauseOrPlay(mpv);

        }
        /// <summary>
        /// Set pause or play, if the button play is visible set to pause, else play.
        /// </summary>
        /// <param name="mpvPersonalized"></param>
        private void MpvSetPauseOrPlay(MpvPersonalized mpvPersonalized)
        {
            //if is playing then playing, if pause then pause
            if (this.buttonPlay.Visible)
            {
                mpvPersonalized.MpvPlayer.AutoPlay = false;
            }
            else
            {
                mpvPersonalized.MpvPlayer.AutoPlay = true;
            }
        }
        /// <summary>
        /// When tick then reload the video.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerReload_Tick(object sender, EventArgs e)
        {
            //if in zero then when reload dont put time
            if (mpvPersonalized.MpvPlayer.Position == TimeSpan.Zero)
            {
                InZero = true;
                CurrentTimeSpan = TimeSpan.Zero;
            }
            else
            {
                InZero = false;
            }
            //set it to analysis and current
            this.setPauseOrPlayWhenRelaod();
            this.addVideo();
        }
        /// <summary>
        /// When the video is loaded set the timespan.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void setMediaPlayerTime(object sender, EventArgs e)
        {
            loaded = true;
            ManualResetEventMedia.Set();
            //if media is loaded then set time span
            //lock it for not use for other threads or process
            lock (mpvPersonalized.MpvPlayer.MpvLock)
                //check it
                if (!InZero && mpvPersonalized.MpvPlayer.IsMediaLoaded && CurrentTimeSpan > TimeSpan.Zero)
                {
                    //if not enabled, set time, mean not reverse play
                    if (!reversePlaying)
                    {
                        //set time
                        SetPlayerTime(this.mpvPersonalized);
                        //add events
                        this.mpvPersonalized.MpvPlayer.MediaPaused += videoPaused;
                        this.mpvPersonalized.MpvPlayer.MediaResumed += videoResumed;
                    }
                    //if reload one stop it
                    //if (winFormAnalysis.ReloadOne)
                    // winFormAnalysis.MpvPersonalized.MpvPlayer.Stop();
                    //set time
                    SetAnalysisTime();
                    //state play or pause
                    if (this.buttonPlay.Visible)
                        (sender as Mpv.NET.Player.MpvPlayer).Pause();
                    else
                        (sender as Mpv.NET.Player.MpvPlayer).Resume();
                }
            //mpv unlock
            manualResetEventMpvUnlock.Set();
        }
        /// <summary>
        /// Set analysis winform time, after load, will automatically move to the time.
        /// </summary>
        private void SetAnalysisTime()
        {
            //check analysis video
            if (this.winFormAnalysis.ReloadOne)
            {
                this.LoadNewVideoAnalysis(winFormAnalysis.MpvPersonalized, pathVideoPlaying, this.playingSignal);
            }
            else if (winFormAnalysis.ReloadFour)
            {
                //load videos
                for (int i = 0; i < winFormAnalysis.FourMpv.Length; i++)
                {
                    LoadNewVideoAnalysis(winFormAnalysis.FourMpv[i], ChangeCameraUrl[i] as string, i);
                    //winFormAnalysis.FourMpv[i].MpvPlayer.Load(ChangeCameraUrl[i] as string, currentTimeSpan.ToString());
                }
            }
            else if (winFormAnalysis.ReloadTwo)
            {
                //load videos
                for (int i = 0; i < winFormAnalysis.TwoMainMpv.Length; i++)
                {
                    LoadNewVideoAnalysis(winFormAnalysis.TwoMainMpv[i],
                        changeCameraUrl[winFormAnalysis.NumberPressedStored[i]] as string, winFormAnalysis.NumberPressedStored[i]);
                    //winFormAnalysis.TwoMainMpv[i].MpvPlayer.Load(winFormAnalysis.TwoMainMpv[i].PlayingMedia, currentTimeSpan.ToString());
                }
            }
        }
        /// <summary>
        /// Set the player time, with the mpv received, for multiuse.
        /// </summary>
        /// <param name="mpvPersonalized"></param>
        private void SetPlayerTime(MpvPersonalized mpvPersonalized)
        {
            //lock it
            lock (mpvPersonalized.MpvPlayer.MpvLock)
            {
                //check if media is loaded, if yes set time.
                if (mpvPersonalized.MpvPlayer.IsMediaLoaded && CurrentTimeSpan >= TimeSpan.Zero && CurrentTimeSpan < mpvPersonalized.MpvPlayer.Duration)
                {
                    mpvPersonalized.MpvPlayer.Position = CurrentTimeSpan;
                }
                //check duration
                else if (mpvPersonalized.MpvPlayer.Duration < CurrentTimeSpan)
                {
                    mpvPersonalized.MpvPlayer.KeepOpen = Mpv.NET.Player.KeepOpen.Yes;
                    mpvPersonalized.MpvPlayer.Position = mpvPersonalized.MpvPlayer.Duration.Subtract(mpvPersonalized.TimeSpanForAddOrSubstract);
                }
            }
            //mpv unlock
            manualResetEventMpvUnlock.Set();
        }
        /// <summary>
        /// Save all timespan when position changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void storeTimeChangeEvent(object sender, EventArgs e)
        {
            lock (mpvPersonalized.MpvPlayer.MpvLock)
                //if is not zero then save
                if (mpvPersonalized.MpvPlayer.Position != TimeSpan.Zero)
                {
                    CurrentTimeSpan = mpvPersonalized.MpvPlayer.Position;
                }
            //mpv unlock
            manualResetEventMpvUnlock.Set();
        }
        /// <summary>
        /// Show the time when the position change.
        /// </summary>
        private void ShowTimeChange(object sender, EventArgs e)
        {
            //only if have media
            if (mpvPersonalized.MpvPlayer.IsMediaLoaded)
            {
                //thread safe call
                if (labelTimePlaying.InvokeRequired)
                {
                    setTextBotTime s = new setTextBotTime(ShowTimeChange);
                    //try it, cannot resolve it, because cant check form closing or not
                    if (CanInvoke)
                        try
                        {
                            this.Invoke(s, new object[] { sender, e });
                        }
                        catch (System.ObjectDisposedException exc)
                        {
                            Debug.WriteLine("Closing winform");
                            Debug.WriteLine(exc);
                        }
                }
                else
                {
                    //time format hh:mm:ss ms
                    labelTimePlaying.Text = mpvPersonalized.MpvPlayer.Position.ToString(@"hh\:mm\:ss\.ff");
                }
            }
        }
        /// <summary>
        /// Delegate the event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private delegate void setTextBotTime(object sender, EventArgs e);
        /// <summary>
        /// Kill all process downloading videos and delete some functions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopButtonPressedEvent(object sender, EventArgs e)
        {

            //same code, so call this
            killProcess(sender, e);
            //not reload again
            this.timerReload.Stop();
            timerLoadingVideo.Stop();
            //restart the video
            CurrentTimeSpan = TimeSpan.Zero;
            //critical section
            lock (arrayListProcessForDownloadVideo)
                //clear the arraylist
                arrayListProcessForDownloadVideo.Clear();
            //restart video
            this.mpvPersonalized.MpvPlayer.Stop();
            //close view
            this.mpvPictureBox.Visible = false;
            //stop the timer and change the image
            stopRecordTimerAndChangeButtonImage();
            //desactivate stop(save) button
            buttonRecordVideo.Enabled = false;

            //check progress bar
            if (progressBarLoadingVideo.Visible == true)
            {
                progressBarLoadingVideo.Visible = false;
                labelProgressBarLoading.Visible = false;
            }
            //convert the video
            if (convertVideo)
                this.convertVideoEvent();
            ChangeCameraUrlForAfterConvert();

        }
        /// <summary>
        /// Change the temp url to videos url.
        /// </summary>
        private void ChangeCameraUrlForAfterConvert()
        {
            for (int i = 0; i < ChangeCameraUrl.Count; i++)
            {
                ChangeCameraUrl[i] = this.path + "\\Sources\\videos\\" + Path.GetFileName(ChangeCameraUrl[i] as string);
            }
        }
        /// <summary>
        /// Stop the record timer, and change the record image.
        /// </summary>
        private void stopRecordTimerAndChangeButtonImage()
        {
            if (buttonRecordVideo.InvokeRequired)
            {
                SetButtonRecord s = new SetButtonRecord(stopRecordTimerAndChangeButtonImage);
                if (CanInvoke)
                    this.Invoke(s);
            }
            else
            {
                //close it
                this.progressBarLoadingVideo.Visible = false;
                this.labelProgressBarLoading.Visible = false;
                //stop display the time
                timerRecording.Stop();
                //change the image
                this.buttonRecordVideo.Image = Properties.Resources.stopButton;
                buttonRecordVideo.Enabled = false;
            }
        }
        private delegate void SetButtonRecord();
        /// <summary>
        /// Call ProcessCmdKey in other class.
        /// </summary>
        /// <param name="keyData"></param>
        public void ProcessCmdDelegate(Keys keyData)
        {
            ProcessCmdKey(ref GlobalMessage, keyData);
        }
        /// <summary>
        /// Override input received before do something in user interface, for not move in buttons.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //if is used then no move in interface, and not use in other panel
            if ((mpvPersonalized.KeysThatIHaveUse(keyData) || keyData == Keys.Tab || keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Enter)
                && !panelTag.Visible && !changeCameraBool)
            {
                //check have video
                if (pathVideoPlaying != null && !pathVideoPlaying.Equals(""))
                {
                    Monitor.Enter(MpvPersonalized.MpvPlayer.MpvLock);
                    if (!loaded && !mpvPersonalized.MpvPlayer.IsMediaLoaded)
                    {
                        Monitor.Exit(MpvPersonalized.MpvPlayer.MpvLock);
                        manualResetEventMpvUnlock.WaitOne();
                        Monitor.Enter(MpvPersonalized.MpvPlayer.MpvLock);
                    }
                    //check reverse playing
                    if (!reversePlaying)
                        //sent the event to the controller
                        mpvPersonalized.MpvController(new KeyEventArgs(keyData));
                    else
                    {
                        //check space
                        if (keyData == Keys.Space)
                        {
                            if (timerReversePlay.Enabled)
                            {
                                //stop the timer
                                timerReversePlay.Stop();
                                //change the visible
                                this.buttonPlay.Show();
                                this.buttonPause.Hide();
                            }
                            else
                            {
                                //start it again
                                timerReversePlay.Start();
                                //change the visible
                                this.buttonPlay.Hide();
                                this.buttonPause.Show();
                            }
                        }
                        else
                        {
                            //check next frame or back frame
                            if (keyData == MpvPersonalized.NextFrame || keyData == MpvPersonalized.PreviousFrame)
                            {
                                //if yes stop it
                                //stop the timer
                                timerReversePlay.Stop();
                                //change the visible
                                this.buttonPlay.Show();
                                this.buttonPause.Hide();
                            }
                            //then sende the command
                            mpvPersonalized.MpvController(new KeyEventArgs(keyData));
                        }
                    }

                    Monitor.Exit(MpvPersonalized.MpvPlayer.MpvLock);
                    winFormAnalysis.CommandToMpv(keyData);
                }
                //interface cannot take the input key
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Event when form is closed, kill all the downlaods started.
        /// </summary>
        /// <param name="sender">The object that called the evend.</param>
        /// <param name="eventArgs">Contains event argument.</param>
        private void killProcess(object sender, EventArgs eventArgs)
        {
            CanInvoke = false;
            //foreach all process and if nod exited kill it
            foreach (Process processDownload in arrayListProcessForDownloadVideo)
            {
                if (processDownload != null && !processDownload.HasExited)
                    processDownload.Kill();
            }
        }
        /// <summary>
        /// Convert video and show progress bar.
        /// </summary>
        private void convertVideoEvent()
        {
            lock (this)
            {
                convertVideo = false;
            }
            Process process;
            ProcessStartInfo info;
            //number of process, for set location
            int cont = 0;
            foreach (string videoName in videoNames)
            {
                cont++;
                process = new Process();
                info = new ProcessStartInfo();
                //create it
                Control c = createCPBForConvert(cont);
                c.BringToFront();
                //add it
                try
                {
                    convertProcessMap.Add(process, c);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                //nuevo comando
                string strArg = "-y -hide_banner " +
                    "-i " + " \"" + this.path + "\\Sources\\temp" + videoName + "\" " +
                    " -c copy " +
                    "-f mp4 " +
                    " \"" + this.path + "\\Sources\\videos" + videoName + "\"";
                info.FileName = ffmpegtool;
                info.UseShellExecute = false;
                info.CreateNoWindow = true;
                info.Arguments = strArg;
                process.StartInfo = info;
                process.EnableRaisingEvents = true;
                process.Exited += new EventHandler(waitConvert);

                process.Start();
            }
        }
        /// <summary>
        /// Wait convert process.
        /// </summary>
        private void waitConvert(object sender, EventArgs e)
        {

            if (this.InvokeRequired)
            {
                //create delegate
                DelegateControl d = new DelegateControl(MainControl);

                //invoke it
                this.Invoke(d, new object[] { convertProcessMap[sender as Process], 0 });
                //same
                d = new DelegateControl(MainControl);
                this.Invoke(d, new object[] { sender, 1 });
            }
            else
            {
                this.Controls.Remove(convertProcessMap[sender as Process]);
                convertProcessMap.Remove(sender as Process);
            }
        }
        /// <summary>
        /// Delegate delete control.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="i"></param>
        private delegate void DelegateControl(object obj, int i);
        /// <summary>
        /// Depends on i do something.
        /// </summary>
        private void MainControl(object obj, int i)
        {
            switch (i)
            {
                case 0:
                    //remove it from controls
                    this.Controls.Remove(obj as Control);
                    break;
                case 1:
                    //critical section
                    lock (convertProcessMap)
                    {
                        //remove if from convert process map
                        convertProcessMap.Remove(obj as Process);
                        //check count
                        if (convertProcessMap.Count == 0)
                        {
                            //show mpv, and play video
                            //show it then play
                            this.mpvPictureBox.Visible = true;
                            this.reloadVideo(null, null);
                            //check if was closed or not
                            if (WasClosed)
                            {
                                this.Close();
                            }
                        }
                    }
                    break;
                case 3:
                    //add to controls
                    this.Controls.Add(obj as Control);
                    break;
                case 4:
                    (obj as Control).BringToFront();
                    break;
            }

        }
        /// <summary>
        /// Create CPG and set it.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="all"></param>
        private Control createCPBForConvert(int i)
        {
            //create new circular progress bar, for show progress
            CircularProgressBar.CircularProgressBar c = new CircularProgressBar.CircularProgressBar();
            //set it
            c.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner;
            c.AnimationSpeed = 500;
            c.BackColor = System.Drawing.Color.Transparent;
            c.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            c.InnerColor = System.Drawing.Color.Transparent;
            c.InnerMargin = 26;
            c.InnerWidth = 25;
            c.Size = new System.Drawing.Size(200, 200);
            //one number one location
            switch (i)
            {
                case 1:
                    c.Location = this.mpvPictureBox.Location;
                    break;
                case 2:
                    c.Location = new Point(this.mpvPictureBox.Location.X + this.mpvPictureBox.Size.Width / 2, this.mpvPictureBox.Location.Y);
                    break;
                case 3:
                    c.Location = new Point(this.mpvPictureBox.Location.X, this.mpvPictureBox.Location.Y + this.mpvPictureBox.Size.Height / 2);
                    break;
                case 4:
                    c.Location = new Point(this.mpvPictureBox.Location.X + this.mpvPictureBox.Size.Width / 2, this.mpvPictureBox.Location.Y + this.mpvPictureBox.Size.Height / 2);
                    break;
            }
            c.MarqueeAnimationSpeed = 2000;
            c.OuterColor = System.Drawing.Color.White;
            c.OuterMargin = -25;
            c.OuterWidth = 30;
            c.ProgressColor = Color.Blue;
            c.ProgressWidth = 5;
            c.StartAngle = 270;
            c.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            c.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            c.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            c.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            c.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            c.Value = 68;
            if (this.InvokeRequired)
            {
                //AÑADIR DELEGETE CONTROL PARA INVOCAR Y CREAR LO DE ABAJO
                //this.Controls.Add(c);
                DelegateControl d = new DelegateControl(MainControl);
                if (CanInvoke)
                {
                    this.Invoke(d, new object[] { c, 3 });
                    this.Invoke(d, new object[] { c, 4 });
                }
            }
            else
            {
                this.Controls.Add(c);
            }
            return c;
        }
        /// <summary>
        /// Start the download of the video.
        /// </summary>
        /// <param name="videoName"></param>
        /// <param name="process"></param>
        private void processStart(string videoName, Process process, int i)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            string videoUrl = (settingForm.ComboBoxSetting.Items[WinFormSetting.LastIndex] as Setting).MainUrl[i];
            //general parameters
            string strArg = "-y -hide_banner " +
                 "-stimeout -1 -rtsp_transport tcp " +
                 "-rtsp_flags prefer_tcp " +
                 "-i " + videoUrl +
                 " -c copy " +
                 "-f mp4 -copyts " +
                 "-buffer_size 1000000 " +
                 "-movflags +empty_moov+default_base_moof+frag_keyframe -loglevel " +
                 "quiet \"" + this.pathStoreVideo + videoName + "\"";
            info.FileName = ffmpegtool;
            info.UseShellExecute = false;
            info.CreateNoWindow = true;
            info.Arguments = strArg;
            process.StartInfo = info;

            process.Start();
            //when process finish close the process
            process.EnableRaisingEvents = true;
            process.Exited += new EventHandler(processEnd);

            //add the name
            videoNames.Add(videoName);
            ChangeCameraUrl.Add(this.pathStoreVideo + videoName);
        }
        /// <summary>
        /// When process end, close.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void processEnd(object source, EventArgs e)
        {
            Process process = source as Process;
            //when finish close
            process.Close();
            //remove from arraylist
            arrayListProcessForDownloadVideo.Remove(process);
            //when all process finish
            if (arrayListProcessForDownloadVideo.Count == 0)
            {
                //stop the reload timer
                this.timerReload.Stop();
                stopRecordTimerAndChangeButtonImage();
            }
            lock (this)
                //convert video when process end            
                if (arrayListProcessForDownloadVideo.Count == 0 && convertVideo)
                    convertVideoEvent();
        }
        /// <summary>
        /// Save button click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void save_Click(object sender, EventArgs e)
        {
            //setting enable=false, dont allow to use it
            this.buttonSetting.Enabled = false;
            if (settingForm.ComboBoxSetting.Items.Count == 0)
            {
                MessageBox.Show("Abre la configuarion y elige setting");
                return;
            }
            //Create all the process.
            for (int i = 0; i < numeroVideos; i++)
            {
                Process p = new Process();
                string nombreVideo = "\\Camera" + (i + 1) + "Main.mp4";
                this.processStart(nombreVideo, p, i);
                arrayListProcessForDownloadVideo.Add(p);
                //save once the path
                if (pathVideoPlaying == null)
                    pathVideoPlaying = this.pathStoreVideo + nombreVideo;
            }
            //set the new image
            buttonRecordVideo.Image = Properties.Resources.stopButtonAnimate;
            //remove the actual funciton and add the new
            buttonRecordVideo.Click -= this.save_Click;
            buttonRecordVideo.Click += this.stopButtonPressedEvent;
            //start the chronometer and timer
            stopwatchVideoRecording.Start();
            timerRecording.Start();
            /*progress bar settings*/
            progressBarLoadingVideo.Visible = true;

            decimal position = 1;//take the position
            //take the map, 9 second show the progress bar
            position = position.Map(0, 100, 0, 9000);
            //assing the position for interval
            timerLoadingVideo.Interval = Decimal.ToInt32(position);
            //start timer
            timerLoadingVideo.Start();
            //show the label
            labelProgressBarLoading.Visible = true;

            timerComprobarProcess.Start();
        }
        /// <summary>
        /// When one camera button is clicked, change it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cameraButton_Click(object sender, EventArgs e)
        {
            if (pathVideoPlaying != null)
            {
                Label l = sender as Label;
                //one button one change, reuse code
                if (l == labelSignal1)
                {
                    changeCameraEvent(sender, new KeyEventArgs(playCameraOne));
                }
                else if (l == labelSignal2)
                {
                    changeCameraEvent(sender, new KeyEventArgs(playCameraTwo));
                }
                else if (l == labelSignal3)
                {
                    changeCameraEvent(sender, new KeyEventArgs(playCameraThree));
                }
                else if (l == labelSignal4)
                {
                    changeCameraEvent(sender, new KeyEventArgs(playCameraFour));
                }

            }
        }
        /// <summary>
        /// PlayOrPause button clicked event, when pressed pause or play the video.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Play_Click(object sender, EventArgs e)
        {
            this.mpvPersonalized.MpvController(new KeyEventArgs(MpvPersonalized.PauseOrPlayVideo));
        }
        /// <summary>
        /// When clicked move to the start point.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Inicio_Click(object sender, EventArgs e)
        {
            ProcessCmdKey(ref GlobalMessage, MpvPersonalized.MoveVideoToStartPoint);
        }
        /// <summary>
        /// When clicked move to the end point.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Fin_Click(object sender, EventArgs e)
        {
            ProcessCmdKey(ref GlobalMessage, MpvPersonalized.MoveVideoToEndPoint);
        }
        /// <summary>
        /// Simulate the frame movement when clicked on button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void antFrame_Click(object sender, EventArgs e)
        {
            ProcessCmdKey(ref GlobalMessage, MpvPersonalized.PreviousFrame);
        }
        /// <summary>
        /// Simulate the frame movement when clicked on button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sigFrame_Click(object sender, EventArgs e)
        {
            ProcessCmdKey(ref GlobalMessage, MpvPersonalized.NextFrame);
        }
        /// <summary>
        /// Event when the label time is clicked, show short menu or move the time.
        /// </summary>
        /// <param name="sender">Label clicked.</param>
        /// <param name="eventArgs">Event with arguments.</param>
        private void clickOnTimeLabel(object sender, EventArgs eventArgs)
        {
            Label labelClicked = sender as Label;
            MouseEventArgs mouseEventArgs = eventArgs as MouseEventArgs;
            //Check right click 
            if (mouseEventArgs.Button == MouseButtons.Right)
            {
                // this.contextMenuRightClickOnTime.Show(Cursor.Position);
                lastRowClicked = labelClicked;
            }
            else if (TimeSpan.Parse(labelClicked.Text) >= TimeSpan.Zero && TimeSpan.Parse(labelClicked.Text) < mpvPersonalized.MpvPlayer.Duration)
            {
                //if not right click and clicked, change current video time
                this.mpvPersonalized.MpvPlayer.Position = TimeSpan.Parse(labelClicked.Text);
                //SetAnalysisTimeWithLabel(labelClicked);
                SetAnalysisTime();
            }
        }
        /// <summary>
        /// Set the time to analysis windows if is openned.
        /// </summary>
        private void SetAnalysisTimeWithLabel(Label label)
        {
            //check analysis video
            if (this.winFormAnalysis.ReloadOne)
            {
                this.LoadNewVideo(winFormAnalysis.MpvPersonalized);
            }
            else if (winFormAnalysis.ReloadFour)
            {
                //load videos
                for (int i = 0; i < winFormAnalysis.FourMpv.Length; i++)
                {
                    winFormAnalysis.FourMpv[i].MpvPlayer.Load(ChangeCameraUrl[i] as string, currentTimeSpan.ToString());
                }
            }
            else if (winFormAnalysis.ReloadTwo)
            {
                //load videos
                for (int i = 0; i < winFormAnalysis.TwoMainMpv.Length; i++)
                {
                    winFormAnalysis.TwoMainMpv[i].MpvPlayer.Load(winFormAnalysis.TwoMainMpv[i].PlayingMedia, currentTimeSpan.ToString());
                }
            }
        }
        /// <summary>
        /// Delete the time if is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteClipRowOnTable(lastRowClicked);
            /*//if multiple selected
            if (checkedClipListBox.Count != 0)
            {
                foreach (CheckBox c in checkedClipListBox)
                {
                    deleteClipRowOnTable(c);
                }
                checkedClipListBox.Clear();
                checkBoxAllClips.Checked = false;
            }
            else//if only one selected
            {
                deleteClipRowOnTable(lastRowClicked);
            }*/
        }
        /// <summary>
        /// Delete one clip in the table.
        /// </summary>
        /// <param name="control"></param>
        private void deleteClipRowOnTable(Control control)
        {
            //get the row
            int row = tableLayoutPanelClips.Controls.IndexOf(control) / 5;
            //remove from arraylist
            mapFullClipName.Remove((tableLayoutPanelClips.Controls[row * 5 + 2]) as Label);
            //remove from all clip
            mapAllClip.Remove((tableLayoutPanelClips.Controls[row * 5]) as CheckBox);
            //remove it
            this.tableLayoutPanelClips.Controls.Remove(tableLayoutPanelClips.Controls[row * 5 + 4]);
            this.tableLayoutPanelClips.Controls.Remove(tableLayoutPanelClips.Controls[row * 5 + 3]);
            this.tableLayoutPanelClips.Controls.Remove(tableLayoutPanelClips.Controls[row * 5 + 2]);
            this.tableLayoutPanelClips.Controls.Remove(tableLayoutPanelClips.Controls[row * 5 + 1]);
            this.tableLayoutPanelClips.Controls.Remove(tableLayoutPanelClips.Controls[row * 5]);
            this.tableLayoutPanelClips.RowCount--;
        }
        /// <summary>
        /// Move the video backward.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBackward_Click(object sender, EventArgs e)
        {
            ProcessCmdKey(ref GlobalMessage, MpvPersonalized.MoveBackwardKeys);
        }
        /// <summary>
        /// Move forward the video whem cliked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonForward_Click(object sender, EventArgs e)
        {
            ProcessCmdKey(ref GlobalMessage, MpvPersonalized.MoveForwardKeys);
        }
        /// <summary>
        /// Change the label every second.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerRecording_Tick(object sender, EventArgs e)
        {
            this.labelRecordTime.Text = stopwatchVideoRecording.Elapsed.ToString(@"hh\:mm\:ss");
        }
        /// <summary>
        /// Timer tick for increment progress bar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerLoadingVideo_Tick(object sender, EventArgs e)
        {
            //increment
            this.incrementProgressBar();
            //check if finished
            if (this.progressBarLoadingVideo.Value == this.progressBarLoadingVideo.Maximum)
            {
                //stop the timer
                timerLoadingVideo.Stop();
                //wait the animation finish
                timerEndProgressBar.Start();
            }
        }
        /// <summary>
        /// Add the increment to the progress bar.
        /// </summary>
        private void incrementProgressBar()
        {
            //increment 1 the progress bar
            this.progressBarLoadingVideo.Increment(1);
            //try to clean
            Application.DoEvents();
        }
        /// <summary>
        /// End the progress bar and show the video
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerEndProgressBar_Tick(object sender, EventArgs e)
        {
            //load the video(add video to the list for play)
            this.addVideo();
            //stop the timer
            timerEndProgressBar.Stop();
            //cameras button have the same function
            this.labelSignal1.Click += new System.EventHandler(cameraButton_Click);
            this.labelSignal2.Click += new System.EventHandler(cameraButton_Click);
            this.labelSignal3.Click += new System.EventHandler(cameraButton_Click);
            this.labelSignal4.Click += new System.EventHandler(cameraButton_Click);
            //controladores teclado de control
            this.KeyDown += changeCameraEvent;
            this.KeyDown += storeTimeEvent;
            labelForDesactivate = labelSignal1;
            labelSignal1.Enabled = false;
            this.timerReload.Start();

            //desactivate visble
            progressBarLoadingVideo.Visible = false;
            labelProgressBarLoading.Visible = false;

            //refresh it
            this.Invalidate();
            this.Refresh();

            //bring to front
            mpvPictureBox.BringToFront();
            mpvPictureBox.Show();
        }

        /// <summary>
        /// Save all locations in tag, all have the margin.
        /// </summary>
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                //save location separate by ;
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                //if this controls have controls save it
                if (con.Controls.Count > 0)
                    setTag(con);
            }

        }
        /// <summary>
        /// Resize the controlls with current windows size.
        /// </summary>
        /// <param name="newx"></param>
        /// <param name="newy"></param>
        /// <param name="cons"></param>
        private void setControls(float newx, float newy, Control cons)
        {
            try
            {
                //check no minimize
                if (!(this.WindowState == FormWindowState.Minimized))
                    //run all controls
                    foreach (Control con in cons.Controls)
                    {
                        //dock style fill, not need to change
                        if (!tableLayoutPanelTags.Controls.Contains(con))
                        {
                            string[] mytag = con.Tag.ToString().Split(new char[] { ':' });//get the date
                            float a = System.Convert.ToSingle(mytag[0]) * newx;//calculate width with winforw 
                            con.Width = (int)a;
                            a = System.Convert.ToSingle(mytag[1]) * newy;///calculate height with winforw 
                            con.Height = (int)(a);
                            a = System.Convert.ToSingle(mytag[2]) * newx;//calculate left margin with winforw 
                            con.Left = (int)(a);
                            a = System.Convert.ToSingle(mytag[3]) * newy;//calculate up margin with winforw 
                            con.Top = (int)(a);
                            Single currentSize = System.Convert.ToSingle(mytag[4]) * newy;//resize the text
                            con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                            //if have chill controls
                            if (con.Controls.Count > 0)
                            {
                                setControls(newx, newy, con);
                            }
                        }
                    }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
        /// <summary>
        /// Event when the winform is loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WinFormWithVideoPlayer_Load(object sender, EventArgs e)
        {
            currentWidht = this.Width;
            currentHeight = this.Height;
            setTag(this);//save tag
            this.Resize += WinForm_Resize;
        }
        /// <summary>
        /// Resize when screen size change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WinForm_Resize(object sender, EventArgs e)
        {
            //calculate the new proportion and then change the size
            float newx = (this.Width) / currentWidht;
            float newy = (this.Height) / currentHeight;
            setControls(newx, newy, this);
        }
        /// <summary>
        /// Add tag to button.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="button"></param>
        private void AddTag(Tag tag, Button button)
        {
            //save the tag
            tagMap.Add(button, tag);
            if (tag.TagMode.IsAutomatic)
                //add click event
                button.Click += tagAutomaticButtonEvent;
            else
            {
                button.Click += tagManualButtonEvent;
            }
            //add the button
            tableLayoutPanelTags.Controls.Add(button);
            //check the size
            if (tableLayoutPanelTags.Controls.Count % 2 == 0)
            {
                tableLayoutPanelTags.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
                tableLayoutPanelTags.RowCount++;
            }

        }
        /// <summary>
        /// Manual tag button event.
        /// </summary>
        private void tagManualButtonEvent(object sender, EventArgs e)
        {
            //check media loaded
            if (mpvPersonalized.MpvPlayer.IsMediaLoaded)
            {
                //get the button
                Button b = sender as Button;
                //get the progressbar
                CircularProgressBar.CircularProgressBar c = sender as CircularProgressBar.CircularProgressBar;
                Tag tag = null;
                //maybe the clicked is progress bar, and not the button
                //if is button
                if (b != null && manualTagMap.ContainsKey(b))
                    //get the tag
                    tag = tagMap[b];
                else if (c != null)//if is progress bar
                {
                    //get the tag
                    b = c.Parent as Button;
                    tag = tagMap[b];
                }
                if (tag != null)
                {
                    //crate new clip
                    Clip clip = createManualClip(tag, mpvPersonalized.MpvPlayer.Position, b);
                    clip.MyColor = b.FlatAppearance.BorderColor;
                    storeInTableLayoutPanelClips(clip);
                    //clean it
                    manualTagMap.Remove(b);
                    //remove the animation
                    b.Controls.Remove(mapAnimation[b]);
                    //change the color and draw it 
                    b.ForeColor = Color.White;
                }
                else if (b != null)
                {
                    createAndStoreAnimation(b);
                }
                //draw it                
                b.Invalidate();
            }

        }
        /// <summary>
        /// Create and store the waiting animation.
        /// </summary>
        /// <param name="b"></param>
        private void createAndStoreAnimation(Button b)
        {
            //if this button dont have the animation create it
            if (!mapAnimation.ContainsKey(b))
            {
                //create
                CircularProgressBar.CircularProgressBar newAnimation = constructorCPB(b,
                    b.FlatAppearance.BorderColor);
                //store
                mapAnimation.Add(b, newAnimation);
                //add to button
                b.Controls.Add(newAnimation);
                //add event
                newAnimation.Click += tagManualButtonEvent;
            }
            //change button style
            b.ForeColor = b.FlatAppearance.BorderColor;
            b.Controls.Add(mapAnimation[b]);
            //add the position
            manualTagMap.Add(b, mpvPersonalized.MpvPlayer.Position);
        }
        /// <summary>
        /// Create and return the progress bar.
        /// </summary>
        /// <returns></returns>
        private CircularProgressBar.CircularProgressBar constructorCPB(Button b, Color color)
        {
            //create it
            CircularProgressBar.CircularProgressBar c = new CircularProgressBar.CircularProgressBar();
            //adjust it
            c.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner;
            c.AnimationSpeed = 500;
            c.BackColor = System.Drawing.Color.Transparent;
            c.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            c.InnerColor = System.Drawing.Color.Transparent;
            c.InnerMargin = 1;
            c.InnerWidth = 1;
            c.Size = new System.Drawing.Size(24, 24);
            c.Location = new System.Drawing.Point(b.Width - c.Size.Width - 2, (b.Height - c.Height) / 2);
            c.MarqueeAnimationSpeed = 2000;
            if (color == Color.Black)
                c.OuterColor = System.Drawing.Color.White;
            else
                c.OuterColor = System.Drawing.Color.Black;
            c.OuterMargin = -2;
            c.OuterWidth = 3;
            c.ProgressColor = color;
            c.ProgressWidth = 5;
            c.StartAngle = 270;
            c.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            c.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            c.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            c.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            c.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            c.Value = 68;
            //return it
            return c;
        }
        /// <summary>
        /// Create the manual clip.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="postTimeSpan"></param>
        /// <param name="b"></param>
        /// <returns>Return the clip created.</returns>
        private Clip createManualClip(Tag tag, TimeSpan postTimeSpan, Button b)
        {
            //tagcount+1
            tag.TagCount++;
            //return the clip
            return new Clip(manualTagMap[b].ToString((@"hh\:mm\:ss\.ff")),
                postTimeSpan.ToString((@"hh\:mm\:ss\.ff")), tag.TagName.ToString() + " " + tag.TagCount);
        }
        /// <summary>
        /// Tag button event, when the tag is pressed create clip.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tagAutomaticButtonEvent(object sender, EventArgs e)
        {
            if (mpvPersonalized.MpvPlayer.IsMediaLoaded)
            {
                Button buttonTag = sender as Button;
                //take the tag
                Tag tag = tagMap[buttonTag];
                //crate new clip
                Clip clip = createAutomaticClip(tag);
                //save color
                clip.MyColor = buttonTag.FlatAppearance.BorderColor;
                //store it
                storeInTableLayoutPanelClips(clip);
            }
        }
        /// <summary>
        /// Store the content in the table.
        /// </summary>
        /// <param name="clip"></param>
        private void storeInTableLayoutPanelClips(Clip clip)
        {

            //create the content in the table
            CheckBox checkBoxClip = new CheckBox();
            checkBoxClip.AutoSize = false;
            checkBoxClip.Text = "";
            checkBoxClip.Dock = DockStyle.Fill;
            Label clipName = createClipText(clip.TagName);
            //create and adjust round
            RoundPictureBox round = new RoundPictureBox(clip.MyColor);
            round.Size = roundSize;
            round.Dock = DockStyle.Bottom;

            //create current time label
            Label previous = createTimeLabel(clip.PreviousTime);
            Label after = createTimeLabel(clip.LaterTime);
            //set it
            //after.Dock = DockStyle.Bottom;
            //previous.Dock = DockStyle.Bottom;
            //clipName.Dock = DockStyle.Bottom;
            clipName.Margin = new Padding(0, 5, 0, 0);
            after.Margin = new Padding(0, 5, 0, 0);
            previous.Margin = new Padding(0, 5, 0, 0);

            tableLayoutPanelClips.Controls.Add(checkBoxClip);
            tableLayoutPanelClips.Controls.Add(round);
            tableLayoutPanelClips.Controls.Add(clipName);
            tableLayoutPanelClips.Controls.Add(previous);
            tableLayoutPanelClips.Controls.Add(after);
            tableLayoutPanelClips.RowCount++;
            this.tableLayoutPanelClips.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            round.Location = new Point(tableLayoutPanelClips.Controls[1].Width, tableLayoutPanelClips.Controls[1].Height / 2);
            //scrol down
            panelClipsSaved.ScrollControlIntoView(after);

            //add event
            tableLayoutPanelClips.Click += rightClickOnClipPanel;
            clipName.Click += clickInClipNameEvent;
            checkBoxClip.CheckedChanged += checkedBoxEvent;
            checkBoxClip.MouseDown += rightClickOnClipPanel;//use mouse down for check right click
            previous.Click += rightClickOnClipPanel;
            clipName.Click += rightClickOnClipPanel;
            clipName.MouseEnter += mouseOnClipNameForShowFullNameEvent;
            after.Click += rightClickOnClipPanel;
            round.Click += rightClickOnClipPanel;
            //store it
            mapAllClip.Add(checkBoxClip, clip);
        }
        /// <summary>
        /// When clip name clicked, move to the previuous time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clickInClipNameEvent(object sender, EventArgs e)
        {
            //get label
            Label labelClicked = sender as Label;
            //get row
            int row = tableLayoutPanelClips.Controls.IndexOf(labelClicked) / 5;
            //save label clicked
            labelClicked = tableLayoutPanelClips.Controls[row * 5 + 3] as Label;
            TimeSpan timeParsed;
            MouseEventArgs mouseEventArgs = e as MouseEventArgs;
            if (labelClicked != null && mouseEventArgs.Button == MouseButtons.Left)
            {
                TimeSpan.TryParse(labelClicked.Text, out timeParsed);
                if (timeParsed != null && timeParsed >= TimeSpan.Zero && timeParsed < mpvPersonalized.MpvPlayer.Duration)
                {
                    //if not right click and clicked, change current video time
                    this.mpvPersonalized.MpvPlayer.Position = TimeSpan.Parse(labelClicked.Text);
                    SetAnalysisTime();
                }
            }
        }
        /// <summary>
        /// Show the full name when the mouse on the clip name.
        /// </summary>
        private void mouseOnClipNameForShowFullNameEvent(object sender, EventArgs e)
        {
            Label label = sender as Label;
            //if is label, show the tooltip
            if (label != null)
                tip.SetToolTip(label, mapFullClipName[label]);
        }
        /// <summary>
        /// Create the label with the new clip name.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private Label createClipText(string text)
        {
            Label newLabel = new Label();
            newLabel.AutoSize = false;
            String showText = "";
            if (text.Length > LIMIT_CLIP_NAME)
            {
                //add ... for the text that not show
                showText += text.Substring(0, 7) + "..." + text.Substring(text.Length - 7);
            }
            else
                showText += text;
            //set the text
            newLabel.Text = showText;
            mapFullClipName.Add(newLabel, text);
            //add dock
            newLabel.Dock = DockStyle.Bottom;
            return newLabel;
        }
        /// <summary>
        /// Create clip with tag information.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private Clip createAutomaticClip(Tag tag)
        {
            TimeSpan currentTime = mpvPersonalized.MpvPlayer.Position;
            //tagcount+1
            tag.TagCount++;
            //return the clip
            return new Clip(currentTime.Subtract(tag.PreviousTime).ToString((@"hh\:mm\:ss\.ff")),
                currentTime.Add(tag.LaterTimer).ToString((@"hh\:mm\:ss\.ff")), tag.TagName.ToString() + " " + tag.TagCount);
        }
        /// <summary>
        /// When checked set all checkbox to check,else to not check.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxAllClips_CheckedChanged(object sender, EventArgs e)
        {
            Boolean state = false;
            //check
            if (checkBoxAllClips.Checked)
                state = true;
            //run all controls
            foreach (object o in tableLayoutPanelClips.Controls)
            {
                CheckBox c = o as CheckBox;
                if (c != null)
                    c.Checked = state;
            }
        }
        /// <summary>
        /// When right click show the menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rightClickOnClipPanel(object sender, EventArgs e)
        {
            MouseEventArgs mouseEventArgs = e as MouseEventArgs;
            //Check right click 
            if (mouseEventArgs.Button == MouseButtons.Right)
            {
                lastRowClicked = sender as Control;
                this.contextMenuRightClickOnTime.Show(Cursor.Position);
            }
        }
        /// <summary>
        /// When opening, check if can delete or not.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuRightClickOnTime_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if have something to delete or export enable it
            if (tableLayoutPanelClips.Controls.Count == 0)
            {
                eliminarToolStripMenuItem.Enabled = false;
                exportarToolStripMenuItem.Enabled = false;
            }
            else
            {
                eliminarToolStripMenuItem.Enabled = true;
                exportarToolStripMenuItem.Enabled = true;
            }
        }
        /// <summary>
        /// Store if checked, else remove.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkedBoxEvent(object sender, EventArgs e)
        {
            //add to arraylist when checked
            CheckBox c = sender as CheckBox;
            if (c.Checked)
            {
                checkedClipListBox.Add(c);
            }
            else
                checkedClipListBox.Remove(c);
        }
        /// <summary>
        /// Set visible the panel create tag.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNewTag_Click(object sender, EventArgs e)
        {
            //lock the panel and show it
            lock (panelTag)
            {
                if (!panelTag.Visible)
                {
                    //adjust start position
                    panelTag.StartPosition = FormStartPosition.Manual;
                    panelTag.Location = new Point(this.Location.X + 300, this.Location.Y + 300);
                    panelTag.BringToFront();
                    //set parent
                    panelTag.Show(this);
                }
                else
                {
                    //hide it
                    panelTag.Visible = false;
                }
            }
        }
        /// <summary>
        /// Create the tag button.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private Button createButtonTag(Tag tag)
        {
            //create new button
            Button buttonTag = new Button();
            //adjust it
            buttonTag.Text = tag.TagName;//set the tag name
            buttonTag.TextAlign = ContentAlignment.MiddleLeft;//text start in left
            buttonTag.Padding = new Padding(0, 0, 40, 0);
            buttonTag.FlatAppearance.BorderSize = 1;//border size
            buttonTag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;//transparent background
            buttonTag.FlatAppearance.BorderColor = tag.TagColor;//border colour
            buttonTag.Dock = DockStyle.Fill;//size

            return buttonTag;

        }
        /// <summary>
        /// Show the pause button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPlay_Click(object sender, EventArgs e)
        {
            ProcessCmdKey(ref GlobalMessage, MpvPersonalized.PauseOrPlayVideo);
        }
        /// <summary>
        /// Save all tags in JSON file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveTags_Click(object sender, EventArgs e)
        {
            //show it for set the path to store tag
            this.saveFileDialogTag.ShowDialog();
        }
        private void SaveTagInDefaultPath()
        {
            if (tagMap.Count != 0)
            {
                //save only the tag, the button only have the event handler, tag have all the information
                StreamWriter sw = File.CreateText(pathStoreTagAndClips + "\\data\\" + tagFileName);
                //encrypt it and save it
                sw.Write(AES128.Encrypt(JsonConvert.SerializeObject(tagMap.Values)));
                //close it
                sw.Close();
            }
        }
        /// <summary>
        /// Check if the file exist, then load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoadTags_Click(object sender, EventArgs e)
        {
            //show it
            openFileDialogTagPath.ShowDialog();
        }
        private void LoadTagFromDefaultPath()
        {
            //if exist tags load it
            if (File.Exists(pathStoreTagAndClips + "\\data\\" + tagFileName))
            {
                List<Tag> listTags = JsonConvert.DeserializeObject<List<Tag>>
                    (AES128.Decrypt(File.ReadAllText(pathStoreTagAndClips + "\\data\\" + tagFileName)));
                foreach (Tag tag in listTags)
                {
                    createAutomaticOrManualTagButton(tag);
                }
            }
        }
        /// <summary>
        /// Show the setting winform.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSetting_Click(object sender, EventArgs e)
        {
            settingForm.centerShow();
        }

        /// <summary>
        /// When clicked, export the video, with these time in /Sources/exported
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exportClipRowOnTable(lastRowClicked, 0);
        }
        /// <summary>
        /// Thread export video clip from table.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="cont"></param>
        private void exportClipRowOnTable(Control control, int cont)
        {
            //get row
            int row = tableLayoutPanelClips.Controls.IndexOf(control) / 5;
            //get clip name
            string clipName = tableLayoutPanelClips.Controls[row * 5 + 2].Text;
            //get the times
            TimeSpan startTime = TimeSpan.Parse(tableLayoutPanelClips.Controls[row * 5 + 3].Text);
            TimeSpan endTime = TimeSpan.Parse(tableLayoutPanelClips.Controls[row * 5 + 4].Text);
            //check times
            if (endTime < startTime)
            {
                startTime = endTime;
                endTime = TimeSpan.Parse(tableLayoutPanelClips.Controls[row * 5 + 4].Text);
            }
            //create new thread for export
            Thread th = new Thread(() => exportVideo(startTime.ToString(), endTime.ToString(), clipName, cont));
            th.Start();
        }
        /// <summary>
        /// Create process for export all video.
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        private void exportVideo(string startTime, string endTime, string clipName, int cont)
        {
            Process process;
            ProcessStartInfo info;
            string strArg;
            //for all checked export it
            foreach (string s in ChangeCameraUrl)
            {
                //check fist if file have been exported or not
                if (File.Exists(this.path + "\\Sources\\exported\\" + clipName + Path.GetFileName(s)))
                {
                    MessageBox.Show("Ya ha sido exportado:" + Path.GetFileName(s));
                }
                else
                {
                    //if process is dowloading
                    if (s.Contains("\\temp\\"))
                    {
                        //create a copy, for export, after finish delete it, in temp cannot directly export
                        process = new Process();
                        info = new ProcessStartInfo();
                        //new name aux video
                        strArg = "-y -hide_banner " +
                            "-i " + " \"" + s + "\" " +
                            " -c copy " +
                            "-f mp4 " +
                            "\"" + s.Substring(0, s.Length - 4) + cont + "aux.mp4\" ";
                        info.FileName = ffmpegtool;
                        info.UseShellExecute = false;
                        info.CreateNoWindow = true;
                        info.Arguments = strArg;
                        process.StartInfo = info;

                        process.Start();
                        //wait complete then convert
                        process.WaitForExit();

                        process = new Process();
                        //info = new ProcessStartInfo();

                        //command create video
                        strArg = "-i " + " \"" + s.Substring(0, s.Length - 4) + cont + "aux.mp4\"" +
                                " -c copy " +
                                "-f mp4 " + "-ss " + startTime + " -t " + endTime +
                                " \"" + this.path + "\\Sources\\exported\\" + clipName + Path.GetFileName(s) + "\"";
                        info.Arguments = strArg;
                        process.StartInfo = info;
                        //start the process
                        process.Start();

                        //wait for exit then delete
                        process.WaitForExit();
                        //check
                        if (File.Exists(s.Substring(0, s.Length - 4) + cont + "aux.mp4"))
                            File.Delete(s.Substring(0, s.Length - 4) + cont + "aux.mp4");
                    }
                    else//if process completed
                    {
                        process = new Process();
                        info = new ProcessStartInfo();

                        //command create video
                        strArg = "-i " + " \"" + s + "\"" +
                                " -c copy " +
                                "-f mp4 " + "-ss " + startTime + " -t " + endTime +
                                " \"" + this.path + "\\Sources\\exported\\" + clipName + Path.GetFileName(s) + "\"";
                        info.FileName = ffmpegtool;
                        info.UseShellExecute = false;
                        info.CreateNoWindow = true;
                        info.Arguments = strArg;
                        process.StartInfo = info;
                        //start the process
                        process.Start();
                    }
                }

            }

        }
        /// <summary>
        /// Export all video when clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExport_Click(object sender, EventArgs e)
        {
            //if have checked, export it
            if (checkedClipListBox.Count != 0)
            {
                //cont number videos aux
                int cont = 0;
                foreach (CheckBox c in checkedClipListBox)
                {
                    Thread th = new Thread(() => exportClipRowOnTable(c, cont));
                    th.Start();
                    th.Join();
                    cont++;
                }
                checkedClipListBox.Clear();
                checkBoxAllClips.Checked = false;
            }
        }
        /// <summary>
        /// Delete all video when clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            //if have checked, delete it
            if (checkedClipListBox.Count != 0)
            {
                DialogResult dialog = MessageBox.Show("¿Estás seguro de eliminar lo/s " +
                    checkedClipListBox.Count + " clip/s?", "Eliminar",
                MessageBoxButtons.YesNoCancel);
                if (DialogResult.Yes == dialog)
                {
                    foreach (CheckBox c in checkedClipListBox)
                    {
                        deleteClipRowOnTable(c);
                    }
                    checkedClipListBox.Clear();
                    checkBoxAllClips.Checked = false;
                }
            }
        }
        /// <summary>
        /// Open analysis view with the current video.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAnalysis_Click(object sender, EventArgs e)
        {
            //if not visible, show it
            if (!winFormAnalysis.Visible)
            {
                if (mpvPersonalized.MpvPlayer.IsPlaying)
                {
                    mpvPersonalized.MpvPlayer.Pause();
                    winFormAnalysis.centerShow(this.pathVideoPlaying);
                    mpvPersonalized.MpvPlayer.Resume();
                }
                else
                    //create analysis view
                    winFormAnalysis.centerShow(this.pathVideoPlaying);
            }
            else
            {
                winFormAnalysis.Close();
            }
        }
        /// <summary>
        /// Pass the key, to do event.
        /// </summary>
        /// <param name="k"></param>
        public void AnalisysFormEvent(KeyEventArgs k)
        {
            changeCameraEvent(null, k);
        }
        /// <summary>
        /// When clicked, show the sync form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSync_Click(object sender, EventArgs e)
        {
            //if not visible, show it
            if (!WinFormSync.Visible)
            {
                //create analysis view
                WinFormSync.centerShow();
            }
            else
            {
                WinFormSync.Close();
            }
        }
        /// <summary>
        /// Load tags from the path selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileDialogTagPath_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //try catch for check the information
            try
            {
                List<Tag> listTags = JsonConvert.DeserializeObject<List<Tag>>
                    (File.ReadAllText(openFileDialogTagPath.FileName));
                foreach (Tag tag in listTags)
                {
                    //check it
                    if (tag.CheckTag())
                        createAutomaticOrManualTagButton(tag);
                    else
                        //throw exception
                        throw new Exception();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Not " + tagFileName + " file");
            }

        }
        /// <summary>
        /// Save tag in the path selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveFileDialogTag_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if have tag to save, save it
            if (tagMap.Count != 0)
            {
                //save only the tag, the button only have the event handler, tag have all the information
                StreamWriter sw = File.CreateText(saveFileDialogTag.FileName);
                sw.Write(JsonConvert.SerializeObject(tagMap.Values));
                //close it
                sw.Close();
            }
            else
            {
                //else show message
                MessageBox.Show("No hay tags");
            }
        }
        /// <summary>
        /// When tick move the position.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerReversePlay_Tick(object sender, EventArgs e)
        {
            //check loaded
            if (mpvPersonalized.MpvPlayer.IsMediaLoaded)
                lock (MpvPersonalized.MpvPlayer.MpvLock)
                {
                    //backframe
                    mpvPersonalized.MpvPlayer.BackFrame(true);
                }
        }
    }
}

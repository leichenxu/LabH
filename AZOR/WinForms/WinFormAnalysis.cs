using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mpv.NET.Player;
using System.IO;
using System.Threading;

namespace AZOR
{
    public partial class WinFormAnalysis : Form
    {
        #region variable
        private Dictionary<MpvPlayer, Boolean> mapLoaded = new Dictionary<MpvPlayer, bool>();
        private Dictionary<MpvPlayer, ManualResetEvent> mapLoadedEvent = new Dictionary<MpvPlayer, ManualResetEvent>();
        /// <summary>
        /// lock when send command
        /// </summary>
        private object CommandLock = new object();
        /// <summary>
        /// Store the first and second number pressed with A.
        /// </summary>
        private int[] numberPressed = new int[2];
        /// <summary>
        /// Store the number pressed before.
        /// </summary>
        private int[] numberPressedStored = new int[2];
        /// <summary>
        /// If a pressed, wait for two number.
        /// </summary>
        private bool APressed = false;
        /// <summary>
        /// Boolean for reload one video.
        /// </summary>
        private bool reloadOne = false;
        /// <summary>
        /// Boolean for reload two video.
        /// </summary>
        private bool reloadTwo = false;
        /// <summary>
        /// Boolean for reload four video.
        /// </summary>
        private bool reloadFour = false;
        /// <summary>
        /// Main win form.
        /// </summary>
        private WinFormWithVideoPlayer winForm;
        /// <summary>
        /// Store mpv for M press.
        /// </summary>
        private MpvPersonalized[] fourMpv = new MpvPersonalized[4];
        /// <summary>
        /// Store mpv for analysis for two videos.
        /// </summary>
        private MpvPersonalized[] twoMainMpv = new MpvPersonalized[2];
        /// <summary>
        /// Own mpv, full screen.
        /// </summary>
        private MpvPersonalized mpvPersonalized;
        private MpvPersonalized parentMpvPersonalized;
        public MpvPersonalized MpvPersonalized { get => mpvPersonalized; set => mpvPersonalized = value; }
        public bool ReloadOne { get => reloadOne; set => reloadOne = value; }
        public bool ReloadFour { get => reloadFour; set => reloadFour = value; }
        public MpvPersonalized[] TwoMainMpv { get => twoMainMpv; set => twoMainMpv = value; }
        public MpvPersonalized[] FourMpv { get => fourMpv; set => fourMpv = value; }
        public bool ReloadTwo { get => reloadTwo; set => reloadTwo = value; }
        public object CommandLock1 { get => CommandLock; set => CommandLock = value; }
        public Dictionary<MpvPlayer, ManualResetEvent> MapLoadedEvent { get => mapLoadedEvent; set => mapLoadedEvent = value; }
        public Dictionary<MpvPlayer, bool> MapLoaded { get => mapLoaded; set => mapLoaded = value; }
        public int[] NumberPressedStored { get => numberPressedStored; set => numberPressedStored = value; }
        #endregion
        /// <summary>
        /// Constructor.
        /// </summary>
        public WinFormAnalysis(MpvPersonalized mpvPersonalized, WinFormWithVideoPlayer winForm)
        {
            InitializeComponent();
            //create it and add all to the map
            this.MpvPersonalized = new MpvPersonalized(this.Handle);
            MapLoaded.Add(MpvPersonalized.MpvPlayer, false);
            MapLoadedEvent.Add(MpvPersonalized.MpvPlayer, new ManualResetEvent(false));

            parentMpvPersonalized = mpvPersonalized;
            this.winForm = winForm;
            //instance all mpv
            fourMpv[0] = new MpvPersonalized(pictureBoxVideoOne.Handle);
            MapLoaded.Add(fourMpv[0].MpvPlayer, false);
            MapLoadedEvent.Add(fourMpv[0].MpvPlayer, new ManualResetEvent(false));
            fourMpv[1] = new MpvPersonalized(pictureBoxVideoTwo.Handle);
            MapLoaded.Add(fourMpv[1].MpvPlayer, false);
            MapLoadedEvent.Add(fourMpv[1].MpvPlayer, new ManualResetEvent(false));
            fourMpv[2] = new MpvPersonalized(pictureBoxVideoThree.Handle);
            MapLoaded.Add(fourMpv[2].MpvPlayer, false);
            MapLoadedEvent.Add(fourMpv[2].MpvPlayer, new ManualResetEvent(false));
            fourMpv[3] = new MpvPersonalized(pictureBoxVideoFour.Handle);
            MapLoaded.Add(fourMpv[3].MpvPlayer, false);
            MapLoadedEvent.Add(fourMpv[3].MpvPlayer, new ManualResetEvent(false));


            TwoMainMpv[0] = new MpvPersonalized(pictureBoxMainVideoOne.Handle);
            MapLoaded.Add(TwoMainMpv[0].MpvPlayer, false);
            MapLoadedEvent.Add(TwoMainMpv[0].MpvPlayer, new ManualResetEvent(false));
            TwoMainMpv[1] = new MpvPersonalized(pictureBoxMainVideoTwo.Handle);
            MapLoaded.Add(TwoMainMpv[1].MpvPlayer, false);
            MapLoadedEvent.Add(TwoMainMpv[1].MpvPlayer, new ManualResetEvent(false));

            //set to -1
            numberPressed[0] = -1;
            numberPressed[1] = -1;
            this.FormClosing += StopIt;

        }
        /// <summary>
        /// Hide it when close.
        /// </summary>
        private void StopIt(object sender, EventArgs e)
        {

            (e as FormClosingEventArgs).Cancel = true;
            this.Visible = false;
            //all reload to false
            reloadOne = false;
            ReloadTwo = false;
            ReloadFour = false;
            //stop all mpv
            foreach (MpvPersonalized m in FourMpv)
            {
                m.MpvPlayer.Stop();
            }
            foreach (MpvPersonalized m in twoMainMpv)
            {
                m.MpvPlayer.Stop();
            }
            MpvPersonalized.MpvPlayer.Stop();
            //hide all
            foreach (Control c in this.Controls)
            {
                c.Visible = false;
            }
            //resert backcolor
            this.BackColor = Color.Black;
            this.Refresh();
            this.Invalidate();

        }
        /// <summary>
        /// Show it in center.
        /// </summary>
        public void centerShow(string playingVideo)
        {
            //when openned, same state
            if (!parentMpvPersonalized.MpvPlayer.IsPlaying)
                mpvPersonalized.MpvPlayer.AutoPlay = false;
            else
                mpvPersonalized.MpvPlayer.AutoPlay = true;
            //add video
            this.MpvPersonalized.MpvPlayer.Load(playingVideo, winForm.CurrentTimeSpan.ToString());
            mpvPersonalized.PlayingMedia = playingVideo;
            //center it
            this.CenterToScreen();
            this.Show();
            mpvPersonalized.MpvPlayer.MediaLoaded += SetTime;
            //when openned set it
            reloadOne = true;
            //bg image to none
            this.BackgroundImage = null;
        }

        /// <summary>
        /// Set time when load.
        /// </summary>
        private void SetTime(object sender, EventArgs e)
        {
            mapLoaded[sender as MpvPlayer] = true;
            MapLoadedEvent[sender as MpvPlayer].Set();

            foreach(MpvPersonalized m in TwoMainMpv)
            {
                if(m.MpvPlayer==(sender as MpvPlayer))
                    Console.WriteLine("video " + m.PlayingMedia+"sania enviado");
            }
            foreach (MpvPersonalized m in fourMpv)
            {
                if (m.MpvPlayer == (sender as MpvPlayer))
                    Console.WriteLine("video " + m.PlayingMedia + "sania enviado");
            }
            
            //get the player
            Mpv.NET.Player.MpvPlayer player = sender as MpvPlayer;
            //enter parent mpv
            Monitor.Enter(parentMpvPersonalized.MpvPlayer.MpvLock);
            //try enter player
            bool enter = Monitor.TryEnter(player.MpvLock, 1000);
            //if not enter
            if (!enter)
            {
                //leave father
                Monitor.Exit(parentMpvPersonalized.MpvPlayer.MpvLock);
                //wait player and enter
                Monitor.Enter(player.MpvLock);
                //try enter parent again
                if (!Monitor.TryEnter(parentMpvPersonalized.MpvPlayer, 1000))
                {
                    //if fail wait senial
                    winForm.ManualResetEventMpvUnlock.WaitOne();
                    //then enter
                    Monitor.Enter(parentMpvPersonalized.MpvPlayer.MpvLock);
                }
            }
            //check loaded or not the media
            if (player.Duration > TimeSpan.Zero && parentMpvPersonalized.MpvPlayer.Duration > TimeSpan.Zero && player.IsMediaLoaded && parentMpvPersonalized.MpvPlayer.IsMediaLoaded
                && player.Duration >= winForm.CurrentTimeSpan && !winForm.InZero)
            {
                //set position
                player.Position = winForm.CurrentTimeSpan;
                //set position to father for sincronize
                if (parentMpvPersonalized.MpvPlayer.IsMediaLoaded && !winForm.InZero)
                    parentMpvPersonalized.MpvPlayer.Position = winForm.CurrentTimeSpan;
            }
            //leave
            Monitor.Exit(player.MpvLock);
            Monitor.Exit(parentMpvPersonalized.MpvPlayer.MpvLock);
            //send senial
            winForm.ManualResetEventMpvUnlock.Set();
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
            if ((mpvPersonalized.KeysThatIHaveUse(keyData) ||
                keyData == Keys.Tab || keyData == Keys.Up ||
                keyData == Keys.Down || keyData == Keys.Enter))
            {

                //sent the event to parent
                winForm.ProcessCmdDelegate(keyData);
                //interface cannot take the input key
                return true;
            }//change camera
            else if (!reloadFour && !ReloadTwo && !APressed && (keyData == Keys.D1 || keyData == Keys.D2 || keyData == Keys.D3 || keyData == Keys.D4))
            {
                winForm.AnalisysFormEvent(new KeyEventArgs(keyData));
            }
            else if (keyData == Keys.M)
            {
                this.BackgroundImage = Properties.Resources.Análisis_View4_2;
                //if current not show all
                if (!reloadFour)
                    ShowAll();
                //reload and control four video, not one
                ReloadOne = false;
                reloadTwo = false;
                reloadFour = true;
            }
            else if (keyData == Keys.A)
            {
                APressed = true;
            }
            else if (APressed && (keyData == Keys.D1 || keyData == Keys.D2 || keyData == Keys.D3 || keyData == Keys.D4))
            {
                this.BackgroundImage = Properties.Resources.Análisis_View4_2;
                //if [0] is -1, then first press,else othercase
                if (numberPressed[0] == -1)
                {
                    //one case one number
                    switch (keyData)
                    {
                        case Keys.D1:
                            numberPressed[0] = 0;
                            break;
                        case Keys.D2:
                            numberPressed[0] = 1;
                            break;
                        case Keys.D3:
                            numberPressed[0] = 2;
                            break;
                        case Keys.D4:
                            numberPressed[0] = 3;
                            break;
                    }
                }
                else
                {
                    //one case one number
                    switch (keyData)
                    {
                        case Keys.D1:
                            numberPressed[1] = 0;
                            break;
                        case Keys.D2:
                            numberPressed[1] = 1;
                            break;
                        case Keys.D3:
                            numberPressed[1] = 2;
                            break;
                        case Keys.D4:
                            numberPressed[1] = 3;
                            break;
                    }
                    //store for reload
                    numberPressedStored[0] = numberPressed[0];
                    numberPressedStored[1] = numberPressed[1];
                    ShowTwoMainVideo();
                    //show label
                    labelMainVideoOne.Show();
                    labelMainVideoTwo.Show();
                    //after show restart
                    APressed = false;
                    //set it to -1, for restart
                    numberPressed[0] = -1;
                    numberPressed[1] = -1;
                    ReloadOne = false;
                    reloadTwo = true;
                    reloadFour = false;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public void CommandToMpv(Keys keyData)
        {
            if (ReloadOne)
            {
                Thread th = new Thread(() => OneCommand(keyData));
                th.Start();
                th.Join();
                //OneCommand(keyData);
            }
            else if (reloadFour)
            {
                //FourCommand(keyData);
                Thread th = new Thread(() => FourCommand(keyData));
                th.Start();
                th.Join();

            }
            else if (ReloadTwo)
            {
                //TwoCommand(keyData);
                Thread th = new Thread(() => TwoCommand(keyData));
                th.Start();
                th.Join();
            }
        }
        /// <summary>
        /// Execute four command for mpv.
        /// </summary>
        private void OneCommand(Keys keyData)
        {
            lock (mpvPersonalized.MpvPlayer.MpvLock)
                MpvPersonalized.MpvController(new KeyEventArgs(keyData));
        }
        /// <summary>
        /// Execute four command for mpv.
        /// </summary>
        private void FourCommand(Keys keyData)
        {
            //lock it for use
            lock (FourMpv)
            {
                //run 4 mpv, lock it, check it and send command it
                foreach (MpvPersonalized mpv in FourMpv)
                {
                    lock (mpv.MpvPlayer.MpvLock)
                        if (mpv.MpvPlayer.IsMediaLoaded && mpv.MpvPlayer.Duration > TimeSpan.Zero)
                            mpv.MpvController(new KeyEventArgs(keyData));
                }

            }
        }
        /// <summary>
        /// Execute two command for mpv.
        /// </summary>s
        private void TwoCommand(Keys keyData)
        {
            lock (TwoMainMpv)
            {
                foreach (MpvPersonalized mpv in twoMainMpv)
                {
                    lock (mpv.MpvPlayer.MpvLock)
                        if (mpv.MpvPlayer.IsMediaLoaded)
                            mpv.MpvController(new KeyEventArgs(keyData));
                }

            }

        }
        /// <summary>
        /// Show two videos.
        /// </summary>
        private void ShowTwoMainVideo()
        {
            //stop
            this.mpvPersonalized.MpvPlayer.Stop();
            foreach (MpvPersonalized m in fourMpv)
            {
                m.MpvPlayer.Stop();
            }
            //hide it
            pictureBoxAzor1.Visible = false;
            pictureBoxAzor2.Visible = false;
            pictureBoxNameVideo1.Visible = false;
            pictureBoxNameVideo2.Visible = false;
            pictureBoxVideoOne.Visible = false;
            pictureBoxVideoTwo.Visible = false;
            pictureBoxVideoThree.Visible = false;
            pictureBoxVideoFour.Visible = false;


            //show mpv picturebox
            pictureBoxMainVideoOne.Visible = true;
            pictureBoxMainVideoTwo.Visible = true;
            lock (TwoMainMpv)
                //load videos
                for (int i = 0; i < twoMainMpv.Length; i++)
                {
                    //when openned, same state
                    if (!parentMpvPersonalized.MpvPlayer.IsPlaying)
                        twoMainMpv[i].MpvPlayer.AutoPlay = false;
                    else
                        twoMainMpv[i].MpvPlayer.AutoPlay = true;
                    twoMainMpv[i].MpvPlayer.Load(winForm.ChangeCameraUrl[numberPressed[i]] as string, winForm.CurrentTimeSpan.ToString());
                    twoMainMpv[i].PlayingMedia = winForm.ChangeCameraUrl[numberPressed[i]] as string;
                    twoMainMpv[i].MpvPlayer.MediaLoaded += SetTime;
                    twoMainMpv[i].PlayingMedia = winForm.ChangeCameraUrl[numberPressed[i]] as string;
                }
            //set name
            SetLabelText(labelMainVideoOne, twoMainMpv[0].PlayingMedia);
            SetLabelText(labelMainVideoTwo, twoMainMpv[1].PlayingMedia);

        }
        private void SetLabelText(Label label, string video)
        {
            if (video == winForm.ChangeCameraUrl[0] as string)
            {
                label.Text = "Signal1";
            }
            else if (video == winForm.ChangeCameraUrl[1] as string)
            {
                label.Text = "Signal2";
            }
            else if (video == winForm.ChangeCameraUrl[2] as string)
            {
                label.Text = "Signal3";
            }
            else if (video == winForm.ChangeCameraUrl[3] as string)
            {
                label.Text = "Signal4";
            }
        }
        /// <summary>
        /// Show four videos.
        /// </summary>
        private void ShowAll()
        {
            //stop
            this.mpvPersonalized.MpvPlayer.Stop();
            foreach (MpvPersonalized m in twoMainMpv)
            {
                m.MpvPlayer.Stop();
            }
            //reset
            labelMainVideoOne.ResetText();
            labelMainVideoTwo.ResetText();
            //hide it
            pictureBoxAzor1.Show();
            pictureBoxAzor2.Show();
            pictureBoxNameVideo1.Show();
            pictureBoxNameVideo2.Show();
            pictureBoxMainVideoOne.Visible = false;
            pictureBoxMainVideoTwo.Visible = false;
            //show mpv picturebox
            pictureBoxVideoOne.Show();
            pictureBoxVideoTwo.Show();
            pictureBoxVideoThree.Show();
            pictureBoxVideoFour.Show();
            lock (FourMpv)
                //load videos
                for (int i = 0; i < fourMpv.Length; i++)
                {
                    //when openned, same state
                    if (!parentMpvPersonalized.MpvPlayer.IsPlaying)
                        fourMpv[i].MpvPlayer.AutoPlay = false;
                    else
                        fourMpv[i].MpvPlayer.AutoPlay = true;
                    fourMpv[i].MpvPlayer.Load(winForm.ChangeCameraUrl[i] as string, winForm.CurrentTimeSpan.ToString());
                    fourMpv[i].PlayingMedia = winForm.ChangeCameraUrl[i] as string;
                    fourMpv[i].MpvPlayer.MediaLoaded += SetTime;
                }

        }


    }
}

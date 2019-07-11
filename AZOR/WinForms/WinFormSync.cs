using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AZOR
{
    public partial class WinFormSync : Form
    {
        /// <summary>
        /// Save the smaller time span number.
        /// </summary>
        private int timeReference = -1;
        /// <summary>
        /// Save 4 time span, 0->mpv[0],1->mpv[1]...
        /// </summary>
        private TimeSpan[] mpvTimeSpan = new TimeSpan[4];
        /// <summary>
        /// Set mpv to interactive.
        /// </summary>
        private int interactiveMPV = -1;
        /// <summary>
        /// Get the url, etc from winform.
        /// </summary>
        private WinFormWithVideoPlayer winForm;
        private MpvPersonalized[] Mpvs;

        public int TimeReference { get => timeReference; set => timeReference = value; }
        public TimeSpan[] MpvTimeSpan { get => mpvTimeSpan; set => mpvTimeSpan = value; }

        public WinFormSync(WinFormWithVideoPlayer mainWinForm)
        {
            InitializeComponent();
            //save it
            this.winForm = mainWinForm;
            //create four
            Mpvs = new MpvPersonalized[4];
            //create mpv
            Mpvs[0] = new MpvPersonalized(pictureBoxVideoOne.Handle);
            Mpvs[1] = new MpvPersonalized(pictureBoxVideoTwo.Handle);
            Mpvs[2] = new MpvPersonalized(pictureBoxVideoThree.Handle);
            Mpvs[3] = new MpvPersonalized(pictureBoxVideoFour.Handle);
            this.BackgroundImage = Properties.Resources.Análisis_View4_2;
            this.FormClosing += StopIt;
        }
        /// <summary>
        /// Stop the event, and just hide it.
        /// </summary>
        private void StopIt(object sender, EventArgs e)
        {
            //not close
            (e as FormClosingEventArgs).Cancel = true;
            //not visible
            this.Visible = false;
            //stop videos
            foreach (MpvPersonalized mpv in Mpvs)
            {
                mpv.MpvPlayer.Stop();
            }
        }

        /// <summary>
        /// Override input received before do something in user interface, for not move in buttons.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //case number, store it
            switch (keyData)
            {
                case Keys.D1:
                    interactiveMPV = 0;
                    break;
                case Keys.D2:
                    interactiveMPV = 1;
                    break;
                case Keys.D3:
                    interactiveMPV = 2;
                    break;
                case Keys.D4:
                    interactiveMPV = 3;
                    break;
            }
            //if is used then no move in interface, and not use in other panel
            if ((Mpvs[0].KeysThatIHaveUse(keyData) ||
                keyData == Keys.Tab || keyData == Keys.Up ||
                keyData == Keys.Down || keyData == Keys.Enter) && (interactiveMPV > -1))
            {
                //sent the event to the controller
                Mpvs[interactiveMPV].MpvController(new KeyEventArgs(keyData));
                //interface cannot take the input key
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        /// <summary>
        /// Save the difference between video.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            SaveTimeEvent();
        }
        /// <summary>
        /// Method save the time difference.
        /// </summary>
        private void SaveTimeEvent()
        {
            lock (Mpvs)
            {
                //First pause all
                for (int i = 0; i < Mpvs.Length; i++)
                {
                    Mpvs[i].MpvPlayer.Pause();
                }
                //save the first time
                TimeSpan time = Mpvs[0].MpvPlayer.Position;
                timeReference = 0;
                //save four time
                for (int i = 0; i < Mpvs.Length; i++)
                {
                    mpvTimeSpan[i] = Mpvs[i].MpvPlayer.Position;
                    //if is smaller then save
                    if (Mpvs[i].MpvPlayer.Position < time)
                    {
                        time = Mpvs[i].MpvPlayer.Position;
                        timeReference = i;
                    }
                }
                //Set the difference
                for (int i = 0; i < Mpvs.Length; i++)
                {
                    mpvTimeSpan[i]=mpvTimeSpan[i].Subtract(time);
                }
            }
        }
        /// <summary>
        /// Show it in center.
        /// </summary>
        public void CenterShow()
        {
            //reset
            interactiveMPV = -1;
            //center it
            this.CenterToScreen();
            this.Show();
            //load videos
            for (int i = 0; i < winForm.ChangeCameraUrl.Capacity; i++)
            {
                //set pause or play when the media is loaded
                Mpvs[i].MpvPlayer.MediaLoaded += SetPauseOrPlay;
                //load video with time
                Mpvs[i].MpvPlayer.Load(winForm.ChangeCameraUrl[i] as string, winForm.CurrentTimeSpan.ToString());
                Mpvs[i].MpvPlayer.KeepOpen = Mpv.NET.Player.KeepOpen.Yes;
            }
        }
        /// <summary>
        /// Set pause or play when the media is loaded
        /// </summary>
        private void SetPauseOrPlay(object sender, EventArgs e)
        {
            //depends on main winform
            if (winForm.MpvPersonalized.MpvPlayer.IsPlaying)
            {
                (sender as Mpv.NET.Player.MpvPlayer).Resume();
            }
            else
            {
                (sender as Mpv.NET.Player.MpvPlayer).Pause();
            }
        }

    }
}

﻿using System;
using System.Threading;
using System.Windows.Forms;
using System.IO;
namespace Practice7
{

    public partial class WinFormLoading : Form
    {
        /// <summary>
        /// The time for "load"(display the progress bar animation)
        /// </summary>
        private int tiempoMax = 2000;
        /// <summary>
        /// Class constructor.
        /// </summary>
        public WinFormLoading()
        {
            InitializeComponent();
            //take the position
            decimal position = 1;
            //take the map
            position = position.Map(0, 100, 0, tiempoMax);
            //assing the position for interval
            this.timer.Interval = Decimal.ToInt32(position);
            //start timer
            this.timer.Start();
            //check if azor existe, if not create
            checkAndCreateAZOR();

            //center
            this.CenterToScreen();
        }
        /// <summary>
        /// Check if azor existe, if not create.
        /// </summary>
        private void checkAndCreateAZOR()
        {
            //get current pc documents path
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //if doest exist azor create
            if (!Directory.Exists(documentsPath + @"\AZOR"))
            {
                Directory.CreateDirectory(documentsPath + "\\AZOR");
            }
            //after create or not check if the file exist, if not create
            if (!File.Exists(documentsPath + "\\AZOR\\lstpjs"))
            {
                var file = File.Create(documentsPath + "\\AZOR\\lstpjs");
                //close after create
                file.Close();
            }
        }
        /// <summary>
        /// Add the increment to the progress bar.
        /// </summary>
        private void incrementProgressBar()
        {
            //increment 1 the progress bar
            this.progressBarLoading.Increment(1);
            //try to clean
            Application.DoEvents();
        }
        /// <summary>
        /// When tick increment progress bar and check if finished.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerProgressBar_Tick(object sender, EventArgs e)
        {
            //increment
            this.incrementProgressBar();
            //check if finished
            if (this.progressBarLoading.Value == this.progressBarLoading.Maximum)
            {
                //stop the timer
                timer.Stop();
                //wait the animation finish
                this.timerEndWinForm.Start();
            }
        }
        /// <summary>
        /// Open the new winform when is called.
        /// </summary>
        private void opennewform()
        {
            Application.Run(new MainWinForm(new WinFormSetting()));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerEndWinForm_Tick(object sender, EventArgs e)
        {
            //stop the timer
            this.timerEndWinForm.Stop();
            //create new thread
            Thread th = new Thread(opennewform);
            th.SetApartmentState(ApartmentState.STA);
            //start thread
            th.Start();
            //close this
            this.Close();
        }
    }

}

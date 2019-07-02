using Mpv.NET.Player;
using System.Windows.Forms;
using System;

namespace Mpv.NET.WinFormsExample
{
	public partial class MainForm : Form
	{
		private MpvPlayer player;

		public MainForm()
		{
			InitializeComponent();

            player = new MpvPlayer(this.Handle)
            {
                Loop = true,
                Volume = 50
            };
            player.Load(@"http://techslides.com/demos/sample-videos/small.mp4");
            //player.Load("C:\\Users\\lei_1\\Desktop\\practicas\\Main1.mp4");
            
			player.Resume();
            player.MediaLoaded += this.seguirLeyendo;
            this.KeyPress += controller;
        }
        private void seguirLeyendo(object sender,EventArgs e)
        {
            
        }
        private void controller(object sender, KeyPressEventArgs e) {
            if (e.KeyChar =='R')
            {
                TimeSpan t = new TimeSpan(0, 0, 0, 50, 0);
                player.Position = t;
            }
        }
		private void MainFormOnFormClosing(object sender, FormClosingEventArgs e)
		{
			player.Dispose();
		}
	}
}
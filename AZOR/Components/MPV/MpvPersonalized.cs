using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpv.NET.Player;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;

namespace AZOR
{
    /// <summary>
    /// Give the handle to the media player.
    /// This class wil control basic functions.
    /// </summary>
    public class MpvPersonalized
    {
        /// <summary>
        /// Check reverse playing or not.
        /// </summary>
        private bool reversePlaying = false;
        /// <summary>
        /// Timer for reverse play.
        /// </summary>
        private Timer timerReversePlay = new Timer();
        /// <summary>
        /// Store the playing video.
        /// </summary>
        private string playingMedia;
        /// <summary>
        /// move the current position to the duraction adding these timespans
        /// </summary>
        #region key list
        private static Keys pauseOrPlayVideo = Keys.Space;
        private static Keys nextFrame = Keys.Right;
        private static Keys previousFrame = Keys.Left;
        private static Keys moveForwardKeys = (Keys.Right | Keys.Shift);
        private static KeyEventArgs moveForward = new KeyEventArgs(MoveForwardKeys);
        private static Keys moveBackwardKeys = (Keys.Left | Keys.Shift);
        private static KeyEventArgs moveBackward = new KeyEventArgs(MoveBackwardKeys);
        private static Keys moveVideoToStartPoint = Keys.R;
        private static Keys moveVideoToEndPoint = Keys.L;
        private static Keys playVideoOne = Keys.D1;
        private static Keys playVideoTwo = Keys.D2;
        private static Keys playVideoThree = Keys.D0;
        private static Keys saveCurrentTime = Keys.I;
        #endregion
        /// <summary>
        /// move the current position to the duraction adding these timespans
        /// </summary>
        #region timespan              
        private TimeSpan timeForMoveToVideoEndPoint = new TimeSpan(0, 0, 0, 11);
        private TimeSpan timeSpanForAddOrSubstract = new TimeSpan(0, 0, 3);
        private TimeSpan timeSpanForReversePlay = new TimeSpan(0, 0, 0, 0, 200);
        private TimeSpan timeSpanForCheckEndOfFile = new TimeSpan(0, 0, 0, 1);
        #endregion
        /*
         * The media player used.
         */
        private Mpv.NET.Player.MpvPlayer mpvPlayer;

        /// <summary>
        /// Getter and setter automatically generated.
        /// </summary>
        #region getters and setters
        public static Keys NextFrame { get => nextFrame; set => nextFrame = value; }
        public static Keys PreviousFrame { get => previousFrame; set => previousFrame = value; }
        public static Keys MoveForwardKeys { get => moveForwardKeys; set => moveForwardKeys = value; }
        public static KeyEventArgs MoveForward { get => moveForward; set => moveForward = value; }
        public static Keys MoveBackwardKeys { get => moveBackwardKeys; set => moveBackwardKeys = value; }
        public static KeyEventArgs MoveBackward { get => moveBackward; set => moveBackward = value; }
        public static Keys MoveVideoToStartPoint { get => moveVideoToStartPoint; set => moveVideoToStartPoint = value; }
        public static Keys MoveVideoToEndPoint { get => moveVideoToEndPoint; set => moveVideoToEndPoint = value; }
        public static Keys PlayVideoOne { get => playVideoOne; set => playVideoOne = value; }
        public static Keys PlayVideoTwo { get => playVideoTwo; set => playVideoTwo = value; }
        public static Keys PlayVideoThree { get => playVideoThree; set => playVideoThree = value; }
        public static Keys SaveCurrentTime { get => saveCurrentTime; set => saveCurrentTime = value; }
        public static Keys PauseOrPlayVideo { get => pauseOrPlayVideo; set => pauseOrPlayVideo = value; }
        public TimeSpan TimeSpanForAddOrSubstract { get => timeSpanForAddOrSubstract; set => timeSpanForAddOrSubstract = value; }
        public TimeSpan TimeForMoveToVideoEndPoint { get => timeForMoveToVideoEndPoint; set => timeForMoveToVideoEndPoint = value; }
        public MpvPlayer MpvPlayer { get => mpvPlayer; set => mpvPlayer = value; }
        public string PlayingMedia { get => playingMedia; set => playingMedia = value; }
        public TimeSpan TimeSpanForReversePlay { get => timeSpanForReversePlay; set => timeSpanForReversePlay = value; }
        public TimeSpan TimeSpanForCheckEndOfFile { get => timeSpanForCheckEndOfFile; set => timeSpanForCheckEndOfFile = value; }
        public bool ReversePlaying { get => reversePlaying; set => reversePlaying = value; }
        public Timer TimerReversePlay { get => timerReversePlay; set => timerReversePlay = value; }
        #endregion

        /// <summary>
        /// Take the handle and assign to the media player.
        /// </summary>
        /// <param name="handle">The handle that use to display the media player.</param>
        public MpvPersonalized(IntPtr handle)
        {
            //assign the handle to the media player
            MpvPlayer = new Mpv.NET.Player.MpvPlayer(handle);
            mpvPlayer.FullScreenOption();
            //change if needed, true if you need to start play when the file is loaded
            MpvPlayer.AutoPlay = true;
            //dont close the mpv after finish the video
            MpvPlayer.KeepOpen = Mpv.NET.Player.KeepOpen.Yes;
            //add tick
            TimerReversePlay.Tick += timerReversePlay_Tick;
        }
        /// <summary>
        /// When tick move the position.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerReversePlay_Tick(object sender, EventArgs e)
        {
            //check loaded
            if (MpvPlayer.IsMediaLoaded)
                //safe lock
                lock (MpvPlayer.MpvLock)
                {
                    //pause it
                    MpvPlayer.Pause();
                    //check time
                    if (MpvPlayer.Position.Subtract(TimeSpanForReversePlay) > TimeSpanForReversePlay)
                    {
                        //time
                        MpvPlayer.Position = MpvPlayer.Position.Subtract(TimeSpanForReversePlay);
                        //then back frame
                        MpvPlayer.BackFrame();
                    }
                    else
                        //set zero for not running all time
                        MpvPlayer.Position = TimeSpan.Zero;
                }
        }
        /// <summary>
        /// Check if the media player need to use this key or not.
        /// </summary>
        /// <param name="keyData">keyData received to check if is used or not.</param>
        /// <returns>Return the comparition with all keys used.</returns>
        public Boolean KeysThatIHaveUse(Keys keyData)
        {
            return keyData == NextFrame || keyData == PreviousFrame ||
                keyData == MoveForwardKeys || keyData == MoveBackwardKeys ||
                keyData == PauseOrPlayVideo || keyData == MoveVideoToStartPoint || keyData == MoveVideoToEndPoint;
        }

        /// <summary>
        /// The controller that manage all event when the key used is pressed.
        /// </summary>       
        /// <param name="keyEventArgs">Take the keyEventArgs for take inputs like shift+left, used to 
        /// check what action to do.</param>
        /// <returns>Return the key pressed if need to use.</returns>
        public KeysUsed MpvController(KeyEventArgs keyEventArgs)
        {
            KeysUsed k = OwnController(keyEventArgs);
            //check add speed or not
            if (k != KeysUsed.AddSpeed)
            {
                //set speed to 1
                MpvPlayer.Speed = 1;
            }
            if (k != KeysUsed.NoRecognized && k != KeysUsed.ReverseSpeed)
            {

                //check reverse play
                if (timerReversePlay.Enabled)
                {
                    //stop the timer
                    timerReversePlay.Stop();
                    //reverse play to false
                    reversePlaying = false;
                    //check space for pause
                    if (keyEventArgs.KeyData == Keys.Space)
                    {
                        //pause it, dont play
                        MpvPlayer.Pause();
                    }
                }
            }
            return k;
        }
        /// <summary>
        /// Do controller things with key received.
        /// </summary>
        /// <param name="keyEventArgs"></param>
        private KeysUsed OwnController(KeyEventArgs keyEventArgs)
        {
            lock (this.MpvPlayer.MpvLock)
                //check if have media loaded, if not doesnt need to do something
                if (this.MpvPlayer.IsMediaLoaded)
                {
                    //check what key is used
                    if (keyEventArgs.KeyCode == PauseOrPlayVideo)
                    {
                        //if mpv is not playing resume the video else pause
                        if (!MpvPlayer.IsPlaying)
                        {
                            MpvPlayer.Resume();
                            return KeysUsed.Play;
                        }
                        else
                            MpvPlayer.Pause();
                        return KeysUsed.Pause;
                    }
                    else if (keyEventArgs.KeyCode == MoveForward.KeyCode && keyEventArgs.Modifiers == MoveForward.Modifiers)
                    {

                        //go to timeSetWithKey for move forward and backward
                        MpvPlayer.Position = TimeSetWithKey(keyEventArgs);
                        //if mpv is in the last second return 
                        if (MpvPlayer.Position == MpvPlayer.Duration)
                        {
                            return KeysUsed.VideoFinished;
                        }
                        return KeysUsed.MoveForward;

                    }
                    else if (keyEventArgs.KeyCode == MoveBackward.KeyCode && keyEventArgs.Modifiers == MoveBackward.Modifiers)
                    {
                        //go to timeSetWithKey for move forward and backward
                        MpvPlayer.Position = TimeSetWithKey(keyEventArgs);
                        return KeysUsed.MoveBackWard;
                    }
                    else if (keyEventArgs.KeyCode == NextFrame)
                    {
                        this.mpvPlayer.NextFrame();
                        //return video finished if ended
                        if (MpvPlayer.Remaining == TimeSpan.Zero)
                        {
                            return KeysUsed.VideoFinished;
                        }
                        return KeysUsed.NextFrame;
                    }
                    else if (keyEventArgs.KeyCode == previousFrame)
                    {
                        mpvPlayer.BackFrame();
                        return KeysUsed.PreviousFrame;
                    }
                    if (keyEventArgs.KeyCode == MoveVideoToStartPoint)
                    {
                        MpvPlayer.Position = TimeSpan.Zero;
                        return KeysUsed.MoveToStartPoint;
                    }
                    else if (keyEventArgs.KeyCode == MoveVideoToEndPoint)
                    {
                        if (mpvPlayer.Duration > timeForMoveToVideoEndPoint)
                            MpvPlayer.Position = this.MpvPlayer.Duration.Subtract(TimeForMoveToVideoEndPoint);
                        else
                            mpvPlayer.Position = TimeSpan.Zero;
                        //keep playing
                        MpvPlayer.Resume();
                        return KeysUsed.MoveToEndPoint;
                    }
                    else
                    {
                        //change speeed
                        if (AddSpeedCheck(keyEventArgs) != -1)
                        {
                            return KeysUsed.AddSpeed;
                        }
                        else if (KeyPressReverseSpeed(keyEventArgs) != -1)
                        {
                            return KeysUsed.ReverseSpeed;
                        }
                    }
                }
            return KeysUsed.NoRecognized;
        }

        /// <summary>
        /// Check if the media player need to use this key for change speed.
        /// </summary>
        /// <param name="keyData">keyData received to check if is used or not.</param>
        /// <returns>Return the comparition with all keys used.</returns>
        public Boolean KeysSpeedUse(Keys keyData)
        {
            return keyData == Keys.Oem6 /*¡*/|| keyData == Keys.Oem4/*'*/ ||
                keyData == Keys.D0 || keyData == Keys.C | keyData == Keys.V
                || keyData == Keys.B ||
                keyData == Keys.N || keyData == Keys.Oemcomma/*,*/ || keyData == Keys.OemPeriod/*.*/
                || keyData == Keys.OemMinus/*-*/  ||
                 keyData == Keys.X ||
                keyData == Keys.Z || keyData == Keys.K ||
                keyData == Keys.J ||
                keyData == Keys.G || keyData == Keys.D ||
                keyData == Keys.E;
        }
        /// <summary>
        /// Check speed add
        /// </summary>
        /// <param name="keyEventArgs"></param>
        private double AddSpeedCheck(KeyEventArgs keyEventArgs)
        {
            double speed = -1;
            //if ¡ pressed, speed 0.25
            if (keyEventArgs.KeyData == Keys.Oem6)
            {
                speed = 0.25;
            }
            //speed x0.5
            else if (keyEventArgs.KeyData == Keys.Oem4)
            {
                speed = 0.5;
            }
            //speed x0.75
            else if (keyEventArgs.KeyData == Keys.D0)
            {
                speed = 0.75;
            }
            //speed x2
            else if (keyEventArgs.KeyData == Keys.C)
            {
                speed = 2;
            }
            //speed x4
            else if (keyEventArgs.KeyData == Keys.V)
            {
                speed = 4;
            }
            //speed x8
            else if (keyEventArgs.KeyData == Keys.B)
            {
                speed = 8;
            }
            //speed x16
            else if (keyEventArgs.KeyData== Keys.N)
            {
                speed = 16;
            }
            //speed x32
            else if (keyEventArgs.KeyData == Keys.Oemcomma)
            {
                speed = 32;
            }
            //speed x64
            else if (keyEventArgs.KeyData == Keys.OemPeriod)
            {
                speed = 64;
            }
            //speed x100
            else if (keyEventArgs.KeyData == Keys.OemMinus/*-*/ )
            {
                speed = 100;
            }
            //check add speed
            if (speed != -1)
            {
                //play it
                MpvPlayer.Resume();
                //stop the timer
                timerReversePlay.Stop();
                //set the time
                MpvPlayer.Speed = speed;
                //set to false
                reversePlaying = false;
            }
            return speed;
        }

        /// <summary>
        /// Reverse play simulation changing time span.
        /// </summary>
        private int KeyPressReverseSpeed(KeyEventArgs k)
        {
            int interval = -1;
            //if x pressed, speed -2
            if (k.KeyData== Keys.X)
            {
                interval = 240;
            }
            //speed X-4
            else if (k.KeyData== Keys.Z)
            {
                interval = 120;
            }
            //speed X-8
            else if (k.KeyData == Keys.K)
            {
                interval = 100;
            }
            //speed x-16
            else if (k.KeyData == Keys.J)
            {
                interval = 80;
            }
            //speed X-32
            else if (k.KeyData == Keys.G)
            {
                interval = 40;
            }
            //speed x-64
            else if (k.KeyData == Keys.D)
            {
                interval = 20;
            }
            //speed x-128
            else if (k.KeyData == Keys.E)
            {
                interval = 10;
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
            return interval;
        }

        /// <summary>
        /// General code after reverse key is pressed.
        /// </summary>
        private void AfterReverseKeyPressed()
        {
            //set true
            reversePlaying = true;
            timerReversePlay.Start();
            MpvPlayer.Pause();
        }
        /// <summary>
        /// Set te time forward or backward with the key received.
        /// </summary>
        /// <param name="keyEventArgs">KeyEventArg for now move forward or backward.</param>
        /// <returns>The new timeSpan position for media player.</returns>
        private TimeSpan TimeSetWithKey(KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.Modifiers == Keys.Shift)
            {
                //check forward or backward
                if (keyEventArgs.KeyCode == moveForward.KeyCode)
                {
                    //if duration-current position>timeforaddorsubstract then add timespan else return duration
                    if (this.MpvPlayer.Duration.Subtract(MpvPlayer.Position) > timeSpanForAddOrSubstract)
                    {
                        return this.MpvPlayer.Position.Add(timeSpanForAddOrSubstract);
                    }
                    else
                    {
                        return this.MpvPlayer.Position;
                    }
                }
                else if (keyEventArgs.KeyCode == MoveBackward.KeyCode)
                {
                    //if duration-current position>timeforaddorsubstract then substract timespan
                    if (timeSpanForAddOrSubstract < this.MpvPlayer.Position)
                    {
                        return this.MpvPlayer.Position.Subtract(timeSpanForAddOrSubstract);
                    }
                }
            }
            //return start point if nothing to do or is movebackward to zero
            return TimeSpan.Zero;
        }
        /// <summary>
        /// dispose the mpvPersonalized
        /// </summary>
        public void Dispose()
        {
            this.MpvPlayer.Dispose();
        }
    }
}

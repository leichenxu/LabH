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
        }

        /// <summary>
        /// Check if the media player need to use this key or not.
        /// </summary>
        /// <param name="keyData">keyData received to check if is used or not.</param>
        /// <returns>Return the comparition with all keys used.</returns>
        public Boolean keysThatIHaveUse(Keys keyData)
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
        public KeysUsed mpvController(KeyEventArgs keyEventArgs)
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
                        MpvPlayer.Position = timeSetWithKey(keyEventArgs);
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
                        MpvPlayer.Position = timeSetWithKey(keyEventArgs);
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
                }
            return KeysUsed.NoRecognized;
        }

        /// <summary>
        /// Set te time forward or backward with the key received.
        /// </summary>
        /// <param name="keyEventArgs">KeyEventArg for now move forward or backward.</param>
        /// <returns>The new timeSpan position for media player.</returns>
        private TimeSpan timeSetWithKey(KeyEventArgs keyEventArgs)
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

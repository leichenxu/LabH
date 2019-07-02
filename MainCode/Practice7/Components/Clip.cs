using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practice7
{
    class Clip
    {
        /// <summary>
        /// Save my color
        /// </summary>
        private Color myColor=Color.Black;
        /// <summary>
        /// Store the previous time.
        /// </summary>
        private string previousTime;
        /// <summary>
        /// Store the later time.
        /// </summary>
        private string laterTime;
        /// <summary>
        /// Store the tag name.
        /// </summary>
        private string tagName;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="previousTime"></param>
        /// <param name="laterTime"></param>
        /// <param name="tagName"></param>
        public Clip(string previousTime, string laterTime, string tagName)
        {
            this.previousTime = previousTime;
            this.laterTime = laterTime;
            this.tagName = tagName;
        }
        /// <summary>
        /// Getters and setters.
        /// </summary>
       #region getters and setters.
        public string PreviousTime { get => previousTime; set => previousTime = value; }
        public string LaterTime { get => laterTime; set => laterTime = value; }
        public string TagName { get => tagName; set => tagName = value; }
        public Color MyColor { get => myColor; set => myColor = value; }
        #endregion
    }
}

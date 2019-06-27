using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practice7
{
    class Clip
    {
        /// <summary>
        /// Store the previous time.
        /// </summary>
        private Label previousTime;
        /// <summary>
        /// Store the later time.
        /// </summary>
        private Label laterTime;
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
        public Clip(Label previousTime, Label laterTime, string tagName)
        {
            this.previousTime = previousTime;
            this.laterTime = laterTime;
            this.tagName = tagName;
        }
        /// <summary>
        /// Getters and setters.
        /// </summary>
       #region getters and setters.
        public Label PreviousTime { get => previousTime; set => previousTime = value; }
        public Label LaterTime { get => laterTime; set => laterTime = value; }
        public string TagName { get => tagName; set => tagName = value; }
        #endregion
    }
}

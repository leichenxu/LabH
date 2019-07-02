using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice7
{
    class Tag
    {
        /// <summary>
        /// The tag color.
        /// </summary>
        private Color tagColor;
        /// <summary>
        /// Tag name associated.
        /// </summary>
        private String tagName;
        /// <summary>
        /// Tag counter.
        /// </summary>
        private int tagCount;
        /// <summary>
        /// Previous time to substract.
        /// </summary>
        private TimeSpan previousTime;
        /// <summary>
        /// Later time for add.
        /// </summary>
        private TimeSpan laterTimer;
        /// <summary>
        /// Mode of tag.
        /// </summary>
        private TagMode tagMode;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="tagCount"></param>
        /// <param name="previousTime"></param>
        /// <param name="laterTimer"></param>
        public Tag(string tagName, TimeSpan previousTime, TimeSpan laterTimer, TagMode tagMode, Color tagColor)
        {
            this.tagName = tagName;
            this.previousTime = previousTime;
            this.laterTimer = laterTimer;
            this.tagMode = tagMode;
            this.tagColor = tagColor;
        }
        /// <summary>
        /// For Json default constructor.
        /// </summary>
        public Tag()
        {

        }
        public bool CheckTag()
        {
            return this.tagName != null && this.previousTime != null && this.laterTimer != null && this.tagMode != null && this.TagColor != null;
        }
        /// <summary>
        /// Getters and setters.
        /// </summary>
        #region getters and setters.
        public string TagName { get => tagName; set => tagName = value; }
        public int TagCount { get => tagCount; set => tagCount = value; }
        public TimeSpan PreviousTime { get => previousTime; set => previousTime = value; }
        public TimeSpan LaterTimer { get => laterTimer; set => laterTimer = value; }
        public TagMode TagMode { get => tagMode; set => tagMode = value; }
        public Color TagColor { get => tagColor; set => tagColor = value; }
        #endregion
    }
}

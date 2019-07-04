using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice7
{
    public class NewTagInfo
    {
        private string tagName;
        private TagMode tagMode;
        private TimeSpan preTime;
        private TimeSpan postTime;
        private Color selectedColor;

        public NewTagInfo(string tagName, TagMode tagMode, TimeSpan preTime, TimeSpan postTime, Color selectedColor)
        {
            this.TagName = tagName;
            this.TagMode = tagMode;
            this.PreTime = preTime;
            this.PostTime = postTime;
            this.SelectedColor = selectedColor;
        }

        public string TagName { get => tagName; set => tagName = value; }
        public TagMode TagMode { get => tagMode; set => tagMode = value; }
        public TimeSpan PreTime { get => preTime; set => preTime = value; }
        public TimeSpan PostTime { get => postTime; set => postTime = value; }
        public Color SelectedColor { get => selectedColor; set => selectedColor = value; }
    }
}

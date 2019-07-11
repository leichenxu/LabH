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
    public partial class PanelTagForm : Form
    {
        /// <summary>
        /// Store new tag information.
        /// </summary>
        private Tag newTagInfo;
        /// <summary>
        /// Check if the panel have information for new tag or not.
        /// </summary>
        private Boolean haveInformation = false;
        /// <summary>
        /// Error msg, if the content of tagname is null.
        /// </summary>
        private string errorTagNameTextBoxMsg = "Nombre incorrecto";
        /// <summary>
        /// Error msg, if the content of previus time is null.
        /// </summary>
        private string errorTagPreviousTimeTextBoxMsg = "Tiempo anterior incorrecto";
        /// <summary>
        /// Error msg, if the content of later time is null.
        /// </summary>
        private string errorTagLaterTimeTextBoxMsg = "Tiempo posterior incorrecto";
        /// <summary>
        /// Error msg, if the select of combobox is wrong.
        /// </summary>
        private string errorModeMsg = "Modo incorrecto";

        public PanelTagForm()
        {
            InitializeComponent();

            // set the limit of tag name
            this.textBoxNameTag.MaxLength = TAG_NAME_MAX_LENGTH;
            //add the colour to the create tag panel
            ColorPanel = new ColorPanel(panelForColour);

            //adjust the combo box
            comboBoxMode.Items.Add(TagMode.AutomaticText());
            comboBoxMode.Items.Add(TagMode.ManualText());

            //set to automatic
            comboBoxMode.SelectedIndex = 0;

            //transparent background,use a  color that never will use
            this.BackColor = Color.LimeGreen;
            this.TransparencyKey = Color.LimeGreen;
        }
        public ColorPanel ColorPanel { get => colorPanel; set => colorPanel = value; }
        public bool HaveInformation { get => haveInformation; set => haveInformation = value; }
        internal Tag NewTagInfo { get => newTagInfo; set => newTagInfo = value; }
        /// <summary>
        /// Hide from alt+tab.
        /// </summary>
        protected override CreateParams CreateParams{
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x80;
                return cp;
            }
        }
        /// <summary>
        /// Maximun lenght of tag name
        /// </summary>
        private static readonly int TAG_NAME_MAX_LENGTH = 40;
        /// <summary>
        /// The class that allow to show colour in panel.
        /// </summary>
        private ColorPanel colorPanel;        
       
        /// <summary>
        /// Clean the content in the panel.
        /// </summary>
        private void CleanCreatePanelTag()
        {
            ColorPanel.GetColor = Color.Black;
            //set to automatic
            comboBoxMode.SelectedIndex = 0;
            //reset the text, set to zero
            numericUpDownPostTime.Value = 0;
            numericUpDownPreTime.Value = 0;

            foreach (Object o in this.Controls)
            {
                TextBox t = o as TextBox;
                if (t != null)
                    t.ResetText();
            }
        }
        /// <summary>
        /// Close the panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCancelCreateTag_Click(object sender, EventArgs e)
        {            
            this.HaveInformation = false;
            this.Visible = false;
            this.CleanCreatePanelTag();
            
        }

        /// <summary>
        /// Create the tag.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCreateTag_Click(object sender, EventArgs e)
        {
            string errorMsg = "";
            int previousTimeSpan = 0;
            int laterTimeSpan = 0;
            //check text box have context, and check is number or not
            if (textBoxNameTag.Text.Equals(""))
            {
                //clean it if the content is wrong
                textBoxNameTag.ResetText();
                errorMsg += errorTagNameTextBoxMsg + "\n";
            }
            if ((numericUpDownPreTime.Text.Equals("") || !Int32.TryParse(numericUpDownPreTime.Text, out previousTimeSpan)) &&
                (!comboBoxMode.Text.Equals(TagMode.ManualText())))
            {
                //clean it if the content is wrong
                numericUpDownPreTime.ResetText();
                errorMsg += errorTagPreviousTimeTextBoxMsg + "\n";
            }
            if ((numericUpDownPostTime.Text.Equals("") || !Int32.TryParse(numericUpDownPostTime.Text, out laterTimeSpan))
                && (!comboBoxMode.Text.Equals(TagMode.ManualText())))
            {
                //clean it if the content is wrong
                numericUpDownPostTime.ResetText();
                errorMsg += errorTagLaterTimeTextBoxMsg;
            }
            if (!comboBoxMode.Text.Equals(TagMode.ManualText()) && !comboBoxMode.Text.Equals(TagMode.AutomaticText()))
            {
                //clean it if the content is wrong
                comboBoxMode.ResetText();
                errorMsg += errorModeMsg;
            }
            //if the date is not correct, show the message
            if (!errorMsg.Equals(""))
            {
                MessageBox.Show(errorMsg);
            }
            else
            {
                //store the info
                NewTagInfo = new Tag(textBoxNameTag.Text, new TimeSpan(0, 0, previousTimeSpan), new TimeSpan(0, 0, laterTimeSpan),
                    new TagMode(comboBoxMode.Text),colorPanel.GetColor);
                this.HaveInformation = true;
                this.Visible = false;
                CleanCreatePanelTag();
            }
        }
        /// <summary>
        /// When select automatic show the time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TagMode.AutomaticText().Equals(comboBoxMode.Text))
            {
                this.SetTimeVisible(true);
            }
            else
            {
                this.SetTimeVisible(false);
            }
        }
        /// <summary>
        /// Set the visible to false or true.
        /// </summary>
        private void SetTimeVisible(Boolean v)
        {
            this.labelLaterTime.Visible = v;
            this.labelPreviousTime.Visible = v;
            this.numericUpDownPostTime.Visible = v;
            this.numericUpDownPreTime.Visible = v;
        }
    }
}

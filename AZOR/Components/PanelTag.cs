using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AZOR
{
    public class PanelTag
    {
        public PanelTag()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Return the panel.
        /// </summary>
        /// <returns></returns>
        public Panel getPanel()
        {
            return panelCreateTag;
        }
        /// <summary>
        /// Create the tag.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCreateTag_Click(object sender, EventArgs e)
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
            if ((textBoxPreviousTime.Text.Equals("") || !Int32.TryParse(textBoxPreviousTime.Text, out previousTimeSpan))&&
                (!comboBoxMode.Text.Equals(TagMode.manualText())))
            {
                //clean it if the content is wrong
                textBoxPreviousTime.ResetText();
                errorMsg += errorTagPreviousTimeTextBoxMsg + "\n";
            }
            if ((textBoxLaterTime.Text.Equals("") || !Int32.TryParse(textBoxLaterTime.Text, out laterTimeSpan))
                &&(!comboBoxMode.Text.Equals(TagMode.manualText())))
            {
                //clean it if the content is wrong
                textBoxLaterTime.ResetText();
                errorMsg += errorTagLaterTimeTextBoxMsg;
            }
            if (!comboBoxMode.Text.Equals(TagMode.manualText()) && !comboBoxMode.Text.Equals(TagMode.automaticText()))
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
                newTagInfo = new Tag(textBoxNameTag.Text, new TimeSpan(0, 0, previousTimeSpan), new TimeSpan(0, 0, laterTimeSpan),
                    new TagMode(comboBoxMode.Text),colorPanel.GetColor);                                
                this.haveInformation = true;
                panelCreateTag.Visible = false;
                cleanCreatePanelTag();
            }
        }
        /// <summary>
        /// Clean the content in the panel.
        /// </summary>
        private void cleanCreatePanelTag()
        {
            ColorPanel.GetColor = Color.Black;
            comboBoxMode.ResetText();
            foreach (Object o in panelCreateTag.Controls)
            {
                TextBox t = o as TextBox;
                if (t != null)
                    t.ResetText();
            }
        }
        /// <summary>
        /// The class that allow to show colour in panel.
        /// </summary>
        private ColorPanel colorPanel;
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
        private readonly string errorTagNameTextBoxMsg = "Nombre incorrecto";
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
        /// <summary>
        /// Maximun lenght of tag name
        /// </summary>
        private static readonly int TAG_NAME_MAX_LENGTH = 40;

        private System.Windows.Forms.Panel panelCreateTag;
        private System.Windows.Forms.ComboBox comboBoxMode;
        private System.Windows.Forms.Panel panelForColour;
        private System.Windows.Forms.Label labelTagColour;
        private System.Windows.Forms.Button buttonCreateTag;
        private System.Windows.Forms.Button buttonCancelCreateTag;
        private System.Windows.Forms.TextBox textBoxLaterTime;
        private System.Windows.Forms.TextBox textBoxPreviousTime;
        private System.Windows.Forms.TextBox textBoxNameTag;
        private System.Windows.Forms.Label labelLaterTime;
        private System.Windows.Forms.Label labelPreviousTime;
        private System.Windows.Forms.Label labelTagName;

        public bool HaveInformation { get => haveInformation; set => haveInformation = value; }
        internal Tag NewTagInfo { get => newTagInfo; }
        public ColorPanel ColorPanel { get => colorPanel; set => colorPanel = value; }

        private void InitializeComponent()
        {
            this.labelTagName = new System.Windows.Forms.Label();
            this.labelPreviousTime = new System.Windows.Forms.Label();
            this.labelLaterTime = new System.Windows.Forms.Label();
            this.textBoxNameTag = new System.Windows.Forms.TextBox();
            this.textBoxPreviousTime = new System.Windows.Forms.TextBox();
            this.textBoxLaterTime = new System.Windows.Forms.TextBox();
            this.buttonCancelCreateTag = new System.Windows.Forms.Button();
            this.panelForColour = new System.Windows.Forms.Panel();
            this.labelTagColour = new System.Windows.Forms.Label();
            this.buttonCreateTag = new System.Windows.Forms.Button();
            this.comboBoxMode = new System.Windows.Forms.ComboBox();
            this.panelCreateTag = new System.Windows.Forms.Panel();
            this.panelForColour.SuspendLayout();
            this.panelCreateTag.SuspendLayout();
            // 
            // labelTagName
            // 
            this.labelTagName.AutoSize = true;
            this.labelTagName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTagName.ForeColor = System.Drawing.SystemColors.Control;
            this.labelTagName.Location = new System.Drawing.Point(125, 15);
            this.labelTagName.Name = "labelTagName";
            this.labelTagName.Size = new System.Drawing.Size(217, 32);
            this.labelTagName.TabIndex = 9;
            this.labelTagName.Text = "Nombre del Tag";
            // 
            // labelPreviousTime
            // 
            this.labelPreviousTime.AutoSize = true;
            this.labelPreviousTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPreviousTime.ForeColor = System.Drawing.SystemColors.Control;
            this.labelPreviousTime.Location = new System.Drawing.Point(50, 225);
            this.labelPreviousTime.Name = "labelPreviousTime";
            this.labelPreviousTime.Size = new System.Drawing.Size(93, 25);
            this.labelPreviousTime.TabIndex = 8;
            this.labelPreviousTime.Text = "Pre-Time";
            // 
            // labelLaterTime
            // 
            this.labelLaterTime.AutoSize = true;
            this.labelLaterTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLaterTime.ForeColor = System.Drawing.SystemColors.Control;
            this.labelLaterTime.Location = new System.Drawing.Point(50, 313);
            this.labelLaterTime.Name = "labelLaterTime";
            this.labelLaterTime.Size = new System.Drawing.Size(102, 25);
            this.labelLaterTime.TabIndex = 7;
            this.labelLaterTime.Text = "Post-Time";
            // 
            // textBoxNameTag
            // 
            this.textBoxNameTag.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.textBoxNameTag.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxNameTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNameTag.Location = new System.Drawing.Point(20, 46);
            this.textBoxNameTag.Name = "textBoxNameTag";
            this.textBoxNameTag.Size = new System.Drawing.Size(354, 41);
            this.textBoxNameTag.TabIndex = 6;
            // 
            // textBoxPreviousTime
            // 
            this.textBoxPreviousTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.textBoxPreviousTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPreviousTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPreviousTime.Location = new System.Drawing.Point(198, 216);
            this.textBoxPreviousTime.Name = "textBoxPreviousTime";
            this.textBoxPreviousTime.Size = new System.Drawing.Size(159, 32);
            this.textBoxPreviousTime.TabIndex = 5;
            // 
            // textBoxLaterTime
            // 
            this.textBoxLaterTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.textBoxLaterTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxLaterTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLaterTime.Location = new System.Drawing.Point(198, 313);
            this.textBoxLaterTime.Name = "textBoxLaterTime";
            this.textBoxLaterTime.Size = new System.Drawing.Size(159, 32);
            this.textBoxLaterTime.TabIndex = 4;
            // 
            // buttonCancelCreateTag
            // 
            this.buttonCancelCreateTag.FlatAppearance.BorderSize = 0;
            this.buttonCancelCreateTag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancelCreateTag.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonCancelCreateTag.Location = new System.Drawing.Point(278, 501);
            this.buttonCancelCreateTag.Name = "buttonCancelCreateTag";
            this.buttonCancelCreateTag.Size = new System.Drawing.Size(100, 32);
            this.buttonCancelCreateTag.TabIndex = 3;
            this.buttonCancelCreateTag.Text = "Cancelar";
            this.buttonCancelCreateTag.UseVisualStyleBackColor = true;
            this.buttonCancelCreateTag.Click += new System.EventHandler(this.buttonCancelCreateTag_Click);
            // 
            // panelForColour
            // 
            this.panelForColour.Controls.Add(this.labelTagColour);
            this.panelForColour.Location = new System.Drawing.Point(24, 446);
            this.panelForColour.Name = "panelForColour";
            this.panelForColour.Size = new System.Drawing.Size(354, 30);
            this.panelForColour.TabIndex = 2;
            // 
            // labelTagColour
            // 
            this.labelTagColour.AutoSize = true;
            this.labelTagColour.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTagColour.ForeColor = System.Drawing.SystemColors.Control;
            this.labelTagColour.Location = new System.Drawing.Point(87, 42);
            this.labelTagColour.Name = "labelTagColour";
            this.labelTagColour.Size = new System.Drawing.Size(124, 25);
            this.labelTagColour.TabIndex = 0;
            this.labelTagColour.Text = "Color [Black]";
            // 
            // buttonCreateTag
            // 
            this.buttonCreateTag.FlatAppearance.BorderSize = 0;
            this.buttonCreateTag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCreateTag.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonCreateTag.Location = new System.Drawing.Point(13, 498);
            this.buttonCreateTag.Name = "buttonCreateTag";
            this.buttonCreateTag.Size = new System.Drawing.Size(86, 39);
            this.buttonCreateTag.TabIndex = 1;
            this.buttonCreateTag.Text = "Crear";
            this.buttonCreateTag.UseVisualStyleBackColor = true;
            this.buttonCreateTag.Click += new System.EventHandler(buttonCreateTag_Click);
            // 
            // comboBoxMode
            // 
            this.comboBoxMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.comboBoxMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBoxMode.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBoxMode.FormattingEnabled = true;
            this.comboBoxMode.Location = new System.Drawing.Point(46, 147);
            this.comboBoxMode.Name = "comboBoxMode";
            this.comboBoxMode.Size = new System.Drawing.Size(311, 28);
            this.comboBoxMode.TabIndex = 0;
            // 
            // panelCreateTag
            // 
            this.panelCreateTag.BackColor = System.Drawing.Color.Transparent;
            this.panelCreateTag.BackgroundImage = global::AZOR.Properties.Resources.BackgroundTags;
            this.panelCreateTag.Controls.Add(this.comboBoxMode);
            this.panelCreateTag.Controls.Add(this.buttonCreateTag);
            this.panelCreateTag.Controls.Add(this.panelForColour);
            this.panelCreateTag.Controls.Add(this.buttonCancelCreateTag);
            this.panelCreateTag.Controls.Add(this.textBoxLaterTime);
            this.panelCreateTag.Controls.Add(this.textBoxPreviousTime);
            this.panelCreateTag.Controls.Add(this.textBoxNameTag);
            this.panelCreateTag.Controls.Add(this.labelLaterTime);
            this.panelCreateTag.Controls.Add(this.labelPreviousTime);
            this.panelCreateTag.Controls.Add(this.labelTagName);
            this.panelCreateTag.Location = new System.Drawing.Point(363, 188);
            this.panelCreateTag.Name = "panelCreateTag";
            this.panelCreateTag.Size = new System.Drawing.Size(393, 544);
            this.panelCreateTag.Visible = false;
            panelCreateTag.VisibleChanged += setControls;
            this.panelForColour.ResumeLayout(false);
            this.panelForColour.PerformLayout();
            this.panelCreateTag.ResumeLayout(false);
            this.panelCreateTag.PerformLayout();
            //set the limit of tag name
            this.textBoxNameTag.MaxLength = TAG_NAME_MAX_LENGTH;
            //add the colour to the create tag panel
            ColorPanel = new ColorPanel(panelForColour);

            //adjust the combo box
            comboBoxMode.Items.Add(TagMode.automaticText());
            comboBoxMode.Items.Add(TagMode.manualText());
            //background image
            panelCreateTag.BackgroundImageLayout = ImageLayout.Stretch;

            
        }

        private void buttonCancelCreateTag_Click(object sender, EventArgs e)
        {
            lock (panelCreateTag)
            {
                this.panelCreateTag.SuspendLayout();

                this.haveInformation = false;
                this.panelCreateTag.Visible = false;                

                this.panelCreateTag.ResumeLayout();
                this.panelCreateTag.PerformLayout();
                getPanel().Enabled =false;
            }
        }
        private void setControls(object sender, EventArgs e)
        {
            Debug.WriteLine(panelCreateTag.Visible);
            foreach (Control c in panelCreateTag.Controls)
            {
                c.Visible = panelCreateTag.Visible;
            }
        }
    }
}

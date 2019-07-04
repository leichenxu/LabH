namespace AZOR
{
    partial class PanelTagForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxMode = new System.Windows.Forms.ComboBox();
            this.panelForColour = new System.Windows.Forms.Panel();
            this.buttonCreateTag = new System.Windows.Forms.Button();
            this.buttonCancelCreateTag = new System.Windows.Forms.Button();
            this.textBoxNameTag = new System.Windows.Forms.TextBox();
            this.labelLaterTime = new System.Windows.Forms.Label();
            this.labelPreviousTime = new System.Windows.Forms.Label();
            this.labelTagName = new System.Windows.Forms.Label();
            this.numericUpDownPreTime = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownPostTime = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPreTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPostTime)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxMode
            // 
            this.comboBoxMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.comboBoxMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBoxMode.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBoxMode.FormattingEnabled = true;
            this.comboBoxMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.comboBoxMode.Location = new System.Drawing.Point(46, 147);
            this.comboBoxMode.Name = "comboBoxMode";
            this.comboBoxMode.Size = new System.Drawing.Size(311, 21);
            this.comboBoxMode.TabIndex = 1;
            this.comboBoxMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxMode_SelectedIndexChanged);
            // 
            // panelForColour
            // 
            this.panelForColour.BackColor = System.Drawing.Color.Transparent;
            this.panelForColour.Location = new System.Drawing.Point(24, 446);
            this.panelForColour.Name = "panelForColour";
            this.panelForColour.Size = new System.Drawing.Size(354, 30);
            this.panelForColour.TabIndex = 2;
            // 
            // buttonCreateTag
            // 
            this.buttonCreateTag.BackColor = System.Drawing.Color.Transparent;
            this.buttonCreateTag.FlatAppearance.BorderSize = 0;
            this.buttonCreateTag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCreateTag.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonCreateTag.Location = new System.Drawing.Point(13, 498);
            this.buttonCreateTag.Name = "buttonCreateTag";
            this.buttonCreateTag.Size = new System.Drawing.Size(86, 39);
            this.buttonCreateTag.TabIndex = 1;
            this.buttonCreateTag.Text = "Crear";
            this.buttonCreateTag.UseVisualStyleBackColor = false;
            this.buttonCreateTag.Click += new System.EventHandler(this.buttonCreateTag_Click);
            // 
            // buttonCancelCreateTag
            // 
            this.buttonCancelCreateTag.BackColor = System.Drawing.Color.Transparent;
            this.buttonCancelCreateTag.FlatAppearance.BorderSize = 0;
            this.buttonCancelCreateTag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancelCreateTag.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonCancelCreateTag.Location = new System.Drawing.Point(278, 501);
            this.buttonCancelCreateTag.Name = "buttonCancelCreateTag";
            this.buttonCancelCreateTag.Size = new System.Drawing.Size(100, 32);
            this.buttonCancelCreateTag.TabIndex = 3;
            this.buttonCancelCreateTag.Text = "Cancelar";
            this.buttonCancelCreateTag.UseVisualStyleBackColor = false;
            this.buttonCancelCreateTag.Click += new System.EventHandler(this.buttonCancelCreateTag_Click);
            // 
            // textBoxNameTag
            // 
            this.textBoxNameTag.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.textBoxNameTag.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxNameTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNameTag.Location = new System.Drawing.Point(20, 46);
            this.textBoxNameTag.Name = "textBoxNameTag";
            this.textBoxNameTag.Size = new System.Drawing.Size(354, 28);
            this.textBoxNameTag.TabIndex = 0;
            // 
            // labelLaterTime
            // 
            this.labelLaterTime.AutoSize = true;
            this.labelLaterTime.BackColor = System.Drawing.Color.Transparent;
            this.labelLaterTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLaterTime.ForeColor = System.Drawing.SystemColors.Control;
            this.labelLaterTime.Location = new System.Drawing.Point(116, 250);
            this.labelLaterTime.Name = "labelLaterTime";
            this.labelLaterTime.Size = new System.Drawing.Size(72, 17);
            this.labelLaterTime.TabIndex = 7;
            this.labelLaterTime.Text = "Post-Time";
            this.labelLaterTime.Visible = false;
            // 
            // labelPreviousTime
            // 
            this.labelPreviousTime.AutoSize = true;
            this.labelPreviousTime.BackColor = System.Drawing.Color.Transparent;
            this.labelPreviousTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPreviousTime.ForeColor = System.Drawing.SystemColors.Control;
            this.labelPreviousTime.Location = new System.Drawing.Point(116, 213);
            this.labelPreviousTime.Name = "labelPreviousTime";
            this.labelPreviousTime.Size = new System.Drawing.Size(66, 17);
            this.labelPreviousTime.TabIndex = 8;
            this.labelPreviousTime.Text = "Pre-Time";
            this.labelPreviousTime.Visible = false;
            // 
            // labelTagName
            // 
            this.labelTagName.AutoSize = true;
            this.labelTagName.BackColor = System.Drawing.Color.Transparent;
            this.labelTagName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTagName.ForeColor = System.Drawing.SystemColors.Control;
            this.labelTagName.Location = new System.Drawing.Point(125, 15);
            this.labelTagName.Name = "labelTagName";
            this.labelTagName.Size = new System.Drawing.Size(148, 24);
            this.labelTagName.TabIndex = 9;
            this.labelTagName.Text = "Nombre del Tag";
            // 
            // numericUpDownPreTime
            // 
            this.numericUpDownPreTime.Location = new System.Drawing.Point(198, 213);
            this.numericUpDownPreTime.Name = "numericUpDownPreTime";
            this.numericUpDownPreTime.ReadOnly = true;
            this.numericUpDownPreTime.Size = new System.Drawing.Size(34, 20);
            this.numericUpDownPreTime.TabIndex = 10;
            this.numericUpDownPreTime.Visible = false;
            // 
            // numericUpDownPostTime
            // 
            this.numericUpDownPostTime.Location = new System.Drawing.Point(198, 247);
            this.numericUpDownPostTime.Name = "numericUpDownPostTime";
            this.numericUpDownPostTime.ReadOnly = true;
            this.numericUpDownPostTime.Size = new System.Drawing.Size(34, 20);
            this.numericUpDownPostTime.TabIndex = 11;
            this.numericUpDownPostTime.Visible = false;
            // 
            // PanelTagForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImage = global::AZOR.Properties.Resources.BackgroundTags;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(398, 544);
            this.Controls.Add(this.numericUpDownPostTime);
            this.Controls.Add(this.numericUpDownPreTime);
            this.Controls.Add(this.labelTagName);
            this.Controls.Add(this.labelPreviousTime);
            this.Controls.Add(this.labelLaterTime);
            this.Controls.Add(this.textBoxNameTag);
            this.Controls.Add(this.buttonCancelCreateTag);
            this.Controls.Add(this.buttonCreateTag);
            this.Controls.Add(this.panelForColour);
            this.Controls.Add(this.comboBoxMode);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PanelTagForm";
            this.ShowInTaskbar = false;
            this.Text = "PanelTagForm";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPreTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPostTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxMode;
        private System.Windows.Forms.Panel panelForColour;
        private System.Windows.Forms.Button buttonCreateTag;
        private System.Windows.Forms.Button buttonCancelCreateTag;
        private System.Windows.Forms.TextBox textBoxNameTag;
        private System.Windows.Forms.Label labelLaterTime;
        private System.Windows.Forms.Label labelPreviousTime;
        private System.Windows.Forms.Label labelTagName;
        private System.Windows.Forms.NumericUpDown numericUpDownPreTime;
        private System.Windows.Forms.NumericUpDown numericUpDownPostTime;
    }
}
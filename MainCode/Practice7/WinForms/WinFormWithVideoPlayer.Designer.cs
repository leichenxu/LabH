namespace Practice7
{
    partial class WinFormWithVideoPlayer
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
            this.components = new System.ComponentModel.Container();
            this.mpvPictureBox = new System.Windows.Forms.PictureBox();
            this.buttonRecordVideo = new System.Windows.Forms.Button();
            this.buttonPause = new System.Windows.Forms.Button();
            this.buttonMoveVideoToEndPoint = new System.Windows.Forms.Button();
            this.buttonMoveVideoToStartPoint = new System.Windows.Forms.Button();
            this.buttonPreviousFrame = new System.Windows.Forms.Button();
            this.buttonNextFrame = new System.Windows.Forms.Button();
            this.recibidor = new System.Windows.Forms.Label();
            this.timerReload = new System.Windows.Forms.Timer(this.components);
            this.contextMenuRightClickOnTime = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanelClips = new System.Windows.Forms.TableLayoutPanel();
            this.checkBoxAllClips = new System.Windows.Forms.CheckBox();
            this.panelClipsSaved = new System.Windows.Forms.Panel();
            this.labelRecord = new System.Windows.Forms.Label();
            this.labelPlayer = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.buttonForward = new System.Windows.Forms.Button();
            this.buttonBackward = new System.Windows.Forms.Button();
            this.labelRecordTime = new System.Windows.Forms.Label();
            this.timerRecording = new System.Windows.Forms.Timer(this.components);
            this.labelTimePlaying = new System.Windows.Forms.Label();
            this.labelSignal4 = new System.Windows.Forms.Label();
            this.labelSignal3 = new System.Windows.Forms.Label();
            this.labelSignal2 = new System.Windows.Forms.Label();
            this.labelSignal1 = new System.Windows.Forms.Label();
            this.labelClips = new System.Windows.Forms.Label();
            this.labelTags = new System.Windows.Forms.Label();
            this.labelLive = new System.Windows.Forms.Label();
            this.timerLoadingVideo = new System.Windows.Forms.Timer(this.components);
            this.timerEndProgressBar = new System.Windows.Forms.Timer(this.components);
            this.labelMarks = new System.Windows.Forms.Label();
            this.panelTags = new System.Windows.Forms.Panel();
            this.tableLayoutPanelTags = new System.Windows.Forms.TableLayoutPanel();
            this.buttonNewTag = new System.Windows.Forms.Button();
            this.colorDialogTag = new System.Windows.Forms.ColorDialog();
            this.timerComprobarProcess = new System.Windows.Forms.Timer(this.components);
            this.buttonPlay = new System.Windows.Forms.Button();
            this.buttonSaveTags = new System.Windows.Forms.Button();
            this.buttonLoadTags = new System.Windows.Forms.Button();
            this.buttonSetting = new System.Windows.Forms.Button();
            this.progressBarLoadingVideo = new System.Windows.Forms.ProgressBar();
            this.labelProgressBarLoading = new System.Windows.Forms.Label();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonAnalysis = new System.Windows.Forms.Button();
            this.buttonSync = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mpvPictureBox)).BeginInit();
            this.contextMenuRightClickOnTime.SuspendLayout();
            this.panelClipsSaved.SuspendLayout();
            this.panelTags.SuspendLayout();
            this.SuspendLayout();
            // 
            // mpvPictureBox
            // 
            this.mpvPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.mpvPictureBox.Location = new System.Drawing.Point(78, 74);
            this.mpvPictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.mpvPictureBox.Name = "mpvPictureBox";
            this.mpvPictureBox.Size = new System.Drawing.Size(1280, 737);
            this.mpvPictureBox.TabIndex = 0;
            this.mpvPictureBox.TabStop = false;
            this.mpvPictureBox.Visible = false;
            // 
            // buttonRecordVideo
            // 
            this.buttonRecordVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRecordVideo.BackColor = System.Drawing.Color.Transparent;
            this.buttonRecordVideo.FlatAppearance.BorderSize = 0;
            this.buttonRecordVideo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonRecordVideo.ForeColor = System.Drawing.Color.Transparent;
            this.buttonRecordVideo.Image = global::Practice7.Properties.Resources.recordButton;
            this.buttonRecordVideo.Location = new System.Drawing.Point(38, 908);
            this.buttonRecordVideo.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRecordVideo.Name = "buttonRecordVideo";
            this.buttonRecordVideo.Size = new System.Drawing.Size(44, 39);
            this.buttonRecordVideo.TabIndex = 1;
            this.buttonRecordVideo.TabStop = false;
            this.buttonRecordVideo.UseVisualStyleBackColor = false;
            this.buttonRecordVideo.Click += new System.EventHandler(this.save_Click);
            // 
            // buttonPause
            // 
            this.buttonPause.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonPause.BackColor = System.Drawing.Color.Transparent;
            this.buttonPause.FlatAppearance.BorderSize = 0;
            this.buttonPause.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonPause.ForeColor = System.Drawing.Color.Transparent;
            this.buttonPause.Image = global::Practice7.Properties.Resources.pauseButton;
            this.buttonPause.Location = new System.Drawing.Point(582, 908);
            this.buttonPause.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(44, 41);
            this.buttonPause.TabIndex = 2;
            this.buttonPause.TabStop = false;
            this.buttonPause.UseVisualStyleBackColor = false;
            this.buttonPause.Click += new System.EventHandler(this.Play_Click);
            // 
            // buttonMoveVideoToEndPoint
            // 
            this.buttonMoveVideoToEndPoint.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonMoveVideoToEndPoint.BackColor = System.Drawing.Color.Transparent;
            this.buttonMoveVideoToEndPoint.FlatAppearance.BorderSize = 0;
            this.buttonMoveVideoToEndPoint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonMoveVideoToEndPoint.Image = global::Practice7.Properties.Resources.liveButton;
            this.buttonMoveVideoToEndPoint.Location = new System.Drawing.Point(777, 908);
            this.buttonMoveVideoToEndPoint.Margin = new System.Windows.Forms.Padding(2);
            this.buttonMoveVideoToEndPoint.Name = "buttonMoveVideoToEndPoint";
            this.buttonMoveVideoToEndPoint.Size = new System.Drawing.Size(44, 41);
            this.buttonMoveVideoToEndPoint.TabIndex = 4;
            this.buttonMoveVideoToEndPoint.TabStop = false;
            this.buttonMoveVideoToEndPoint.UseVisualStyleBackColor = false;
            this.buttonMoveVideoToEndPoint.Click += new System.EventHandler(this.Fin_Click);
            // 
            // buttonMoveVideoToStartPoint
            // 
            this.buttonMoveVideoToStartPoint.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonMoveVideoToStartPoint.BackColor = System.Drawing.Color.Transparent;
            this.buttonMoveVideoToStartPoint.FlatAppearance.BorderSize = 0;
            this.buttonMoveVideoToStartPoint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonMoveVideoToStartPoint.ForeColor = System.Drawing.Color.Transparent;
            this.buttonMoveVideoToStartPoint.Image = global::Practice7.Properties.Resources.replayButton;
            this.buttonMoveVideoToStartPoint.Location = new System.Drawing.Point(387, 908);
            this.buttonMoveVideoToStartPoint.Margin = new System.Windows.Forms.Padding(2);
            this.buttonMoveVideoToStartPoint.Name = "buttonMoveVideoToStartPoint";
            this.buttonMoveVideoToStartPoint.Size = new System.Drawing.Size(44, 41);
            this.buttonMoveVideoToStartPoint.TabIndex = 3;
            this.buttonMoveVideoToStartPoint.TabStop = false;
            this.buttonMoveVideoToStartPoint.UseVisualStyleBackColor = false;
            this.buttonMoveVideoToStartPoint.Click += new System.EventHandler(this.Inicio_Click);
            // 
            // buttonPreviousFrame
            // 
            this.buttonPreviousFrame.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonPreviousFrame.BackColor = System.Drawing.Color.Transparent;
            this.buttonPreviousFrame.FlatAppearance.BorderSize = 0;
            this.buttonPreviousFrame.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonPreviousFrame.Image = global::Practice7.Properties.Resources.backwardButton;
            this.buttonPreviousFrame.Location = new System.Drawing.Point(517, 908);
            this.buttonPreviousFrame.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPreviousFrame.Name = "buttonPreviousFrame";
            this.buttonPreviousFrame.Size = new System.Drawing.Size(44, 41);
            this.buttonPreviousFrame.TabIndex = 5;
            this.buttonPreviousFrame.TabStop = false;
            this.buttonPreviousFrame.UseVisualStyleBackColor = false;
            this.buttonPreviousFrame.Click += new System.EventHandler(this.antFrame_Click);
            // 
            // buttonNextFrame
            // 
            this.buttonNextFrame.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonNextFrame.BackColor = System.Drawing.Color.Transparent;
            this.buttonNextFrame.FlatAppearance.BorderSize = 0;
            this.buttonNextFrame.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonNextFrame.Image = global::Practice7.Properties.Resources.forwardButton;
            this.buttonNextFrame.Location = new System.Drawing.Point(647, 908);
            this.buttonNextFrame.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNextFrame.Name = "buttonNextFrame";
            this.buttonNextFrame.Size = new System.Drawing.Size(44, 41);
            this.buttonNextFrame.TabIndex = 6;
            this.buttonNextFrame.TabStop = false;
            this.buttonNextFrame.UseVisualStyleBackColor = false;
            this.buttonNextFrame.Click += new System.EventHandler(this.sigFrame_Click);
            // 
            // recibidor
            // 
            this.recibidor.AutoSize = true;
            this.recibidor.Location = new System.Drawing.Point(471, 75);
            this.recibidor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.recibidor.Name = "recibidor";
            this.recibidor.Size = new System.Drawing.Size(0, 13);
            this.recibidor.TabIndex = 7;
            // 
            // timerReload
            // 
            this.timerReload.Interval = 10000;
            this.timerReload.Tick += new System.EventHandler(this.timerReload_Tick);
            // 
            // contextMenuRightClickOnTime
            // 
            this.contextMenuRightClickOnTime.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eliminarToolStripMenuItem,
            this.exportarToolStripMenuItem});
            this.contextMenuRightClickOnTime.Name = "contextMenuClickEnTiempo";
            this.contextMenuRightClickOnTime.Size = new System.Drawing.Size(118, 48);
            this.contextMenuRightClickOnTime.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuRightClickOnTime_Opening);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // exportarToolStripMenuItem
            // 
            this.exportarToolStripMenuItem.Name = "exportarToolStripMenuItem";
            this.exportarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.exportarToolStripMenuItem.Text = "Exportar";
            this.exportarToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // tableLayoutPanelClips
            // 
            this.tableLayoutPanelClips.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(78)))), ((int)(((byte)(78)))));
            this.tableLayoutPanelClips.ColumnCount = 5;
            this.tableLayoutPanelClips.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelClips.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanelClips.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableLayoutPanelClips.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanelClips.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanelClips.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelClips.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanelClips.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelClips.Name = "tableLayoutPanelClips";
            this.tableLayoutPanelClips.RowCount = 1;
            this.tableLayoutPanelClips.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelClips.Size = new System.Drawing.Size(350, 24);
            this.tableLayoutPanelClips.TabIndex = 15;
            // 
            // checkBoxAllClips
            // 
            this.checkBoxAllClips.AutoSize = true;
            this.checkBoxAllClips.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(78)))), ((int)(((byte)(78)))));
            this.checkBoxAllClips.Location = new System.Drawing.Point(1454, 606);
            this.checkBoxAllClips.Name = "checkBoxAllClips";
            this.checkBoxAllClips.Size = new System.Drawing.Size(15, 14);
            this.checkBoxAllClips.TabIndex = 16;
            this.checkBoxAllClips.UseVisualStyleBackColor = false;
            this.checkBoxAllClips.CheckedChanged += new System.EventHandler(this.checkBoxAllClips_CheckedChanged);
            // 
            // panelClipsSaved
            // 
            this.panelClipsSaved.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panelClipsSaved.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(78)))), ((int)(((byte)(78)))));
            this.panelClipsSaved.Controls.Add(this.tableLayoutPanelClips);
            this.panelClipsSaved.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(78)))), ((int)(((byte)(78)))));
            this.panelClipsSaved.Location = new System.Drawing.Point(1456, 621);
            this.panelClipsSaved.Name = "panelClipsSaved";
            this.panelClipsSaved.Size = new System.Drawing.Size(350, 303);
            this.panelClipsSaved.TabIndex = 16;
            // 
            // labelRecord
            // 
            this.labelRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelRecord.AutoSize = true;
            this.labelRecord.BackColor = System.Drawing.Color.Transparent;
            this.labelRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRecord.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.labelRecord.Location = new System.Drawing.Point(92, 853);
            this.labelRecord.Name = "labelRecord";
            this.labelRecord.Size = new System.Drawing.Size(119, 22);
            this.labelRecord.TabIndex = 17;
            this.labelRecord.Text = "RECORDING";
            // 
            // labelPlayer
            // 
            this.labelPlayer.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelPlayer.AutoSize = true;
            this.labelPlayer.BackColor = System.Drawing.Color.Transparent;
            this.labelPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlayer.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.labelPlayer.Location = new System.Drawing.Point(564, 853);
            this.labelPlayer.Name = "labelPlayer";
            this.labelPlayer.Size = new System.Drawing.Size(81, 22);
            this.labelPlayer.TabIndex = 19;
            this.labelPlayer.Text = "PLAYER";
            // 
            // labelTime
            // 
            this.labelTime.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelTime.AutoSize = true;
            this.labelTime.BackColor = System.Drawing.Color.Transparent;
            this.labelTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.labelTime.Location = new System.Drawing.Point(1002, 853);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(52, 22);
            this.labelTime.TabIndex = 20;
            this.labelTime.Text = "TIME";
            // 
            // buttonForward
            // 
            this.buttonForward.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonForward.BackColor = System.Drawing.Color.Transparent;
            this.buttonForward.FlatAppearance.BorderSize = 0;
            this.buttonForward.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonForward.Image = global::Practice7.Properties.Resources.fastForwardButton;
            this.buttonForward.Location = new System.Drawing.Point(712, 908);
            this.buttonForward.Name = "buttonForward";
            this.buttonForward.Size = new System.Drawing.Size(44, 41);
            this.buttonForward.TabIndex = 21;
            this.buttonForward.UseVisualStyleBackColor = false;
            this.buttonForward.Click += new System.EventHandler(this.buttonForward_Click);
            // 
            // buttonBackward
            // 
            this.buttonBackward.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonBackward.BackColor = System.Drawing.Color.Transparent;
            this.buttonBackward.FlatAppearance.BorderSize = 0;
            this.buttonBackward.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonBackward.Image = global::Practice7.Properties.Resources.fastBackwardButton;
            this.buttonBackward.Location = new System.Drawing.Point(452, 908);
            this.buttonBackward.Name = "buttonBackward";
            this.buttonBackward.Size = new System.Drawing.Size(44, 41);
            this.buttonBackward.TabIndex = 22;
            this.buttonBackward.UseVisualStyleBackColor = false;
            this.buttonBackward.Click += new System.EventHandler(this.buttonBackward_Click);
            // 
            // labelRecordTime
            // 
            this.labelRecordTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelRecordTime.AutoSize = true;
            this.labelRecordTime.BackColor = System.Drawing.Color.Transparent;
            this.labelRecordTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRecordTime.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.labelRecordTime.Location = new System.Drawing.Point(103, 911);
            this.labelRecordTime.Name = "labelRecordTime";
            this.labelRecordTime.Size = new System.Drawing.Size(143, 37);
            this.labelRecordTime.TabIndex = 23;
            this.labelRecordTime.Text = "00:00:00";
            // 
            // timerRecording
            // 
            this.timerRecording.Interval = 50;
            this.timerRecording.Tick += new System.EventHandler(this.timerRecording_Tick);
            // 
            // labelTimePlaying
            // 
            this.labelTimePlaying.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelTimePlaying.AutoSize = true;
            this.labelTimePlaying.BackColor = System.Drawing.Color.Transparent;
            this.labelTimePlaying.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTimePlaying.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.labelTimePlaying.Location = new System.Drawing.Point(935, 911);
            this.labelTimePlaying.Name = "labelTimePlaying";
            this.labelTimePlaying.Size = new System.Drawing.Size(188, 37);
            this.labelTimePlaying.TabIndex = 24;
            this.labelTimePlaying.Text = "00:00:00:00";
            // 
            // labelSignal4
            // 
            this.labelSignal4.AutoSize = true;
            this.labelSignal4.BackColor = System.Drawing.Color.Transparent;
            this.labelSignal4.Font = new System.Drawing.Font("Calibri Light", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSignal4.Location = new System.Drawing.Point(357, 23);
            this.labelSignal4.Name = "labelSignal4";
            this.labelSignal4.Size = new System.Drawing.Size(108, 33);
            this.labelSignal4.TabIndex = 36;
            this.labelSignal4.Text = "SIGNAL4";
            // 
            // labelSignal3
            // 
            this.labelSignal3.AutoSize = true;
            this.labelSignal3.BackColor = System.Drawing.Color.Transparent;
            this.labelSignal3.Font = new System.Drawing.Font("Calibri Light", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSignal3.Location = new System.Drawing.Point(243, 23);
            this.labelSignal3.Name = "labelSignal3";
            this.labelSignal3.Size = new System.Drawing.Size(108, 33);
            this.labelSignal3.TabIndex = 35;
            this.labelSignal3.Text = "SIGNAL3";
            // 
            // labelSignal2
            // 
            this.labelSignal2.AutoSize = true;
            this.labelSignal2.BackColor = System.Drawing.Color.Transparent;
            this.labelSignal2.Font = new System.Drawing.Font("Calibri Light", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSignal2.Location = new System.Drawing.Point(129, 23);
            this.labelSignal2.Name = "labelSignal2";
            this.labelSignal2.Size = new System.Drawing.Size(108, 33);
            this.labelSignal2.TabIndex = 34;
            this.labelSignal2.Text = "SIGNAL2";
            // 
            // labelSignal1
            // 
            this.labelSignal1.AutoSize = true;
            this.labelSignal1.BackColor = System.Drawing.Color.Transparent;
            this.labelSignal1.Font = new System.Drawing.Font("Calibri Light", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSignal1.Location = new System.Drawing.Point(15, 23);
            this.labelSignal1.Name = "labelSignal1";
            this.labelSignal1.Size = new System.Drawing.Size(108, 33);
            this.labelSignal1.TabIndex = 33;
            this.labelSignal1.Text = "SIGNAL1";
            // 
            // labelClips
            // 
            this.labelClips.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelClips.AutoSize = true;
            this.labelClips.BackColor = System.Drawing.Color.Transparent;
            this.labelClips.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelClips.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.labelClips.Location = new System.Drawing.Point(1602, 561);
            this.labelClips.Name = "labelClips";
            this.labelClips.Size = new System.Drawing.Size(61, 22);
            this.labelClips.TabIndex = 37;
            this.labelClips.Text = "CLIPS";
            // 
            // labelTags
            // 
            this.labelTags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTags.AutoSize = true;
            this.labelTags.BackColor = System.Drawing.Color.Transparent;
            this.labelTags.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTags.ForeColor = System.Drawing.SystemColors.Control;
            this.labelTags.Location = new System.Drawing.Point(1602, 21);
            this.labelTags.Name = "labelTags";
            this.labelTags.Size = new System.Drawing.Size(60, 22);
            this.labelTags.TabIndex = 38;
            this.labelTags.Text = "TAGS";
            // 
            // labelLive
            // 
            this.labelLive.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelLive.AutoSize = true;
            this.labelLive.BackColor = System.Drawing.Color.Transparent;
            this.labelLive.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLive.ForeColor = System.Drawing.SystemColors.Control;
            this.labelLive.Location = new System.Drawing.Point(1358, 31);
            this.labelLive.Name = "labelLive";
            this.labelLive.Size = new System.Drawing.Size(55, 25);
            this.labelLive.TabIndex = 42;
            this.labelLive.Text = "LIVE";
            // 
            // timerLoadingVideo
            // 
            this.timerLoadingVideo.Tick += new System.EventHandler(this.timerLoadingVideo_Tick);
            // 
            // timerEndProgressBar
            // 
            this.timerEndProgressBar.Interval = 650;
            this.timerEndProgressBar.Tick += new System.EventHandler(this.timerEndProgressBar_Tick);
            // 
            // labelMarks
            // 
            this.labelMarks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMarks.AutoSize = true;
            this.labelMarks.BackColor = System.Drawing.Color.Transparent;
            this.labelMarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMarks.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.labelMarks.Location = new System.Drawing.Point(1268, 853);
            this.labelMarks.Name = "labelMarks";
            this.labelMarks.Size = new System.Drawing.Size(73, 22);
            this.labelMarks.TabIndex = 18;
            this.labelMarks.Text = "MARKS";
            // 
            // panelTags
            // 
            this.panelTags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTags.AutoScroll = true;
            this.panelTags.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(78)))), ((int)(((byte)(78)))));
            this.panelTags.Controls.Add(this.tableLayoutPanelTags);
            this.panelTags.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(78)))), ((int)(((byte)(78)))));
            this.panelTags.Location = new System.Drawing.Point(1455, 62);
            this.panelTags.Name = "panelTags";
            this.panelTags.Size = new System.Drawing.Size(349, 429);
            this.panelTags.TabIndex = 45;
            // 
            // tableLayoutPanelTags
            // 
            this.tableLayoutPanelTags.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(78)))), ((int)(((byte)(78)))));
            this.tableLayoutPanelTags.ColumnCount = 2;
            this.tableLayoutPanelTags.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanelTags.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 167F));
            this.tableLayoutPanelTags.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanelTags.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelTags.Name = "tableLayoutPanelTags";
            this.tableLayoutPanelTags.RowCount = 1;
            this.tableLayoutPanelTags.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanelTags.Size = new System.Drawing.Size(327, 48);
            this.tableLayoutPanelTags.TabIndex = 15;
            // 
            // buttonNewTag
            // 
            this.buttonNewTag.BackColor = System.Drawing.Color.Transparent;
            this.buttonNewTag.FlatAppearance.BorderSize = 0;
            this.buttonNewTag.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonNewTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNewTag.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonNewTag.Location = new System.Drawing.Point(1458, 497);
            this.buttonNewTag.Name = "buttonNewTag";
            this.buttonNewTag.Size = new System.Drawing.Size(34, 36);
            this.buttonNewTag.TabIndex = 46;
            this.buttonNewTag.Text = "+";
            this.buttonNewTag.UseVisualStyleBackColor = false;
            this.buttonNewTag.Click += new System.EventHandler(this.buttonNewTag_Click);
            // 
            // timerComprobarProcess
            // 
            this.timerComprobarProcess.Interval = 5000;
            // 
            // buttonPlay
            // 
            this.buttonPlay.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonPlay.BackColor = System.Drawing.Color.Transparent;
            this.buttonPlay.FlatAppearance.BorderSize = 0;
            this.buttonPlay.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonPlay.ForeColor = System.Drawing.Color.Transparent;
            this.buttonPlay.Image = global::Practice7.Properties.Resources.playButton;
            this.buttonPlay.Location = new System.Drawing.Point(582, 906);
            this.buttonPlay.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(44, 41);
            this.buttonPlay.TabIndex = 47;
            this.buttonPlay.TabStop = false;
            this.buttonPlay.UseVisualStyleBackColor = false;
            this.buttonPlay.Visible = false;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // buttonSaveTags
            // 
            this.buttonSaveTags.Location = new System.Drawing.Point(1525, 505);
            this.buttonSaveTags.Name = "buttonSaveTags";
            this.buttonSaveTags.Size = new System.Drawing.Size(137, 52);
            this.buttonSaveTags.TabIndex = 48;
            this.buttonSaveTags.Text = "saveTags";
            this.buttonSaveTags.UseVisualStyleBackColor = true;
            this.buttonSaveTags.Click += new System.EventHandler(this.buttonSaveTags_Click);
            // 
            // buttonLoadTags
            // 
            this.buttonLoadTags.Location = new System.Drawing.Point(1680, 508);
            this.buttonLoadTags.Name = "buttonLoadTags";
            this.buttonLoadTags.Size = new System.Drawing.Size(104, 49);
            this.buttonLoadTags.TabIndex = 49;
            this.buttonLoadTags.Text = "loadTags";
            this.buttonLoadTags.UseVisualStyleBackColor = true;
            this.buttonLoadTags.Click += new System.EventHandler(this.buttonLoadTags_Click);
            // 
            // buttonSetting
            // 
            this.buttonSetting.BackColor = System.Drawing.Color.Transparent;
            this.buttonSetting.FlatAppearance.BorderSize = 0;
            this.buttonSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetting.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonSetting.Location = new System.Drawing.Point(1109, 27);
            this.buttonSetting.Name = "buttonSetting";
            this.buttonSetting.Size = new System.Drawing.Size(111, 29);
            this.buttonSetting.TabIndex = 50;
            this.buttonSetting.Text = "SETTING";
            this.buttonSetting.UseVisualStyleBackColor = false;
            this.buttonSetting.Click += new System.EventHandler(this.buttonSetting_Click);
            // 
            // progressBarLoadingVideo
            // 
            this.progressBarLoadingVideo.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.progressBarLoadingVideo.Location = new System.Drawing.Point(325, 149);
            this.progressBarLoadingVideo.Name = "progressBarLoadingVideo";
            this.progressBarLoadingVideo.Size = new System.Drawing.Size(789, 31);
            this.progressBarLoadingVideo.TabIndex = 43;
            this.progressBarLoadingVideo.Visible = false;
            // 
            // labelProgressBarLoading
            // 
            this.labelProgressBarLoading.AutoSize = true;
            this.labelProgressBarLoading.BackColor = System.Drawing.Color.Transparent;
            this.labelProgressBarLoading.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProgressBarLoading.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.labelProgressBarLoading.Location = new System.Drawing.Point(670, 183);
            this.labelProgressBarLoading.Name = "labelProgressBarLoading";
            this.labelProgressBarLoading.Size = new System.Drawing.Size(99, 25);
            this.labelProgressBarLoading.TabIndex = 44;
            this.labelProgressBarLoading.Text = "Cargando";
            this.labelProgressBarLoading.Visible = false;
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(1458, 923);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(85, 26);
            this.buttonExport.TabIndex = 51;
            this.buttonExport.Text = "buttonExport";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(1723, 923);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(83, 26);
            this.buttonDelete.TabIndex = 52;
            this.buttonDelete.Text = "buttonDelete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonAnalysis
            // 
            this.buttonAnalysis.BackColor = System.Drawing.Color.Transparent;
            this.buttonAnalysis.Enabled = false;
            this.buttonAnalysis.FlatAppearance.BorderSize = 0;
            this.buttonAnalysis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAnalysis.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAnalysis.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonAnalysis.Location = new System.Drawing.Point(1215, 27);
            this.buttonAnalysis.Name = "buttonAnalysis";
            this.buttonAnalysis.Size = new System.Drawing.Size(126, 29);
            this.buttonAnalysis.TabIndex = 53;
            this.buttonAnalysis.Text = "ANALYSIS";
            this.buttonAnalysis.UseVisualStyleBackColor = false;
            this.buttonAnalysis.Click += new System.EventHandler(this.ButtonAnalysis_Click);
            // 
            // buttonSync
            // 
            this.buttonSync.BackColor = System.Drawing.Color.Transparent;
            this.buttonSync.Enabled = false;
            this.buttonSync.FlatAppearance.BorderSize = 0;
            this.buttonSync.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSync.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSync.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonSync.Location = new System.Drawing.Point(1038, 27);
            this.buttonSync.Name = "buttonSync";
            this.buttonSync.Size = new System.Drawing.Size(76, 29);
            this.buttonSync.TabIndex = 54;
            this.buttonSync.Text = "SYNC";
            this.buttonSync.UseVisualStyleBackColor = false;
            this.buttonSync.Click += new System.EventHandler(this.buttonSync_Click);
            // 
            // WinFormWithVideoPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Practice7.Properties.Resources.BackGroundControl;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1826, 997);
            this.Controls.Add(this.buttonSync);
            this.Controls.Add(this.buttonAnalysis);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.buttonSetting);
            this.Controls.Add(this.buttonLoadTags);
            this.Controls.Add(this.buttonSaveTags);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.checkBoxAllClips);
            this.Controls.Add(this.buttonNewTag);
            this.Controls.Add(this.panelClipsSaved);
            this.Controls.Add(this.panelTags);
            this.Controls.Add(this.labelMarks);
            this.Controls.Add(this.labelProgressBarLoading);
            this.Controls.Add(this.progressBarLoadingVideo);
            this.Controls.Add(this.labelLive);
            this.Controls.Add(this.labelTags);
            this.Controls.Add(this.labelClips);
            this.Controls.Add(this.labelSignal4);
            this.Controls.Add(this.labelSignal3);
            this.Controls.Add(this.labelSignal2);
            this.Controls.Add(this.labelSignal1);
            this.Controls.Add(this.labelTimePlaying);
            this.Controls.Add(this.labelRecordTime);
            this.Controls.Add(this.buttonBackward);
            this.Controls.Add(this.buttonForward);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.labelPlayer);
            this.Controls.Add(this.labelRecord);
            this.Controls.Add(this.buttonNextFrame);
            this.Controls.Add(this.buttonPreviousFrame);
            this.Controls.Add(this.buttonMoveVideoToStartPoint);
            this.Controls.Add(this.buttonMoveVideoToEndPoint);
            this.Controls.Add(this.buttonPause);
            this.Controls.Add(this.buttonRecordVideo);
            this.Controls.Add(this.recibidor);
            this.Controls.Add(this.mpvPictureBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "WinFormWithVideoPlayer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WinFormWithVideoPlayer";
            this.Load += new System.EventHandler(this.WinFormWithVideoPlayer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mpvPictureBox)).EndInit();
            this.contextMenuRightClickOnTime.ResumeLayout(false);
            this.panelClipsSaved.ResumeLayout(false);
            this.panelTags.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox mpvPictureBox;
        private System.Windows.Forms.Button buttonRecordVideo;
        private System.Windows.Forms.Button buttonPause;
        private System.Windows.Forms.Button buttonMoveVideoToEndPoint;
        private System.Windows.Forms.Button buttonMoveVideoToStartPoint;
        private System.Windows.Forms.Button buttonPreviousFrame;
        private System.Windows.Forms.Button buttonNextFrame;
        private System.Windows.Forms.Label recibidor;
        private System.Windows.Forms.Timer timerReload;
        private System.Windows.Forms.ContextMenuStrip contextMenuRightClickOnTime;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelClips;
        private System.Windows.Forms.Panel panelClipsSaved;
        private System.Windows.Forms.Label labelRecord;
        private System.Windows.Forms.Label labelPlayer;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Button buttonForward;
        private System.Windows.Forms.Button buttonBackward;
        private System.Windows.Forms.Label labelRecordTime;
        private System.Windows.Forms.Timer timerRecording;
        private System.Windows.Forms.Label labelTimePlaying;
        private System.Windows.Forms.Label labelSignal4;
        private System.Windows.Forms.Label labelSignal3;
        private System.Windows.Forms.Label labelSignal2;
        private System.Windows.Forms.Label labelSignal1;
        private System.Windows.Forms.Label labelClips;
        private System.Windows.Forms.Label labelTags;
        private System.Windows.Forms.Label labelLive;
        private System.Windows.Forms.Timer timerLoadingVideo;
        private System.Windows.Forms.Timer timerEndProgressBar;
        private System.Windows.Forms.Label labelMarks;
        private System.Windows.Forms.Panel panelTags;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTags;
        private System.Windows.Forms.CheckBox checkBoxAllClips;
        private System.Windows.Forms.Button buttonNewTag;
        private System.Windows.Forms.ColorDialog colorDialogTag;
        private System.Windows.Forms.Timer timerComprobarProcess;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Button buttonSaveTags;
        private System.Windows.Forms.Button buttonLoadTags;
        private System.Windows.Forms.Button buttonSetting;
        private System.Windows.Forms.ToolStripMenuItem exportarToolStripMenuItem;
        private System.Windows.Forms.ProgressBar progressBarLoadingVideo;
        private System.Windows.Forms.Label labelProgressBarLoading;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonAnalysis;
        private System.Windows.Forms.Button buttonSync;
    }
}
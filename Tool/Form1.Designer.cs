namespace Tool
{
    partial class MainWindow
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.bOpenDir = new System.Windows.Forms.Button();
            this.clbInputImagesList = new System.Windows.Forms.CheckedListBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lPreview = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelPreviewImage = new System.Windows.Forms.Panel();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.panelOutput = new System.Windows.Forms.Panel();
            this.pbOutput = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pConfig = new System.Windows.Forms.Panel();
            this.bSave = new System.Windows.Forms.Button();
            this.bRun = new System.Windows.Forms.Button();
            this.nudDepth = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nudEdge = new System.Windows.Forms.NumericUpDown();
            this.lEdgeThreshold = new System.Windows.Forms.Label();
            this.cbMergeStrategy = new System.Windows.Forms.ComboBox();
            this.cbEdgeDetector = new System.Windows.Forms.ComboBox();
            this.pbDepth = new System.Windows.Forms.PictureBox();
            this.lOutput = new System.Windows.Forms.TextBox();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panelPreviewImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.panelOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOutput)).BeginInit();
            this.pConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEdge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDepth)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(1203, 481);
            this.splitContainer1.SplitterDistance = 192;
            this.splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.progressBar, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.bOpenDir, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.clbInputImagesList, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(190, 479);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar.Location = new System.Drawing.Point(3, 453);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(184, 23);
            this.progressBar.Step = 1;
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 1;
            // 
            // bOpenDir
            // 
            this.bOpenDir.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bOpenDir.Location = new System.Drawing.Point(3, 3);
            this.bOpenDir.Name = "bOpenDir";
            this.bOpenDir.Size = new System.Drawing.Size(184, 26);
            this.bOpenDir.TabIndex = 1;
            this.bOpenDir.Text = "Open directory with images";
            this.bOpenDir.UseVisualStyleBackColor = true;
            this.bOpenDir.Click += new System.EventHandler(this.button1_Click);
            // 
            // clbInputImagesList
            // 
            this.clbInputImagesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbInputImagesList.FormattingEnabled = true;
            this.clbInputImagesList.Location = new System.Drawing.Point(3, 35);
            this.clbInputImagesList.Name = "clbInputImagesList";
            this.clbInputImagesList.Size = new System.Drawing.Size(184, 412);
            this.clbInputImagesList.TabIndex = 4;
            this.clbInputImagesList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbInputImagesList_ItemCheck);
            this.clbInputImagesList.SelectedValueChanged += new System.EventHandler(this.clbInputImagesList_SelectedValueChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.12438F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.28358F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.59204F));
            this.tableLayoutPanel2.Controls.Add(this.lPreview, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panelPreviewImage, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.panelOutput, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.pConfig, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.lOutput, 1, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1005, 479);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // lPreview
            // 
            this.lPreview.BackColor = System.Drawing.SystemColors.Control;
            this.lPreview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lPreview.CausesValidation = false;
            this.lPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lPreview.Location = new System.Drawing.Point(3, 412);
            this.lPreview.Multiline = true;
            this.lPreview.Name = "lPreview";
            this.lPreview.ReadOnly = true;
            this.lPreview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.lPreview.Size = new System.Drawing.Size(347, 64);
            this.lPreview.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(356, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Output:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Image Preview:";
            // 
            // panelPreviewImage
            // 
            this.panelPreviewImage.AutoScroll = true;
            this.panelPreviewImage.Controls.Add(this.pbPreview);
            this.panelPreviewImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPreviewImage.Location = new System.Drawing.Point(3, 16);
            this.panelPreviewImage.Name = "panelPreviewImage";
            this.panelPreviewImage.Size = new System.Drawing.Size(347, 390);
            this.panelPreviewImage.TabIndex = 4;
            // 
            // pbPreview
            // 
            this.pbPreview.Location = new System.Drawing.Point(0, 0);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(237, 290);
            this.pbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPreview.TabIndex = 3;
            this.pbPreview.TabStop = false;
            // 
            // panelOutput
            // 
            this.panelOutput.AutoScroll = true;
            this.panelOutput.Controls.Add(this.pbOutput);
            this.panelOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOutput.Location = new System.Drawing.Point(356, 16);
            this.panelOutput.Name = "panelOutput";
            this.panelOutput.Size = new System.Drawing.Size(428, 390);
            this.panelOutput.TabIndex = 5;
            // 
            // pbOutput
            // 
            this.pbOutput.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbOutput.Location = new System.Drawing.Point(0, 0);
            this.pbOutput.Name = "pbOutput";
            this.pbOutput.Size = new System.Drawing.Size(195, 173);
            this.pbOutput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbOutput.TabIndex = 3;
            this.pbOutput.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(790, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Configuration and depth map";
            // 
            // pConfig
            // 
            this.pConfig.Controls.Add(this.bSave);
            this.pConfig.Controls.Add(this.bRun);
            this.pConfig.Controls.Add(this.nudDepth);
            this.pConfig.Controls.Add(this.label4);
            this.pConfig.Controls.Add(this.nudEdge);
            this.pConfig.Controls.Add(this.lEdgeThreshold);
            this.pConfig.Controls.Add(this.cbMergeStrategy);
            this.pConfig.Controls.Add(this.cbEdgeDetector);
            this.pConfig.Controls.Add(this.pbDepth);
            this.pConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pConfig.Location = new System.Drawing.Point(790, 16);
            this.pConfig.Name = "pConfig";
            this.pConfig.Size = new System.Drawing.Size(212, 390);
            this.pConfig.TabIndex = 10;
            // 
            // bSave
            // 
            this.bSave.Dock = System.Windows.Forms.DockStyle.Top;
            this.bSave.Enabled = false;
            this.bSave.Location = new System.Drawing.Point(0, 346);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(212, 26);
            this.bSave.TabIndex = 12;
            this.bSave.Text = "Save images";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // bRun
            // 
            this.bRun.Dock = System.Windows.Forms.DockStyle.Top;
            this.bRun.Enabled = false;
            this.bRun.Location = new System.Drawing.Point(0, 320);
            this.bRun.Name = "bRun";
            this.bRun.Size = new System.Drawing.Size(212, 26);
            this.bRun.TabIndex = 11;
            this.bRun.Text = "Run processing";
            this.bRun.UseVisualStyleBackColor = true;
            this.bRun.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // nudDepth
            // 
            this.nudDepth.Dock = System.Windows.Forms.DockStyle.Top;
            this.nudDepth.Location = new System.Drawing.Point(0, 300);
            this.nudDepth.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
            this.nudDepth.Name = "nudDepth";
            this.nudDepth.Size = new System.Drawing.Size(212, 20);
            this.nudDepth.TabIndex = 10;
            this.nudDepth.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(0, 287);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(212, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Depth threshold";
            // 
            // nudEdge
            // 
            this.nudEdge.Dock = System.Windows.Forms.DockStyle.Top;
            this.nudEdge.Location = new System.Drawing.Point(0, 267);
            this.nudEdge.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudEdge.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudEdge.Name = "nudEdge";
            this.nudEdge.Size = new System.Drawing.Size(212, 20);
            this.nudEdge.TabIndex = 8;
            this.nudEdge.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            // 
            // lEdgeThreshold
            // 
            this.lEdgeThreshold.Dock = System.Windows.Forms.DockStyle.Top;
            this.lEdgeThreshold.Location = new System.Drawing.Point(0, 254);
            this.lEdgeThreshold.Name = "lEdgeThreshold";
            this.lEdgeThreshold.Size = new System.Drawing.Size(212, 13);
            this.lEdgeThreshold.TabIndex = 7;
            this.lEdgeThreshold.Text = "Edge detection threshold";
            // 
            // cbMergeStrategy
            // 
            this.cbMergeStrategy.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbMergeStrategy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMergeStrategy.Items.AddRange(new object[] {
            "Maximum",
            "Maximum with average"});
            this.cbMergeStrategy.Location = new System.Drawing.Point(0, 233);
            this.cbMergeStrategy.Name = "cbMergeStrategy";
            this.cbMergeStrategy.Size = new System.Drawing.Size(212, 21);
            this.cbMergeStrategy.TabIndex = 6;
            // 
            // cbEdgeDetector
            // 
            this.cbEdgeDetector.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbEdgeDetector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEdgeDetector.Items.AddRange(new object[] {
            "Laplace8",
            "Laplace4",
            "LoG"});
            this.cbEdgeDetector.Location = new System.Drawing.Point(0, 212);
            this.cbEdgeDetector.Name = "cbEdgeDetector";
            this.cbEdgeDetector.Size = new System.Drawing.Size(212, 21);
            this.cbEdgeDetector.TabIndex = 5;
            // 
            // pbDepth
            // 
            this.pbDepth.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbDepth.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbDepth.Location = new System.Drawing.Point(0, 0);
            this.pbDepth.Name = "pbDepth";
            this.pbDepth.Size = new System.Drawing.Size(212, 212);
            this.pbDepth.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbDepth.TabIndex = 4;
            this.pbDepth.TabStop = false;
            this.pbDepth.Click += new System.EventHandler(this.pbDepth_Click);
            // 
            // lOutput
            // 
            this.lOutput.BackColor = System.Drawing.SystemColors.Control;
            this.lOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lOutput.CausesValidation = false;
            this.lOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lOutput.Location = new System.Drawing.Point(356, 412);
            this.lOutput.Multiline = true;
            this.lOutput.Name = "lOutput";
            this.lOutput.ReadOnly = true;
            this.lOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.lOutput.Size = new System.Drawing.Size(428, 64);
            this.lOutput.TabIndex = 11;
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.WorkerSupportsCancellation = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 481);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainWindow";
            this.Text = "Focus Stacking Simple";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panelPreviewImage.ResumeLayout(false);
            this.panelPreviewImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.panelOutput.ResumeLayout(false);
            this.panelOutput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOutput)).EndInit();
            this.pConfig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEdge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDepth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button bOpenDir;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.CheckedListBox clbInputImagesList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelPreviewImage;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.Panel panelOutput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pbOutput;
        private System.Windows.Forms.Panel pConfig;
        private System.Windows.Forms.PictureBox pbDepth;
        private System.Windows.Forms.ComboBox cbEdgeDetector;
        private System.Windows.Forms.ComboBox cbMergeStrategy;
        private System.Windows.Forms.TextBox lOutput;
        private System.Windows.Forms.TextBox lPreview;
        private System.Windows.Forms.NumericUpDown nudDepth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudEdge;
        private System.Windows.Forms.Label lEdgeThreshold;
        private System.Windows.Forms.Button bRun;
        private System.Windows.Forms.Button bSave;
    }
}


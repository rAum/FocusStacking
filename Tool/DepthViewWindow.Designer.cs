namespace Tool
{
    partial class DepthViewWindow
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
            this.pbDepthMap = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbDepthMap)).BeginInit();
            this.SuspendLayout();
            // 
            // pbDepthMap
            // 
            this.pbDepthMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbDepthMap.Location = new System.Drawing.Point(0, 0);
            this.pbDepthMap.Name = "pbDepthMap";
            this.pbDepthMap.Size = new System.Drawing.Size(539, 335);
            this.pbDepthMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbDepthMap.TabIndex = 0;
            this.pbDepthMap.TabStop = false;
            // 
            // DepthViewWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 335);
            this.Controls.Add(this.pbDepthMap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "DepthViewWindow";
            this.Text = "Depth map";
            this.Load += new System.EventHandler(this.DepthViewWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbDepthMap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbDepthMap;
    }
}
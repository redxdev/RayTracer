namespace RTFrontend
{
    partial class RenderSettingsWindow
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.resHeight = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.resWidth = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.renderButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.threadCount = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.threadCount)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.resHeight);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.resWidth);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 119);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Resolution";
            // 
            // resHeight
            // 
            this.resHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resHeight.Location = new System.Drawing.Point(158, 58);
            this.resHeight.Maximum = new decimal(new int[] {
            2160,
            0,
            0,
            0});
            this.resHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.resHeight.Name = "resHeight";
            this.resHeight.Size = new System.Drawing.Size(120, 31);
            this.resHeight.TabIndex = 3;
            this.resHeight.Value = new decimal(new int[] {
            480,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Height";
            // 
            // resWidth
            // 
            this.resWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resWidth.Location = new System.Drawing.Point(158, 21);
            this.resWidth.Maximum = new decimal(new int[] {
            3840,
            0,
            0,
            0});
            this.resWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.resWidth.Name = "resWidth";
            this.resWidth.Size = new System.Drawing.Size(120, 31);
            this.resWidth.TabIndex = 1;
            this.resWidth.Value = new decimal(new int[] {
            640,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Width";
            // 
            // renderButton
            // 
            this.renderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.renderButton.Location = new System.Drawing.Point(176, 217);
            this.renderButton.Name = "renderButton";
            this.renderButton.Size = new System.Drawing.Size(120, 50);
            this.renderButton.TabIndex = 1;
            this.renderButton.Text = "Render";
            this.renderButton.UseVisualStyleBackColor = true;
            this.renderButton.Click += new System.EventHandler(this.renderButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Threads";
            // 
            // threadCount
            // 
            this.threadCount.Location = new System.Drawing.Point(170, 153);
            this.threadCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.threadCount.Name = "threadCount";
            this.threadCount.Size = new System.Drawing.Size(120, 31);
            this.threadCount.TabIndex = 3;
            this.threadCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // RenderSettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 279);
            this.Controls.Add(this.threadCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.renderButton);
            this.Controls.Add(this.groupBox1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RenderSettingsWindow";
            this.Text = "RenderSettingsWindow";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.threadCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown resHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown resWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button renderButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown threadCount;
    }
}
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
            this.resHeight = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.resWidth = new System.Windows.Forms.NumericUpDown();
            this.renderButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.threadCount = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.fieldOfView = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nearClipPlane = new System.Windows.Forms.NumericUpDown();
            this.farClipPlane = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.maxRecursionDepth = new System.Windows.Forms.NumericUpDown();
            this.saveButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.resHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.threadCount)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fieldOfView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nearClipPlane)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.farClipPlane)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxRecursionDepth)).BeginInit();
            this.SuspendLayout();
            // 
            // resHeight
            // 
            this.resHeight.AutoSize = true;
            this.resHeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resHeight.Location = new System.Drawing.Point(108, 26);
            this.resHeight.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.resHeight.Size = new System.Drawing.Size(103, 20);
            this.resHeight.TabIndex = 2;
            this.resHeight.Value = new decimal(new int[] {
            480,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Height";
            // 
            // resWidth
            // 
            this.resWidth.AutoSize = true;
            this.resWidth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resWidth.Location = new System.Drawing.Point(108, 2);
            this.resWidth.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.resWidth.Size = new System.Drawing.Size(103, 20);
            this.resWidth.TabIndex = 1;
            this.resWidth.Value = new decimal(new int[] {
            640,
            0,
            0,
            0});
            // 
            // renderButton
            // 
            this.renderButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderButton.Location = new System.Drawing.Point(108, 178);
            this.renderButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.renderButton.Name = "renderButton";
            this.renderButton.Size = new System.Drawing.Size(103, 35);
            this.renderButton.TabIndex = 8;
            this.renderButton.Text = "Render";
            this.renderButton.UseVisualStyleBackColor = true;
            this.renderButton.Click += new System.EventHandler(this.renderButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 126);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Threads";
            // 
            // threadCount
            // 
            this.threadCount.AutoSize = true;
            this.threadCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.threadCount.Location = new System.Drawing.Point(108, 128);
            this.threadCount.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.threadCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.threadCount.Name = "threadCount";
            this.threadCount.Size = new System.Drawing.Size(103, 20);
            this.threadCount.TabIndex = 6;
            this.threadCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.resHeight, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.resWidth, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.fieldOfView, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.threadCount, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.nearClipPlane, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.farClipPlane, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.renderButton, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.maxRecursionDepth, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.saveButton, 0, 8);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(213, 215);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Width";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Field of View";
            // 
            // fieldOfView
            // 
            this.fieldOfView.AutoSize = true;
            this.fieldOfView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldOfView.Location = new System.Drawing.Point(109, 51);
            this.fieldOfView.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.fieldOfView.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.fieldOfView.Name = "fieldOfView";
            this.fieldOfView.Size = new System.Drawing.Size(101, 20);
            this.fieldOfView.TabIndex = 3;
            this.fieldOfView.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Near Clipping Plane";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Far Clipping Plane";
            // 
            // nearClipPlane
            // 
            this.nearClipPlane.AutoSize = true;
            this.nearClipPlane.DecimalPlaces = 1;
            this.nearClipPlane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nearClipPlane.Location = new System.Drawing.Point(109, 77);
            this.nearClipPlane.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nearClipPlane.Name = "nearClipPlane";
            this.nearClipPlane.Size = new System.Drawing.Size(101, 20);
            this.nearClipPlane.TabIndex = 4;
            // 
            // farClipPlane
            // 
            this.farClipPlane.DecimalPlaces = 1;
            this.farClipPlane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.farClipPlane.Location = new System.Drawing.Point(109, 103);
            this.farClipPlane.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.farClipPlane.Name = "farClipPlane";
            this.farClipPlane.Size = new System.Drawing.Size(101, 20);
            this.farClipPlane.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 26);
            this.label7.TabIndex = 10;
            this.label7.Text = "Max Recursion Depth";
            // 
            // maxRecursionDepth
            // 
            this.maxRecursionDepth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.maxRecursionDepth.Location = new System.Drawing.Point(109, 153);
            this.maxRecursionDepth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.maxRecursionDepth.Name = "maxRecursionDepth";
            this.maxRecursionDepth.Size = new System.Drawing.Size(101, 20);
            this.maxRecursionDepth.TabIndex = 7;
            this.maxRecursionDepth.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // saveButton
            // 
            this.saveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveButton.Location = new System.Drawing.Point(3, 179);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(100, 33);
            this.saveButton.TabIndex = 11;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // RenderSettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(213, 215);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RenderSettingsWindow";
            this.Text = "RenderSettingsWindow";
            ((System.ComponentModel.ISupportInitialize)(this.resHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.threadCount)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fieldOfView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nearClipPlane)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.farClipPlane)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxRecursionDepth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown resHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown resWidth;
        private System.Windows.Forms.Button renderButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown threadCount;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown fieldOfView;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nearClipPlane;
        private System.Windows.Forms.NumericUpDown farClipPlane;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown maxRecursionDepth;
        private System.Windows.Forms.Button saveButton;
    }
}
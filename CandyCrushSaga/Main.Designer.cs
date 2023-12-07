namespace CandyCrushSaga
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.ThemeWrapper = new CandyCrushSaga.UI.MonoControls.MonoTheme();
            this.toggleSpeed = new CandyCrushSaga.UI.MonoControls.MonoToggle();
            this.lblScore = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGridWidth = new CandyCrushSaga.UI.MonoControls.MonoTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtGridHeight = new CandyCrushSaga.UI.MonoControls.MonoTextBox();
            this.screen = new System.Windows.Forms.PictureBox();
            this.btnLoadValues = new CandyCrushSaga.UI.MonoControls.MonoButton();
            this.monoControlBox1 = new CandyCrushSaga.UI.MonoControls.MonoControlBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ThemeWrapper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.screen)).BeginInit();
            this.SuspendLayout();
            // 
            // ThemeWrapper
            // 
            this.ThemeWrapper.AcceptButton = null;
            this.ThemeWrapper.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ThemeWrapper.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(31)))), ((int)(((byte)(40)))));
            this.ThemeWrapper.BorderRadius = 4;
            this.ThemeWrapper.CancelButton = null;
            this.ThemeWrapper.ControlBox = true;
            this.ThemeWrapper.ControlMode = false;
            this.ThemeWrapper.Controls.Add(this.label4);
            this.ThemeWrapper.Controls.Add(this.toggleSpeed);
            this.ThemeWrapper.Controls.Add(this.lblScore);
            this.ThemeWrapper.Controls.Add(this.label3);
            this.ThemeWrapper.Controls.Add(this.label2);
            this.ThemeWrapper.Controls.Add(this.txtGridWidth);
            this.ThemeWrapper.Controls.Add(this.label1);
            this.ThemeWrapper.Controls.Add(this.txtGridHeight);
            this.ThemeWrapper.Controls.Add(this.screen);
            this.ThemeWrapper.Controls.Add(this.btnLoadValues);
            this.ThemeWrapper.Controls.Add(this.monoControlBox1);
            this.ThemeWrapper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ThemeWrapper.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ThemeWrapper.ForeColor = System.Drawing.Color.White;
            this.ThemeWrapper.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(31)))), ((int)(((byte)(40)))));
            this.ThemeWrapper.HeaderHeight = 36;
            this.ThemeWrapper.Icon = ((System.Drawing.Icon)(resources.GetObject("ThemeWrapper.Icon")));
            this.ThemeWrapper.IsMdiContainer = false;
            this.ThemeWrapper.KeyPreview = false;
            this.ThemeWrapper.Location = new System.Drawing.Point(0, 0);
            this.ThemeWrapper.MainMenuStrip = null;
            this.ThemeWrapper.MinimizeBox = true;
            this.ThemeWrapper.Name = "ThemeWrapper";
            this.ThemeWrapper.Opacity = 1D;
            this.ThemeWrapper.Padding = new System.Windows.Forms.Padding(10, 40, 10, 9);
            this.ThemeWrapper.RightToLeftLayout = false;
            this.ThemeWrapper.RoundCorners = true;
            this.ThemeWrapper.ShowIcon = true;
            this.ThemeWrapper.ShowInTaskbar = true;
            this.ThemeWrapper.Sizable = false;
            this.ThemeWrapper.Size = new System.Drawing.Size(739, 518);
            this.ThemeWrapper.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.ThemeWrapper.SmartBounds = true;
            this.ThemeWrapper.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ThemeWrapper.TabIndex = 0;
            this.ThemeWrapper.Text = "Candy Crush Saga Algorithm";
            this.ThemeWrapper.TopMost = false;
            this.ThemeWrapper.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.ThemeWrapper.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.ThemeWrapper.Resize += new System.EventHandler(this.ThemeWrapper_Resize);
            // 
            // toggleSpeed
            // 
            this.toggleSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.toggleSpeed.BackColor = System.Drawing.Color.Transparent;
            this.toggleSpeed.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(41)))), ((int)(((byte)(42)))));
            this.toggleSpeed.ForeColor = System.Drawing.Color.White;
            this.toggleSpeed.ForeColor2 = System.Drawing.Color.White;
            this.toggleSpeed.Location = new System.Drawing.Point(13, 473);
            this.toggleSpeed.Name = "toggleSpeed";
            this.toggleSpeed.Size = new System.Drawing.Size(76, 33);
            this.toggleSpeed.TabIndex = 12;
            this.toggleSpeed.Text = "monoToggle1";
            this.toggleSpeed.ThumbColor = System.Drawing.Color.White;
            this.toggleSpeed.Toggled = false;
            this.toggleSpeed.Type = CandyCrushSaga.UI.MonoControls.MonoToggle.ToggleType.OnOff;
            // 
            // lblScore
            // 
            this.lblScore.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.Location = new System.Drawing.Point(12, 223);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(138, 64);
            this.lblScore.TabIndex = 11;
            this.lblScore.Text = "0";
            this.lblScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 24);
            this.label3.TabIndex = 10;
            this.label3.Text = "Score";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Horizontal";
            // 
            // txtGridWidth
            // 
            this.txtGridWidth.BackColor = System.Drawing.Color.Transparent;
            this.txtGridWidth.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(76)))), ((int)(((byte)(85)))));
            this.txtGridWidth.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtGridWidth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(183)))), ((int)(((byte)(191)))));
            this.txtGridWidth.HighlightColor = System.Drawing.Color.White;
            this.txtGridWidth.Image = null;
            this.txtGridWidth.Location = new System.Drawing.Point(12, 125);
            this.txtGridWidth.MaxLength = 50000;
            this.txtGridWidth.Multiline = false;
            this.txtGridWidth.Name = "txtGridWidth";
            this.txtGridWidth.ReadOnly = false;
            this.txtGridWidth.Size = new System.Drawing.Size(79, 41);
            this.txtGridWidth.TabIndex = 8;
            this.txtGridWidth.Text = "18";
            this.txtGridWidth.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtGridWidth.UseSystemPasswordChar = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Vertical";
            // 
            // txtGridHeight
            // 
            this.txtGridHeight.BackColor = System.Drawing.Color.Transparent;
            this.txtGridHeight.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(76)))), ((int)(((byte)(85)))));
            this.txtGridHeight.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtGridHeight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(183)))), ((int)(((byte)(191)))));
            this.txtGridHeight.HighlightColor = System.Drawing.Color.White;
            this.txtGridHeight.Image = null;
            this.txtGridHeight.Location = new System.Drawing.Point(12, 43);
            this.txtGridHeight.MaxLength = 50000;
            this.txtGridHeight.Multiline = false;
            this.txtGridHeight.Name = "txtGridHeight";
            this.txtGridHeight.ReadOnly = false;
            this.txtGridHeight.Size = new System.Drawing.Size(79, 41);
            this.txtGridHeight.TabIndex = 6;
            this.txtGridHeight.Text = "18";
            this.txtGridHeight.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtGridHeight.UseSystemPasswordChar = false;
            // 
            // screen
            // 
            this.screen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.screen.BackColor = System.Drawing.Color.Transparent;
            this.screen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.screen.Location = new System.Drawing.Point(163, 43);
            this.screen.Name = "screen";
            this.screen.Size = new System.Drawing.Size(563, 463);
            this.screen.TabIndex = 5;
            this.screen.TabStop = false;
            this.screen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.screen_MouseDown);
            this.screen.MouseMove += new System.Windows.Forms.MouseEventHandler(this.screen_MouseMove);
            this.screen.MouseUp += new System.Windows.Forms.MouseEventHandler(this.screen_MouseUp);
            // 
            // btnLoadValues
            // 
            this.btnLoadValues.BackColor = System.Drawing.Color.Transparent;
            this.btnLoadValues.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoadValues.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(41)))), ((int)(((byte)(42)))));
            this.btnLoadValues.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btnLoadValues.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnLoadValues.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnLoadValues.Image = null;
            this.btnLoadValues.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoadValues.ImageSize = new System.Drawing.Size(46, 46);
            this.btnLoadValues.Location = new System.Drawing.Point(30, 290);
            this.btnLoadValues.Name = "btnLoadValues";
            this.btnLoadValues.RoundCorners = true;
            this.btnLoadValues.Size = new System.Drawing.Size(105, 34);
            this.btnLoadValues.TabIndex = 2;
            this.btnLoadValues.Text = "New Session";
            this.btnLoadValues.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnLoadValues.Click += new System.EventHandler(this.btnLoadValues_Click);
            // 
            // monoControlBox1
            // 
            this.monoControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.monoControlBox1.AutoRelocate = true;
            this.monoControlBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(31)))), ((int)(((byte)(40)))));
            this.monoControlBox1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.monoControlBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.monoControlBox1.EnableHoverHighlight = true;
            this.monoControlBox1.EnableMaximizeButton = true;
            this.monoControlBox1.EnableMinimizeButton = true;
            this.monoControlBox1.ForeColor = System.Drawing.Color.Silver;
            this.monoControlBox1.ForeColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.monoControlBox1.Location = new System.Drawing.Point(639, 0);
            this.monoControlBox1.Name = "monoControlBox1";
            this.monoControlBox1.Size = new System.Drawing.Size(100, 25);
            this.monoControlBox1.TabIndex = 0;
            this.monoControlBox1.Text = "monoControlBox1";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 450);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "Super Speed";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(739, 518);
            this.Controls.Add(this.ThemeWrapper);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(583, 480);
            this.Name = "Main";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Candy Crush Saga Algorithm";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.ThemeWrapper.ResumeLayout(false);
            this.ThemeWrapper.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.screen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UI.MonoControls.MonoTheme ThemeWrapper;
        private UI.MonoControls.MonoControlBox monoControlBox1;
        private UI.MonoControls.MonoButton btnLoadValues;
        private System.Windows.Forms.PictureBox screen;
        private UI.MonoControls.MonoTextBox txtGridHeight;
        private System.Windows.Forms.Label label2;
        private UI.MonoControls.MonoTextBox txtGridWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label label3;
        private UI.MonoControls.MonoToggle toggleSpeed;
        private System.Windows.Forms.Label label4;
    }
}


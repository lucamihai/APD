﻿namespace APD.L1
{
    partial class MainForm
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
            this.labelBalls = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelBalls
            // 
            this.labelBalls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelBalls.Location = new System.Drawing.Point(12, 9);
            this.labelBalls.Name = "labelBalls";
            this.labelBalls.Size = new System.Drawing.Size(553, 432);
            this.labelBalls.TabIndex = 0;
            this.labelBalls.Text = "labelBalls";
            this.labelBalls.Paint += new System.Windows.Forms.PaintEventHandler(this.MyForm_Paint);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelBalls);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MyForm_FormClosing);
            this.Load += new System.EventHandler(this.MyForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelBalls;
    }
}


namespace APD.L2
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
            this.labelAds = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelBalls
            // 
            this.labelBalls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelBalls.Location = new System.Drawing.Point(12, 9);
            this.labelBalls.Name = "labelBalls";
            this.labelBalls.Size = new System.Drawing.Size(553, 432);
            this.labelBalls.TabIndex = 0;
            this.labelBalls.Paint += new System.Windows.Forms.PaintEventHandler(this.MyForm_Paint);
            // 
            // labelAds
            // 
            this.labelAds.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelAds.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelAds.Location = new System.Drawing.Point(575, 9);
            this.labelAds.Name = "labelAds";
            this.labelAds.Size = new System.Drawing.Size(400, 300);
            this.labelAds.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(571, 323);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(375, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Quality ads go here (expect a Raid Shadow Legends ad too)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(717, 342);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "Also, pls click :)";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelAds);
            this.Controls.Add(this.labelBalls);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MyForm_FormClosing);
            this.Load += new System.EventHandler(this.MyForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelBalls;
        private System.Windows.Forms.Label labelAds;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}


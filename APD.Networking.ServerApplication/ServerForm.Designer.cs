namespace APD.Networking.ServerApplication
{
    partial class ServerForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonServerStatus = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.labelServerStatus = new System.Windows.Forms.Label();
            this.labelClientsConnected = new System.Windows.Forms.Label();
            this.numericUpDownPort = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(44, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Clients connected:";
            // 
            // buttonServerStatus
            // 
            this.buttonServerStatus.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonServerStatus.Location = new System.Drawing.Point(6, 7);
            this.buttonServerStatus.Name = "buttonServerStatus";
            this.buttonServerStatus.Size = new System.Drawing.Size(105, 33);
            this.buttonServerStatus.TabIndex = 1;
            this.buttonServerStatus.Text = "Start server";
            this.buttonServerStatus.UseVisualStyleBackColor = true;
            this.buttonServerStatus.Click += new System.EventHandler(this.buttonServerStatus_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(44, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Server status:";
            // 
            // labelServerStatus
            // 
            this.labelServerStatus.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelServerStatus.ForeColor = System.Drawing.Color.Maroon;
            this.labelServerStatus.Location = new System.Drawing.Point(132, 66);
            this.labelServerStatus.Name = "labelServerStatus";
            this.labelServerStatus.Size = new System.Drawing.Size(63, 23);
            this.labelServerStatus.TabIndex = 3;
            this.labelServerStatus.Text = "Offline";
            // 
            // labelClientsConnected
            // 
            this.labelClientsConnected.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelClientsConnected.Location = new System.Drawing.Point(163, 43);
            this.labelClientsConnected.Name = "labelClientsConnected";
            this.labelClientsConnected.Size = new System.Drawing.Size(38, 23);
            this.labelClientsConnected.TabIndex = 4;
            this.labelClientsConnected.Text = "0";
            // 
            // numericUpDownPort
            // 
            this.numericUpDownPort.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownPort.Location = new System.Drawing.Point(166, 10);
            this.numericUpDownPort.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.numericUpDownPort.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDownPort.Name = "numericUpDownPort";
            this.numericUpDownPort.Size = new System.Drawing.Size(82, 26);
            this.numericUpDownPort.TabIndex = 5;
            this.numericUpDownPort.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(117, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "Port";
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 94);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDownPort);
            this.Controls.Add(this.labelClientsConnected);
            this.Controls.Add(this.labelServerStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonServerStatus);
            this.Controls.Add(this.label1);
            this.Name = "ServerForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonServerStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelServerStatus;
        private System.Windows.Forms.Label labelClientsConnected;
        private System.Windows.Forms.NumericUpDown numericUpDownPort;
        private System.Windows.Forms.Label label3;
    }
}


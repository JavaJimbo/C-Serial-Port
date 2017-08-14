namespace CodeProjectSerialComms
{
    partial class FormMain
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
            this.rtbIncoming = new System.Windows.Forms.RichTextBox();
            this.lblBreakStatus = new System.Windows.Forms.Label();
            this.lblCTSStatus = new System.Windows.Forms.Label();
            this.lblDSRStatus = new System.Windows.Forms.Label();
            this.lblRIStatus = new System.Windows.Forms.Label();
            this.btnPortState = new System.Windows.Forms.Button();
            this.rtbOutgoing = new System.Windows.Forms.RichTextBox();
            this.lblTimer = new System.Windows.Forms.Label();
            this.btnTimer = new System.Windows.Forms.Button();
            this.btnPort = new System.Windows.Forms.Button();
            this.lblPortNumber = new System.Windows.Forms.Label();
            this.lblPortStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rtbIncoming
            // 
            this.rtbIncoming.Location = new System.Drawing.Point(12, 328);
            this.rtbIncoming.Name = "rtbIncoming";
            this.rtbIncoming.Size = new System.Drawing.Size(260, 31);
            this.rtbIncoming.TabIndex = 1;
            this.rtbIncoming.Text = "";
            this.rtbIncoming.TextChanged += new System.EventHandler(this.rtbIncoming_TextChanged);
            // 
            // lblBreakStatus
            // 
            this.lblBreakStatus.AutoSize = true;
            this.lblBreakStatus.Location = new System.Drawing.Point(24, 259);
            this.lblBreakStatus.Name = "lblBreakStatus";
            this.lblBreakStatus.Size = new System.Drawing.Size(35, 13);
            this.lblBreakStatus.TabIndex = 8;
            this.lblBreakStatus.Text = "Break";
            this.lblBreakStatus.Visible = false;
            // 
            // lblCTSStatus
            // 
            this.lblCTSStatus.AutoSize = true;
            this.lblCTSStatus.Location = new System.Drawing.Point(83, 259);
            this.lblCTSStatus.Name = "lblCTSStatus";
            this.lblCTSStatus.Size = new System.Drawing.Size(28, 13);
            this.lblCTSStatus.TabIndex = 9;
            this.lblCTSStatus.Text = "CTS";
            this.lblCTSStatus.Visible = false;
            // 
            // lblDSRStatus
            // 
            this.lblDSRStatus.AutoSize = true;
            this.lblDSRStatus.Location = new System.Drawing.Point(129, 259);
            this.lblDSRStatus.Name = "lblDSRStatus";
            this.lblDSRStatus.Size = new System.Drawing.Size(30, 13);
            this.lblDSRStatus.TabIndex = 10;
            this.lblDSRStatus.Text = "DSR";
            this.lblDSRStatus.Visible = false;
            // 
            // lblRIStatus
            // 
            this.lblRIStatus.AutoSize = true;
            this.lblRIStatus.Location = new System.Drawing.Point(188, 259);
            this.lblRIStatus.Name = "lblRIStatus";
            this.lblRIStatus.Size = new System.Drawing.Size(18, 13);
            this.lblRIStatus.TabIndex = 11;
            this.lblRIStatus.Text = "RI";
            this.lblRIStatus.Visible = false;
            // 
            // btnPortState
            // 
            this.btnPortState.Location = new System.Drawing.Point(12, 75);
            this.btnPortState.Name = "btnPortState";
            this.btnPortState.Size = new System.Drawing.Size(75, 23);
            this.btnPortState.TabIndex = 13;
            this.btnPortState.Text = "Closed";
            this.btnPortState.UseVisualStyleBackColor = true;
            this.btnPortState.Click += new System.EventHandler(this.btnPortState_Click);
            // 
            // rtbOutgoing
            // 
            this.rtbOutgoing.Location = new System.Drawing.Point(12, 291);
            this.rtbOutgoing.Name = "rtbOutgoing";
            this.rtbOutgoing.Size = new System.Drawing.Size(260, 31);
            this.rtbOutgoing.TabIndex = 15;
            this.rtbOutgoing.Text = "";
            this.rtbOutgoing.TextChanged += new System.EventHandler(this.rtbOutgoing_TextChanged);
            this.rtbOutgoing.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rtbOutgoing_KeyPress);
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Location = new System.Drawing.Point(140, 85);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(85, 13);
            this.lblTimer.TabIndex = 16;
            this.lblTimer.Text = "Time: 0 seconds";
            this.lblTimer.Click += new System.EventHandler(this.lblTimer_Click);
            // 
            // btnTimer
            // 
            this.btnTimer.Location = new System.Drawing.Point(12, 44);
            this.btnTimer.Name = "btnTimer";
            this.btnTimer.Size = new System.Drawing.Size(75, 23);
            this.btnTimer.TabIndex = 17;
            this.btnTimer.Text = "Start";
            this.btnTimer.UseVisualStyleBackColor = true;
            this.btnTimer.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnPort
            // 
            this.btnPort.Location = new System.Drawing.Point(12, 139);
            this.btnPort.Name = "btnPort";
            this.btnPort.Size = new System.Drawing.Size(75, 23);
            this.btnPort.TabIndex = 21;
            this.btnPort.Text = "Port Settings";
            this.btnPort.UseVisualStyleBackColor = true;
            this.btnPort.Click += new System.EventHandler(this.btnPort_Click);
            // 
            // lblPortNumber
            // 
            this.lblPortNumber.AutoSize = true;
            this.lblPortNumber.Location = new System.Drawing.Point(140, 144);
            this.lblPortNumber.Name = "lblPortNumber";
            this.lblPortNumber.Size = new System.Drawing.Size(36, 13);
            this.lblPortNumber.TabIndex = 22;
            this.lblPortNumber.Text = "Port #";
            // 
            // lblPortStatus
            // 
            this.lblPortStatus.AutoSize = true;
            this.lblPortStatus.Location = new System.Drawing.Point(12, 440);
            this.lblPortStatus.Name = "lblPortStatus";
            this.lblPortStatus.Size = new System.Drawing.Size(115, 13);
            this.lblPortStatus.TabIndex = 23;
            this.lblPortStatus.Text = "No COM Port Selected";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 462);
            this.Controls.Add(this.lblPortStatus);
            this.Controls.Add(this.lblPortNumber);
            this.Controls.Add(this.btnPort);
            this.Controls.Add(this.btnTimer);
            this.Controls.Add(this.lblTimer);
            this.Controls.Add(this.rtbOutgoing);
            this.Controls.Add(this.btnPortState);
            this.Controls.Add(this.lblRIStatus);
            this.Controls.Add(this.lblDSRStatus);
            this.Controls.Add(this.lblCTSStatus);
            this.Controls.Add(this.lblBreakStatus);
            this.Controls.Add(this.rtbIncoming);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "C# Serial Simple";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox rtbIncoming;
        private System.Windows.Forms.Label lblBreakStatus;
        private System.Windows.Forms.Label lblCTSStatus;
        private System.Windows.Forms.Label lblDSRStatus;
        private System.Windows.Forms.Label lblRIStatus;
        private System.Windows.Forms.Button btnPortState;
        private System.Windows.Forms.RichTextBox rtbOutgoing;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Button btnTimer;
        private System.Windows.Forms.Button btnPort;
        private System.Windows.Forms.Label lblPortNumber;
        private System.Windows.Forms.Label lblPortStatus;
    }
}


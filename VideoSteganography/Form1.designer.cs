namespace VideoSteganography
{
    partial class Form1
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
            this.btnSelectVideo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSecrectMessage = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnEmbed = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnStegoVideo = new System.Windows.Forms.Button();
            this.btnRetrive = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtStegoSecrectMessage = new System.Windows.Forms.TextBox();
            this.btnValidation = new System.Windows.Forms.Button();
            this.lblValidationHeading = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSelect1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblSelect2 = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.radioXORLSB = new System.Windows.Forms.RadioButton();
            this.radioLSBXOR8Dir = new System.Windows.Forms.RadioButton();
            this.radioLSB8Dir = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.btnPrewitt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSelectVideo
            // 
            this.btnSelectVideo.BackColor = System.Drawing.Color.Black;
            this.btnSelectVideo.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectVideo.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSelectVideo.Location = new System.Drawing.Point(137, 34);
            this.btnSelectVideo.Name = "btnSelectVideo";
            this.btnSelectVideo.Size = new System.Drawing.Size(157, 29);
            this.btnSelectVideo.TabIndex = 0;
            this.btnSelectVideo.Text = "Browse";
            this.btnSelectVideo.UseVisualStyleBackColor = false;
            this.btnSelectVideo.Click += new System.EventHandler(this.btnSelectVideo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(38, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select a video:";
            // 
            // txtSecrectMessage
            // 
            this.txtSecrectMessage.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSecrectMessage.Location = new System.Drawing.Point(29, 93);
            this.txtSecrectMessage.Multiline = true;
            this.txtSecrectMessage.Name = "txtSecrectMessage";
            this.txtSecrectMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSecrectMessage.Size = new System.Drawing.Size(320, 169);
            this.txtSecrectMessage.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Input Secrect Message:";
            // 
            // btnEmbed
            // 
            this.btnEmbed.BackColor = System.Drawing.Color.Black;
            this.btnEmbed.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmbed.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEmbed.Location = new System.Drawing.Point(30, 268);
            this.btnEmbed.Name = "btnEmbed";
            this.btnEmbed.Size = new System.Drawing.Size(319, 38);
            this.btnEmbed.TabIndex = 9;
            this.btnEmbed.Text = "Embed";
            this.btnEmbed.UseVisualStyleBackColor = false;
            this.btnEmbed.Click += new System.EventHandler(this.btnEmbed_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(570, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Select Stegovideo:";
            // 
            // btnStegoVideo
            // 
            this.btnStegoVideo.BackColor = System.Drawing.Color.Black;
            this.btnStegoVideo.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStegoVideo.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnStegoVideo.Location = new System.Drawing.Point(689, 36);
            this.btnStegoVideo.Name = "btnStegoVideo";
            this.btnStegoVideo.Size = new System.Drawing.Size(120, 29);
            this.btnStegoVideo.TabIndex = 10;
            this.btnStegoVideo.Text = "Browse";
            this.btnStegoVideo.UseVisualStyleBackColor = false;
            this.btnStegoVideo.Click += new System.EventHandler(this.btnStegoVideo_Click);
            // 
            // btnRetrive
            // 
            this.btnRetrive.BackColor = System.Drawing.Color.Black;
            this.btnRetrive.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRetrive.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRetrive.Location = new System.Drawing.Point(573, 268);
            this.btnRetrive.Name = "btnRetrive";
            this.btnRetrive.Size = new System.Drawing.Size(320, 38);
            this.btnRetrive.TabIndex = 12;
            this.btnRetrive.Text = "Extract";
            this.btnRetrive.UseVisualStyleBackColor = false;
            this.btnRetrive.Click += new System.EventHandler(this.btnRetrive_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(572, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "Secrect Message:";
            // 
            // txtStegoSecrectMessage
            // 
            this.txtStegoSecrectMessage.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtStegoSecrectMessage.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStegoSecrectMessage.Location = new System.Drawing.Point(573, 93);
            this.txtStegoSecrectMessage.Multiline = true;
            this.txtStegoSecrectMessage.Name = "txtStegoSecrectMessage";
            this.txtStegoSecrectMessage.ReadOnly = true;
            this.txtStegoSecrectMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStegoSecrectMessage.Size = new System.Drawing.Size(320, 171);
            this.txtStegoSecrectMessage.TabIndex = 13;
            // 
            // btnValidation
            // 
            this.btnValidation.BackColor = System.Drawing.Color.Black;
            this.btnValidation.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValidation.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnValidation.Location = new System.Drawing.Point(748, 422);
            this.btnValidation.Name = "btnValidation";
            this.btnValidation.Size = new System.Drawing.Size(145, 156);
            this.btnValidation.TabIndex = 15;
            this.btnValidation.Text = "Check";
            this.btnValidation.UseVisualStyleBackColor = false;
            this.btnValidation.Click += new System.EventHandler(this.btnValidation_Click);
            // 
            // lblValidationHeading
            // 
            this.lblValidationHeading.AutoSize = true;
            this.lblValidationHeading.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValidationHeading.Location = new System.Drawing.Point(27, 402);
            this.lblValidationHeading.Name = "lblValidationHeading";
            this.lblValidationHeading.Size = new System.Drawing.Size(106, 17);
            this.lblValidationHeading.TabIndex = 16;
            this.lblValidationHeading.Text = "Validation Result";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(145, -3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Embedding Panel";
            // 
            // lblSelect1
            // 
            this.lblSelect1.AutoSize = true;
            this.lblSelect1.BackColor = System.Drawing.Color.Lime;
            this.lblSelect1.Location = new System.Drawing.Point(300, 44);
            this.lblSelect1.Name = "lblSelect1";
            this.lblSelect1.Size = new System.Drawing.Size(49, 13);
            this.lblSelect1.TabIndex = 10;
            this.lblSelect1.Text = "Selected";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(674, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 17);
            this.label7.TabIndex = 16;
            this.label7.Text = "Retrieving Panel";
            // 
            // lblSelect2
            // 
            this.lblSelect2.AutoSize = true;
            this.lblSelect2.BackColor = System.Drawing.Color.Lime;
            this.lblSelect2.Location = new System.Drawing.Point(815, 50);
            this.lblSelect2.Name = "lblSelect2";
            this.lblSelect2.Size = new System.Drawing.Size(49, 13);
            this.lblSelect2.TabIndex = 15;
            this.lblSelect2.Text = "Selected";
            // 
            // txtLog
            // 
            this.txtLog.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.Location = new System.Drawing.Point(29, 422);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(713, 156);
            this.txtLog.TabIndex = 17;
            // 
            // radioXORLSB
            // 
            this.radioXORLSB.AutoSize = true;
            this.radioXORLSB.BackColor = System.Drawing.Color.Transparent;
            this.radioXORLSB.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioXORLSB.Location = new System.Drawing.Point(584, 343);
            this.radioXORLSB.Name = "radioXORLSB";
            this.radioXORLSB.Size = new System.Drawing.Size(107, 19);
            this.radioXORLSB.TabIndex = 21;
            this.radioXORLSB.TabStop = true;
            this.radioXORLSB.Text = "XOR with LSB";
            this.radioXORLSB.UseVisualStyleBackColor = false;
            // 
            // radioLSBXOR8Dir
            // 
            this.radioLSBXOR8Dir.AutoSize = true;
            this.radioLSBXOR8Dir.BackColor = System.Drawing.Color.Transparent;
            this.radioLSBXOR8Dir.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioLSBXOR8Dir.Location = new System.Drawing.Point(359, 343);
            this.radioLSBXOR8Dir.Name = "radioLSBXOR8Dir";
            this.radioLSBXOR8Dir.Size = new System.Drawing.Size(180, 19);
            this.radioLSBXOR8Dir.TabIndex = 20;
            this.radioLSBXOR8Dir.TabStop = true;
            this.radioLSBXOR8Dir.Text = "8 Direction (LSB with XOR)";
            this.radioLSBXOR8Dir.UseVisualStyleBackColor = false;
            // 
            // radioLSB8Dir
            // 
            this.radioLSB8Dir.AutoSize = true;
            this.radioLSB8Dir.BackColor = System.Drawing.Color.Transparent;
            this.radioLSB8Dir.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioLSB8Dir.Location = new System.Drawing.Point(225, 343);
            this.radioLSB8Dir.Name = "radioLSB8Dir";
            this.radioLSB8Dir.Size = new System.Drawing.Size(75, 19);
            this.radioLSB8Dir.TabIndex = 19;
            this.radioLSB8Dir.TabStop = true;
            this.radioLSB8Dir.Text = "Proposed";
            this.radioLSB8Dir.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(421, 313);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 17);
            this.label6.TabIndex = 17;
            this.label6.Text = "Algorithms";
            // 
            // btnPrewitt
            // 
            this.btnPrewitt.BackColor = System.Drawing.Color.Black;
            this.btnPrewitt.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrewitt.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnPrewitt.Location = new System.Drawing.Point(402, 152);
            this.btnPrewitt.Name = "btnPrewitt";
            this.btnPrewitt.Size = new System.Drawing.Size(128, 64);
            this.btnPrewitt.TabIndex = 12;
            this.btnPrewitt.Text = "Prewitt Check Test";
            this.btnPrewitt.UseVisualStyleBackColor = false;
            this.btnPrewitt.Click += new System.EventHandler(this.btnPrewitt_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(923, 618);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnEmbed);
            this.Controls.Add(this.lblSelect2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnStegoVideo);
            this.Controls.Add(this.radioXORLSB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblSelect1);
            this.Controls.Add(this.btnRetrive);
            this.Controls.Add(this.btnPrewitt);
            this.Controls.Add(this.txtStegoSecrectMessage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnSelectVideo);
            this.Controls.Add(this.radioLSBXOR8Dir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.txtSecrectMessage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioLSB8Dir);
            this.Controls.Add(this.lblValidationHeading);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnValidation);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectVideo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSecrectMessage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnEmbed;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnStegoVideo;
        private System.Windows.Forms.Button btnRetrive;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtStegoSecrectMessage;
        private System.Windows.Forms.Button btnValidation;
        private System.Windows.Forms.Label lblValidationHeading;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton radioXORLSB;
        private System.Windows.Forms.RadioButton radioLSBXOR8Dir;
        private System.Windows.Forms.RadioButton radioLSB8Dir;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label lblSelect1;
        private System.Windows.Forms.Label lblSelect2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnPrewitt;
    }
}


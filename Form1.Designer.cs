﻿namespace GATE_SCAN2
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
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pbCamQr = new System.Windows.Forms.PictureBox();
            this.pbCam = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbCamOut = new System.Windows.Forms.RadioButton();
            this.rbCamIn = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cbbQr = new System.Windows.Forms.ComboBox();
            this.cbbCam = new System.Windows.Forms.ComboBox();
            this.lbTxtLicense = new System.Windows.Forms.Label();
            this.pbPlate = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbName = new System.Windows.Forms.Label();
            this.lbId = new System.Windows.Forms.Label();
            this.lbTimesScan = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCamQr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCam)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlate)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(27, 57);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(314, 356);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pbCamQr);
            this.tabPage1.Controls.Add(this.pbCam);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(306, 330);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Camera";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pbCamQr
            // 
            this.pbCamQr.Location = new System.Drawing.Point(44, 70);
            this.pbCamQr.Name = "pbCamQr";
            this.pbCamQr.Size = new System.Drawing.Size(193, 166);
            this.pbCamQr.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCamQr.TabIndex = 5;
            this.pbCamQr.TabStop = false;
            // 
            // pbCam
            // 
            this.pbCam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbCam.Location = new System.Drawing.Point(3, 3);
            this.pbCam.Name = "pbCam";
            this.pbCam.Size = new System.Drawing.Size(300, 324);
            this.pbCam.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCam.TabIndex = 4;
            this.pbCam.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.cbbQr);
            this.tabPage2.Controls.Add(this.cbbCam);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(306, 330);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Config";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(35, 181);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 13);
            this.label12.TabIndex = 7;
            this.label12.Text = "Adress IP:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(35, 154);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(26, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "QR:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(35, 127);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(46, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Camera:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbCamOut);
            this.groupBox2.Controls.Add(this.rbCamIn);
            this.groupBox2.Location = new System.Drawing.Point(21, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(260, 49);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Select camera";
            // 
            // rbCamOut
            // 
            this.rbCamOut.AutoSize = true;
            this.rbCamOut.Location = new System.Drawing.Point(158, 19);
            this.rbCamOut.Name = "rbCamOut";
            this.rbCamOut.Size = new System.Drawing.Size(66, 17);
            this.rbCamOut.TabIndex = 1;
            this.rbCamOut.Text = "Cam Out";
            this.rbCamOut.UseVisualStyleBackColor = true;
            // 
            // rbCamIn
            // 
            this.rbCamIn.AutoSize = true;
            this.rbCamIn.Checked = true;
            this.rbCamIn.Location = new System.Drawing.Point(17, 19);
            this.rbCamIn.Name = "rbCamIn";
            this.rbCamIn.Size = new System.Drawing.Size(58, 17);
            this.rbCamIn.TabIndex = 0;
            this.rbCamIn.TabStop = true;
            this.rbCamIn.Text = "Cam In";
            this.rbCamIn.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(206, 176);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(100, 178);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 2;
            // 
            // cbbQr
            // 
            this.cbbQr.FormattingEnabled = true;
            this.cbbQr.Location = new System.Drawing.Point(100, 151);
            this.cbbQr.Name = "cbbQr";
            this.cbbQr.Size = new System.Drawing.Size(121, 21);
            this.cbbQr.TabIndex = 1;
            // 
            // cbbCam
            // 
            this.cbbCam.FormattingEnabled = true;
            this.cbbCam.Location = new System.Drawing.Point(100, 124);
            this.cbbCam.Name = "cbbCam";
            this.cbbCam.Size = new System.Drawing.Size(121, 21);
            this.cbbCam.TabIndex = 0;
            // 
            // lbTxtLicense
            // 
            this.lbTxtLicense.AutoSize = true;
            this.lbTxtLicense.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTxtLicense.Location = new System.Drawing.Point(562, 131);
            this.lbTxtLicense.Name = "lbTxtLicense";
            this.lbTxtLicense.Size = new System.Drawing.Size(48, 42);
            this.lbTxtLicense.TabIndex = 10;
            this.lbTxtLicense.Text = "...";
            // 
            // pbPlate
            // 
            this.pbPlate.Location = new System.Drawing.Point(354, 82);
            this.pbPlate.Name = "pbPlate";
            this.pbPlate.Size = new System.Drawing.Size(195, 130);
            this.pbPlate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPlate.TabIndex = 9;
            this.pbPlate.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(365, 233);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 23);
            this.label1.TabIndex = 11;
            this.label1.Text = "Status :";
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatus.Location = new System.Drawing.Point(443, 233);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(55, 23);
            this.lbStatus.TabIndex = 12;
            this.lbStatus.Text = "None";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(365, 292);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 23);
            this.label2.TabIndex = 13;
            this.label2.Text = "Id:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(365, 347);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 23);
            this.label3.TabIndex = 14;
            this.label3.Text = "Name:";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbName.Location = new System.Drawing.Point(443, 347);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(55, 23);
            this.lbName.TabIndex = 15;
            this.lbName.Text = "None";
            // 
            // lbId
            // 
            this.lbId.AutoSize = true;
            this.lbId.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbId.Location = new System.Drawing.Point(443, 292);
            this.lbId.Name = "lbId";
            this.lbId.Size = new System.Drawing.Size(55, 23);
            this.lbId.TabIndex = 16;
            this.lbId.Text = "None";
            // 
            // lbTimesScan
            // 
            this.lbTimesScan.AutoSize = true;
            this.lbTimesScan.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTimesScan.Location = new System.Drawing.Point(416, 56);
            this.lbTimesScan.Name = "lbTimesScan";
            this.lbTimesScan.Size = new System.Drawing.Size(55, 23);
            this.lbTimesScan.TabIndex = 17;
            this.lbTimesScan.Text = "None";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 441);
            this.Controls.Add(this.lbTimesScan);
            this.Controls.Add(this.lbId);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbTxtLicense);
            this.Controls.Add(this.pbPlate);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Gate_Scan";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCamQr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCam)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.PictureBox pbCamQr;
        private System.Windows.Forms.PictureBox pbCam;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbCamOut;
        private System.Windows.Forms.RadioButton rbCamIn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox cbbQr;
        private System.Windows.Forms.ComboBox cbbCam;
        private System.Windows.Forms.Label lbTxtLicense;
        private System.Windows.Forms.PictureBox pbPlate;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Label lbId;
        private System.Windows.Forms.Label lbTimesScan;
    }
}

using System;
using System.Windows.Forms;
using System.Drawing;
namespace OVPN_Au
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.labtimecheck = new System.Windows.Forms.Label();
            this.labcurrentip = new System.Windows.Forms.Label();
            this.labipport = new System.Windows.Forms.Label();
            this.btconnect = new System.Windows.Forms.Button();
            this.labtimertick = new System.Windows.Forms.Label();
            this.lblisp = new System.Windows.Forms.Label();
            this.labcontry = new System.Windows.Forms.Label();
            this.timerCheckConnet = new System.Windows.Forms.Timer(this.components);
            this.timer1_ChangeSSH = new System.Windows.Forms.Timer(this.components);
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.timerChangeConfigFile = new System.Windows.Forms.Timer(this.components);
            this.bttestcopy = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(0, 82);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(508, 264);
            this.listBox1.TabIndex = 0;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(12, 12);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(53, 20);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // labtimecheck
            // 
            this.labtimecheck.AutoSize = true;
            this.labtimecheck.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labtimecheck.ForeColor = System.Drawing.Color.ForestGreen;
            this.labtimecheck.Location = new System.Drawing.Point(235, 3);
            this.labtimecheck.Name = "labtimecheck";
            this.labtimecheck.Size = new System.Drawing.Size(40, 16);
            this.labtimecheck.TabIndex = 10;
            this.labtimecheck.Text = "label2";
            // 
            // labcurrentip
            // 
            this.labcurrentip.AutoSize = true;
            this.labcurrentip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labcurrentip.ForeColor = System.Drawing.Color.Blue;
            this.labcurrentip.Location = new System.Drawing.Point(234, 33);
            this.labcurrentip.Name = "labcurrentip";
            this.labcurrentip.Size = new System.Drawing.Size(57, 20);
            this.labcurrentip.TabIndex = 9;
            this.labcurrentip.Text = "label1";
            // 
            // labipport
            // 
            this.labipport.AutoSize = true;
            this.labipport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labipport.ForeColor = System.Drawing.Color.Crimson;
            this.labipport.Location = new System.Drawing.Point(234, 53);
            this.labipport.Name = "labipport";
            this.labipport.Size = new System.Drawing.Size(57, 20);
            this.labipport.TabIndex = 8;
            this.labipport.Text = "label1";
            // 
            // btconnect
            // 
            this.btconnect.Location = new System.Drawing.Point(12, 50);
            this.btconnect.Name = "btconnect";
            this.btconnect.Size = new System.Drawing.Size(75, 23);
            this.btconnect.TabIndex = 11;
            this.btconnect.Text = "Connect";
            this.btconnect.UseVisualStyleBackColor = true;
            this.btconnect.Click += new System.EventHandler(this.btconnect_Click);
            // 
            // labtimertick
            // 
            this.labtimertick.AutoSize = true;
            this.labtimertick.Location = new System.Drawing.Point(85, 34);
            this.labtimertick.Name = "labtimertick";
            this.labtimertick.Size = new System.Drawing.Size(35, 13);
            this.labtimertick.TabIndex = 12;
            this.labtimertick.Text = "label1";
            // 
            // lblisp
            // 
            this.lblisp.AutoSize = true;
            this.lblisp.Location = new System.Drawing.Point(397, 18);
            this.lblisp.Name = "lblisp";
            this.lblisp.Size = new System.Drawing.Size(22, 13);
            this.lblisp.TabIndex = 13;
            this.lblisp.Text = "VN";
            // 
            // labcontry
            // 
            this.labcontry.AutoSize = true;
            this.labcontry.Location = new System.Drawing.Point(397, 38);
            this.labcontry.Name = "labcontry";
            this.labcontry.Size = new System.Drawing.Size(35, 13);
            this.labcontry.TabIndex = 13;
            this.labcontry.Text = "label1";
            // 
            // timerCheckConnet
            // 
            this.timerCheckConnet.Interval = 10000;
            this.timerCheckConnet.Tick += new System.EventHandler(this.timerCheckConnet_Tick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(115, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(101, 17);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.Text = "runcommand firt";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(115, 35);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(114, 17);
            this.checkBox2.TabIndex = 14;
            this.checkBox2.Text = "runcommand affter";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // timerChangeConfigFile
            // 
            this.timerChangeConfigFile.Enabled = true;
            this.timerChangeConfigFile.Interval = 1200000;
            this.timerChangeConfigFile.Tick += new System.EventHandler(this.timerChangeConfigFile_Tick);
            // 
            // bttestcopy
            // 
            this.bttestcopy.Location = new System.Drawing.Point(357, 54);
            this.bttestcopy.Name = "bttestcopy";
            this.bttestcopy.Size = new System.Drawing.Size(139, 23);
            this.bttestcopy.TabIndex = 15;
            this.bttestcopy.Text = "test copy config";
            this.bttestcopy.UseVisualStyleBackColor = true;
            this.bttestcopy.Click += new System.EventHandler(this.bttestcopy_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(508, 347);
            this.Controls.Add(this.bttestcopy);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.labcontry);
            this.Controls.Add(this.lblisp);
            this.Controls.Add(this.labtimertick);
            this.Controls.Add(this.btconnect);
            this.Controls.Add(this.labtimecheck);
            this.Controls.Add(this.labcurrentip);
            this.Controls.Add(this.labipport);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.listBox1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Auto OpenVPN";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button btconnect;
        //private IContainer components = null;
        private Label labcurrentip;
        private Label labipport;
        private Label labtimecheck;
        private Label labtimertick;
        private ListBox listBox1;
        private NumericUpDown numericUpDown1;
        
        private Label lblisp;
        private Label labcontry;
        private Timer timerCheckConnet;
        private Timer timer1_ChangeSSH;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private Timer timerChangeConfigFile;
        private Button bttestcopy;
    }
}


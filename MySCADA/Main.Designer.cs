using System.Windows.Forms;

namespace MySCADA
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lbSpeedometer;
        private VerticalProgressBar speedProgressBar;
        private System.Windows.Forms.Label lbCurrentRuntime;
        private System.Windows.Forms.Label lbTotalRuntime;
        private System.Windows.Forms.ComboBox cmbMode;
        private System.Windows.Forms.Label lbMode;
        private System.Windows.Forms.PictureBox pbFault;
        private System.Windows.Forms.Label lbFaultStatus;
        private System.Windows.Forms.Button btReset;

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
            this.label1 = new System.Windows.Forms.Label();
            this.btSTART = new System.Windows.Forms.Button();
            this.Monitortimer = new System.Windows.Forms.Timer(this.components);
            this.btStop = new System.Windows.Forms.Button();
            this.lbMotor = new System.Windows.Forms.Label();
            this.lbMotor2 = new System.Windows.Forms.Label();
            this.Simulationtimer = new System.Windows.Forms.Timer(this.components);
            this.pbMotor = new System.Windows.Forms.PictureBox();
            this.pbButton = new System.Windows.Forms.PictureBox();
            this.pbAgitator = new System.Windows.Forms.PictureBox();

            // New controls
            this.cmbMode = new System.Windows.Forms.ComboBox();
            this.lbMode = new System.Windows.Forms.Label();
            this.pbFault = new System.Windows.Forms.PictureBox();
            this.lbFaultStatus = new System.Windows.Forms.Label();
            this.btReset = new System.Windows.Forms.Button();
            this.lbCurrentRuntime = new System.Windows.Forms.Label();
            this.lbTotalRuntime = new System.Windows.Forms.Label();

            // Speed limit controls
            this.lbSpeedLimit = new System.Windows.Forms.Label();
            this.numSpeedLimit = new System.Windows.Forms.NumericUpDown();

            ((System.ComponentModel.ISupportInitialize)(this.pbMotor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAgitator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFault)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeedLimit)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(273, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(352, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "SCADA for Pumping Station";
            // 
            // btSTART
            // 
            this.btSTART.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btSTART.Location = new System.Drawing.Point(87, 156);
            this.btSTART.Margin = new System.Windows.Forms.Padding(4);
            this.btSTART.Name = "btSTART";
            this.btSTART.Size = new System.Drawing.Size(119, 48);
            this.btSTART.TabIndex = 1;
            this.btSTART.TabStop = false;
            this.btSTART.Text = "START";
            this.btSTART.UseVisualStyleBackColor = true;
            this.btSTART.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btSTART_MouseDown);
            this.btSTART.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btSTART_MouseUp);
            // 
            // Monitortimer
            // 
            this.Monitortimer.Enabled = true;
            this.Monitortimer.Interval = 250;
            this.Monitortimer.Tick += new System.EventHandler(this.Monitortimer_Tick);
            // 
            // btStop
            // 
            this.btStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btStop.Location = new System.Drawing.Point(87, 224);
            this.btStop.Margin = new System.Windows.Forms.Padding(4);
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(119, 48);
            this.btStop.TabIndex = 2;
            this.btStop.TabStop = false;
            this.btStop.Text = "STOP";
            this.btStop.UseVisualStyleBackColor = true;
            this.btStop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btStop_MouseDown);
            this.btStop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btStop_MouseUp);
            // 
            // lbMotor
            // 
            this.lbMotor.AutoSize = true;
            this.lbMotor.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMotor.Location = new System.Drawing.Point(254, 159);
            this.lbMotor.Name = "lbMotor";
            this.lbMotor.Size = new System.Drawing.Size(83, 31);
            this.lbMotor.TabIndex = 3;
            this.lbMotor.Text = "Motor";
            // 
            // lbMotor2
            // 
            this.lbMotor2.AutoSize = true;
            this.lbMotor2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMotor2.Location = new System.Drawing.Point(254, 227);
            this.lbMotor2.Name = "lbMotor2";
            this.lbMotor2.Size = new System.Drawing.Size(83, 31);
            this.lbMotor2.TabIndex = 4;
            this.lbMotor2.Text = "Motor";
            // 
            // Simulationtimer
            // 
            this.Simulationtimer.Enabled = true;
            this.Simulationtimer.Tick += new System.EventHandler(this.Simulationtimer_Tick);
            // 
            // pbMotor
            // 
            this.pbMotor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbMotor.Location = new System.Drawing.Point(405, 168);
            this.pbMotor.Name = "pbMotor";
            this.pbMotor.Size = new System.Drawing.Size(110, 90);
            this.pbMotor.TabIndex = 5;
            this.pbMotor.TabStop = false;
            // 
            // pbButton
            // 
            this.pbButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbButton.Location = new System.Drawing.Point(554, 168);
            this.pbButton.Name = "pbButton";
            this.pbButton.Size = new System.Drawing.Size(110, 90);
            this.pbButton.TabIndex = 6;
            this.pbButton.TabStop = false;
            // 
            // pbAgitator
            // 
            this.pbAgitator.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbAgitator.Location = new System.Drawing.Point(700, 168);
            this.pbAgitator.Name = "pbAgitator";
            this.pbAgitator.Size = new System.Drawing.Size(110, 90);
            this.pbAgitator.TabIndex = 7;
            this.pbAgitator.TabStop = false;
            // 
            // Speedometer
            // 
            this.lbSpeedometer = new System.Windows.Forms.Label();
            this.lbSpeedometer.AutoSize = true;
            this.lbSpeedometer.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSpeedometer.Location = new System.Drawing.Point(800, 120);
            this.lbSpeedometer.Name = "lbSpeedometer";
            this.lbSpeedometer.Size = new System.Drawing.Size(150, 31);
            this.lbSpeedometer.TabIndex = 8;
            this.lbSpeedometer.Text = "Speed: 0";
            // 
            // speedProgressBar
            // 
            this.speedProgressBar = new VerticalProgressBar();
            this.speedProgressBar.Location = new System.Drawing.Point(850, 168);
            this.speedProgressBar.Name = "speedProgressBar";
            this.speedProgressBar.Size = new System.Drawing.Size(30, 300);
            this.speedProgressBar.TabIndex = 9;
            this.speedProgressBar.Maximum = 1000;
            this.speedProgressBar.Style = ProgressBarStyle.Continuous;
            // 
            // lbMode
            // 
            this.lbMode.AutoSize = true;
            this.lbMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMode.ForeColor = System.Drawing.Color.White;
            this.lbMode.Location = new System.Drawing.Point(87, 90);
            this.lbMode.Name = "lbMode";
            this.lbMode.Size = new System.Drawing.Size(67, 25);
            this.lbMode.TabIndex = 10;
            this.lbMode.Text = "Mode:";
            // 
            // cmbMode
            // 
            this.cmbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMode.FormattingEnabled = true;
            this.cmbMode.Location = new System.Drawing.Point(160, 90);
            this.cmbMode.Name = "cmbMode";
            this.cmbMode.Size = new System.Drawing.Size(121, 28);
            this.cmbMode.TabIndex = 11;
            // 
            // pbFault
            // 
            this.pbFault.BackgroundImage = this.warning_icon;
            this.pbFault.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbFault.Location = new System.Drawing.Point(87, 330);
            this.pbFault.Name = "pbFault";
            this.pbFault.Size = new System.Drawing.Size(50, 50);
            this.pbFault.TabIndex = 12;
            this.pbFault.TabStop = false;
            // 
            // lbFaultStatus
            // 
            this.lbFaultStatus.AutoSize = true;
            this.lbFaultStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFaultStatus.ForeColor = System.Drawing.Color.White;
            this.lbFaultStatus.Location = new System.Drawing.Point(147, 342);
            this.lbFaultStatus.Name = "lbFaultStatus";
            this.lbFaultStatus.Size = new System.Drawing.Size(159, 25);
            this.lbFaultStatus.TabIndex = 13;
            this.lbFaultStatus.Text = "Status: Normal";
            // 
            // btReset
            // 
            this.btReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btReset.Location = new System.Drawing.Point(87, 390);
            this.btReset.Name = "btReset";
            this.btReset.Size = new System.Drawing.Size(119, 48);
            this.btReset.TabIndex = 14;
            this.btReset.TabStop = false;
            this.btReset.Text = "RESET";
            this.btReset.UseVisualStyleBackColor = true;
            this.btReset.Click += new System.EventHandler(this.btReset_Click);
            // 
            // lbCurrentRuntime
            // 
            this.lbCurrentRuntime.AutoSize = true;
            this.lbCurrentRuntime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCurrentRuntime.ForeColor = System.Drawing.Color.White;
            this.lbCurrentRuntime.Location = new System.Drawing.Point(400, 330);
            this.lbCurrentRuntime.Name = "lbCurrentRuntime";
            this.lbCurrentRuntime.Size = new System.Drawing.Size(226, 25);
            this.lbCurrentRuntime.TabIndex = 15;
            this.lbCurrentRuntime.Text = "Current: 00:00:00.000";
            // 
            // lbTotalRuntime
            // 
            this.lbTotalRuntime.AutoSize = true;
            this.lbTotalRuntime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalRuntime.ForeColor = System.Drawing.Color.White;
            this.lbTotalRuntime.Location = new System.Drawing.Point(400, 370);
            this.lbTotalRuntime.Name = "lbTotalRuntime";
            this.lbTotalRuntime.Size = new System.Drawing.Size(196, 25);
            this.lbTotalRuntime.TabIndex = 16;
            this.lbTotalRuntime.Text = "Total: 00:00:00.000";
            //
            // lbSpeedLimit
            //
            this.lbSpeedLimit = new System.Windows.Forms.Label();
            this.lbSpeedLimit.AutoSize = true;
            this.lbSpeedLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSpeedLimit.ForeColor = System.Drawing.Color.White;
            this.lbSpeedLimit.Location = new System.Drawing.Point(600, 330);
            this.lbSpeedLimit.Name = "lbSpeedLimit";
            this.lbSpeedLimit.Size = new System.Drawing.Size(180, 25);
            this.lbSpeedLimit.TabIndex = 17;
            this.lbSpeedLimit.Text = "Speed Limit: 1000";
            //
            // numSpeedLimit
            //
            this.numSpeedLimit = new System.Windows.Forms.NumericUpDown();
            this.numSpeedLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numSpeedLimit.Location = new System.Drawing.Point(600, 370);
            this.numSpeedLimit.Maximum = 1000;
            this.numSpeedLimit.Minimum = 100;
            this.numSpeedLimit.Name = "numSpeedLimit";
            this.numSpeedLimit.Size = new System.Drawing.Size(120, 30);
            this.numSpeedLimit.TabIndex = 18;
            this.numSpeedLimit.Value = 1000;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(963, 544);
            this.Controls.Add(this.lbTotalRuntime);
            this.Controls.Add(this.lbCurrentRuntime);
            this.Controls.Add(this.btReset);
            this.Controls.Add(this.lbFaultStatus);
            this.Controls.Add(this.pbFault);
            this.Controls.Add(this.cmbMode);
            this.Controls.Add(this.lbMode);
            this.Controls.Add(this.speedProgressBar);
            this.Controls.Add(this.lbSpeedometer);
            this.Controls.Add(this.pbButton);
            this.Controls.Add(this.pbAgitator);
            this.Controls.Add(this.pbMotor);
            this.Controls.Add(this.lbMotor2);
            this.Controls.Add(this.lbMotor);
            this.Controls.Add(this.btStop);
            this.Controls.Add(this.btSTART);
            this.Controls.Add(this.label1);
            // Add new controls to the form
            this.Controls.Add(this.numSpeedLimit);
            this.Controls.Add(this.lbSpeedLimit);
            this.ForeColor = System.Drawing.Color.Red;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.Text = "Main Page";
            ((System.ComponentModel.ISupportInitialize)(this.pbMotor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAgitator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFault)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btSTART;
        private System.Windows.Forms.Timer Monitortimer;
        private System.Windows.Forms.Button btStop;
        private System.Windows.Forms.Label lbMotor;
        private System.Windows.Forms.Label lbMotor2;
        private System.Windows.Forms.Timer Simulationtimer;
        private System.Windows.Forms.PictureBox pbMotor;
        private System.Windows.Forms.PictureBox pbButton;
        private System.Windows.Forms.PictureBox pbAgitator;
        private System.Windows.Forms.Label lbSpeedLimit;
        private System.Windows.Forms.NumericUpDown numSpeedLimit;
    }
}
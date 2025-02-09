namespace MySCADA
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
            ((System.ComponentModel.ISupportInitialize)(this.pbMotor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAgitator)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(273, 95);
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
            this.pbAgitator.Location = new System.Drawing.Point(700, 168); // Right next to pbButton
            this.pbAgitator.Name = "pbAgitator";
            this.pbAgitator.Size = new System.Drawing.Size(110, 90); // Same size as other PictureBoxes
            this.pbAgitator.TabIndex = 7;
            this.pbAgitator.TabStop = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(963, 544);
            this.Controls.Add(this.pbButton);
            this.Controls.Add(this.pbAgitator);
            this.Controls.Add(this.pbMotor);
            this.Controls.Add(this.lbMotor2);
            this.Controls.Add(this.lbMotor);
            this.Controls.Add(this.btStop);
            this.Controls.Add(this.btSTART);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.Red;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.Text = "Main Page";
            ((System.ComponentModel.ISupportInitialize)(this.pbMotor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbButton)).EndInit();
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
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySCADA
{
    public partial class Main : Form
    {
        Image pump_green = Image.FromFile(@"images\pump_base_green.gif");
        Image pump_red = Image.FromFile(@"images\pump_base_red.gif");
        Image button_off = Image.FromFile(@"images\red_button_off.png");
        Image button_on = Image.FromFile(@"images\red_button_on.png");

        // Add agitator images
        Image[] agitatorFrames = new Image[]
        {
            Image.FromFile(@"images\agitator_1.gif"),
            Image.FromFile(@"images\agitator_2.gif"),
            Image.FromFile(@"images\agitator_3.gif"),
            Image.FromFile(@"images\agitator_4.gif")
        };
        private int currentFrame = 0;

        public bool Start;
        public bool Stop;
        public bool Motor;
        public Main()
        {
            InitializeComponent();
            pbButton.MouseDown += new MouseEventHandler(pbButton_MouseDown);
            pbButton.MouseUp += new MouseEventHandler(pbButton_MouseUp);
        }

        private void pbButton_MouseDown(object sender, MouseEventArgs e)
        {
            // If motor is off, treat as START button
            if (!Motor)
            {
                Start = true;
            }
            // If motor is on, treat as STOP button
            else
            {
                Stop = true;
            }
        }

        private void pbButton_MouseUp(object sender, MouseEventArgs e)
        {
            // Reset both Start and Stop flags
            Start = false;
            Stop = false;
        }



        private void btSTART_MouseDown(object sender, MouseEventArgs e)
        {
            Start = true;
        }

        private void btSTART_MouseUp(object sender, MouseEventArgs e)
        {
            Start = false;
        }

        private void Monitortimer_Tick(object sender, EventArgs e)
        {
          
            Console.WriteLine($"Start= {Start}, Stop = {Stop}, Motor = {Motor}");
            lbMotor.Text = Motor.ToString();
            lbMotor2.Text = (Motor) ? "Chạy" : "Dừng";
            
            if(Motor)
            {
                pbMotor.BackgroundImage = pump_green;
                pbButton.BackgroundImage = button_on;

                // Animate agitator when motor is running
                currentFrame = (currentFrame + 1) % agitatorFrames.Length;
                pbAgitator.BackgroundImage = agitatorFrames[currentFrame];
            }
            else
            {
                pbMotor.BackgroundImage = pump_red;
                pbButton.BackgroundImage = button_off;

                // Reset agitator to first frame when stopped
                currentFrame = 0;
                pbAgitator.BackgroundImage = agitatorFrames[currentFrame];
            }
        }

        private void btStop_MouseDown(object sender, MouseEventArgs e)
        {
            Stop = true;
        }

        private void btStop_MouseUp(object sender, MouseEventArgs e)
        {
            Stop = false;
        }

        private void Simulationtimer_Tick(object sender, EventArgs e)
        {
            Motor = (Motor || Start) && !Stop;
        }
    }
}

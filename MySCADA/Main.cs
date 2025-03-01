using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace MySCADA
{
    public class VerticalProgressBar : ProgressBar
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x04; // PBS_VERTICAL style
                return cp;
            }
        }
    }

    public partial class Main : Form
    {
        Image pump_green = Image.FromFile(@"images\pump_base_green.gif");
        Image pump_red = Image.FromFile(@"images\pump_base_red.gif");
        Image button_off = Image.FromFile(@"images\red_button_off.png");
        Image button_on = Image.FromFile(@"images\red_button_on.png");
        Image warning_icon = Image.FromFile(@"images\warning.png");

        // Add agitator images
        Image[] agitatorFrames = new Image[]
        {
            Image.FromFile(@"images\agitator_1.gif"),
            Image.FromFile(@"images\agitator_2.gif"),
            Image.FromFile(@"images\agitator_3.gif"),
            Image.FromFile(@"images\agitator_4.gif")
        };
        private int currentFrame = 0;
        private int currentSpeed = 0;

        // Control flags
        public bool Start;
        public bool Stop;
        public bool Motor;
        public bool Fail; // New fault flag
        public bool Reset; // New reset flag
        public string Mode = "Manual"; // Default mode

        // Timer for motor runtime tracking
        private Stopwatch currentRuntime = new Stopwatch();
        private TimeSpan cumulativeRuntime = TimeSpan.Zero;
        private DateTime lastUpdateTime;

        public Main()
        {
            InitializeComponent();
            pbButton.MouseDown += new MouseEventHandler(pbButton_MouseDown);
            pbButton.MouseUp += new MouseEventHandler(pbButton_MouseUp);

            // Initialize ComboBox for mode selection
            cmbMode.Items.Add("Auto");
            cmbMode.Items.Add("Manual");
            cmbMode.SelectedItem = "Manual";
            cmbMode.SelectedIndexChanged += new EventHandler(cmbMode_SelectedIndexChanged);

            // Initialize fault indicator to hidden
            pbFault.Visible = false;
            lbFaultStatus.Text = "Status: Normal";

            // Initialize timers
            lastUpdateTime = DateTime.Now;
        }

        private void cmbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Mode = cmbMode.SelectedItem.ToString();
            UpdateControlStates();
        }

        private void UpdateControlStates()
        {
            // Update button states based on mode
            btSTART.Enabled = Mode == "Manual" && !Fail;
            btStop.Enabled = Mode == "Manual" && !Fail;
            btReset.Enabled = Fail; // Reset is only enabled during fault condition

            // In Auto mode, disable start/stop buttons
            if (Mode == "Auto" && !Fail)
            {
                // Auto-start the motor if in Auto mode and no fault
                Start = true;
            }
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            Reset = true;
        }

        private void pbButton_MouseDown(object sender, MouseEventArgs e)
        {
            // Only respond if in Manual mode and no fault
            if (Mode == "Manual" && !Fail)
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
        }

        private void pbButton_MouseUp(object sender, MouseEventArgs e)
        {
            // Reset both Start and Stop flags
            Start = false;
            Stop = false;
        }

        private void btSTART_MouseDown(object sender, MouseEventArgs e)
        {
            if (Mode == "Manual" && !Fail)
            {
                Start = true;
            }
        }

        private void btSTART_MouseUp(object sender, MouseEventArgs e)
        {
            Start = false;
        }

        private void Monitortimer_Tick(object sender, EventArgs e)
        {
            Console.WriteLine($"Mode = {Mode}, Start= {Start}, Stop = {Stop}, Motor = {Motor}, Fail = {Fail}");
            // Update motor status displays
            lbMotor.Text = Motor.ToString();
            lbMotor2.Text = (Motor) ? "Chạy" : "Dừng";
            // Update fault status
            if (Fail)
            {
                lbFaultStatus.Text = "Status: FAULT";
                pbFault.Visible = true;
            }
            else
            {
                lbFaultStatus.Text = "Status: Normal";
                pbFault.Visible = false;
            }
            // Speed control logic
            if (Motor && !Fail)
            {
                // Start runtime counter if not already running
                if (!currentRuntime.IsRunning)
                {
                    currentRuntime.Start();
                }
                // Gradually increase speed when motor is running
                if (currentSpeed < 1000)
                {
                    currentSpeed += 10;
                }
                pbMotor.BackgroundImage = pump_green;
                pbButton.BackgroundImage = button_on;
                // Animate agitator when motor is running
                currentFrame = (currentFrame + 1) % agitatorFrames.Length;
                pbAgitator.BackgroundImage = agitatorFrames[currentFrame];
                // Simulate random fault (approx. 0.1% chance per tick)
                if (new Random().Next(1000) == 0)
                {
                    Fail = true;
                }
            }
            else
            {
                // Stop runtime counter if it's running
                if (currentRuntime.IsRunning)
                {
                    currentRuntime.Stop();
                    // Add current session to cumulative time
                    cumulativeRuntime += currentRuntime.Elapsed;
                    // Reset the current runtime to 0
                    currentRuntime.Reset();
                }
                // Gradually decrease speed when motor is stopped
                if (currentSpeed > 0)
                {
                    currentSpeed -= 20;
                    // Ensure we don't go below 0
                    if (currentSpeed < 0)
                    {
                        currentSpeed = 0;
                    }
                }
                pbMotor.BackgroundImage = pump_red;
                pbButton.BackgroundImage = button_off;
                // Reset agitator to first frame when stopped
                currentFrame = 0;
                pbAgitator.BackgroundImage = agitatorFrames[currentFrame];
            }
            // Update UI elements
            currentSpeed = Math.Max(0, Math.Min(1000, currentSpeed));
            lbSpeedometer.Text = $"Speed: {currentSpeed}";
            speedProgressBar.Value = currentSpeed;
            // Update runtime displays
            TimeSpan currentSession = currentRuntime.Elapsed;
            lbCurrentRuntime.Text = string.Format("Current: {0:00}:{1:00}:{2:00}.{3:000}",
                currentSession.Hours, currentSession.Minutes, currentSession.Seconds, currentSession.Milliseconds);
            TimeSpan total = cumulativeRuntime + (currentRuntime.IsRunning ? currentRuntime.Elapsed : TimeSpan.Zero);
            lbTotalRuntime.Text = string.Format("Total: {0:00}:{1:00}:{2:00}.{3:000}",
                total.Hours, total.Minutes, total.Seconds, total.Milliseconds);
            // Update control states
            UpdateControlStates();
        }

        private void btStop_MouseDown(object sender, MouseEventArgs e)
        {
            if (Mode == "Manual" && !Fail)
            {
                Stop = true;
            }
        }

        private void btStop_MouseUp(object sender, MouseEventArgs e)
        {
            Stop = false;
        }

        private void Simulationtimer_Tick(object sender, EventArgs e)
        {
            // Auto mode logic - motor runs automatically unless there's a fault
            if (Mode == "Auto" && !Fail)
            {
                Motor = true;
            }
            // Manual mode logic - motor controlled by Start/Stop 
            else if (Mode == "Manual" && !Fail)
            {
                Motor = (Motor || Start) && !Stop;
            }
            // If fault occurs, stop motor regardless of mode
            else if (Fail)
            {
                Motor = false;
            }

            // Reset fault if reset button pressed
            if (Fail && Reset)
            {
                Fail = false;
                Reset = false;
            }
        }
    }
}
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
using System.Drawing.Drawing2D;

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
        Image fanRotor = Image.FromFile(@"images\Fan-rotor_grey.png");
        private float fanAngle = 0;

        // Add agitator images
        Image[] agitatorFrames = new Image[]
        {
            Image.FromFile(@"images\agitator_1.gif"),
            Image.FromFile(@"images\agitator_2.gif"),
            Image.FromFile(@"images\agitator_3.gif"),
            Image.FromFile(@"images\agitator_4.gif")
        };
        private int currentFrame = 0;
        private int targetSpeed = 0;
        private float currentSpeed = 0;
        private int speedLimit = 1000; // Default speed limit

        // Speed warning limits
        private int speedMax = 900;
        private int speedMin = 100;

        // Add startup timer to prevent immediate tripping
        private bool motorStartupPhase = false;
        private int startupCounter = 0;
        private int startupTimeAllowed = 50; // ~2.5 seconds at 50ms intervals

        // Motor speed calculation
        private double motorTime = 0;
        private List<float> speedHistory = new List<float>();
        private Timer timerSpeedMotor = new Timer();

        // Control flags
        public bool Start;
        public bool Stop;
        public bool Motor;
        public bool Fail; // Fault flag
        public bool Reset; // Reset flag
        public bool Trip;  // Trip flag
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

            // Initialize the speed limit controls
            numSpeedLimit.Value = speedLimit;
            numSpeedLimit.ValueChanged += new EventHandler(numSpeedLimit_ValueChanged);

            // Initialize motor speed timer
            timerSpeedMotor.Interval = 50;
            timerSpeedMotor.Tick += TimerSpeedMotor_Tick;

            // Initialize timers
            lastUpdateTime = DateTime.Now;

            // Set warning thresholds
            speedMax = 1000; // Fixed threshold at 1000
            speedMin = (int)(speedLimit * 0.1); // 10% of max speed as warning
        }

        private void numSpeedLimit_ValueChanged(object sender, EventArgs e)
        {
            // Update the speed limit
            speedLimit = (int)numSpeedLimit.Value;

            // Update the progress bar's maximum value
            speedProgressBar.Maximum = speedLimit;

            // Update the speed display to show the current limit
            lbSpeedLimit.Text = $"Speed Limit: {speedLimit}";

            // Update warning thresholds
            speedMax = speedMax = 1000; // 90% of max speed as warning
            speedMin = (int)(speedLimit * 0.1); // 10% of min speed as warning

            // If target speed exceeds the new limit, reduce it
            if (targetSpeed > speedLimit)
            {
                targetSpeed = speedLimit;
            }
        }

        private void cmbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Mode = cmbMode.SelectedItem.ToString();
            UpdateControlStates();
        }

        private void UpdateControlStates()
        {
            // Update button states based on mode
            btSTART.Enabled = Mode == "Manual" && !Fail && !Trip;
            btStop.Enabled = Mode == "Manual" && !Fail && !Trip;
            btReset.Enabled = Fail || Trip; // Reset is only enabled during fault or trip condition
            numSpeedLimit.Enabled = !Fail && !Trip; // Disable speed limit control during fault

            // In Auto mode, disable start/stop buttons
            if (Mode == "Auto" && !Fail && !Trip)
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
            // Only respond if in Manual mode and no fault or trip
            if (Mode == "Manual" && !Fail && !Trip)
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
            if (Mode == "Manual" && !Fail && !Trip)
            {
                Start = true;
            }
        }

        private void btSTART_MouseUp(object sender, MouseEventArgs e)
        {
            Start = false;
        }

        // Second-order motor speed calculation: 
        //Speed(t) = K * (1 - e^(-ζωₙt) * (cos(ωdt) + (ζ/√(1-ζ²)) * sin(ωdt)))
        //K = 1.0 (gain coefficient)
        //ζ(zeta) = 0.7 (damping ratio)
        //ωₙ(omega_n) = 0.3 (natural frequency)
        //ωd(omega_d) = ωₙ* √(1-ζ²) (damped natural frequency)
        //t = time in seconds
        private float SpeedMotor(double t)
        {
            float K = 1.0f;      // Gain
            float omega_n = 0.3f; // Natural frequency
            float zeta = 0.7f;    // Damping coefficient
            float omega_d = omega_n * (float)Math.Sqrt(1 - zeta * zeta);

            float expTerm = (float)Math.Exp(-zeta * omega_n * t);
            float cosTerm = (float)Math.Cos(omega_d * t);
            float sinTerm = (float)Math.Sin(omega_d * t);
            float dampingFactor = zeta / (float)Math.Sqrt(1 - zeta * zeta);

            return K * (1.0f - expTerm * (cosTerm + dampingFactor * sinTerm));
        }

        // Timer method to calculate motor speed using second-order model
        private void TimerSpeedMotor_Tick(object sender, EventArgs e)
        {
            motorTime += 0.05; // Increase by 0.05s
            currentSpeed = targetSpeed * SpeedMotor(motorTime);
            speedHistory.Add(currentSpeed);

            // Handle startup phase counter
            if (motorStartupPhase)
            {
                startupCounter++;

                // Exit startup phase after allowed time
                if (startupCounter >= startupTimeAllowed)
                {
                    motorStartupPhase = false;
                    startupCounter = 0;
                }
            }

            // Limit speedHistory to prevent memory issues
            if (speedHistory.Count > 1000)
            {
                speedHistory.RemoveAt(0);
            }
        }

        // Drawing method for speed history graph
        private void DrawSpeedGraph(PictureBox pictureBox)
        {
            if (pictureBox.Image == null ||
                pictureBox.Image.Width != pictureBox.Width ||
                pictureBox.Image.Height != pictureBox.Height)
            {
                pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            }

            using (Graphics g = Graphics.FromImage(pictureBox.Image))
            {
                g.Clear(Color.White);
                g.DrawLine(Pens.Black, 0, pictureBox.Height / 2, pictureBox.Width, pictureBox.Height / 2);

                // Draw speed history if we have data
                if (speedHistory.Count > 1)
                {
                    for (int i = 0; i < speedHistory.Count - 1 && i < pictureBox.Width - 1; i++)
                    {
                        int index = speedHistory.Count - i - 1;
                        int prevIndex = speedHistory.Count - i - 2;

                        if (index >= 0 && prevIndex >= 0)
                        {
                            float y1 = pictureBox.Height / 2 - (speedHistory[prevIndex] / speedLimit) * (pictureBox.Height / 2);
                            float y2 = pictureBox.Height / 2 - (speedHistory[index] / speedLimit) * (pictureBox.Height / 2);
                            g.DrawLine(Pens.Blue, pictureBox.Width - i - 2, y1, pictureBox.Width - i - 1, y2);
                        }
                    }
                }
            }

            pictureBox.Refresh();
        }

        private void Monitortimer_Tick(object sender, EventArgs e)
        {
            // Update motor status displays
            lbMotor.Text = Motor.ToString();
            lbMotor2.Text = (Motor) ? "Chạy" : "Dừng";

            // Update fault and trip status
            if (Fail || Trip)
            {
                lbFaultStatus.Text = Trip ? "Status: TRIP" : "Status: FAULT";
                pbFault.Visible = true;
            }
            else
            {
                lbFaultStatus.Text = motorStartupPhase ? "Status: Starting..." : "Status: Normal";
                pbFault.Visible = false;
            }

            // Speed control logic with improved dynamic model
            if (Motor && !Fail && !Trip)
            {
                // Start runtime counter if not already running
                if (!currentRuntime.IsRunning)
                {
                    currentRuntime.Start();
                    // Enter startup phase when motor starts
                    motorStartupPhase = true;
                    startupCounter = 0;
                }

                // Set target speed to the current limit
                targetSpeed = speedLimit;

                // Start the motor speed calculation timer if not running
                if (!timerSpeedMotor.Enabled)
                {
                    timerSpeedMotor.Start();
                }

                pbMotor.BackgroundImage = pump_green;
                pbButton.BackgroundImage = button_on;

                // Animate agitator when motor is running
                currentFrame = (currentFrame + 1) % agitatorFrames.Length;
                pbAgitator.BackgroundImage = agitatorFrames[currentFrame];

                // Check if speed exceeds limits
                // Only check after startup phase is complete and if the motor is running at 
                // a significant speed (avoid checking when stopping)
                if (!motorStartupPhase && currentSpeed > 0 &&
                    (currentSpeed > speedMax || (currentSpeed < speedMin && currentSpeed > 5)) &&
                    !Reset)
                {
                    Trip = true;
                }

                // Simulate random fault (approx. 0.1% chance per tick)
                // Don't trigger random faults during startup phase
                if (!motorStartupPhase && new Random().Next(1000) == 0 && !Fail && !Trip)
                {
                    Fail = true;
                }
            }
            else
            {
                // Exit startup phase if motor is stopped
                motorStartupPhase = false;
                startupCounter = 0;

                // Stop runtime counter if it's running
                if (currentRuntime.IsRunning)
                {
                    currentRuntime.Stop();
                    // Add current session to cumulative time
                    cumulativeRuntime += currentRuntime.Elapsed;
                    // Reset the current runtime to 0
                    currentRuntime.Reset();
                }

                // Stop the motor speed timer
                timerSpeedMotor.Stop();

                // Gradually decrease speed when motor is stopped
                currentSpeed = Math.Max(0, currentSpeed - 10);

                // Reset time when speed reaches zero
                if (currentSpeed <= 0)
                {
                    motorTime = 0;
                    currentSpeed = 0;
                }

                pbMotor.BackgroundImage = pump_red;
                pbButton.BackgroundImage = button_off;

                // Reset agitator to first frame when stopped
                currentFrame = 0;
                pbAgitator.BackgroundImage = agitatorFrames[currentFrame];
            }

            // Update UI elements
            lbSpeedometer.Text = $"Speed: {currentSpeed:F1}";
            speedProgressBar.Value = (int)Math.Min(speedLimit, currentSpeed);

            // Update runtime displays
            TimeSpan currentSession = currentRuntime.Elapsed;
            lbCurrentRuntime.Text = string.Format("Current: {0:00}:{1:00}:{2:00}.{3:000}",
                currentSession.Hours, currentSession.Minutes, currentSession.Seconds, currentSession.Milliseconds);
            TimeSpan total = cumulativeRuntime + (currentRuntime.IsRunning ? currentRuntime.Elapsed : TimeSpan.Zero);
            lbTotalRuntime.Text = string.Format("Total: {0:00}:{1:00}:{2:00}.{3:000}",
                total.Hours, total.Minutes, total.Seconds, total.Milliseconds);

            // Rotate fan according to speed (proportional to speed)
            fanAngle = (fanAngle + currentSpeed / 10) % 360;

            // Update control states
            UpdateControlStates();

            // Draw the rotated fan image
            DrawRotatedFan();

            // Update speed graph if pictureBox available
            if (this.Controls.Find("pbSpeedGraph", true).Length > 0)
            {
                PictureBox pbSpeedGraph = (PictureBox)this.Controls.Find("pbSpeedGraph", true)[0];
                DrawSpeedGraph(pbSpeedGraph);
            }
        }

        private void btStop_MouseDown(object sender, MouseEventArgs e)
        {
            if (Mode == "Manual" && !Fail && !Trip)
            {
                Stop = true;
            }
        }

        private void btStop_MouseUp(object sender, MouseEventArgs e)
        {
            Stop = false;
        }

        // Rotated fan image drawing
        private void DrawRotatedFan()
        {
            // Create a new bitmap if needed
            if (pbMotor.Image == null ||
                pbMotor.Image.Width != pbMotor.Width ||
                pbMotor.Image.Height != pbMotor.Height)
            {
                pbMotor.Image = new Bitmap(pbMotor.Width, pbMotor.Height);
            }

            using (Graphics g = Graphics.FromImage(pbMotor.Image))
            {
                // Clear the previous drawing
                g.Clear(Color.Transparent);

                // Calculate the center position
                int centerX = pbMotor.Width / 2;
                int centerY = pbMotor.Height / 2;

                // Setup transformation for rotation around center
                g.TranslateTransform(centerX, centerY);
                g.RotateTransform(fanAngle);

                // Set high quality interpolation
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // Draw the fan centered on rotation point
                g.DrawImage(fanRotor, -fanRotor.Width / 2, -fanRotor.Height / 2);
            }

            // Refresh to show changes
            pbMotor.Refresh();
        }

        private void Simulationtimer_Tick(object sender, EventArgs e)
        {
            // Reset fault if reset button pressed
            if ((Fail || Trip) && Reset)
            {
                Fail = false;
                Trip = false;
                Reset = false;
            }

            // Auto mode logic - motor runs automatically unless there's a fault or trip
            if (Mode == "Auto" && !Fail && !Trip)
            {
                Motor = true;
            }
            // Manual mode logic - motor controlled by Start/Stop 
            else if (Mode == "Manual" && !Fail && !Trip)
            {
                Motor = (Motor || Start) && !Stop;
            }
            // If fault occurs, stop motor regardless of mode
            else if (Fail || Trip)
            {
                Motor = false;
            }
        }
    }
}
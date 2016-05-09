using System;
using System.IO.Ports;

namespace CortanaCarConsole.Services
{
    public class MortorController : IDisposable
    {
        private SerialPort ComPort { get; set; }

        private System.Timers.Timer Timer { get; } = new System.Timers.Timer { AutoReset = false, Enabled = false };

        public MortorController(string portName)
        {
            this.ComPort = new SerialPort(portName);
            this.ComPort.Open();
            this.Action(MortorActions.StopMoving);

            this.Timer.Elapsed += Timer_Elapsed;
        }

        public void Action(string commandName, int timeout = 0)
        {
            var action = (MortorActions)Enum.Parse(typeof(MortorActions), commandName);
            Action(action, timeout);
        }

        public void Action(MortorActions action, int timeout = 0)
        {
            lock (this.ComPort)
            {
                this.Timer.Stop();

                var commandText = "";
                switch (action)
                {
                    case MortorActions.MoveForward:
                        commandText = "M(1010)";
                        break;
                    case MortorActions.MoveBackward:
                        commandText = "M(0101)";
                        break;
                    case MortorActions.TurnLeft:
                        commandText = "M(0110)";
                        break;
                    case MortorActions.TurnRight:
                        commandText = "M(1001)";
                        break;
                    case MortorActions.StopMoving:
                        commandText = "M(1111)";
                        break;
                    default:
                        throw new Exception($"Unknown command {action}");
                }

                this.ComPort.WriteLine(commandText);

                if (timeout != 0)
                {
                    this.Timer.Interval = timeout;
                    this.Timer.Start();
                }
            }
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Action(MortorActions.StopMoving);
        }

        public void Dispose()
        {
            this.Timer.Stop();
            this.Timer.Dispose();
            this.ComPort.Dispose();
        }
    }
}

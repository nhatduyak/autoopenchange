namespace Socket_Test
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Timers;

    public class ValueChange : INotifyPropertyChanged
    {
        private readonly Timer _timer;
        private string _val;
        private const int BN_CLICKED = 0xf5;
        public CustNumberValueChange CustNumberChange;
        public TranxValueChange TranxChange;
        private const int WM_CLOSE = 0x10;

        public event PropertyChangedEventHandler PropertyChanged;

        public ValueChange(double polingInterval, string val)
        {
            this._val = val;
            this._timer = new Timer();
            this._timer.AutoReset = false;
            this._timer.Interval = polingInterval;
            this._timer.Elapsed += new ElapsedEventHandler(this.TimerElapsed);
            this._timer.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process[] processesByName = Process.GetProcessesByName("Bvssh");
            IntPtr zero = IntPtr.Zero;
            while (zero == IntPtr.Zero)
            {
                zero = (IntPtr) FindWindow("#32768", "Key Verification");
                SendMessage(FindWindowEx(zero, IntPtr.Zero, "Button", "Accept and &Save"), 0xf5, IntPtr.Zero, IntPtr.Zero);
            }
        }

        [DllImport("User32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        private void RaisePropertyChanged(string caller)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
        public int sendWindowsMessage(int hWnd, int Msg, int wParam, int lParam)
        {
            int num = 0;
            if (hWnd > 0)
            {
                num = SendMessage((IntPtr) hWnd, Msg, wParam, lParam.ToString());
            }
            return num;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            this._timer.Start();
        }

        public string CustNumber
        {
            get
            {
                return "";
            }
            set
            {
                this.RaisePropertyChanged("CustNumber");
            }
        }

        public string Value
        {
            get
            {
                return this._val;
            }
            set
            {
                this._val = value;
                this.RaisePropertyChanged("Val");
            }
        }

        public delegate void CustNumberValueChange(bool _isChange, string strCustnum);

        public delegate void TranxValueChange(bool _isChange);
    }
}


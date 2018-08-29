namespace OVPN_Au
{
    using Socket_Test;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using WindowScrape.Types;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;

 
    
    public partial class Form1 : Form
    {
        private List<FileInfo> _listFile;
        private const int BN_CLICKED = 0xf5;
        
        private Thread t_Accept;
        private Thread t_CheckConnect;
        
        private const int WM_CLOSE = 0x10;
        private const int WM_GETTEXT = 13;
        public const uint WM_SETTEXT = 12;

        public Form1()
        {
            InitializeComponent();
           //string ip= GetLocalIPv4(NetworkInterfaceType.Ethernet);
            
            //test
           //SetComboItem(2);
            //OKChange();
            
            //



            LoadFileTime();

            this._listFile = new List<FileInfo>();
            this.LoadFile();
            this.ConnectOVPN();
            this.timer1_ChangeSSH.Enabled = true;
            this.timer1_ChangeSSH.Interval = ((int)this.numericUpDown1.Value) * 60000;
            this.timer1_ChangeSSH.Tick += new EventHandler(this.timer1_ChangeSSH_Tick);
            killChales("");
            timerCheckConnet_Tick(null, null);
        }

        Thread t_checkcontry;
        public void AccetpButtonSave()
        {
            IntPtr zero = IntPtr.Zero;
            for (int i = 0; (zero == IntPtr.Zero) && (i < 10); i++)
            {
                Thread.Sleep(0x3e8);
                zero = (IntPtr) FindWindow("#32770", "OpenVPN - User Authentication");
                if (zero == IntPtr.Zero)
                {
                    string[] filename = this.listBox1.Items[this.listBox1.SelectedIndex].ToString().Split('.');
                    if(filename.Length>1)
                    {
                        zero = (IntPtr)FindWindow("#32770", filename[0]);
                    }
                }
            }
            this.InteropSetText(zero, 0xb5, Config._user);
            this.InteropSetText(zero, 0xb6, Config._Pass);
            SendMessage(FindWindowEx(zero, IntPtr.Zero, "Button", "OK"), 0xf5, IntPtr.Zero, IntPtr.Zero);
        }

        private void btconnect_Click(object sender, EventArgs e)
        {
            this.timer1_ChangeSSH_Tick(null, null);
        }

        public void checkconnect()
        {
            try
            {
                uint num2;
                Process process = Process.GetProcessesByName("openvpn-gui")[0];
                int num = 0;
                string str = DateTime.Now.ToString();
                IntPtr windowThreadProcessId = (IntPtr) GetWindowThreadProcessId(process.Handle, out num2);
                string str2 = "Current State: Connecting";
                StringBuilder lpString = new StringBuilder(50);
                GetWindowText(windowThreadProcessId, lpString, 50);
                while ((lpString.ToString().Trim() != str2) && (num < 10))
                {
                    Thread.Sleep(0x3e8);
                    GetWindowText(windowThreadProcessId, lpString, 50);
                    num++;
                }
                if (num == 10)
                {
                    str = str + " *********** Faile";
                    this.ConnectOVPN();
                }
                else
                {
                    str = str + " ---------- OK";
                }
                this.labtimecheck.Text = str;
                this.Refresh();
            }
            catch (Exception exception)
            {
                CTLError.WriteError("check connect loi ....", exception.Message);
            }
        }

        public void ConnectOVPN()
        {
            try
            {
                coutFormCheck = 0;
                this.KillOVPN();
                this.killChales(Config._charlesname);
                if(checkBox1.Checked)
                    OpenBatFile();
                string command = string.Format("openvpn-gui.exe --connect {0}", this.listBox1.Items[this.listBox1.SelectedIndex]);
                FileInfo info = new FileInfo(Config._PathOVPN);
                this.RunCommand(command, info.Directory.FullName);
                Config.Setvalue("Index", this.listBox1.SelectedIndex.ToString());
                this.AccetpButtonSave();
                this.labtimecheck.Text = DateTime.Now.ToString();
                //timerCheckConnet.Enabled = true;
                //timerCheckConnet.Interval = 10000;
                //timerCheckConnet.Tick+=new EventHandler(timerCheckConnet_Tick);
                //coutFormCheck = 0;
                //countStart = 0;

                t_checkcontry = new Thread(btCheckContry);
                t_checkcontry.Start();
                if (checkBox2.Checked)
                    OpenBatFile();
                Random ran = new Random();
                int timeran= ran.Next(97);
                SetSystemTimeZone(_listTime[timeran]);
                string ranmac = MacAddress.GetRandomMac();
                senddatatoapp(ranmac);
                
            }
            catch (Exception exception)
            {
                CTLError.WriteError("Loi ConnectOVPN ....", exception.Message);
            }
        }
        public void btCheckContry()
        {
            CheckContry();
            //Thread.Sleep(30000);
            if (labcontry.Text == "VN" || labcontry.Text == "Vietnam")
            {
                Thread.Sleep(10000);
                bool c = CheckContry();
                if (!c)
                {
                    //Thread.Sleep(10000);
                    // c = CheckContry();
                    // if (!c)
                    // {
                         this.timer1_ChangeSSH_Tick(null, null);
                         return;
                     //}
                }
                else
                {
                    StartAPP(@"C:\Program Files\Charles\Charles.exe");
                }
            }
            else
            {
                StartAPP(@"C:\Program Files\Charles\Charles.exe");
            }
            
        }
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (this.components != null))
        //    {
        //        this.components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        [DllImport("User32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Config.Setvalue("TimeChange", this.numericUpDown1.Value.ToString());
                Config.Setvalue("Index", this.listBox1.SelectedIndex.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.timer1_ChangeSSH.Enabled = true;
            this.timer1_ChangeSSH.Interval = ((int) this.numericUpDown1.Value) * 60000;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetDlgItem(IntPtr hDlg, int nIDDlgItem);
        public string getipproxy()
        {
            try
            {
                return new WebClient().DownloadString("http://icanhazip.com");
            }
            catch (Exception exception)
            {
                CTLError.WriteError("Loi GetIPProxy......", exception.Message);
                return "";
            }
        }

        private string getValueControlByHandle(string title)
        {
            try
            {
                Process process = Process.GetProcessesByName("Openvpn-Gui")[0];
                Process process2 = Process.GetProcessesByName("Openvpn")[0];
                IntPtr dlgItem = GetDlgItem(process.MainWindowHandle, 0xa1);
                IntPtr ptr2 = FindWindowEx(process.MainWindowHandle, IntPtr.Zero, "Static", "Current State: Connecting");
                IntPtr handle = process.Handle;
                StringBuilder lpString = new StringBuilder(0xffff);
                GetWindowText(dlgItem, lpString, 50);
                IntPtr mainWindowHandle = process2.MainWindowHandle;
                string mainWindowTitle = process.MainWindowTitle;
                List<HwndObject> children = HwndObject.GetWindows()[0].GetChildren();
                return HwndObject.GetWindowByTitle(title).Title;
            }
            catch (Exception)
            {
                return "";
            }
        }

        [DllImport("user32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll", SetLastError=true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);
       
        private void InteropSetText(IntPtr iptrHWndDialog, int iControlID, string strTextToSet)
        {
            IntPtr dlgItem = GetDlgItem(iptrHWndDialog, iControlID);
            HandleRef hWnd = new HandleRef(null, dlgItem);
            SendMessage(hWnd, 12, IntPtr.Zero, strTextToSet);
        }

        public void killChales(string Name)
        {
            string namekill=Name;
            if(Name.Trim()==string.Empty)
                namekill="Charles";
            Process[] processesByName = Process.GetProcessesByName(namekill);
            foreach (Process process in processesByName)
            {
                process.Kill();
                Thread.Sleep(1000);
            }
        }

        public void KillOVPN()
        {
            foreach (Process process in Process.GetProcessesByName("openvpn-gui"))
            {
                process.Kill();
            }
            foreach (Process process in Process.GetProcessesByName("openvpn"))
            {
                process.Kill();
            }
        }

        public void LoadFile()
        {
            DirectoryInfo info = new DirectoryInfo(Config._PathConfigs);
            foreach (FileInfo info2 in info.GetFiles("*.ovpn"))
            {
                this._listFile.Add(info2);
                this.listBox1.Items.Add(info2.Name);
            }
            this.listBox1.SelectedIndex = Config._index;
            this.numericUpDown1.Value = Config._TimeChange;
        }

        public string RunCommand(string command, string folder)
        {
            try
            {
                string str = string.Empty;
                ProcessStartInfo startInfo = new ProcessStartInfo("cmd", "/c " + command) {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WorkingDirectory = folder
                };
                Process process = Process.Start(startInfo);
                return str;
            }
            catch (Exception exception)
            {
                CTLError.WriteError("RunCommand loi ...........", exception.Message);
                return "##Loi";
            }
        }

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, string lParam);
        public void StartAPP(string path)
        {
            try
            {
                Process.Start(path);
            }
            catch (Exception ex)
            { CTLError.WriteError(" loi start app ", ex.Message); }
        }

        private void timer1_ChangeSSH_Tick(object sender, EventArgs e)
        {
            this.labtimertick.Text = DateTime.Now.ToString();
            timerCheckConnet.Enabled = false;
            if (this.listBox1.SelectedIndex == (this.listBox1.Items.Count - 1))
            {
                this.listBox1.SelectedIndex = 0;
                this.ConnectOVPN();
            }
            else
            {
                this.listBox1.SelectedIndex++;
                this.ConnectOVPN();
            }
        }

        int countCheckContry = 0;
        public bool CheckContry()
        {
            while (countCheckContry < 6)
            {
                try
                {
                    string ip = "";
                    HttpWebRequest request;

                    request = (HttpWebRequest)WebRequest.Create("http://whoer.net");
                    //WebProxy proxy = new WebProxy("http://127.0.0.1:8888", true);

                    //request.Credentials = CredentialCache.DefaultCredentials;

                    //request.Proxy = proxy;
                    request.Timeout = 10;
                    request.Proxy = WebRequest.DefaultWebProxy;
                    request.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string str = reader.ReadToEnd();
                    Match match;
                    //string responseFromServer = reader.ReadToEnd();
                    //match = Regex.Match(str, @"ico-holder isp[^\n]+[^>]+>([^<]+)", RegexOptions.IgnoreCase);
                    match = Regex.Match(str, @"strong data-clipboard-target=+\w*[^>]+\w*[^<][^>]*", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        string[] myip = match.Value.Split('<');
                        if (myip.Length >= 2)
                        {
                            string[] getmyip = myip[0].Split('>');
                            if (getmyip.Length > 0)
                            {
                                ip = getmyip[1];
                                this.Invoke((MethodInvoker)delegate()
                                {
                                    lblisp.Text = ip;
                                    this.Refresh();
                                });

                            }
                        }
                        //this.Invoke((MethodInvoker)delegate() { lblisp.Text = match.Groups[1].Value; lblisp.Visible = true; });
                    }
                    else
                    {
                        //this.Invoke((MethodInvoker)delegate() { lblisp.Text = "ERROR"; lblisp.Visible = true; });
                    }
                    Match math1;
                    //math1 = Regex.Match(str, "<span class=\"country-holder\">[^<span][^\n]s*[^>]*[^<img][^\n]s*[^>]*[^</span>][^\n][^<span][^\n][<span class=\"cont overdots\"]*[^>]*[<dt>Region:</dt>]", RegexOptions.IgnoreCase);
                    System.Text.RegularExpressions.MatchCollection tt = Regex.Matches(str, "<span class=\"cont overdots\">[a-zA-Z]*[^</span>]", RegexOptions.IgnoreCase);
                    bool checkcon = false;
                    if (tt.Count>0)
                    {
                        string namecon = "VN";
                        string[] lname;
                        foreach (Match m in tt)
                        {
                            if (m.Value.Contains("Vietnam"))
                            {
                                checkcon = true;
                                lname = m.Value.Split(new string[]{ ">"},StringSplitOptions.None);
                                if (lname.Length > 1)
                                    namecon = lname[1];
                                break;
                            }
                        }
                        //checkcon = math1.Value.Contains("vn.png");
                        
                        if (checkcon)
                        {
                            this.Invoke((MethodInvoker)delegate()
                            {
                                labcontry.Text = namecon;
                                labcontry.Refresh();
                                this.Refresh();
                            });

                            return false;
                        }
                        else
                        {
                            labcontry.Text = namecon;
                        }
                    }
                    reader.Close();
                    dataStream.Close();
                    response.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    CTLError.WriteError("Loi CheckContry ", ex.Message);
                    this.Invoke((MethodInvoker)delegate()
                    {
                        labcontry.Text = "VN";
                        labcontry.Refresh();
                        this.Refresh();
                    });
                    //return false;
                    Thread.Sleep(5000);
                }
                countCheckContry++;
                //MsgBox(responseFromServer);
            }
            countCheckContry = 0;
            return true;
        }
        //check whoer
        public void OpenBatFile()
        {
            try
            {
                if (Config._strRunFile.Length <= 0)
                    return;
                string[] list = Config._strRunFile.Split(';');
               
                foreach (string s in list)
                {
                    System.Diagnostics.Process.Start(s);
                    Thread.Sleep(500);
                }
            }
            catch (Exception ex)
            {
                CTLError.WriteError("Loi openBatFile ", ex.Message);
            }

        }
        int coutFormCheck = 0;
        int countStart = 0;
        private void timerCheckConnet_Tick(object sender, EventArgs e)
        {
            //////form user pass 
            ////IntPtr formUser = (IntPtr)FindWindow("#32770", "OpenVPN - User Authentication");
            ////MessageBox.Show("form user" + formUser.ToString());
            //bool checkcon = false;
            //checkcon = CheckContry();
            ////Thread.Sleep(1000);
            ////MessageBox.Show("form check" + formCheck.ToString());
            //if (!checkcon)
            //{
            //    coutFormCheck++;
            //}
            //else
            //{
            //    countStart++;
            //}
            //if (coutFormCheck > 2)
            //{
            //    //
            //    timerCheckConnet.Enabled = false;
            //    coutFormCheck = 0;
            //    btconnect_Click(null, null);
            //}
            ////if (countStart > 2)
            ////{
            ////    StartAPP(@"C:\Program Files\Charles\Charles.exe");
            ////    timerCheckConnet.Enabled = false;
            ////    countStart = 0;
            ////}

        }
        public string GetLocalIPv4(NetworkInterfaceType _type)
        {
            string output = "";
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            if(ip.Address.ToString().Contains("192"))
                                output = ip.Address.ToString();
                        }
                    }
                }
            }
            return output;
        }
        public void SetSystemTimeZone(string timeZoneId)
        {
            try
            {
                var process = Process.Start(new ProcessStartInfo
                {
                    FileName = "tzutil.exe",
                    Arguments = "/s \"" + timeZoneId + "\"",
                    UseShellExecute = false,
                    CreateNoWindow = true
                });

                if (process != null)
                {
                    process.WaitForExit();
                    //TimeZoneInfo.ClearCachedData();

                }
            }
            catch (Exception ex)
            {
                CTLError.WriteError("Loi SetSystemTimeZone ", ex.Message);
            }
        }
        List<string> _listTime = new List<string>();
        public void LoadFileTime()
        {
            _listTime.Clear();
            StreamReader read = new StreamReader(Path.Combine(Application.StartupPath, "time.txt"));
            while (!read.EndOfStream)
            {
                string s = read.ReadLine();
                if (s.Trim() != string.Empty)
                    _listTime.Add(s);
            }
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError =
 false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int
        wParam, string lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError =
        false, EntryPoint = "SendMessage")]
        public static extern IntPtr SendRefMessage(IntPtr hWnd, uint Msg, int
        wParam, StringBuilder lParam);
        uint CB_GETLBTEXT = 0x0148;
        uint CB_SETCURSEL = 0x014E;
        public string GetComboItem(int index)
        {
            StringBuilder ssb = new StringBuilder(256, 256);
            SendRefMessage(this.Handle, CB_GETLBTEXT, index, ssb);
            return ssb.ToString();
        }
        public void SetComboItem(int index)
        {
            //IntPtr hand = new IntPtr(0xCD1948);
            IntPtr hand = GetHandbyControlID(0);
            SendMessage(hand, CB_SETCURSEL, index, "0");
        }
        public void senddatatoapp(string macadd)
        {
            try
            {
                //for (int i = 1; i >=0; i--)
                //{
                //    SetComboItem(i);
                //    SetTextt(GetHandbyControlID(1), macadd);
                //    SendMessage(GetHandbyControlID(2), BM_CLICK, IntPtr.Zero, IntPtr.Zero);
                //    Thread.Sleep(3000); 
                //    OKChange();
                //}

            }
            catch (Exception ex)
            {
                CTLError.WriteError("Loi GetHandBycontrolID ", ex.Message);
            }
        }

        const int BM_CLICK = 0x00F5;

        //Combobox id =1000
        //0 combobox
        //1 textbox
        //2 btchange
        //3 
        public IntPtr GetHandbyControlID(int control)
        {
            try
            {
                Process[] p = Process.GetProcessesByName("MACAddressChanger");
                IntPtr mainhan = p[0].MainWindowHandle;
                List<WindowScrape.Types.HwndObject> arr, arr1, arr2, arr3, arr4;

                arr = WindowScrape.Types.HwndObject.GetWindows();
                int mainInt = -1;

                for (int i = 0; i < arr.Count; i++)
                {
                    if (arr[i].Hwnd == (IntPtr)mainhan)
                    {
                        mainInt = i;
                        break;
                    }
                }

                arr1 = arr[mainInt].GetChildren();
               


                return arr1[control].Hwnd;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Restar service !!!");
                CTLError.WriteError("GetHandbyControlID ", ex.Message);
                return IntPtr.Zero;
            }
        }
        private void SetTextt(IntPtr hWnd, string text)
        {
            //IntPtr boxHwnd = GetDlgItem(hWnd, 114);
            SendMessage(hWnd, WM_SETTEXT, 0, text);
        }
        public void OKChange()
        {
            try
            {
                IntPtr zero = (IntPtr)FindWindow("#32770", "MAC Address Changer ");
                ////IntPtr hand = GetHandbyControlIDByHand(zero);
                SendMessage(FindWindowEx(zero, IntPtr.Zero, "Button", "OK"), 0xf5, IntPtr.Zero, IntPtr.Zero);
                   //InputSimulator.SimulateKeyPress((VirtualKeyCode)Keys.Enter);
            }
            catch (Exception ex)
            {
                CTLError.WriteError("OKChange ", ex.Message);
            }
        }
        public IntPtr GetHandbyControlIDByHand(IntPtr hand)
        {
            try
            {
                //Process[] p = Process.GetProcessesByName("MACAddressChanger");
                IntPtr mainhan = hand;
                List<WindowScrape.Types.HwndObject> arr, arr1, arr2, arr3, arr4;

                arr = WindowScrape.Types.HwndObject.GetWindows();
                int mainInt = -1;

                for (int i = 0; i < arr.Count; i++)
                {
                    if (arr[i].Hwnd == (IntPtr)mainhan)
                    {
                        mainInt = i;
                        break;
                    }
                }

                arr1 = arr[mainInt].GetChildren();



                return arr1[0].Hwnd;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Restar service !!!");
                CTLError.WriteError("GetHandbyControlID ", ex.Message);
                return IntPtr.Zero;
            }
        }
        public static class MacAddress
        {
            private static readonly Random Random = new Random();

            public static string GetSignatureRandomMac(string generic = "AA")
            {
                string[] macBytes = new[]
        {
            generic,
            generic,
            generic,
            Random.Next(1, 256).ToString("X"),
            Random.Next(1, 256).ToString("X"),
            Random.Next(1, 256).ToString("X")
        };

                return string.Join("-", macBytes);
            }

            public static string GetRandomMac()
            {
                string[] macBytes = new[]
        {
            Random.Next(1, 256).ToString("X"),
            Random.Next(1, 256).ToString("X"),
            Random.Next(1, 256).ToString("X"),
            Random.Next(1, 256).ToString("X"),
            Random.Next(1, 256).ToString("X"),
            Random.Next(1, 256).ToString("X")
        };

                return string.Join(" ", macBytes);
            }
        }
        public void ChangeOpenVpnConfig()
        {
            try
            {
                DateTime test = DateTime.Now;
                DateTime utcTime = test.ToUniversalTime(); // convert it to Utc using timezone setting of server computer
                if (utcTime.Hour != Convert.ToInt32("0" + Config._HourChange) && (utcTime.Hour == 12 || utcTime.Hour == 0)||_istestCopyConfig)
                {
                    if (utcTime.Hour == 12||_istestCopyConfig)
                    {
                        DirectoryInfo d = new DirectoryInfo(Path.Combine(Application.StartupPath, "a1"));
                        if (d.Exists)
                        {
                            DirectoryInfo doconfig = new DirectoryInfo(@"C:\Program Files\OpenVPN\config");
                            if (doconfig.Exists)
                            {
                                foreach (FileInfo f in doconfig.GetFiles())
                                {
                                    File.SetAttributes(f.FullName, FileAttributes.Normal);

                                    f.Delete();
                                }
                                foreach (FileInfo f in d.GetFiles())
                                {
                                    File.SetAttributes(f.FullName, FileAttributes.Normal);
                                    f.CopyTo(Path.Combine(doconfig.FullName, f.Name), true);
                                }
                            }
                        }
                    }
                    else if (utcTime.Hour == 0)
                    {
                        DirectoryInfo d = new DirectoryInfo(Path.Combine(Application.StartupPath, "a2"));
                        if (d.Exists)
                        {
                            DirectoryInfo doconfig = new DirectoryInfo(@"C:\Program Files\OpenVPN\config");
                            if (doconfig.Exists)
                            {
                                foreach (FileInfo f in doconfig.GetFiles())
                                    f.Delete();
                                foreach (FileInfo f in d.GetFiles())
                                {
                                    f.CopyTo(Path.Combine(doconfig.FullName, f.Name), true);
                                }
                            }
                        }
                    }
                    
                    Config._HourChange = utcTime.Hour.ToString();
                    Config.Setvalue("HourChange", utcTime.Hour.ToString());
                    RestartAPP();
                }
                _istestCopyConfig = false;
            }
            catch (Exception ex)
            {
                CTLError.WriteError("Loi ChangeOpenVpnConfig ", ex.Message);
            }
        }

        private void timerChangeConfigFile_Tick(object sender, EventArgs e)
        {
            ChangeOpenVpnConfig();
        }
        public void RestartAPP()
        {
            StartAPP(Path.Combine(Application.StartupPath,"OVPN-AuNEW.exe"));
            //Application.Restart();
            this.Close();
        }

        private void bttestcopy_Click(object sender, EventArgs e)
        {

            //test copy config
            try
            {
                _istestCopyConfig = true;

                ChangeOpenVpnConfig();

                _istestCopyConfig = false;
            }
            catch (Exception ex) { CTLError.WriteError("Loi testcopy config", ex.Message); }
        }

        bool _istestCopyConfig = false;
        
    }
}


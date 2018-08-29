namespace Socket_Test
{
    using System;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;

    internal class Config
    {
        public static int _index;
        public static string _Pass;
        public static string _PathConfigs;
        public static string _PathOVPN;
        public static int _TimeChange;
        public static string _user;
        public static string _charlesname;
        public static string _strRunFile;
        public static string _HourChange = "00";
        public static void GetConfig()
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(Application.StartupPath + @"\Config.xml");
                _PathOVPN = document.SelectSingleNode("//PathOVPN").Attributes["Value"].Value;
                _PathConfigs = document.SelectSingleNode("//PathConfigs").Attributes["Value"].Value;
                _user = document.SelectSingleNode("//User").Attributes["Value"].Value;
                _Pass = document.SelectSingleNode("//Pass").Attributes["Value"].Value;
                _index = Convert.ToInt32(document.SelectSingleNode("//Index").Attributes["Value"].Value);
                _TimeChange = Convert.ToInt32(document.SelectSingleNode("//TimeChange").Attributes["Value"].Value);
                try
                {
                    _strRunFile = document.SelectSingleNode("//RunFile").Attributes["Value"].Value;
                    _charlesname = document.SelectSingleNode("//charles").Attributes["Value"].Value;
                }
                catch (Exception ex)
                {
                    CTLError.WriteError("Loi GetConfig ", ex.Message);
                }
                _HourChange = document.SelectSingleNode("//HourChange").Attributes["Value"].Value;
            }
            catch (Exception exception)
            {
                CTLError.WriteError("loi Config GetDateUpdate ", exception.Message);
            }
        }

        public static void Setvalue(string KeyConfig, string value)
        {
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(Environment.CurrentDirectory + @"\Config.xml");
                document.SelectSingleNode("//" + KeyConfig).Attributes["Value"].Value = value;
                document.Save(Path.Combine(Application.StartupPath, "Config.xml"));
            }
            catch (Exception exception)
            {
                CTLError.WriteError("Loi Setvalue Config.....", exception.Message);
            }
        }

        //public static string GetRandomWifiMacAddress()
        //{
        //    var random = new Random();
        //    var buffer = new byte[6];
        //    random.NextBytes(buffer);
        //    buffer[0] = 02;
        //    var result = string.Concat(buffer.Select(x => string.Format("{0}", x.ToString("X2"))).ToArray());
        //    return result;
        //}
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

            return string.Join("-", macBytes);
        }

        //public static void Do
    }

}


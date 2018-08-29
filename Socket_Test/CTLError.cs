namespace Socket_Test
{
    using System;
    using System.IO;
    using System.Windows.Forms;

    internal class CTLError
    {
        public static string _path = Application.StartupPath;

        public static void RemoveLineFileText(string pathFile, int numberLine)
        {
            try
            {
                FileInfo info = new FileInfo(pathFile);
                if (info.Exists)
                {
                }
            }
            catch (Exception)
            {
            }
        }

        public static bool WriteError(string title, string ex)
        {
            try
            {
                FileStream stream;
                bool flag = false;
                FileInfo info = new FileInfo(Path.Combine(_path, "ErrorLog"));
                if (!info.Exists)
                {
                    flag = true;
                }
                if (flag)
                {
                    stream = new FileStream(info.FullName, FileMode.Create);
                }
                else
                {
                    stream = new FileStream(info.FullName, FileMode.Append);
                }
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine("");
                writer.WriteLine("");
                writer.WriteLine("");
                writer.WriteLine("");
                writer.WriteLine("-------------------------" + DateTime.Now + "---------------------------------");
                writer.WriteLine("C\x00f3 vấn đề sảy ra tại lớp " + title);
                writer.WriteLine(ex);
                writer.Dispose();
                writer.Close();
                stream.Dispose();
                stream.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool WriteError(string file_name, string Title, string ex)
        {
            try
            {
                FileInfo info = new FileInfo(Path.Combine(_path, file_name));
                if (!info.Exists)
                {
                    info.Create();
                }
                FileStream stream = new FileStream(info.FullName, FileMode.Append);
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine("");
                writer.WriteLine("");
                writer.WriteLine("");
                writer.WriteLine("");
                writer.WriteLine("-------------------------" + DateTime.Now + "---------------------------------");
                writer.WriteLine(Title);
                writer.WriteLine(ex);
                writer.Dispose();
                writer.Close();
                stream.Dispose();
                stream.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool WriteLogFileFin400(DateTime finCreate)
        {
            try
            {
                FileStream stream;
                StreamWriter writer;
                bool flag = false;
                FileInfo info = new FileInfo(Path.Combine(_path, "LogFile"));
                if (!info.Exists)
                {
                    flag = true;
                }
                if (flag)
                {
                    stream = new FileStream(info.FullName, FileMode.Create);
                    writer = new StreamWriter(stream);
                    writer.WriteLine("");
                    writer.WriteLine("");
                    writer.WriteLine("");
                    writer.WriteLine("");
                    writer.WriteLine("-------------------------" + DateTime.Now + "---------------------------------");
                    writer.WriteLine("File FIN.400 Create " + finCreate.ToString());
                    writer.Dispose();
                    writer.Close();
                    stream.Dispose();
                    stream.Close();
                }
                else
                {
                    stream = new FileStream(info.FullName, FileMode.Append);
                    writer = new StreamWriter(stream);
                    writer.WriteLine("");
                    writer.WriteLine("");
                    writer.WriteLine("");
                    writer.WriteLine("");
                    writer.WriteLine("-------------------------" + DateTime.Now + "---------------------------------");
                    writer.WriteLine("File FIN.400 Create " + finCreate.ToString());
                    writer.Dispose();
                    writer.Close();
                    stream.Dispose();
                    stream.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool WriteLogFileFin400(string str)
        {
            try
            {
                FileStream stream;
                StreamWriter writer;
                bool flag = false;
                FileInfo info = new FileInfo(Path.Combine(_path, "LogFile"));
                if (!info.Exists)
                {
                    flag = true;
                }
                if (flag)
                {
                    stream = new FileStream(info.FullName, FileMode.Create);
                    writer = new StreamWriter(stream);
                    writer.WriteLine("");
                    writer.WriteLine("");
                    writer.WriteLine("");
                    writer.WriteLine("");
                    writer.WriteLine("-------------------------" + DateTime.Now + "---------------------------------");
                    writer.WriteLine(str);
                    writer.Dispose();
                    writer.Close();
                    stream.Dispose();
                    stream.Close();
                }
                else
                {
                    stream = new FileStream(info.FullName, FileMode.Append);
                    writer = new StreamWriter(stream);
                    writer.WriteLine("");
                    writer.WriteLine("");
                    writer.WriteLine("");
                    writer.WriteLine("");
                    writer.WriteLine("-------------------------" + DateTime.Now + "---------------------------------");
                    writer.WriteLine(str);
                    writer.Dispose();
                    writer.Close();
                    stream.Dispose();
                    stream.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}


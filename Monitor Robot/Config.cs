using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Monitor_Robot
{
    public class Config
    {
        private string filePath;
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

        public Config(string filePath)
        {
            this.filePath = filePath;
        }
        public string Read(string Section, string Key)
        {
            StringBuilder tmp = new StringBuilder(255);
            long i = GetPrivateProfileString(Section, Key, "", tmp, 255, this.filePath);
            return tmp.ToString();
        }
        public void Write(string Section, string Key, string value)
        {
            WritePrivateProfileString(Section, Key, value, this.filePath);
        }
        public string FilePath
        {
            get { return this.filePath; }
            set { this.filePath = value; }
        }
    }
}

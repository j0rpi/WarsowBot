/*
 *   ============================
 * 
 *       j0rpi's Warsow Bot
 *      Multi-Functional Bot 
 *          For Warsow
 * 
 *   File: INIClass.cs
 *   Purpose: Class for INI configs
 *   Author: j0rpi
 *   
 *   ============================
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Runtime.InteropServices;

namespace INIClass
{
    class INIClass
    {
        public class INIConfigClass
        {

            public string path;

            [DllImport("kernel32")]
            private static extern long WritePrivateProfileString(string section,
                string key, string val, string filePath);
            [DllImport("kernel32")]
            private static extern int GetPrivateProfileString(string section,
                     string key, string def, StringBuilder retVal,
                int size, string filePath);

            public INIConfigClass(string INIPath)
            {
                path = INIPath;
            }

            public void WriteValue(string Section, string Key, string Value)
            {
                WritePrivateProfileString(Section, Key, Value, this.path);
            }

            public string ReadValue(string Section, string Key)
            {
                StringBuilder temp = new StringBuilder(255);
                int i = GetPrivateProfileString(Section, Key, "", temp,
                                                255, this.path);
                return temp.ToString();

            }
        }
    }
}

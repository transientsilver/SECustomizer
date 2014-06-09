using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;

namespace SECustomizer
{
    class FileUtility
    {
        static public string quickFind(string filename, string filerootpath = @"\Content\Data", string gamerootpath = @"C:\Program Files (x86)\Steam\SteamApps\common\SpaceEngineers")
        {
            var fullpath = gamerootpath + filerootpath + @"\" + filename;

            if (!File.Exists(fullpath))
            {
                fullpath = null;
                RegistryKey rk = Registry.LocalMachine;
                var autogamerootpath = rk.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 244850").GetValue("InstallLocation").ToString();
                fullpath = autogamerootpath + filerootpath + @"\" + filename;

                if (!File.Exists(fullpath))
                {
                    fullpath = null;

                    Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                    string df = filename.Split(new Char[] { '.' })[0];
                    string de = filename.Split(new Char[] { '.' })[1];
                    dlg.InitialDirectory = gamerootpath + filerootpath;
                    dlg.FileName = df;
                    dlg.DefaultExt = de;
                    dlg.Filter = "SE File |*." + de;

                    Nullable<bool> result = dlg.ShowDialog();

                    if (result == true)
                    {
                        fullpath = dlg.FileName;
                    }

                }
            }

            return fullpath;
        }
    }
}

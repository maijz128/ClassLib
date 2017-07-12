/*
 * 
 *  Auther: MaiJZ
 *  Date: 2017-07-13
 *  GitHub: http://github.com/maijz128/ClassLib
 * 
 */


namespace MaiJZ.ClassLib
{
    using Microsoft.Win32;
    using System;
    using System.Windows.Forms;

    public class AutoRunHelper
    {
        const string REGISTRY_KEY_NAME = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

        string _AppName;
        string _AppPath;

        public AutoRunHelper(string appName = null, string appPath = null)
        {
            _AppName = appName;
            _AppPath = appPath;

            if (_AppName == null)
            {
                _AppName = Application.ProductName;
            }
            if (_AppPath == null)
            {
                _AppPath = Application.ExecutablePath;
            }
        }

        public bool Disable()
        {
            return SetAutoRun(false);
        }

        public bool Enable()
        {
            return SetAutoRun(true);
        }

        public bool IsAutoRun()
        {
            return IsAutoRun(_AppName, _AppPath);
        }

        private bool SetAutoRun(bool isRun)
        {
            return RunWhenStart(isRun, _AppName, _AppPath);
        }

        private bool IsAutoRun(string appName, string appPath)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(REGISTRY_KEY_NAME, true);
            if (key == null) return false;

            object value = key.GetValue(appName);
            if (value == null) return false;

            return ((string)value == appPath);
        }

        private bool RunWhenStart(bool started, string appName, string appPath)
        {
            RegistryKey key = null;
            try
            {
                key = Registry.CurrentUser.OpenSubKey(REGISTRY_KEY_NAME, true);
                if (key == null)
                {
                    key = Registry.CurrentUser.CreateSubKey(REGISTRY_KEY_NAME);
                }
                if (started == true)
                {
                    key.SetValue(appName, appPath);
                }
                else
                {
                    key.DeleteValue(appName);
                }
            }
            catch (Exception exp)
            {
                Console.Error.WriteLine(exp.StackTrace);
                return false;
            }
            finally
            {
                if (key != null) key.Close();
            }

            return true;
        }

    }
}

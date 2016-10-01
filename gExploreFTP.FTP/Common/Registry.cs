using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gExploreFTP.Common
{
    public class Registry
    {

        public static int getPort()
        {

            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("gExploreFTP");
            int port = (int)key.GetValue("Port", 21);
            key.Close();

            return port;

        }

        public static void setPort(int port)
        {

            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("gExploreFTP");
            key.SetValue("Port", port);
            key.Close();

        }

        public static bool getStart()
        {

            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("gExploreFTP");
            string start = key.GetValue("Start", "True").ToString();
            key.Close();

            if (start == "True")
                return true;
            else
                return false;

        }

        public static void setStart(Boolean start)
        {

            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("gExploreFTP");
            key.SetValue("Start", start);
            key.Close();

        }

        public static string getLicenseKey()
        {

            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("gExploreFTP");
            string licenseKey = key.GetValue("LicenseKey", "nokey").ToString();
            key.Close();

            return licenseKey;

        }

        public static void setLicenseKey(string licenseKey)
        {

            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("gExploreFTP");
            key.SetValue("LicenseKey", licenseKey);
            key.Close();

        }

        public static string getVerificationKey()
        {

            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("gExploreFTP");
            string licenseKey = key.GetValue("VerificationKey", "nokey").ToString();
            key.Close();

            return licenseKey;

        }

        public static void setVerificationKey(string licenseKey)
        {

            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("gExploreFTP");
            key.SetValue("VerificationKey", licenseKey);
            key.Close();

        }

    }

}

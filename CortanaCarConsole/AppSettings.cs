using System.Configuration;

namespace CortanaCarConsole
{
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
    public static class AppSettings
    {
        public static string AutoStop
        {
            get { return ConfigurationManager.AppSettings["autoStop"]; }
        }

        public static string Port
        {
            get { return ConfigurationManager.AppSettings["port"]; }
        }

        public static string WatchFolder
        {
            get { return ConfigurationManager.AppSettings["watchFolder"]; }
        }
    }
}


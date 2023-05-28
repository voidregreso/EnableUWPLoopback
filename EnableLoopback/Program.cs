using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace EnableLoopback
{
	internal static class Program
	{
		[DllImport("user32.dll")]
		internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SetForegroundWindow(IntPtr hWnd);

		[STAThread]
		private static int Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(defaultValue: false);
			Version version = Environment.OSVersion.Version;
			if (version.Major < 6 || (version.Major == 6 && version.Minor < 2))
			{
				MessageBox.Show("This tool is only useful when running on Windows 8 or later.\n\nYou are running " + version.ToString(), "Unsupported Windows Version. Exiting...");
				return -1;
			}
			Mutex mutex;
			try
			{
				mutex = new Mutex(initiallyOwned: false, "EnableLoopbackTool");
			}
			catch (Exception ex)
			{
				MessageBox.Show("This tool appears to be running already.\r\n" + ex.Message, "Exiting...");
				return -2;
			}
			using (mutex)
			{
				if (!mutex.WaitOne(40))
				{
					IntPtr foregroundWindow = FindWindow(null, "AppContainer Loopback Exemption Utility");
					if (0 != foregroundWindow.ToInt64())
					{
						SetForegroundWindow(foregroundWindow);
						return -3;
					}
					MessageBox.Show("This tool appears to be running already, although I cannot switch to it.", "Exiting...");
					return -5;
				}
				Application.Run(new frmMain());
			}
			return 0;
		}
	}
}

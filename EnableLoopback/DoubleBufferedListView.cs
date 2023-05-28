using System.Diagnostics;
using System.Windows.Forms;

namespace EnableLoopback
{
	public class DoubleBufferedListView : ListView
	{
		public DoubleBufferedListView()
		{
			if (!base.DesignMode)
			{
				if (SystemInformation.TerminalServerSession)
				{
					Trace.WriteLine("Running Fiddler under Terminal Server. No double-buffering.");
				}
				else
				{
					DoubleBuffered = true;
				}
			}
		}
	}
}

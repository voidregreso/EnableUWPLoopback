using System;
using System.Runtime.InteropServices;

namespace IsolationAPI
{
	internal struct INET_FIREWALL_APP_CONTAINER
	{
		internal IntPtr appContainerSid;

		internal IntPtr userSid;

		[MarshalAs(UnmanagedType.LPWStr)]
		internal string appContainerName;

		[MarshalAs(UnmanagedType.LPWStr)]
		internal string displayName;

		[MarshalAs(UnmanagedType.LPWStr)]
		internal string description;

		internal INET_FIREWALL_AC_CAPABILITIES capabilities;

		internal INET_FIREWALL_AC_BINARIES binaries;

		[MarshalAs(UnmanagedType.LPWStr)]
		internal string workingDirectory;

		[MarshalAs(UnmanagedType.LPWStr)]
		internal string packageFullName;
	}
}

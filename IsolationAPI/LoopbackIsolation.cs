using System;
using System.Runtime.InteropServices;
using System.Text;

namespace IsolationAPI
{
	internal class LoopbackIsolation
	{
		private class NativeMethods
		{
			private NativeMethods()
			{
			}

			[DllImport("advapi32.dll", SetLastError = true)]
			internal static extern bool ConvertStringSidToSid(string StringSid, out IntPtr ptrSid);

			[DllImport("FirewallAPI.dll", EntryPoint = "NetworkIsolationEnumAppContainers")]
			internal static extern uint NetworkIsolationEnumAppContainers_BUILD(out uint pdwCntPublicACs, out IntPtr ppPublicACs);

			[DllImport("FirewallAPI.dll")]
			internal static extern uint NetworkIsolationEnumAppContainers(uint Flags, out uint pdwCntPublicACs, out IntPtr ppPublicACs);

			[DllImport("FirewallAPI.dll")]
			internal static extern void NetworkIsolationFreeAppContainers(IntPtr pACs);

			[DllImport("FirewallAPI.dll")]
			internal static extern uint NetworkIsolationGetAppContainerConfig(out uint pdwCntACs, out IntPtr appContainerSids);

			[DllImport("FirewallAPI.dll")]
			internal static extern uint NetworkIsolationSetAppContainerConfig(uint pdwCntACs, SID_AND_ATTRIBUTES[] appContainerSids);

			[DllImport("shlwapi.dll", BestFitMapping = false, CharSet = CharSet.Unicode, ExactSpelling = true, ThrowOnUnmappableChar = true)]
			internal static extern int SHLoadIndirectString(string pszSource, StringBuilder pszOutBuf, int cchOutBuf, IntPtr ppvReserved);
		}

		private LoopbackIsolation()
		{
		}

		internal static string LoadIndirectString(string sSource)
		{
			if (string.IsNullOrEmpty(sSource) || !sSource.StartsWith("@"))
			{
				return sSource;
			}
			StringBuilder stringBuilder = new StringBuilder(1024);
			if (NativeMethods.SHLoadIndirectString(sSource, stringBuilder, stringBuilder.Capacity, IntPtr.Zero) == 0)
			{
				return stringBuilder.ToString();
			}
			return sSource;
		}

		internal static bool ConvertStringSidToSid(string sSID, out IntPtr ptrSid)
		{
			return NativeMethods.ConvertStringSidToSid(sSID, out ptrSid);
		}

		internal static uint NetIsoGetAppContainerConfig(out uint pdwCntACs, out IntPtr appContainerSIDS)
		{
			return NativeMethods.NetworkIsolationGetAppContainerConfig(out pdwCntACs, out appContainerSIDS);
		}

		internal static uint NetIsoSetAppContainerConfig(uint pdwCntACs, SID_AND_ATTRIBUTES[] appContainerSIDS)
		{
			return NativeMethods.NetworkIsolationSetAppContainerConfig(pdwCntACs, appContainerSIDS);
		}

		internal static uint NetIsoEnumAppContainers(out uint pdwCntACs, out IntPtr ppACs, bool bUseBetaAndLaterEnumFunction, bool bForceBinaryNames)
		{
			if (bUseBetaAndLaterEnumFunction)
			{
				return NativeMethods.NetworkIsolationEnumAppContainers((uint)(bForceBinaryNames ? 1 : 0), out pdwCntACs, out ppACs);
			}
			return NativeMethods.NetworkIsolationEnumAppContainers_BUILD(out pdwCntACs, out ppACs);
		}

		internal static void NetIsoFreeAppContainers(IntPtr pNetworks)
		{
			NativeMethods.NetworkIsolationFreeAppContainers(pNetworks);
		}
	}
}

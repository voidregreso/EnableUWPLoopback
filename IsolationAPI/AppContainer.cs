using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;

namespace IsolationAPI
{
	internal class AppContainer
	{
		private static string sThisMachine = Environment.MachineName + "\\";

		private SecurityIdentifier appContainerSid;

		private SecurityIdentifier userSid;

		private List<string> listUsernames = new List<string>();

		private bool bWasLoopbackExemptAtLastCheck;

		private string appContainerName;

		private string displayName;

		private string description;

		private string packageFullName;

		private string[] binaries;

		private static bool bWarnOnUnmatchedExemptions = true;

		private IntPtr appContainerRawSid = IntPtr.Zero;

		internal bool WasLoopbackExemptAtLastCheck
		{
			get
			{
				return bWasLoopbackExemptAtLastCheck;
			}
			set
			{
				bWasLoopbackExemptAtLastCheck = value;
			}
		}

		internal IntPtr AppContainerRawSid => appContainerRawSid;

		internal string PackageFullName => packageFullName ?? "(No Package Name)";

		internal string AppContainerSid
		{
			get
			{
				if (null != appContainerSid)
				{
					return appContainerSid.ToString();
				}
				return "(No AC SID)";
			}
		}

		internal string AppContainerName => appContainerName ?? "(No AC Name)";

		internal string DisplayName => displayName ?? "(No DisplayName)";

		internal string Description => description ?? "(No Description)";

		internal string GetBinaryList()
		{
			if (binaries == null || binaries.Length < 1)
			{
				return "(None)";
			}
			return string.Join("; ", binaries);
		}

		internal string GetUsers()
		{
			if (listUsernames.Count < 1)
			{
				return "(No User SID)";
			}
			return string.Join("; ", listUsernames);
		}

		internal AppContainer(INET_FIREWALL_APP_CONTAINER_PREVIEWBUILD ifacNative)
		{
			appContainerRawSid = ifacNative.appContainerSid;
			try
			{
				appContainerSid = new SecurityIdentifier(appContainerRawSid);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to parse AppContainerSID\n\n" + ex.Message, "Unexpected");
				appContainerSid = null;
			}
			try
			{
				userSid = new SecurityIdentifier(ifacNative.userSid);
				AddUser(userSid);
			}
			catch (Exception ex2)
			{
				MessageBox.Show("Failed to parse userSID\n\n" + ex2.Message, "Unexpected");
				userSid = null;
			}
			appContainerName = ifacNative.appContainerName;
			displayName = LoopbackIsolation.LoadIndirectString(ifacNative.displayName);
			description = LoopbackIsolation.LoadIndirectString(ifacNative.description);
			packageFullName = "Unknown";
			INET_FIREWALL_AC_BINARIES iNET_FIREWALL_AC_BINARIES = ifacNative.binaries;
			if (iNET_FIREWALL_AC_BINARIES.count != 0 && IntPtr.Zero != iNET_FIREWALL_AC_BINARIES.binaries)
			{
				binaries = new string[iNET_FIREWALL_AC_BINARIES.count];
				long num = iNET_FIREWALL_AC_BINARIES.binaries.ToInt64();
				for (int i = 0; i < iNET_FIREWALL_AC_BINARIES.count; i++)
				{
					binaries[i] = Marshal.PtrToStringUni(Marshal.ReadIntPtr((IntPtr)num));
					num += IntPtr.Size;
				}
			}
		}

		internal AppContainer(INET_FIREWALL_APP_CONTAINER ifacNative)
		{
			appContainerRawSid = ifacNative.appContainerSid;
			try
			{
				appContainerSid = new SecurityIdentifier(appContainerRawSid);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to parse AppContainerSID\n\n" + ex.Message, "Unexpected");
				appContainerSid = null;
			}
			try
			{
				userSid = new SecurityIdentifier(ifacNative.userSid);
				AddUser(userSid);
			}
			catch (Exception ex2)
			{
				MessageBox.Show("Failed to parse userSID\n\n" + ex2.Message, "Unexpected");
				userSid = null;
			}
			appContainerName = ifacNative.appContainerName;
			displayName = LoopbackIsolation.LoadIndirectString(ifacNative.displayName);
			description = LoopbackIsolation.LoadIndirectString(ifacNative.description);
			packageFullName = ifacNative.packageFullName;
			INET_FIREWALL_AC_BINARIES iNET_FIREWALL_AC_BINARIES = ifacNative.binaries;
			if (iNET_FIREWALL_AC_BINARIES.count != 0 && IntPtr.Zero != iNET_FIREWALL_AC_BINARIES.binaries)
			{
				binaries = new string[iNET_FIREWALL_AC_BINARIES.count];
				long num = iNET_FIREWALL_AC_BINARIES.binaries.ToInt64();
				for (int i = 0; i < iNET_FIREWALL_AC_BINARIES.count; i++)
				{
					binaries[i] = Marshal.PtrToStringUni(Marshal.ReadIntPtr((IntPtr)num));
					num += IntPtr.Size;
				}
			}
		}

		private void AddUser(SecurityIdentifier oSID)
		{
			if (null != userSid)
			{
				string text;
				try
				{
					text = ((NTAccount)userSid.Translate(typeof(NTAccount))).ToString();
					if (text.StartsWith(sThisMachine))
					{
						text = text.Substring(sThisMachine.Length);
					}
				}
				catch (Exception)
				{
					text = oSID.Value;
				}
				AddUser(text);
			}
			else
			{
				AddUser("<null>");
			}
		}

		private void AddUser(string sNewUser)
		{
			listUsernames.Add(sNewUser);
			listUsernames.Sort();
		}

		internal static void ExemptFromLoopbackBlocking(AppContainer[] oACToExempt)
		{
			uint num = (uint)oACToExempt.Length;
			SID_AND_ATTRIBUTES[] array = new SID_AND_ATTRIBUTES[num];
			int num2 = 0;
			foreach (AppContainer appContainer in oACToExempt)
			{
				array[num2] = default(SID_AND_ATTRIBUTES);
				array[num2].Attributes = 0u;
				array[num2].Sid = appContainer.appContainerRawSid;
				num2++;
			}
			uint num3 = LoopbackIsolation.NetIsoSetAppContainerConfig(num, array);
			switch (num3)
			{
			case 0u:
				break;
			case 5u:
				throw new UnauthorizedAccessException();
			default:
				throw new Exception($"Failed to set IsolationExempt AppContainers; call returned 0x{num3:X}\r\n");
			}
		}

		internal static void MarkLoopbackExemptAppContainers(AppContainer[] oACs)
		{
			foreach (AppContainer appContainer in oACs)
			{
				appContainer.bWasLoopbackExemptAtLastCheck = false;
			}
			IntPtr appContainerSIDS = IntPtr.Zero;
			uint pdwCntACs;
			uint num = LoopbackIsolation.NetIsoGetAppContainerConfig(out pdwCntACs, out appContainerSIDS);
			if (num != 0)
			{
				throw new Exception($"Failed to get IsolationExempt AppContainers; call returned 0x{num:X}\r\n");
			}
			try
			{
				if (pdwCntACs != 0)
				{
					long num2 = appContainerSIDS.ToInt64();
					for (int j = 0; j < pdwCntACs; j++)
					{
						SID_AND_ATTRIBUTES sID_AND_ATTRIBUTES = (SID_AND_ATTRIBUTES)Marshal.PtrToStructure((IntPtr)num2, typeof(SID_AND_ATTRIBUTES));
						bool flag = false;
						SecurityIdentifier securityIdentifier;
						try
						{
							securityIdentifier = new SecurityIdentifier(sID_AND_ATTRIBUTES.Sid);
						}
						catch (Exception ex)
						{
							MessageBox.Show("Unable to convert LoopbackExempt SID to SecurityIdentifier\n\n" + ex.Message, "Unexpected");
							continue;
						}
						foreach (AppContainer appContainer2 in oACs)
						{
							if (appContainer2.appContainerSid == securityIdentifier)
							{
								appContainer2.bWasLoopbackExemptAtLastCheck = true;
								flag = true;
								break;
							}
						}
						if (!flag && bWarnOnUnmatchedExemptions && DialogResult.No == MessageBox.Show($"A Loopback exemption exists for SID:\n\n{securityIdentifier}\n\n...but no AppContainer with that SID could be found.\n\nWarn if other orphaned exemptions are found in this session?", "Orphaned Exemption Record Found", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2))
						{
							bWarnOnUnmatchedExemptions = false;
						}
						num2 += Marshal.SizeOf(typeof(SID_AND_ATTRIBUTES));
					}
				}
			}
			finally
			{
				if (appContainerSIDS != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(appContainerSIDS);
				}
			}
		}

		internal static AppContainer[] GetAppContainers(bool bUseDevPreviewBuildMemoryLayout, bool bUseBetaEnumFunction, bool bForceBinaryNames)
		{
			Dictionary<string, AppContainer> dictionary = null;
			if (LoopbackIsolation.NetIsoEnumAppContainers(out uint pdwCntACs, out IntPtr ppACs, bUseBetaEnumFunction, bForceBinaryNames) == 0)
			{
				try
				{
					if (pdwCntACs != 0)
					{
						dictionary = new Dictionary<string, AppContainer>();
						long num = ppACs.ToInt64();
						for (int i = 0; i < pdwCntACs; i++)
						{
							if (bUseDevPreviewBuildMemoryLayout)
							{
								INET_FIREWALL_APP_CONTAINER_PREVIEWBUILD ifacNative = (INET_FIREWALL_APP_CONTAINER_PREVIEWBUILD)Marshal.PtrToStructure((IntPtr)num, typeof(INET_FIREWALL_APP_CONTAINER_PREVIEWBUILD));
								AppContainer appContainer = new AppContainer(ifacNative);
								if (dictionary.ContainsKey(appContainer.AppContainerSid))
								{
									dictionary[appContainer.AppContainerSid].AddUser(appContainer.GetUsers());
								}
								else
								{
									dictionary.Add(appContainer.AppContainerSid, appContainer);
								}
								num += Marshal.SizeOf(typeof(INET_FIREWALL_APP_CONTAINER_PREVIEWBUILD));
							}
							else
							{
								INET_FIREWALL_APP_CONTAINER ifacNative2 = (INET_FIREWALL_APP_CONTAINER)Marshal.PtrToStructure((IntPtr)num, typeof(INET_FIREWALL_APP_CONTAINER));
								AppContainer appContainer2 = new AppContainer(ifacNative2);
								if (dictionary.ContainsKey(appContainer2.AppContainerSid))
								{
									dictionary[appContainer2.AppContainerSid].AddUser(appContainer2.GetUsers());
								}
								else
								{
									dictionary.Add(appContainer2.AppContainerSid, appContainer2);
								}
								num += Marshal.SizeOf(typeof(INET_FIREWALL_APP_CONTAINER));
							}
						}
					}
				}
				finally
				{
					LoopbackIsolation.NetIsoFreeAppContainers(ppACs);
				}
			}
			if (dictionary == null)
			{
				return null;
			}
			AppContainer[] array = new AppContainer[dictionary.Count];
			dictionary.Values.CopyTo(array, 0);
			return array;
		}
	}
}

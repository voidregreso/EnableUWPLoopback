using EnableLoopback.Properties;
using IsolationAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace EnableLoopback
{
	public class frmMain : Form
	{
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		private class LVHITTESTINFO
		{
			public int pt_x;

			public int pt_y;

			public int flags;

			public int iItem;

			public int iSubItem;
		}

		private const int LVM_SUBITEMHITTEST = 4153;

		private IContainer components;

		private StatusStrip statusStrip1;

		private ToolStripStatusLabel tsslStatus;

		private ContextMenuStrip mnuContext;

		private ToolStripMenuItem tsmiCheckAll;

		private Button btnSave;

		private Button btnRefresh;

		private Button btnExemptNone;

		private DoubleBufferedListView lvAppContainers = new DoubleBufferedListView();

		private ColumnHeader chDisplayName;

		private ColumnHeader chDescription;

		private ColumnHeader chPackageFullName;

		private ColumnHeader chACName;

		private ColumnHeader chACSID;

		private ColumnHeader chUserSID;

		private Button btnExemptAll;

		private Label lblExplainText;

		private ToolStripMenuItem tsmiUncheckAll;

		private ToolStripMenuItem tsmiCopyRows;

		private ToolStripMenuItem tsmiCopyColumn;

		private ToolStripSeparator toolStripMenuItem2;

		private ColumnHeader chBinaries;

		private LinkLabel lnkLearn;

		private bool bIsDevPreviewBuild;

		private bool bIsWin8Beta;

		private bool bIsWin8RTM;

		private Point _ptContextPopup;

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCopyRows = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiUncheckAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCheckAll = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExemptNone = new System.Windows.Forms.Button();
            this.btnExemptAll = new System.Windows.Forms.Button();
            this.lblExplainText = new System.Windows.Forms.Label();
            this.lnkLearn = new System.Windows.Forms.LinkLabel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lvAppContainers = new EnableLoopback.DoubleBufferedListView();
            this.chDisplayName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPackageFullName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chACName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chACSID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUserSID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chBinaries = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip1.SuspendLayout();
            this.mnuContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 319);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(761, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslStatus
            // 
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(207, 17);
            this.tsslStatus.Text = "©2012 Telerik. All rights reserved.";
            // 
            // mnuContext
            // 
            this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopyRows,
            this.tsmiCopyColumn,
            this.toolStripMenuItem2,
            this.tsmiUncheckAll,
            this.tsmiCheckAll});
            this.mnuContext.Name = "mnuContext";
            this.mnuContext.Size = new System.Drawing.Size(191, 98);
            this.mnuContext.Opening += new System.ComponentModel.CancelEventHandler(this.mnuContext_Opening);
            // 
            // tsmiCopyRows
            // 
            this.tsmiCopyRows.Name = "tsmiCopyRows";
            this.tsmiCopyRows.Size = new System.Drawing.Size(190, 22);
            this.tsmiCopyRows.Text = "&Copy selected rows";
            this.tsmiCopyRows.Click += new System.EventHandler(this.tsmiCopyRows_Click);
            // 
            // tsmiCopyColumn
            // 
            this.tsmiCopyColumn.Name = "tsmiCopyColumn";
            this.tsmiCopyColumn.Size = new System.Drawing.Size(190, 22);
            this.tsmiCopyColumn.Text = "Copy &this column";
            this.tsmiCopyColumn.Click += new System.EventHandler(this.tsmiCopyColumn_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(187, 6);
            // 
            // tsmiUncheckAll
            // 
            this.tsmiUncheckAll.Name = "tsmiUncheckAll";
            this.tsmiUncheckAll.Size = new System.Drawing.Size(190, 22);
            this.tsmiUncheckAll.Text = "Exempt &None";
            this.tsmiUncheckAll.Click += new System.EventHandler(this.tsmiUncheckAll_Click);
            // 
            // tsmiCheckAll
            // 
            this.tsmiCheckAll.Name = "tsmiCheckAll";
            this.tsmiCheckAll.Size = new System.Drawing.Size(190, 22);
            this.tsmiCheckAll.Text = "Exempt &All";
            this.tsmiCheckAll.Click += new System.EventHandler(this.btnExemptAll_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSave.Location = new System.Drawing.Point(371, 42);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(103, 31);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "&Save Changes";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExemptNone
            // 
            this.btnExemptNone.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnExemptNone.Location = new System.Drawing.Point(269, 42);
            this.btnExemptNone.Name = "btnExemptNone";
            this.btnExemptNone.Size = new System.Drawing.Size(96, 31);
            this.btnExemptNone.TabIndex = 2;
            this.btnExemptNone.Text = "Exempt &None";
            this.btnExemptNone.UseVisualStyleBackColor = true;
            this.btnExemptNone.Click += new System.EventHandler(this.btnExemptNone_Click);
            // 
            // btnExemptAll
            // 
            this.btnExemptAll.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnExemptAll.Location = new System.Drawing.Point(186, 42);
            this.btnExemptAll.Name = "btnExemptAll";
            this.btnExemptAll.Size = new System.Drawing.Size(77, 31);
            this.btnExemptAll.TabIndex = 1;
            this.btnExemptAll.Text = "Exempt &All";
            this.btnExemptAll.UseVisualStyleBackColor = true;
            this.btnExemptAll.Click += new System.EventHandler(this.btnExemptAll_Click);
            // 
            // lblExplainText
            // 
            this.lblExplainText.AutoEllipsis = true;
            this.lblExplainText.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblExplainText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExplainText.Location = new System.Drawing.Point(0, 0);
            this.lblExplainText.Name = "lblExplainText";
            this.lblExplainText.Padding = new System.Windows.Forms.Padding(5);
            this.lblExplainText.Size = new System.Drawing.Size(761, 50);
            this.lblExplainText.TabIndex = 0;
            this.lblExplainText.Text = "For security and reliability reasons, Windows 8 blocks apps from sending network " +
    "traffic to the local computer. This utility enables removal of this restriction " +
    "for debugging purposes.";
            this.lblExplainText.UseMnemonic = false;
            // 
            // lnkLearn
            // 
            this.lnkLearn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkLearn.AutoSize = true;
            this.lnkLearn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lnkLearn.LinkArea = new System.Windows.Forms.LinkArea(0, 14);
            this.lnkLearn.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkLearn.Location = new System.Drawing.Point(670, 50);
            this.lnkLearn.Name = "lnkLearn";
            this.lnkLearn.Size = new System.Drawing.Size(76, 15);
            this.lnkLearn.TabIndex = 5;
            this.lnkLearn.TabStop = true;
            this.lnkLearn.Text = "&Learn more...";
            this.lnkLearn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLearn_LinkClicked);
            this.lnkLearn.MouseHover += new System.EventHandler(this.lnkLearn_MouseHover);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRefresh.Image = global::EnableLoopback.Properties.Resources.EnableLoopbackRefresh;
            this.btnRefresh.Location = new System.Drawing.Point(8, 42);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(80, 31);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lvAppContainers
            // 
            this.lvAppContainers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvAppContainers.BackgroundImageTiled = true;
            this.lvAppContainers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvAppContainers.CheckBoxes = true;
            this.lvAppContainers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDisplayName,
            this.chDescription,
            this.chPackageFullName,
            this.chACName,
            this.chACSID,
            this.chUserSID,
            this.chBinaries});
            this.lvAppContainers.ContextMenuStrip = this.mnuContext;
            this.lvAppContainers.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lvAppContainers.FullRowSelect = true;
            this.lvAppContainers.GridLines = true;
            this.lvAppContainers.HideSelection = false;
            this.lvAppContainers.Location = new System.Drawing.Point(0, 79);
            this.lvAppContainers.Name = "lvAppContainers";
            this.lvAppContainers.Size = new System.Drawing.Size(761, 237);
            this.lvAppContainers.TabIndex = 6;
            this.lvAppContainers.UseCompatibleStateImageBehavior = false;
            this.lvAppContainers.View = System.Windows.Forms.View.Details;
            this.lvAppContainers.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvAppContainers_ColumnClick);
            this.lvAppContainers.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvAppContainers_KeyDown);
            // 
            // chDisplayName
            // 
            this.chDisplayName.Text = "DisplayName";
            this.chDisplayName.Width = 143;
            // 
            // chDescription
            // 
            this.chDescription.Text = "Description";
            this.chDescription.Width = 120;
            // 
            // chPackageFullName
            // 
            this.chPackageFullName.Text = "Package";
            // 
            // chACName
            // 
            this.chACName.Text = "AC Name";
            this.chACName.Width = 65;
            // 
            // chACSID
            // 
            this.chACSID.Text = "AC SID";
            this.chACSID.Width = 99;
            // 
            // chUserSID
            // 
            this.chUserSID.Text = "AC User(s)";
            this.chUserSID.Width = 118;
            // 
            // chBinaries
            // 
            this.chBinaries.Text = "Binaries";
            this.chBinaries.Width = 150;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 341);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnExemptNone);
            this.Controls.Add(this.lvAppContainers);
            this.Controls.Add(this.btnExemptAll);
            this.Controls.Add(this.lblExplainText);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lnkLearn);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(516, 270);
            this.Name = "frmMain";
            this.Text = "AppContainer Loopback Exemption Utility";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyUp);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.mnuContext.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		public frmMain()
		{
			Version version = Environment.OSVersion.Version;
			if (version.Major == 6 && version.Minor == 2 && version.Build < 8140)
			{
				bIsDevPreviewBuild = true;
			}
			else
			{
				if (version.Major == 6 && version.Minor == 2 && version.Build > 8186)
				{
					bIsWin8Beta = true;
				}
				if (version.Major == 6 && version.Minor == 2 && version.Build > 9199)
				{
					bIsWin8RTM = true;
				}
			}
			InitializeComponent();
			lvAppContainers.ListViewItemSorter = new ListViewItemComparer();
		}

		private void actSelectAll()
		{
			lvAppContainers.BeginUpdate();
			foreach (ListViewItem item in lvAppContainers.Items)
			{
				item.Selected = true;
			}
			lvAppContainers.EndUpdate();
		}

		private void actCopyRows()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = lvAppContainers.SelectedItems.Count > 1;
			foreach (ListViewItem selectedItem in lvAppContainers.SelectedItems)
			{
				stringBuilder.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}{8}", selectedItem.Checked ? "LoopbackExempt" : "LoopbackBlocked", selectedItem.Text, selectedItem.SubItems[1].Text, selectedItem.SubItems[2].Text, selectedItem.SubItems[3].Text, selectedItem.SubItems[4].Text, selectedItem.SubItems[5].Text, selectedItem.SubItems[6].Text, flag ? "\r\n" : string.Empty);
			}
			if (stringBuilder.Length > 0)
			{
				Clipboard.SetText(stringBuilder.ToString());
			}
		}

		private void actUncheckAll()
		{
			lvAppContainers.BeginUpdate();
			foreach (ListViewItem item in lvAppContainers.Items)
			{
				item.Checked = false;
			}
			lvAppContainers.EndUpdate();
			Application.DoEvents();
			if (btnSave.Enabled)
			{
				tsslStatus.Text = "Loopback Exemption pending removal for all AppContainers. Click Save Changes to commit updates.";
			}
		}

		private void actCheckAll()
		{
			lvAppContainers.BeginUpdate();
			foreach (ListViewItem item in lvAppContainers.Items)
			{
				item.Checked = true;
			}
			lvAppContainers.EndUpdate();
			Application.DoEvents();
			if (btnSave.Enabled)
			{
				tsslStatus.Text = "Loopback Exemption pending for all AppContainers. Click Save Changes to commit updates.";
			}
		}

		private void actRefresh(bool bForceBinaryNames)
		{
			Cursor = Cursors.WaitCursor;
			lvAppContainers.Items.Clear();
			tsslStatus.Text = "Refreshing AppContainer information...";
			DoubleBufferedListView doubleBufferedListView = lvAppContainers;
			Button button = btnExemptAll;
			Button button2 = btnExemptNone;
			Button button3 = btnRefresh;
			bool flag2 = btnSave.Enabled = false;
			bool flag4 = button3.Enabled = flag2;
			bool flag6 = button2.Enabled = flag4;
			bool enabled = button.Enabled = flag6;
			doubleBufferedListView.Enabled = enabled;
			lvAppContainers.ItemChecked -= lvAppContainers_ItemChecked;
			Application.DoEvents();
			BackgroundWorker backgroundWorker = new BackgroundWorker();
			backgroundWorker.RunWorkerCompleted += oBW_RunWorkerCompleted;
			backgroundWorker.DoWork += oBW_DoWork;
			backgroundWorker.RunWorkerAsync(bForceBinaryNames);
		}

		private void oBW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (e.Result is Exception)
				{
					tsslStatus.Text = $"Failed to get AppContainer info: {(e.Result as Exception).Message}";
				}
				else
				{
					AppContainer[] array = (AppContainer[])e.Result;
					string value = Environment.UserDomainName + "\\" + Environment.UserName;
					List<ListViewItem> list = new List<ListViewItem>();
					AppContainer[] array2 = array;
					foreach (AppContainer appContainer in array2)
					{
						ListViewItem listViewItem = new ListViewItem(appContainer.DisplayName);
						string users = appContainer.GetUsers();
						if (users.Contains(value))
						{
							listViewItem.BackColor = Color.LightCyan;
						}
						list.Add(listViewItem);
						listViewItem.SubItems.AddRange(new string[6]
						{
							appContainer.Description,
							appContainer.PackageFullName,
							appContainer.AppContainerName,
							appContainer.AppContainerSid,
							users,
							appContainer.GetBinaryList()
						});
						listViewItem.Tag = appContainer;
						listViewItem.Checked = appContainer.WasLoopbackExemptAtLastCheck;
					}
					lvAppContainers.Items.AddRange(list.ToArray());
					tsslStatus.Text = $"Refreshed AppContainer information at {DateTime.Now.ToLongTimeString()}.";
				}
			}
			finally
			{
				Cursor = Cursors.Default;
				DoubleBufferedListView doubleBufferedListView = lvAppContainers;
				Button button = btnExemptAll;
				bool flag2 = btnExemptNone.Enabled = (lvAppContainers.Items.Count > 0);
				bool enabled = button.Enabled = flag2;
				doubleBufferedListView.Enabled = enabled;
				if (lvAppContainers.Enabled)
				{
					lvAppContainers.Focus();
				}
				btnRefresh.Enabled = true;
				lvAppContainers.ItemChecked += lvAppContainers_ItemChecked;
			}
		}

		private void oBW_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				AppContainer[] appContainers = AppContainer.GetAppContainers(bIsDevPreviewBuild, bIsWin8Beta, (bool)e.Argument);
				if (appContainers == null)
				{
					throw new Exception("Unable to enumerate AppContainers. Is the Windows Firewall Service started?");
				}
				AppContainer.MarkLoopbackExemptAppContainers(appContainers);
				e.Result = appContainers;
			}
			catch (Exception ex)
			{
				Exception ex3 = (Exception)(e.Result = ex);
			}
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			if (!btnSave.Enabled || DialogResult.No != MessageBox.Show("You have not saved your changes. Would you like to refresh anyway?", "Discard changes?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
			{
				actRefresh(bForceBinaryNames: true);
			}
		}

		private void lvAppContainers_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			btnSave.Enabled = IsListViewDirty();
			if (btnSave.Enabled)
			{
				tsslStatus.Text = "Click Save Changes to commit updates.";
			}
			else
			{
				tsslStatus.Text = string.Empty;
			}
		}

		private bool IsListViewDirty()
		{
			foreach (ListViewItem item in lvAppContainers.Items)
			{
				if (item.Checked != (item.Tag as AppContainer).WasLoopbackExemptAtLastCheck)
				{
					return true;
				}
			}
			return false;
		}

		private void btnExemptNone_Click(object sender, EventArgs e)
		{
			actUncheckAll();
		}

		private void btnExemptAll_Click(object sender, EventArgs e)
		{
			actCheckAll();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			btnSave.Enabled = false;
			try
			{
				List<AppContainer> list = new List<AppContainer>();
				foreach (ListViewItem checkedItem in lvAppContainers.CheckedItems)
				{
					list.Add(checkedItem.Tag as AppContainer);
				}
				AppContainer.ExemptFromLoopbackBlocking(list.ToArray());
				tsslStatus.Text = "Loopback exemption configuration successfully updated.";
			}
			catch (UnauthorizedAccessException)
			{
				MessageBox.Show("You must run this tool elevated to change AppContainer configurations.", "Changes not saved");
				tsslStatus.Text = "Changes not saved. Tool must be run by an Administrator.";
			}
			catch (Exception ex2)
			{
				MessageBox.Show(ex2.Message, "Failed to save settings.");
				tsslStatus.Text = "Changes not saved.";
			}
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			try
			{
				if (!Environment.CommandLine.Contains("manual"))
				{
					actRefresh(bForceBinaryNames: false);
				}
			}
			catch (Exception ex)
			{
				tsslStatus.Text = "Failed to collect list of AppContainers";
				MessageBox.Show("Failed to collect list of AppContainers.\r\n" + ex.Message, "Unexpected");
			}
		}

		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (btnSave.Enabled && DialogResult.No == MessageBox.Show("You have not saved your changes. Would you like to exit anyway?", "Exit without saving?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
			{
				e.Cancel = true;
			}
		}

		private void tsmiUncheckAll_Click(object sender, EventArgs e)
		{
			actUncheckAll();
		}

		private void tsmiCopyRows_Click(object sender, EventArgs e)
		{
			actCopyRows();
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SendMessage")]
		private static extern IntPtr SendHitTestMessage(HandleRef hWnd, int msg, int wParam, LVHITTESTINFO lParam);

		public int GetSubItemIndexFromPoint(Point ptClient)
		{
			LVHITTESTINFO lVHITTESTINFO = new LVHITTESTINFO();
			lVHITTESTINFO.pt_x = ptClient.X;
			lVHITTESTINFO.pt_y = ptClient.Y;
			if (-1 < (int)SendHitTestMessage(new HandleRef(lvAppContainers, lvAppContainers.Handle), 4153, 0, lVHITTESTINFO))
			{
				return lVHITTESTINFO.iSubItem;
			}
			return -1;
		}

		private void tsmiCopyColumn_Click(object sender, EventArgs e)
		{
			try
			{
				ListView.SelectedListViewItemCollection selectedItems = lvAppContainers.SelectedItems;
				if (selectedItems.Count >= 1)
				{
					int subItemIndexFromPoint = GetSubItemIndexFromPoint(lvAppContainers.PointToClient(_ptContextPopup));
					if (subItemIndexFromPoint >= 0)
					{
						StringBuilder stringBuilder = new StringBuilder(512);
						foreach (ListViewItem item in selectedItems)
						{
							string value = (item.SubItems.Count > subItemIndexFromPoint) ? item.SubItems[subItemIndexFromPoint].Text : string.Empty;
							stringBuilder.AppendLine(value);
						}
						if (stringBuilder.Length > 0)
						{
							Clipboard.SetText(stringBuilder.ToString());
						}
					}
				}
			}
			catch
			{
			}
		}

		private void lvAppContainers_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
			{
				bool handled = e.SuppressKeyPress = true;
				e.Handled = handled;
				actSelectAll();
			}
			if (e.Alt && e.KeyCode == Keys.Space)
			{
				bool suppressKeyPress = e.Handled = true;
				e.SuppressKeyPress = suppressKeyPress;
				lnkLearn.Focus();
				SendKeys.Send("% ");
			}
		}

		private void lvAppContainers_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			((ListViewItemComparer)lvAppContainers.ListViewItemSorter).Column = e.Column;
			lvAppContainers.BeginUpdate();
			lvAppContainers.Sort();
			if (lvAppContainers.SelectedIndices.Count > 0)
			{
				lvAppContainers.EnsureVisible(lvAppContainers.SelectedIndices[0]);
			}
			lvAppContainers.EndUpdate();
		}

		private void mnuContext_Opening(object sender, CancelEventArgs e)
		{
			_ptContextPopup = Cursor.Position;
		}

		private void lnkLearn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			actGetHelp();
		}

		private static void actGetHelp()
		{
			Process.Start("http://fiddler2.com/r/?WIN8EL");
		}

		private void frmMain_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F1 || (e.Alt && e.KeyCode == Keys.L))
			{
				bool suppressKeyPress = e.Handled = true;
				e.SuppressKeyPress = suppressKeyPress;
				actGetHelp();
			}
		}

		private void lnkLearn_MouseHover(object sender, EventArgs e)
		{
			tsslStatus.Text = string.Format("©2012 Telerik. All rights reserved. [v{0} {1}{2}]", Application.ProductVersion.ToString(), (IntPtr.Size == 4) ? "32bit" : "64bit", bIsDevPreviewBuild ? " DevPreviewBuild" : (bIsWin8RTM ? " Win8RTM" : (bIsWin8Beta ? " Win8Beta" : string.Empty)));
		}
	}
}

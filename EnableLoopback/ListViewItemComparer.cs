using System;
using System.Collections;
using System.Windows.Forms;

namespace EnableLoopback
{
	internal class ListViewItemComparer : IComparer
	{
		private int col;

		internal bool ascending;

		public int Column
		{
			get
			{
				return col;
			}
			set
			{
				if (value == col)
				{
					ascending = !ascending;
				}
				col = value;
			}
		}

		public ListViewItemComparer()
		{
			col = 0;
			ascending = true;
		}

		public int Compare(object x, object y)
		{
			int num = -1;
			ListViewItem listViewItem = (ListViewItem)x;
			ListViewItem listViewItem2 = (ListViewItem)y;
			num = ((listViewItem.SubItems.Count <= col) ? (-1) : ((listViewItem2.SubItems.Count <= col) ? 1 : string.Compare(listViewItem.SubItems[col].Text, listViewItem2.SubItems[col].Text, StringComparison.Ordinal)));
			if (!ascending)
			{
				num = -num;
			}
			return num;
		}
	}
}

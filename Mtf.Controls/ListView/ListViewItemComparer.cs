using System;
using System.Collections;
using System.Windows.Forms;

namespace Mtf.Controls.ListView
{
    public class ListViewItemComparer : IComparer
    {
        private readonly int column;

        public ListViewItemComparer(int column = 0)
        {
            this.column = column;
        }

        public int Compare(object x, object y)
        {
            var item1 = (ListViewItem) x;
            var item2 = (ListViewItem) y;

            var str1 = item1.SubItems[column].Text;
            var str2 = item2.SubItems[column].Text;

            if (column > 0)
            {
                return String.CompareOrdinal(str1, str2);
            }

            var firstInt64Value = Convert.ToInt64(str1);
            var secondInt64Value = Convert.ToInt64(str2);

            if (firstInt64Value - secondInt64Value > 0)
            {
                return -1;
            }

            return firstInt64Value - secondInt64Value < 0 ? 1 : 0;
        }
    }
}
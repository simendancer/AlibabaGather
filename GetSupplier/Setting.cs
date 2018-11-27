using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetSupplier
{
    public class Setting
    {
        public static int LastCatelogID
        {
            get { return Properties.Settings.Default.LastCatelogId; }
            set { Properties.Settings.Default.LastCatelogId = value; Properties.Settings.Default.Save(); }
        }

        public static int LastCatelogPageIndex
        {
            get { return Properties.Settings.Default.CatelogPageIndex; }
            set { Properties.Settings.Default.CatelogPageIndex = value; Properties.Settings.Default.Save(); }
        }
    }
}

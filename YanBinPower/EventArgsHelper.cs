using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YanBinPower
{
    public static class EventArgsHelper
    {
        /// <summary>
        /// 查看鼠标是否在控制上
        /// </summary>
        /// <param name="e">MouseEventArgs</param>
        /// <param name="rect">Rectangle</param>
        /// <returns>bool</returns>
        public static bool IsBoundaryLine(MouseEventArgs e, Rectangle rect)
        {
            if ((((e.X > rect.X) && (e.X < rect.Right)) && (e.Y > rect.Y)) && (e.Y < rect.Bottom)) return true;
            return false;
        }
    }
}
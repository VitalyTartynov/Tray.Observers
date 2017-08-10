// \***************************************************************************/
// Solution:           Tray.Observers
// Project:            Tray.Observers
// Filename:           IconHelper.cs
// Created:            05.05.2017
// \***************************************************************************/

using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Tray.Observers
{
    static class IconHelper
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern bool DestroyIcon(IntPtr handle);

        public static bool Destroy(IntPtr iconHandle)
        {
            return DestroyIcon(iconHandle);
        }

        public static Icon Create(int value, Color color)
        {
            var brush = new SolidBrush(color);
            var bitmap = new Bitmap(32, 32);
            var font = new Font("Tahoma", 16, FontStyle.Bold);
            var graphics = Graphics.FromImage(bitmap);
            var leftMargin = value > 9 ? 1 : 7;
            graphics.DrawString(value.ToString(), font, brush, leftMargin, 2);

            return Icon.FromHandle(bitmap.GetHicon());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EvilEye.SDK
{
    class Misc
    {
        public static string RandomString(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!§$%&/()=?";
            string s = "";
            System.Random rand = new System.Random();
            for (int i = 0; i < length; i++)
            {
                s += chars[rand.Next(chars.Length - 1)];
            }
            return s;
        }
        internal static string GetClipboard()
        {
            bool flag = Clipboard.ContainsText();
            string result;
            if (flag)
            {
                result = Clipboard.GetText();
            }
            else
            {
                result = null;
            }
            return result;
        }
        internal static void SetClipboard(string Set) //good shit fishyboi
        {
            bool flag = Clipboard.ContainsText();
            if (flag)
            {
                Clipboard.Clear();
                Clipboard.SetText(Set);
            }
            else
            {
                Clipboard.SetText(Set);
            }
        }
    }
}

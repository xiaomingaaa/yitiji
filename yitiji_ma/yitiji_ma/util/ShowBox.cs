using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yitiji_ma.util
{
    /// <summary>
    /// 自定义窗口的展示
    /// </summary>
    class ShowBox
    {
        /// <summary>
        /// 自定义弹窗，
        /// </summary>
        /// <param name="message">弹窗展示内容</param>
        public static void ShowMessageBox(string message)
        {
            MessagesBox box = new MessagesBox();
            box.message = message;
            box.TopMost = true;
            box.ShowDialog();
        }
    }
}

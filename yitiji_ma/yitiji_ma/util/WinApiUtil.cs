using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace yitiji_ma.util
{
    /// <summary>
    /// 使用此工具类来完成对系统开始菜单与任务栏的隐藏与显示 
    /// </summary>
    class WinApiUtil
    {
        private const int SW_HIDE = 0;  //隐藏
        private const int SW_RESTORE = 5;  //显示
        /// <summary>
        /// 使用winapi寻找任务栏
        /// </summary>
        /// <param name="ClassName"></param>
        /// <param name="WindowName"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern int FindWindow(string ClassName, string WindowName);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(int handle, int cmdShow);

        public static void HideTaskList()
        {
            ShowWindow(FindWindow("Shell_TrayWnd", null), SW_HIDE);//隐藏系统任务栏
            ShowWindow(FindWindow("Button", null), SW_HIDE);//隐藏系统开始菜单栏按钮
        }
        public static void ShowTaskList()
        {
            ShowWindow(FindWindow("Shell_TrayWnd", null), SW_RESTORE);//显示系统任务栏
            ShowWindow(FindWindow("Button", null), SW_RESTORE);//显示系统开始菜单栏按钮
        }

    }
}

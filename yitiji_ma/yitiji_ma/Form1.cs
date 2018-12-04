using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using yitiji_ma.util;
using yitiji_ma.entity;
namespace yitiji_ma
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void Test()
        {
            //Config temp = ConfigUtil.getConfig();
            //MessageBox.Show(temp.ToString());
            //MessageBox.Show(temp.Host+temp.User+temp.Ledport);

            MessageBox.Show( Log.WriteJsonData("马腾飞","123456",123456789,36.2));
            //Log.WriteJsonData("马腾飞", "123456", 123456789, 36.2);
            //Log.WriteError("出现报错信息");
            //Log.WriteLog("用户操作日志");

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            Test();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Guashi g_frm = new Guashi();
            g_frm.WindowState = FormWindowState.Maximized;
            g_frm.Show(this);
            g_frm.showMainEvent += delegate { this.Show(); };
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Buka g_frm = new Buka();
            g_frm.WindowState = FormWindowState.Maximized;
            g_frm.Show(this);
            g_frm.showMainEvent += delegate { this.Show(); };
            Hide();
        }
    }
}

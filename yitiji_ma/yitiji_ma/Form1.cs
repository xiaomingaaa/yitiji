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
       

        private void Form1_Load(object sender, EventArgs e)
        {
            //WinApiUtil.HideTaskList();
            //MessageBox.Show(ConfigUtil.getConfig().Cardtype.ToString());
        }
        Guashi g_frm = new Guashi();
        private void button1_Click(object sender, EventArgs e)
        {
            
            g_frm.WindowState = FormWindowState.Maximized;
            g_frm.Show(this);
            g_frm.showMainEvent += delegate { this.Show(); };
            //Hide();
        }
        Buka b_frm = new Buka();
        private void button2_Click(object sender, EventArgs e)
        {
           
            b_frm.WindowState = FormWindowState.Maximized;
            b_frm.Show(this);
            b_frm.showMainEvent += delegate { this.Show(); };
            //Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Test();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            WinApiUtil.ShowTaskList();
        }
    }
}

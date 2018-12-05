using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using yitiji_ma.controller;
namespace yitiji_ma
{
    public partial class Guashi : Form
    {
        //使用此事件做为返回主界面的代理
        public delegate void showMain();
        public event showMain showMainEvent;
        public Guashi()
        {
            InitializeComponent();
        }

        private void Guashi_Load(object sender, EventArgs e)
        {

        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            telone.Text = "";
            teltwo.Text = "";
            this.showMainEvent();
            Hide();
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            GuashiController guashi=new GuashiController();
            Error ex= guashi.GuaShi(telone.Text.Trim(),teltwo.Text.Trim());
            if (ex.Equals(Error.GUASHI_SUCCESS))
            {
                telone.Text = "";
                teltwo.Text = "";
            }
            MessageBox.Show(error.errorMessage(ex));
        }
        
    }
}

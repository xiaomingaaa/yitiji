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
            telone.Text = "亲情号1";
            telone.ForeColor = Color.DarkGray;
            teltwo.Text = "亲情号2";
            teltwo.ForeColor = Color.DarkGray;
            this.showMainEvent();
            Hide();
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            GuashiController guashi=new GuashiController();
            Error ex= guashi.GuaShi(telone.Text.Trim(),teltwo.Text.Trim());
            telone.Text = "亲情号1";
            telone.ForeColor = Color.DarkGray;
            teltwo.Text = "亲情号2";
            teltwo.ForeColor = Color.DarkGray;
            MessageBox.Show(error.errorMessage(ex));
        }

        private void telone_Leave(object sender, EventArgs e)
        {
            if (telone.Text == "")
            {
                telone.Text = "亲情号1";
                telone.ForeColor = Color.DarkGray;
            }
        }

        private void teltwo_Leave(object sender, EventArgs e)
        {
            if(teltwo.Text=="")
            {
                teltwo.Text = "亲情号2";
                teltwo.ForeColor = Color.DarkGray;
            }           
        }

        private void telone_Enter(object sender, EventArgs e)
        {
            if (telone.Text == "亲情号1")
            {
                telone.Text = "";
                telone.ForeColor = Color.White;
            }
           
        }

        private void teltwo_Enter(object sender, EventArgs e)
        {
            if (teltwo.Text == "亲情号2")
            {
                teltwo.Text = "";
                teltwo.ForeColor = Color.White;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using yitiji_ma.controller;
using yitiji_ma.util;
namespace yitiji_ma
{
    public partial class Buka : Form
    {
        public delegate void showEvent();
        public event showEvent showMainEvent;
        public Buka()
        {
            InitializeComponent();            
        }
        //
        private void backBtn_Click(object sender, EventArgs e)
        {

            telone.Text = "亲情号1";
            telone.ForeColor = Color.DarkGray;
            teltwo.Text = "亲情号2";
            teltwo.ForeColor = Color.DarkGray;
            showMainEvent();
            Hide();
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            BukaController buka = new BukaController();
            Error ex=  buka.Buka(telone.Text.Trim(),teltwo.Text.Trim(),stuInfo.SelectedItem.ToString());
            telone.Text = "亲情号1";
            telone.ForeColor = Color.DarkGray;
            teltwo.Text = "亲情号2";
            teltwo.ForeColor = Color.DarkGray;
            ShowBox.ShowMessageBox(error.errorMessage(ex));
            if (ex == Error.BUKA_SUCCESS||ex==Error.UPDATE_INFO_ERROR)
            {
                telone.Text = "亲情号1";
                telone.ForeColor = Color.DarkGray;
                teltwo.Text = "亲情号2";
                teltwo.ForeColor = Color.DarkGray;
                showMainEvent();
                Hide();
            }
        }

        private void telone_Leave(object sender, EventArgs e)
        {
            if (telone.Text == "")
            {
                telone.Text = "亲情号1";
                telone.ForeColor = Color.DarkGray;
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

        private void teltwo_Leave(object sender, EventArgs e)
        {
            if (teltwo.Text == "")
            {
                teltwo.Text = "亲情号2";
                teltwo.ForeColor = Color.DarkGray;
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

        private void telone_TextChanged(object sender, EventArgs e)
        {
            string[] students = checkInput(telone.Text.Trim(), teltwo.Text.Trim());
            if (students != null)
            {
                stuInfo.Items.RemoveAt(0);
                stuInfo.Items.AddRange(students);
                stuInfo.SelectedIndex = 0;
            }
            else
            {
                stuInfo.Items.Clear();
                stuInfo.Items.Add("无此人补卡缴费信息");
                stuInfo.SelectedIndex = 0;
            }
        }

        private void teltwo_TextChanged(object sender, EventArgs e)
        {
            string[] students = checkInput(telone.Text.Trim(),teltwo.Text.Trim());
            if (students != null)
            {
                stuInfo.Items.RemoveAt(0);
                stuInfo.Items.AddRange(students);
                stuInfo.SelectedIndex = 0;
            }
            else
            {
                stuInfo.Items.Clear();
                stuInfo.Items.Add("无此人补卡缴费信息");
                stuInfo.SelectedIndex = 0;
            }
        }
        private string[] checkInput(string str1,string str2)
        {
            string[] students =null;
            if (ValidateUtil.ValidateTelNumber(str1) && ValidateUtil.ValidateTelNumber(str2))
            {
                Dictionary<string, object>[] stus = HttpUtil.getBukaInfo(str1,str2);
                if (stus == null)
                {
                    return null;
                }
                //MessageBox.Show(stus.Length.ToString());
                students = new string[stus.Length];
                for (int i = 0; i < stus.Length; i++)
                {
                    students[i] = stus[i]["name"].ToString() + "|" + stus[i]["stuno2"].ToString();
                }
                 
                
            }
            return students;
        }        
    }
}

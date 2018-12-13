using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using yitiji_ma.controller;
using yitiji_ma.util;
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
            Error ex= guashi.GuaShi(telone.Text.Trim(),teltwo.Text.Trim(),stuInfo.SelectedItem.ToString());
            telone.Text = "亲情号1";
            telone.ForeColor = Color.DarkGray;
            teltwo.Text = "亲情号2";
            teltwo.ForeColor = Color.DarkGray;
            ShowBox.ShowMessageBox(error.errorMessage(ex));
            if (ex == Error.GUASHI_SUCCESS)
            {
                telone.Text = "亲情号1";
                telone.ForeColor = Color.DarkGray;
                teltwo.Text = "亲情号2";
                teltwo.ForeColor = Color.DarkGray;
                this.showMainEvent();
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

        private void teltwo_Leave(object sender, EventArgs e)
        {
            if(teltwo.Text=="")
            {
                teltwo.Text = "亲情号2";
                teltwo.ForeColor = Color.DarkGray;
            }           
        }
        string flag = "";
        private void telone_Enter(object sender, EventArgs e)
        {
            flag = "t1";
            if (telone.Text == "亲情号1")
            {
                telone.Text = "";
                telone.ForeColor = Color.White;
            }
           
        }

        private void teltwo_Enter(object sender, EventArgs e)
        {
            flag = "t2";
            if (teltwo.Text == "亲情号2")
            {
                teltwo.Text = "";
                teltwo.ForeColor = Color.White;
            }
        }

        private void teltwo_TextChanged(object sender, EventArgs e)
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
                stuInfo.Items.Add("(无学生信息)");
                stuInfo.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 通过电话号码查找出学生信息表
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        private string[] checkInput(string str1, string str2)
        {
            string[] students = null;
            if (ValidateUtil.ValidateTelNumber(str1) && ValidateUtil.ValidateTelNumber(str2))
            {
                try
                {
                    Dictionary<string, object>[] stus = HttpUtil.getStudentInfo(str1, str2);
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
                catch (Exception e)
                {
                    Log.WriteError("获取学生信息出现错误："+e.Message);
                }
               


            }
            return students;
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
                stuInfo.Items.Add("(无学生信息)");
                stuInfo.SelectedIndex = 0;
            }
        }
        private void KeyBoardBtn_Click(object sender, EventArgs e)
        {
            Button btnTemp = (Button)sender;
            string digital = btnTemp.Text;
            if (flag=="t1")
            {
                telone.Focus();
                telone.Text += digital;
            }
            else if(flag=="t2")
            {
                teltwo.Focus();
                teltwo.Text += digital;
            }
        }
    }
}

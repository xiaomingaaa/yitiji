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
            telone.Text = "";
            teltwo.Text = "";
            showMainEvent();
            Hide();
            
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            BukaController buka = new BukaController();
            Error ex=  buka.Buka(telone.Text.Trim(),teltwo.Text.Trim());
            if (ex.Equals(Error.BUKA_SUCCESS) || ex.Equals(Error.UPDATE_INFO_ERROR))
            {
                telone.Text = "";
                teltwo.Text = "";
            }
            MessageBox.Show(error.errorMessage(ex));
        }
    }
}

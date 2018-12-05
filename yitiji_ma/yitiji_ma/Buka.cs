﻿using System;
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
            showMainEvent();
            Hide();
            this.Dispose();
            
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            BukaController buka = new BukaController();
            Error ex=  buka.Buka(telone.Text.Trim(),teltwo.Text.Trim());
            MessageBox.Show(error.errorMessage(ex));
        }
    }
}

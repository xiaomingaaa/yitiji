using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yitiji_ma
{
    public partial class MessagesBox : Form
    {
        public string message;
        public MessagesBox()
        {
            InitializeComponent();
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void MessagesBox_Load(object sender, EventArgs e)
        {
            this.label1.Text = message;
        }
    }
}

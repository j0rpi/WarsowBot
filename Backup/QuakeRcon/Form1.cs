using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuakeRcon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            RCON rcon = new RCON();
            string response = rcon.sendCommand(txtCommand.Text, txtGameServerIP.Text, "123456", 12203);

            //replacing \n with \r\n so that it could be treated as enter in text area
            response = response.Replace("\n", "\r\n");
            txtResponse.Text = response;
            
        }
    }
}
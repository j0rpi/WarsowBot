/*
 *   ============================
 * 
 *              j0rpi's
 *        Multi-Functional Bot 
 *            For Warsow
 *            
 *   File: Form1.cs
 *   Purpose: Main UI For App
 *   Author: j0rpi
 * 
 *   ============================
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;
using System.IO;
using INIClass;
using System.Threading;
using Rage.Library;


namespace QuakeRcon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Create a string, in which has a dynamic string value
        string variable;
        string modifierval;

        private void btnSend_Click(object sender, EventArgs e)
        {
            // Try to make connection
            try
            {
                // Setup rcon connection
                RCON rcon = new RCON();

                // Tell the server that the bot is in it's init. state
                string response = rcon.sendCommand("say " + "\"^3Initializing j0rpi's WS Bot v0.4 ... [DONE]\"", textBox5.Text, textBox7.Text, Convert.ToInt32(textBox6.Text));
                textBox12.AppendText(response);
                timer1.Enabled = true;
                timer1.Interval = Convert.ToInt32(textBox8.Text);

                // Disable this button (gray it out) and enable the stop button
                btnSend.Enabled = false;
                button1.Enabled = true;

                // Tell the user the connection succeeded
                label22.Text = "Connected!";
                label22.ForeColor = Color.Green;
            }
            catch
            {
                // Instead of getting a ugly error form, we'll just tell the user connection failed.
                label22.Text = "Connection Failed!";
                label22.ForeColor = Color.Red;
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            // Setup Combobox with items + icons
            comboBoxEx1.ImageList = imageList1;
            comboBoxEx1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxEx1.Items.Add(new ComboBoxExItem("Warsow", 0));
            comboBoxEx1.Items.Add(new ComboBoxExItem("Quake", 3));
            comboBoxEx1.Items.Add(new ComboBoxExItem("Quake 2", 2));
            comboBoxEx1.Items.Add(new ComboBoxExItem("Quake 3 Arena", 1));

            comboBoxEx1.SelectedIndex = comboBoxEx1.FindStringExact("Warsow");
            // Define application root
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);

            // Define config file
            INIClass.INIClass.INIConfigClass ini = new INIClass.INIClass.INIConfigClass(appPath + @"\config.ini");
            

            // Write values to controls
            textBox1.Text = ini.ReadValue("JWSB", "MESSAGE1");
            textBox2.Text = ini.ReadValue("JWSB", "MESSAGE2");
            textBox3.Text = ini.ReadValue("JWSB", "MESSAGE3");
            textBox4.Text = ini.ReadValue("JWSB", "BOT_PREFIX");
            textBox5.Text = ini.ReadValue("JWSB", "IPADRESS");
            textBox6.Text = ini.ReadValue("JWSB", "PORT");
            textBox7.Text = ini.ReadValue("JWSB", "RCON_PASSWORD");
            textBox8.Text = ini.ReadValue("JWSB", "INTERVAL");
            textBox10.Text = ini.ReadValue("JWSB", "ADMIN_ALERT_MESSAGE");
            
            // Prefix Check Button
            if (ini.ReadValue("JWSB", "USE_PREFIX") == null)
            {
                checkBox1.Checked = false;
            }
            if (ini.ReadValue("JWSB", "USE_PREFIX") == "True")
            {
                checkBox1.Checked = true;
            }
            if (ini.ReadValue("JWSB", "USE_PREFIX") == "False")
            {
                checkBox1.Checked = false;
            }

            // Admin Alert Check Button
            if (ini.ReadValue("JWSB", "ADMIN_ALERT") == null)
            {
                checkBox2.Checked = false;
            }
            if (ini.ReadValue("JWSB", "ADMIN_ALERT") == "True")
            {
                checkBox2.Checked = true;
            }
            if (ini.ReadValue("JWSB", "ADMIN_ALERT") == "False")
            {
                checkBox2.Checked = false;
            }

            // Disable stop button, we havn't started the bot yet..
            button1.Enabled = false;
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "OldSchool FFA")
            {
                textBox5.Text = "127.0.0.1";
                textBox6.Text = "44400";
                textBox7.Text = "kanelbulle";
            }

            if (comboBox1.Text == "OldSchool INSTAGIB")
            {
                textBox5.Text = "127.0.0.1";
                textBox6.Text = "44401";
                textBox7.Text = "kanelbulle";
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // Stop timers
            timer1.Enabled = false;
            timer2.Enabled = false;
            timer3.Enabled = false;

            // Enable start button again, since we're stopping the bot. Also disable the stop button.
            btnSend.Enabled = true;
            button1.Enabled = false;

            // Tell the user the connection has aborted.
            label22.Text = "Connection Aborted!";
            label22.ForeColor = Color.Orange;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            JWBS.AboutBox1 about = new JWBS.AboutBox1();
            about.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (textBox9.Text.Contains("rcon"))
                {
                    MessageBox.Show("Please do not use 'rcon' like you would do in-game - This is done automaticly for you!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox9.Text = "";
                    label23.Text = "Attempting RCON query ...";
                }
                else
                {
                    // Setup rcon connection
                    RCON rcon = new RCON();
                    string response = rcon.sendCommand("\"" + textBox9.Text + "\"", "192.168.1.194", "kanelbulle", 44400).Replace("???print", "").Replace("?", "");
                    response = response.Replace("\n", "\r\n");
                    textBox12.AppendText(response);
                    label23.Text = "Successfully Sent RCON Command!";
                }
            }
            catch (Exception)
            {
                label23.Text = "Failed to send command!";
            }
                
                
                
            
        }
        private void button6_Click(object sender, EventArgs e)
        {
            textBox12.Clear();
            timer4.Interval = 1000;
            timer4.Start();
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            // Try to make a connection
            try
            {

                // Setup rcon connection
                RCON rcon = new RCON();

                // Get connected players
                string response = rcon.sendCommand("status", textBox5.Text, textBox7.Text, Convert.ToInt32(textBox6.Text));

                //replacing \n with \r\n so that it could be treated as enter in text area
                response = response.Replace("\n", "\r\n").Replace("????printRcon", "Rcon").Replace("????print", "").Replace("????print", "").Replace("????", "").Replace("print", "").Replace("????print", "");
                textBox12.AppendText(response);

                // Tell the user the connection succeeded
                label22.Text = "Connected!";
                label22.ForeColor = Color.Green;

                if (response.Contains(listBox1.Items.Count.ToString()))
                {
                    timer5.Start();
                }
            }
            catch
            {
                // Instead of getting a ugly error form, we'll just tell the user connection failed.
                label22.Text = "Connection Failed!";
                label22.ForeColor = Color.Red;
            }
           
           
            
        }


        private void timer5_Tick(object sender, EventArgs e)
        {
            // Try to make a connection
            try
            {


                // Setup rcon connection
                RCON rcon = new RCON();


                // Get connected players
                string response = rcon.sendCommand("status", textBox5.Text, textBox7.Text, Convert.ToInt32(textBox6.Text));

                //replacing \n with \r\n so that it could be treated as enter in text area
                response = response.Replace("\n", "\r\n").Replace("????printRcon", "Rcon").Replace("????print", "").Replace("????print", "").Replace("????", "").Replace("print", "").Replace("????print", "");
                textBox12.AppendText(response);
                // Tell the user the connection succeeded
                label22.Text = "Connected!";
                label22.ForeColor = Color.Green;
                if (response.Contains("j0rpi"))
                {
                    rcon.sendCommand("say " + "\"" + textBox10.Text + textBox11.Text + " j0rpi" + "\"", textBox5.Text, textBox7.Text, Convert.ToInt32(textBox6.Text));
                    timer5.Interval = 180000;
                }
                else if (response.Contains("j0rpi h0tt0w"))
                {
                    rcon.sendCommand("say " + "\"" + textBox10.Text + textBox11.Text + " j0rpi, h0tt0w" + "\"", textBox5.Text, textBox7.Text, Convert.ToInt32(textBox6.Text));
                    timer5.Interval = 180000;
                }
                else if (response.Contains("h0tt0w"))
                {
                    rcon.sendCommand("say " + "\"" + textBox10.Text + textBox11.Text + " h0tt0w" + "\"", textBox5.Text, textBox7.Text, Convert.ToInt32(textBox6.Text));
                    timer5.Interval = 180000;
                }
                else
                {
                    // Do nothing...
                }

            }
            catch
            {
                // Instead of getting a ugly error form, we'll just tell the user connection failed.
                label22.Text = "Connection Failed!";
                label22.ForeColor = Color.Red;
            }
            
        }

        private void btnSend_Click_1(object sender, EventArgs e)
        {
            label22.Text = "Connecting ...";
            // Try to make a connection
            try
            {

                // Tell the server that the bot is in it's init. state
                RCON rcon = new RCON();
                
                rcon.sendCommand("say " + "\"^3Initializing j0rpi's WS Bot v0.5 ... [DONE]\"", "127.0.0.1", "kanelbulle", 44400);
                timer1.Enabled = true;
                timer1.Interval = Convert.ToInt32(textBox8.Text);

                // Disable this button (gray it out) and enable the stop button
                btnSend.Enabled = false;
                button1.Enabled = true;

                // Tell the user the connection succeeded
                label22.Text = "Connected!";
                label22.ForeColor = Color.Green;

                // Change Form Title
                this.Text = "j0rpi's WS Bot v0.5";
                this.Text += " - Connected to" + textBox5.Text;

                // Disable IP/PORT/PW/ textboxes
                textBox5.Enabled = false;
                textBox6.Enabled = false;
                textBox7.Enabled = false;
                comboBox1.Enabled = false;
            }
            catch
            {
                // Instead of getting a ugly error form, we'll just tell the user connection failed.
                label22.Text = "Connection Failed!";
                label22.ForeColor = Color.Red;
            }

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            // Try to make a connection
            try
            {

                // Setup rcon connection
                RCON rcon = new RCON();

                // Send message 1
                string response = rcon.sendCommand("say " + "\"" + textBox1.Text + "\"", textBox5.Text, textBox7.Text, Convert.ToInt32(textBox6.Text));

                //replacing \n with \r\n so that it could be treated as enter in text area
                response = response.Replace("\n", "\r\n").Replace("????printRcon", "Rcon").Replace("????print", "").Replace("????print", "").Replace("????", "").Replace("print", "").Replace("????print", "").Replace("????print", "");
                textBox12.AppendText(response);
                timer1.Enabled = false;
                timer2.Enabled = true;
                timer2.Interval = Convert.ToInt32(textBox8.Text);

                // Tell the user the connection succeeded
                label22.Text = "Connected!";
                label22.ForeColor = Color.Green;
            }
            catch
            {
                // Instead of getting a ugly error form, we'll just tell the user connection failed.
                label22.Text = "Connection Failed!";
                label22.ForeColor = Color.Red;
            }
        }

        private void timer2_Tick_1(object sender, EventArgs e)
        {
            // Ugly way of making modifiers for text messages ...

            if (textBox2.Text.Contains("%time%"))
            {
                modifierval = "%time%";
                variable = DateTime.Now.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + " (Server Time)";
            }

            if (textBox2.Text.Contains("%botversion%"))
            {
                modifierval = "%botversion%";
                variable = "j0rpiWSBot 0.5";
            }


            
            
            // Try to make a connection
            try
            {

                // Setup rcon connection
                RCON rcon = new RCON();

                // Send message 2
                string response = rcon.sendCommand("say " + "\"" + textBox2.Text.ToString().Replace(modifierval, variable) + "\"", textBox5.Text, textBox7.Text, Convert.ToInt32(textBox6.Text));

                //replacing \n with \r\n so that it could be treated as enter in text area
                response = response.Replace("\n", "\r\n").Replace("????printRcon", "Rcon").Replace("????print", "").Replace("????print", "").Replace("????", "").Replace("print", "").Replace("????print", "");
                textBox12.AppendText(response);
                timer2.Enabled = false;
                timer3.Enabled = true;
                timer3.Interval = Convert.ToInt32(textBox8.Text);

                // Tell the user the connection succeeded
                label22.Text = "Connected!";
                label22.ForeColor = Color.Green;
            }
            catch
            {
                // Instead of getting a ugly error form, we'll just tell the user connection failed.
                label22.Text = "Connection Failed!";
                label22.ForeColor = Color.Red;
            }
        }

        private void timer3_Tick_1(object sender, EventArgs e)
        {
            // Try to make a connection
            try
            {

                // Setup rcon connection
                RCON rcon = new RCON();

                // Send message 3
                string response = rcon.sendCommand("say " + "\"" + textBox3.Text + "\"", textBox5.Text, textBox7.Text, Convert.ToInt32(textBox6.Text));

                //replacing \n with \r\n so that it could be treated as enter in text area
                response = response.Replace("\n", "\r\n").Replace("????printRcon", "Rcon").Replace("????print", "").Replace("????print", "").Replace("????", "").Replace("print", "").Replace("????print", "");
                textBox12.AppendText(response);

                // Tell the user the connection succeeded
                label22.Text = "Connected!";
                label22.ForeColor = Color.Green;

                if (checkBox2.Checked == true)
                {
                    timer1.Enabled = true;
                    timer3.Enabled = false;
                    timer7.Enabled = true;
                    timer7.Interval = Convert.ToInt32(textBox8.Text);
                }
                else
                {
                    timer3.Enabled = false;
                    timer1.Enabled = true;
                }
            }
            catch
            {
                // Instead of getting a ugly error form, we'll just tell the user connection failed.
                label22.Text = "Connection Failed!";
                label22.ForeColor = Color.Red;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Stop timers
            timer1.Enabled = false;
            timer2.Enabled = false;
            timer3.Enabled = false;

            // Enable start button again, since we're stopping the bot. Also disable the stop button.
            btnSend.Enabled = true;
            button1.Enabled = false;

            // Tell the user the connection has aborted.
            label22.Text = "Connection Aborted!";
            label22.ForeColor = Color.Black;

            // Change Form Title
            this.Text = "j0rpi's WS Bot v0.5 - Idle";

            // Enable IP/PORT/PW/ComboBox textboxes again
            textBox5.Enabled = true;
            textBox6.Enabled = true;
            textBox7.Enabled = true;
            comboBox1.Enabled = true;
        }

        private void timer7_Tick(object sender, EventArgs e)
        {
            // Try to make a connection
            try
            {

                // Setup rcon connection
                RCON rcon = new RCON();

                // Get connected players
                
                string response = rcon.sendCommand("status", textBox5.Text, textBox7.Text, Convert.ToInt32(textBox6.Text));

                // Ugly and messy - Check if the response contains j0rpi, h0tt0w, or both
                if (response.Contains("j0rpi"))
                {
                    rcon.sendCommand("say " + "\"" + textBox10.Text + textBox11.Text + " j0rpi" + "\"", textBox5.Text, textBox7.Text, Convert.ToInt32(textBox6.Text));
                    timer7.Enabled = false;
                }
                else if (response.Contains("h0tt0w"))
                {
                    rcon.sendCommand("say " + "\"" + textBox10.Text + textBox11.Text + " h0tt0w" + "\"", textBox5.Text, textBox7.Text, Convert.ToInt32(textBox6.Text));
                    timer7.Enabled = false;
                }
                else if (response.Contains("j0rpi") && (response.Contains("h0tt0w")))
                {
                    rcon.sendCommand("say " + "\"" + textBox10.Text + textBox11.Text + " j0rpi / h0tt0w" + "\"", textBox5.Text, textBox7.Text, Convert.ToInt32(textBox6.Text));
                    timer7.Enabled = false;
                }
                else
                {
                    // Do nothin'
                }
            }
            catch
            {
                // Instead of getting a ugly error form, we'll just tell the user connection failed.
                label22.Text = "Connection Failed!";
                label22.ForeColor = Color.Red;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // Define application root
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);

            // Define config file
            INIClass.INIClass.INIConfigClass ini = new INIClass.INIClass.INIConfigClass(appPath + @"\config.ini");

            // Write values to config file
            ini.WriteValue("JWSB", "IPADRESS", textBox5.Text);
            ini.WriteValue("JWSB", "PORT", textBox6.Text);
            ini.WriteValue("JWSB", "RCON_PASSWORD", textBox7.Text);
            ini.WriteValue("JWSB", "BOT_PREFIX", textBox4.Text);
            ini.WriteValue("JWSB", "USE_PREFIX", checkBox1.Checked.ToString());
            ini.WriteValue("JWSB", "ADMIN_ALERT", checkBox2.Checked.ToString());
            ini.WriteValue("JWSB", "INTERVAL", textBox8.Text);
            ini.WriteValue("JWSB", "MESSAGE1", textBox1.Text);
            ini.WriteValue("JWSB", "MESSAGE2", textBox2.Text);
            ini.WriteValue("JWSB", "MESSAGE3", textBox3.Text);
            ini.WriteValue("JWSB", "ADMIN_ALERT_MESSAGE", textBox10.Text);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {



        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }

        private void imagedComboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem.ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox13.Text == "Enter admin name to add to admin list ...")
            {
                // Do nothing actually...
            }
            else
            {
                listBox1.Items.Add(textBox13.Text);
                textBox13.Text = "Enter admin name to add to admin list ...";
            }
        }

        private void textBox13_MouseClick(object sender, MouseEventArgs e)
        {
            textBox13.Text = "";
        }

        private void textBox13_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void textBox13_Leave(object sender, EventArgs e)
        {
            
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Now, time for some boring fuckin' code...

            if (comboBoxEx1.Text == "Warsow")
            {
                // Define CVAR Config for Warsow
                string appPath = Path.GetDirectoryName(Application.ExecutablePath);
                INIClass.INIClass.INIConfigClass ini = new INIClass.INIClass.INIConfigClass(appPath + @"\games\warsow_cvar.ini");

                // Read 'em
                cvar1.Text = ini.ReadValue("cvar1", "default");
                cvar2.Text = ini.ReadValue("cvar2", "default");
                cvar3.Text = ini.ReadValue("cvar3", "default");
                cvar4.Text = ini.ReadValue("cvar4", "default");
                cvar5.Text = ini.ReadValue("cvar5", "default");
                cvar6.Text = ini.ReadValue("cvar6", "default");
                cvar7.Text = ini.ReadValue("cvar7", "default");
                cvar8.Text = ini.ReadValue("cvar8", "default");
                cvar9.Text = ini.ReadValue("cvar9", "default");
                cvar10.Text = ini.ReadValue("cvar10", "default");
                cvar11.Text = ini.ReadValue("cvar11", "default");
                cvar12.Text = ini.ReadValue("cvar12", "default");
                cvar13.Text = ini.ReadValue("cvar13", "default");
                cvar14.Text = ini.ReadValue("cvar14", "default");
                cvar15.Text = ini.ReadValue("cvar15", "default");
                cvar16.Text = ini.ReadValue("cvar16", "default");
                cvar17.Text = ini.ReadValue("cvar17", "default");
                cvar18.Text = ini.ReadValue("cvar18", "default");

                cvartext1.Text = ini.ReadValue("cvar1", "desc");
                cvartext2.Text = ini.ReadValue("cvar2", "desc");
                cvartext3.Text = ini.ReadValue("cvar3", "desc");
                cvartext4.Text = ini.ReadValue("cvar4", "desc");
                cvartext5.Text = ini.ReadValue("cvar5", "desc");
                cvartext6.Text = ini.ReadValue("cvar6", "desc");
                cvartext7.Text = ini.ReadValue("cvar7", "desc");
                cvartext8.Text = ini.ReadValue("cvar8", "desc");
                cvartext9.Text = ini.ReadValue("cvar9", "desc");
                cvartext10.Text = ini.ReadValue("cvar10", "desc");
                cvartext11.Text = ini.ReadValue("cvar11", "desc");
                cvartext12.Text = ini.ReadValue("cvar12", "desc");
                cvartext13.Text = ini.ReadValue("cvar13", "desc");
                cvartext14.Text = ini.ReadValue("cvar14", "desc");
                cvartext15.Text = ini.ReadValue("cvar15", "desc");
                cvartext16.Text = ini.ReadValue("cvar16", "desc");
                cvartext17.Text = ini.ReadValue("cvar17", "desc");
                cvartext18.Text = ini.ReadValue("cvar18", "desc");
            }
        }
        
    }
}
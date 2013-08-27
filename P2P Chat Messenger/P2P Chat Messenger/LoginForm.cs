using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace P2P_Chat_Messenger
{
    public partial class LoginForm : Form
    {
        String userName = "";


        // This returns a user name. However, this only allows it to be
        // read only and not to allow other forms to have full access.
        public string UserName
        {
            get { return userName; }
        }

        // Init of the form.
        public LoginForm()
        {
            InitializeComponent();

            // Adds the closing event
           // this.FormClosing += new FormClosingEventHandler(LoginForm_FormClosing);
            // This adds the clicked method to validate the user name.
            btnSignIn.Click += new EventHandler(Validate);
        }

        //Validate method.
        void Validate(object sender, EventArgs e)
        {
            //Grabs user name minus white space.
            userName = txtUserName.Text.Trim();

            //If the user name is empty
            if (string.IsNullOrEmpty(userName))
            {
                // Warn user
                MessageBox.Show("Please type in a user name.", "Error");
                // return to login screen.
                return;
            }
            
        }

        void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Sets user name back to default
            userName = "";
        }
    }
}

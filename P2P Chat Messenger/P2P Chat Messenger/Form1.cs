using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace P2P_Chat_Messenger
{
    public partial class ChatForm : Form
    {
        //This is similar to a pointer
        delegate void AddMessage(string message);

        string userName;

        const int port = 54545;
        const string BroadCast = "255.255.255.255";

        UdpClient ReceivingClient;
        UdpClient SenderClient;

        Thread receivingThread;

        public ChatForm()
        {
            InitializeComponent();

            this.Load += new EventHandler(ChatForm_load);
            btnSend.Click += new EventHandler(Send);
        }
        // Start of method
        void ChatForm_load(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm lf = new LoginForm();
            lf.Show();
            
            if (!string.IsNullOrEmpty(userName))
            {
                userName = lf.UserName;
               // lf.Close();
            }
           // MessageBox.Show(userName + " " + "persons name");
            
            //txtUserInput.Focus();
           // InitReceiver();
           // InitSender();
        }
        // End of Method

        //Enable Sender
        private void InitSender()
        {
            SenderClient = new UdpClient(BroadCast, port);
            SenderClient.EnableBroadcast = true;
        }
        // End of Method

        // Enable Receiver
        private void InitReceiver()
        {
            ReceivingClient = new UdpClient(port);

            ThreadStart start = new ThreadStart(Receiver);
            receivingThread = new Thread(start);
            receivingThread.IsBackground = true;
            receivingThread.Start();
        }
        //End of Method

        private void Receiver()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
            AddMessage messageDelgate = MessageReceived;

            while (true)
            {
                byte[] data = ReceivingClient.Receive(ref endPoint);
                string message = Encoding.ASCII.GetString(data);
                Invoke(messageDelgate, message);
            }
        }
        //End of Method
        //Adds to chat and new line.
        private void MessageReceived(string message)
        {
            // Add message to log.
            txtChat.Text += message + "\n";
        }
        // End of Message
        // This is the information required to send a message
        private void Send(object sender, EventArgs e)
        {
            // Cleans up text typed
            txtUserInput.Text = txtUserInput.Text.Trim();
            // If the text field has text in it.
            if (string.IsNullOrEmpty(txtUserInput.Text) == false)
            {
                // Set up the text to be sent
                // User name + text
                string toSend = userName + ":" + txtUserInput.Text + "\n";
                //Turn the string into bytes to be sent over the net
                byte[] data = Encoding.ASCII.GetBytes(toSend);
                // Send that message!
                SenderClient.Send(data, data.Length);
                // Empty the text field for convience.
                txtUserInput.Text = "";
            }
            // focus on text field.
            txtUserInput.Focus();
        }
        // End of method
    }
}

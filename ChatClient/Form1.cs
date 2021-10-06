using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        private string nickName;
        private TcpClient socket;
        private NetworkStream stream;
        private BinaryWriter writer;
        private BinaryReader reader;
        private Thread getMessages;

            //.CheckForIllegalCrossThreadCalls = false;

        public Form1()
        {
            InitializeComponent();
            //Esto no se hace, pero no me voy a poner a configurar un delegado para un write safe xd.
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (this.txtIp.Text != "" && this.txtPort.Text != "")
            {
                this.EnableDisableItems();

                try
                {
                    this.socket = new TcpClient(this.txtIp.Text, int.Parse(this.txtPort.Text));
                    if (this.txtNickName.Text == "")
                    {
                        this.nickName = "NONICKNAME";
                    }
                    else
                    {
                        this.nickName = this.txtNickName.Text;
                    }
                    this.stream = this.socket.GetStream();
                    this.writer = new BinaryWriter(this.stream);
                    this.reader = new BinaryReader(this.stream);

                    this.writer.Write(this.nickName);

                    this.getMessages = new Thread(this.GetMessages);
                    this.getMessages.Start();

                }
                catch (Exception error)
                {

                    throw;
                }


            }
            
        }

        private void EnableDisableItems()
        {
            this.txtNickName.Enabled = !this.txtNickName.Enabled;
            this.txtIp.Enabled = !this.txtIp.Enabled;
            this.txtPort.Enabled = !this.txtPort.Enabled;
            if (this.btnConnect.Text == "Connect")
            {
                this.btnConnect.Text = "Disconnect";
            }
            else
            {
                this.btnConnect.Text = "Connect";
            }

            this.txtMessageLog.Enabled = !this.txtMessageLog.Enabled;
            this.txtMessage.Enabled = !this.txtMessage.Enabled;
            this.btnSendMessage.Enabled = !this.btnSendMessage.Enabled;
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            if (this.txtMessage.Text != "")
            {
                this.writer.Write(this.txtMessage.Text);
                this.txtMessage.Text = "";
            }
        }

        private void GetMessages()
        {
            string message;
            while (true)
            {
                try
                {
                    message = reader.ReadString() + Environment.NewLine;
                    this.txtMessageLog.Text += message;
                    this.txtMessageLog.SelectionStart = this.txtMessageLog.TextLength;
                    this.txtMessageLog.ScrollToCaret();
                    this.txtMessage.Focus();
                }
                catch (Exception) {}
            }
        }

        private void txtMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                this.btnSendMessage.PerformClick();
                this.txtMessage.Focus();
            }
        }
    }
}

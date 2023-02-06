using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Monitor_Robot
{
    public partial class Form1 : Form
    {
        Thread threadRobot, threadSend;
        //private IPEndPoint IPEndPointRobot;
        //private Socket SocketRobot;
        //TcpListener tcpListener;
        TcpClient client;
        //Socket clientRobot;
        //NetworkStream networkStream;
        StreamReader streamReader;
        StreamWriter streamWriter;

        //byte[] dataSendRobot;
        //byte[] dataReceiveRobot;
        bool startBt = false;
        //bool stopThread = false;
        string ip = "";
        int port = 0;
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btConnect.Select();

            string configPath = string.Format(@"{0}\config.ini", Environment.CurrentDirectory);
            Config config = new Config(configPath);
            //MessageBox.Show(config.Read("Application Config", "Port"));
            if (!File.Exists(configPath))
            {
                config.Write("Application Config", "IP Address", "192.168.0.20");
                config.Write("Application Config", "Port", "10003");
            }
            ip = config.Read("Application Config", "IP Address");
            port = Convert.ToInt32(config.Read("Application Config", "Port"));

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            threadRobot = new Thread(RobotCommunication);
            //threadSend = new Thread(SendToRobot);

            threadRobot.IsBackground = true;
            //threadSend.IsBackground = true;
        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            startBt = true;
            threadRobot.Start();
            //threadSend.Start();
            btConnect.Enabled = false;
        }

        private void btRun_Click(object sender, EventArgs e)
        {
            streamWriter.WriteLine("Contact");
            streamWriter.Flush();
            tbShow.Text += "Enter Command as Putty..." + "\r\n";

            btManual.Enabled = true;
            btRun.Enabled = false;
            btExit.Visible = false;
            btOvrd.Visible = false;
            cbDimention.Visible = false;
            cbOvrd.Visible = false;

            tbSend1.Enabled = true;
            tbSend2.Enabled = true;
            tbSend3.Enabled = true;
            btSend1.Enabled = true;
            btSend2.Enabled = true;
            btSend3.Enabled = true;

            btJog.Enabled = false;
            bt100.Visible = false;
            btXplus.Enabled = false;
            btXsub.Enabled = false;
            btYplus.Enabled = false;
            btYsub.Enabled = false;
            btZplus.Enabled = false;
            btZsub.Enabled = false;

            btAplus.Enabled = false;
            btAsub.Enabled = false;
            btBplus.Enabled = false;
            btBsub.Enabled = false;
            btCplus.Enabled = false;
            btCsub.Enabled = false;
            btL2plus.Enabled = false;
            btL2sub.Enabled = false;

            btJ1plus.Enabled = false;
            btJ1sub.Enabled = false;
            btJ2plus.Enabled = false;
            btJ2sub.Enabled = false;
            btJ3plus.Enabled = false;
            btJ3sub.Enabled = false;
            btJ4plus.Enabled = false;
            btJ4sub.Enabled = false;
            btJ5plus.Enabled = false;
            btJ5sub.Enabled = false;
            btJ6plus.Enabled = false;
            btJ6sub.Enabled = false;

            btXL1.Enabled = false;
            btHU1.Enabled = false;
            btXL2.Enabled = false;
            btHU2.Enabled = false;
            btRst.Enabled = false;

            btF1.Enabled = false;
            btF2.Enabled = false;
            btF3.Enabled = false;
            btF4.Enabled = false;
            btF5.Enabled = false;
            btF6.Enabled = false;
            btF7.Enabled = false;
            btF8.Enabled = false;
            btF9.Enabled = false;
            btF10.Enabled = false;
            btF11.Enabled = false;
            btF12.Enabled = false;
            btF13.Enabled = false;
            btF14.Enabled = false;
            btF15.Enabled = false;
            btF16.Enabled = false;
            btF17.Enabled = false;
            btF18.Enabled = false;
            btF19.Enabled = false;
            btF20.Enabled = false;
            btH1.Enabled = false;
            btH2.Enabled = false;
            btH3.Enabled = false;
            btH4.Enabled = false;
            btH5.Enabled = false;
            btH6.Enabled = false;
            btH7.Enabled = false;
            btH8.Enabled = false;
            btH9.Enabled = false;
            btH10.Enabled = false;
            btH11.Enabled = false;
            btH12.Enabled = false;
            btH13.Enabled = false;
            btH14.Enabled = false;
            btH15.Enabled = false;
            btH16.Enabled = false;
            btH17.Enabled = false;
            btH18.Enabled = false;
            btH19.Enabled = false;
            btH20.Enabled = false;

            btW11.Enabled = false;
            btW21.Enabled = false;
            btW12.Enabled = false;
            btW22.Enabled = false;
            btP11.Enabled = false;
            btP21.Enabled = false;
            btP12.Enabled = false;
            btP22.Enabled = false;
        }

        private void btManual_Click(object sender, EventArgs e)
        {
            streamWriter.WriteLine("Manual");
            streamWriter.Flush();

            btRun.Enabled = true;
            btManual.Enabled = false;
            
            btExit.Visible = true;
            btOvrd.Visible = true;
            cbDimention.Visible = true;
            cbOvrd.Visible = true;

            tbSend1.Enabled = true;
            tbSend2.Enabled = true;
            tbSend3.Enabled = true;
            btSend1.Enabled = true;
            btSend2.Enabled = true;
            btSend3.Enabled = true;

            btJog.Enabled = true;
            bt100.Visible = true;
            btXplus.Enabled = true;
            btXsub.Enabled = true;
            btYplus.Enabled = true;
            btYsub.Enabled = true;
            btZplus.Enabled = true;
            btZsub.Enabled = true;

            btAplus.Enabled = true;
            btAsub.Enabled = true;
            btBplus.Enabled = true;
            btBsub.Enabled = true;
            btCplus.Enabled = true;
            btCsub.Enabled = true;
            btL2plus.Enabled = true;
            btL2sub.Enabled = true;

            btJ1plus.Enabled = true;
            btJ1sub.Enabled = true;
            btJ2plus.Enabled = true;
            btJ2sub.Enabled = true;
            btJ3plus.Enabled = true;
            btJ3sub.Enabled = true;
            btJ4plus.Enabled = true;
            btJ4sub.Enabled = true;
            btJ5plus.Enabled = true;
            btJ5sub.Enabled = true;
            btJ6plus.Enabled = true;
            btJ6sub.Enabled = true;

            btXL1.Enabled = true;
            btHU1.Enabled = true;
            btXL2.Enabled = true;
            btHU2.Enabled = true;
            btRst.Enabled = true;

            btF1.Enabled = true;
            btF2.Enabled = true;
            btF3.Enabled = true;
            btF4.Enabled = true;
            btF5.Enabled = true;
            btF6.Enabled = true;
            btF7.Enabled = true;
            btF8.Enabled = true;
            btF9.Enabled = true;
            btF10.Enabled = true;
            btF11.Enabled = true;
            btF12.Enabled = true;
            btF13.Enabled = true;
            btF14.Enabled = true;
            btF15.Enabled = true;
            btF16.Enabled = true;
            btF17.Enabled = true;
            btF18.Enabled = true;
            btF19.Enabled = true;
            btF20.Enabled = true;

            btH1.Enabled = true;
            btH2.Enabled = true;
            btH3.Enabled = true;
            btH4.Enabled = true;
            btH5.Enabled = true;
            btH6.Enabled = true;
            btH7.Enabled = true;
            btH8.Enabled = true;
            btH9.Enabled = true;
            btH10.Enabled = true;
            btH11.Enabled = true;
            btH12.Enabled = true;
            btH13.Enabled = true;
            btH14.Enabled = true;
            btH15.Enabled = true;
            btH16.Enabled = true;
            btH17.Enabled = true;
            btH18.Enabled = true;
            btH19.Enabled = true;
            btH20.Enabled = true;

            btW11.Enabled = true;
            btW21.Enabled = true;
            btP11.Enabled = true;
            btP21.Enabled = true;
            btW12.Enabled = true;
            btW22.Enabled = true;
            btP12.Enabled = true;
            btP22.Enabled = true;            
        }

        private void RobotCommunication()
        {
            tbPing.Text += "Waiting connect Robot ..." + "\r\n";
            
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), port);
            client = new TcpClient();
            client.Connect(ipe);
            tbPing.Text += GetTime() + "Connected with Robot: " + ipe.Address + " - " + ipe.Port + "\r\n";
            
            //streamWriter.AutoFlush = true;
            streamReader = new StreamReader(client.GetStream());
            streamWriter = new StreamWriter(client.GetStream());
            btRun.Enabled = true;
            btManual.Enabled = true;
            sendByteRobot("READY");

            while (client.Connected)
            {
                try
                {
                    string theString = streamReader.ReadLine();
                    if (theString.Length > 0)
                    {
                        tbShow.Text += GetTime() + "Robot Respond: " + theString + "\r\n";
                    }
                    else
                        Task.Delay(100).Wait();
                }
                catch
                {
                    //MessageBox.Show(ex.Message);
                    tbPing.Text += GetTime() + "Just Disconnect" + "\r\n";
                    break;
                }
            }
            streamReader.Close();
            //networkStream.Close();
            streamWriter.Close();
            //clientRobot.Close();
        }
        private void sendByteRobot(string data0)
        {
            streamWriter.WriteLine(data0);            
            streamWriter.Flush();
            tbShow.Text += GetTime() + "Send Robot: " + data0 + "\r\n";
        }
        private string GetTime()
        {
            return DateTime.Now.ToString("yyyy-M-dd HH:mm:ss ");
        }
        private void btExit_Click(object sender, EventArgs e)
        {
            //sendByteRobot("EXIT");            
            btExit.Visible = false;
            btOvrd.Visible = false;
            cbDimention.Visible = false;
            cbOvrd.Visible = false;

            btRun.Enabled = true;
            btManual.Enabled = true;

            tbSend1.Enabled = false;
            tbSend2.Enabled = false;
            tbSend3.Enabled = false;
            btSend1.Enabled = false;
            btSend2.Enabled = false;
            btSend3.Enabled = false;

            btJog.Enabled = false;
            bt100.Visible = true;            
            btXplus.Enabled = false;
            btXsub.Enabled = false;
            btYplus.Enabled = false;
            btYsub.Enabled = false;
            btZplus.Enabled = false;
            btZsub.Enabled = false;

            btAplus.Enabled = false;
            btAsub.Enabled = false;
            btBplus.Enabled = false;
            btBsub.Enabled = false;
            btCplus.Enabled = false;
            btCsub.Enabled = false;
            btL2plus.Enabled = false;
            btL2sub.Enabled = false;

            btJ1plus.Enabled = false;
            btJ1sub.Enabled = false;
            btJ2plus.Enabled = false;
            btJ2sub.Enabled = false;
            btJ3plus.Enabled = false;
            btJ3sub.Enabled = false;
            btJ4plus.Enabled = false;
            btJ4sub.Enabled = false;
            btJ5plus.Enabled = false;
            btJ5sub.Enabled = false;
            btJ6plus.Enabled = false;
            btJ6sub.Enabled = false;

            btXL1.Enabled = false;
            btHU1.Enabled = false;
            btXL2.Enabled = false;
            btHU2.Enabled = false;
            btRst.Enabled = false;

            btF1.Enabled = false;
            btF2.Enabled = false;
            btF3.Enabled = false;
            btF4.Enabled = false;
            btF5.Enabled = false;
            btF6.Enabled = false;
            btF7.Enabled = false;
            btF8.Enabled = false;
            btF9.Enabled = false;
            btF10.Enabled = false;
            btF11.Enabled = false;
            btF12.Enabled = false;
            btF13.Enabled = false;
            btF14.Enabled = false;
            btF15.Enabled = false;
            btF16.Enabled = false;
            btF17.Enabled = false;
            btF18.Enabled = false;
            btF19.Enabled = false;
            btF20.Enabled = false;

            btH1.Enabled = false;
            btH2.Enabled = false;
            btH3.Enabled = false;
            btH4.Enabled = false;
            btH5.Enabled = false;
            btH6.Enabled = false;
            btH7.Enabled = false;
            btH8.Enabled = false;
            btH9.Enabled = false;
            btH10.Enabled = false;
            btH11.Enabled = false;
            btH12.Enabled = false;
            btH13.Enabled = false;
            btH14.Enabled = false;
            btH15.Enabled = false;
            btH16.Enabled = false;
            btH17.Enabled = false;
            btH18.Enabled = false;
            btH19.Enabled = false;
            btH20.Enabled = false;

            btW11.Enabled = false;
            btW21.Enabled = false;
            btP11.Enabled = false;
            btP21.Enabled = false;
            btW12.Enabled = false;
            btW22.Enabled = false;
            btP12.Enabled = false;
            btP22.Enabled = false;
            bt100.Visible = false;
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (startBt && client.Connected)
                {
                    sendByteRobot("CLOSE");
                }
                Task.Delay(200).Wait();
                Application.ExitThread();
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btXplus_Click(object sender, EventArgs e)
        {
            sendByteRobot(cbDimention.Text + "X+");
        }
        private void btXsub_Click(object sender, EventArgs e)
        {
            sendByteRobot(cbDimention.Text + "X-");
        }
        private void btYplus_Click(object sender, EventArgs e)
        {
            sendByteRobot(cbDimention.Text + "Y+");
        }
        private void btYsub_Click(object sender, EventArgs e)
        {
            sendByteRobot(cbDimention.Text + "Y-");
        }
        private void btZplus_Click(object sender, EventArgs e)
        {
            sendByteRobot(cbDimention.Text + "Z+");
        }
        private void btZsub_Click(object sender, EventArgs e)
        {
            sendByteRobot(cbDimention.Text + "Z-");
        }
        private void btAplus_Click(object sender, EventArgs e)
        {
            sendByteRobot(cbDimention.Text + "A+");
        }
        private void btAsub_Click(object sender, EventArgs e)
        {
            sendByteRobot(cbDimention.Text + "A-");
        }
        private void btBplus_Click(object sender, EventArgs e)
        {
            sendByteRobot(cbDimention.Text + "B+");
        }
        private void btBsub_Click(object sender, EventArgs e)
        {
            sendByteRobot(cbDimention.Text + "B-");
        }
        private void btCplus_Click(object sender, EventArgs e)
        {
            sendByteRobot(cbDimention.Text + "C+");
        }
        private void btCsub_Click(object sender, EventArgs e)
        {
            sendByteRobot(cbDimention.Text + "C-");
        }
        private void btL2plus_Click(object sender, EventArgs e)
        {
            sendByteRobot(cbDimention.Text + "L2+");
        }
        private void btL2sub_Click(object sender, EventArgs e)
        {
            sendByteRobot(cbDimention.Text + "L2-");
        }
        private void btJ1plus_Click(object sender, EventArgs e)
        {
            sendByteRobot(cbDimention.Text + "J1+");
        }
        private void btJ1sub_Click(object sender, EventArgs e)
        {
            sendByteRobot(cbDimention.Text + "J1-");
        }
        private void btJ2plus_Click(object sender, EventArgs e)
        {
            sendByteRobot(cbDimention.Text + "J2+");
        }
        private void btJ2sub_Click(object sender, EventArgs e)
        {
            sendByteRobot(cbDimention.Text + "J2-");
        }
        private void btJ3plus_Click(object sender, EventArgs e)
        {
            sendByteRobot(cbDimention.Text + "J3+");
        }
        private void btJ3sub_Click(object sender, EventArgs e)
        {
            sendByteRobot(cbDimention.Text + "J3-");
        }
        private void btJ4plus_Click(object sender, EventArgs e)
        {
            sendByteRobot(cbDimention.Text + "J4+");
        }
        private void btJ4sub_Click(object sender, EventArgs e)
        {
            sendByteRobot(cbDimention.Text + "J4-");
        }
        private void btJ5plus_Click(object sender, EventArgs e)
        {
            sendByteRobot(cbDimention.Text + "J5+");
        }
        private void btJ5sub_Click(object sender, EventArgs e)
        {
            sendByteRobot(cbDimention.Text + "J5-");
        }
        private void btJ6plus_Click(object sender, EventArgs e)
        {
            sendByteRobot(cbDimention.Text + "J6+");
        }
        private void btJ6sub_Click(object sender, EventArgs e)
        {
            sendByteRobot(cbDimention.Text + "J6-");
        }
        private void btOvrd_Click(object sender, EventArgs e)
        {
            sendByteRobot(cbOvrd.Text + "Ovrd");
        }

        private void btF1_Click(object sender, EventArgs e)
        {
            if (btF1.Text == "F11")
            {
                sendByteRobot("F11");
                btF1.Text = "H11";
                btF1.BackColor = Color.Red;
            }
            else if (btF1.Text == "H11")
            {
                sendByteRobot("H11");
                btF1.Text = "F11";
                btF1.BackColor = Color.MediumSpringGreen;
            }
        }

        private void btF2_Click(object sender, EventArgs e)
        {
            if (btF2.Text == "F21")
            {
                sendByteRobot("F21");
                btF2.Text = "H21";
                btF2.BackColor = Color.Red;
            }
            else if (btF2.Text == "H21")
            {
                sendByteRobot("H21");
                btF2.Text = "F21";
                btF2.BackColor = Color.MediumSpringGreen;
            }        
        }

        private void btF3_Click(object sender, EventArgs e)
        {
            if (btF3.Text == "F31")
            {
                sendByteRobot("F31");
                btF3.Text = "H31";
                btF3.BackColor = Color.Red;
            }
            else if (btF3.Text == "H31")
            {
                sendByteRobot("H31");
                btF3.Text = "F31";
                btF3.BackColor = Color.MediumSpringGreen;
            } 
        }

        private void btF4_Click(object sender, EventArgs e)
        {
            if (btF4.Text == "F41")
            {
                sendByteRobot("F41");
                btF4.Text = "H41";
                btF4.BackColor = Color.Red;
            }
            else if (btF4.Text == "H41")
            {
                sendByteRobot("H41");
                btF4.Text = "F41";
                btF4.BackColor = Color.MediumSpringGreen;
            } 
        }

        private void btF5_Click(object sender, EventArgs e)
        {
            if (btF5.Text == "F51")
            {
                sendByteRobot("F51");
                btF5.Text = "H51";
                btF5.BackColor = Color.Red;
            }
            else if (btF5.Text == "H51")
            {
                sendByteRobot("H51");
                btF5.Text = "F51";
                btF5.BackColor = Color.MediumSpringGreen;
            } 
        }

        private void btF6_Click(object sender, EventArgs e)
        {
            if (btF6.Text == "F61")
            {
                sendByteRobot("F61");
                btF6.Text = "H61";
                btF6.BackColor = Color.Red;
            }
            else if (btF6.Text == "H61")
            {
                sendByteRobot("H61");
                btF6.Text = "F61";
                btF6.BackColor = Color.MediumSpringGreen;
            } 
        }

        private void btF7_Click(object sender, EventArgs e)
        {
            if (btF7.Text == "F71")
            {
                sendByteRobot("F71");
                btF7.Text = "H71";
                btF7.BackColor = Color.Red;
            }
            else if (btF7.Text == "H71")
            {
                sendByteRobot("H71");
                btF7.Text = "F71";
                btF7.BackColor = Color.MediumSpringGreen;
            } 
        }

        private void btF8_Click(object sender, EventArgs e)
        {
            if (btF8.Text == "F81")
            {
                sendByteRobot("F81");
                btF8.Text = "H81";
                btF8.BackColor = Color.Red;
            }
            else if (btF8.Text == "H81")
            {
                sendByteRobot("H81");
                btF8.Text = "F81";
                btF8.BackColor = Color.MediumSpringGreen;
            }
        }

        private void btF9_Click(object sender, EventArgs e)
        {
            if (btF9.Text == "F91")
            {
                sendByteRobot("F91");
                btF9.Text = "H91";
                btF9.BackColor = Color.Red;
            }
            else if (btF9.Text == "H91")
            {
                sendByteRobot("H91");
                btF9.Text = "F91";
                btF9.BackColor = Color.MediumSpringGreen;
            }
        }

        private void btF10_Click(object sender, EventArgs e)
        {
            if (btF10.Text == "F101")
            {
                sendByteRobot("F101");
                btF10.Text = "H101";
                btF10.BackColor = Color.Red;
            }
            else if (btF10.Text == "H101")
            {
                sendByteRobot("H101");
                btF10.Text = "F101";
                btF10.BackColor = Color.MediumSpringGreen;
            }
        }

        private void btF11_Click(object sender, EventArgs e)
        {
            if (btF11.Text == "F111")
            {
                sendByteRobot("F111");
                btF11.Text = "H111";
                btF11.BackColor = Color.Red;
            }
            else if (btF11.Text == "H111")
            {
                sendByteRobot("H111");
                btF11.Text = "F111";
                btF11.BackColor = Color.MediumSpringGreen;
            }
        }

        private void btF12_Click(object sender, EventArgs e)
        {
            if (btF12.Text == "F121")
            {
                sendByteRobot("F121");
                btF12.Text = "H121";
                btF12.BackColor = Color.Red;
            }
            else if (btF12.Text == "H121")
            {
                sendByteRobot("H121");
                btF12.Text = "F121";
                btF12.BackColor = Color.MediumSpringGreen;
            }
        }

        private void btF13_Click(object sender, EventArgs e)
        {
            if (btF13.Text == "F131")
            {
                sendByteRobot("F131");
                btF13.Text = "H131";
                btF13.BackColor = Color.Red;
            }
            else if (btF13.Text == "H131")
            {
                sendByteRobot("H131");
                btF13.Text = "F131";
                btF13.BackColor = Color.MediumSpringGreen;
            }
        }

        private void btF14_Click(object sender, EventArgs e)
        {
            if (btF14.Text == "F141")
            {
                sendByteRobot("F141");
                btF14.Text = "H141";
                btF14.BackColor = Color.Red;
            }
            else if (btF14.Text == "H141")
            {
                sendByteRobot("H141");
                btF14.Text = "F141";
                btF14.BackColor = Color.MediumSpringGreen;
            }
        }

        private void btF15_Click(object sender, EventArgs e)
        {
            if (btF15.Text == "F151")
            {
                sendByteRobot("F151");
                btF15.Text = "H151";
                btF15.BackColor = Color.Red;
            }
            else if (btF15.Text == "H151")
            {
                sendByteRobot("H151");
                btF15.Text = "F151";
                btF15.BackColor = Color.MediumSpringGreen;
            }
        }

        private void btF16_Click(object sender, EventArgs e)
        {
            if (btF16.Text == "F161")
            {
                sendByteRobot("F161");
                btF16.Text = "H161";
                btF16.BackColor = Color.Red;
            }
            else if (btF16.Text == "H161")
            {
                sendByteRobot("H161");
                btF16.Text = "F161";
                btF16.BackColor = Color.MediumSpringGreen;
            }
        }

        private void btF17_Click(object sender, EventArgs e)
        {
            if (btF17.Text == "F171")
            {
                sendByteRobot("F171");
                btF17.Text = "H171";
                btF17.BackColor = Color.Red;
            }
            else if (btF17.Text == "H171")
            {
                sendByteRobot("H171");
                btF17.Text = "F171";
                btF17.BackColor = Color.MediumSpringGreen;
            }
        }

        private void btF18_Click(object sender, EventArgs e)
        {
            if (btF18.Text == "F181")
            {
                sendByteRobot("F181");
                btF18.Text = "H181";
                btF18.BackColor = Color.Red;
            }
            else if (btF18.Text == "H181")
            {
                sendByteRobot("H181");
                btF18.Text = "F181";
                btF18.BackColor = Color.MediumSpringGreen;
            }
        }

        private void btF19_Click(object sender, EventArgs e)
        {
            if (btF19.Text == "F191")
            {
                sendByteRobot("F191");
                btF19.Text = "H191";
                btF19.BackColor = Color.Red;
            }
            else if (btF19.Text == "H191")
            {
                sendByteRobot("H191");
                btF19.Text = "F191";
                btF19.BackColor = Color.MediumSpringGreen;
            }
        }

        private void btF20_Click(object sender, EventArgs e)
        {
            if (btF20.Text == "F201")
            {
                sendByteRobot("F201");
                btF20.Text = "H201";
                btF20.BackColor = Color.Red;
            }
            else if (btF20.Text == "H201")
            {
                sendByteRobot("H201");
                btF20.Text = "F201";
                btF20.BackColor = Color.MediumSpringGreen;
            }
        }                

        private void btH1_Click(object sender, EventArgs e)
        {
            if (btH1.Text == "F12")
            {
                sendByteRobot("F12");
                btH1.Text = "H12";
                btH1.BackColor = Color.Red;
            }
            else if (btH1.Text == "H12")
            {
                sendByteRobot("H12");
                btH1.Text = "F12";
                btH1.BackColor = Color.DodgerBlue;
            }
        }

        private void btH2_Click(object sender, EventArgs e)
        {
            if (btH2.Text == "F22")
            {
                sendByteRobot("F22");
                btH2.Text = "H22";
                btH2.BackColor = Color.Red;
            }
            else if (btH2.Text == "H22")
            {
                sendByteRobot("H22");
                btH2.Text = "F22";
                btH2.BackColor = Color.DodgerBlue;
            } 
        }

        private void btH3_Click(object sender, EventArgs e)
        {
            if (btH3.Text == "F32")
            {
                sendByteRobot("F32");
                btH3.Text = "H32";
                btH3.BackColor = Color.Red;
            }
            else if (btH3.Text == "H32")
            {
                sendByteRobot("H32");
                btH3.Text = "F32";
                btH3.BackColor = Color.DodgerBlue;
            } 
        }

        private void btH4_Click(object sender, EventArgs e)
        {
            if (btH4.Text == "F42")
            {
                sendByteRobot("F42");
                btH4.Text = "H42";
                btH4.BackColor = Color.Red;
            }
            else if (btH4.Text == "H42")
            {
                sendByteRobot("H42");
                btH4.Text = "F42";
                btH4.BackColor = Color.DodgerBlue;
            } 
        }

        private void btH5_Click(object sender, EventArgs e)
        {
            if (btH5.Text == "F52")
            {
                sendByteRobot("F52");
                btH5.Text = "H52";
                btH5.BackColor = Color.Red;
            }
            else if (btH5.Text == "H52")
            {
                sendByteRobot("H52");
                btH5.Text = "F52";
                btH5.BackColor = Color.DodgerBlue;
            } 
        }

        private void btH6_Click(object sender, EventArgs e)
        {
            if (btH6.Text == "F62")
            {
                sendByteRobot("F62");
                btH6.Text = "H62";
                btH6.BackColor = Color.Red;
            }
            else if (btH6.Text == "H62")
            {
                sendByteRobot("H62");
                btH6.Text = "F62";
                btH6.BackColor = Color.DodgerBlue;
            } 
        }

        private void btH7_Click(object sender, EventArgs e)
        {
            if (btH7.Text == "F72")
            {
                sendByteRobot("F72");
                btH7.Text = "H72";
                btH7.BackColor = Color.Red;
            }
            else if (btH7.Text == "H72")
            {
                sendByteRobot("H72");
                btH7.Text = "F72";
                btH7.BackColor = Color.DodgerBlue;
            }
        }

        private void btH8_Click(object sender, EventArgs e)
        {
            if (btH8.Text == "F82")
            {
                sendByteRobot("F82");
                btH8.Text = "H82";
                btH8.BackColor = Color.Red;
            }
            else if (btH8.Text == "H82")
            {
                sendByteRobot("H82");
                btH8.Text = "F82";
                btH8.BackColor = Color.DodgerBlue;
            }
        }

        private void btH9_Click(object sender, EventArgs e)
        {
            if (btH9.Text == "F92")
            {
                sendByteRobot("F92");
                btH9.Text = "H92";
                btH9.BackColor = Color.Red;
            }
            else if (btH9.Text == "H92")
            {
                sendByteRobot("H92");
                btH9.Text = "F92";
                btH9.BackColor = Color.DodgerBlue;
            }
        }

        private void btH10_Click(object sender, EventArgs e)
        {
            if (btH10.Text == "F102")
            {
                sendByteRobot("F102");
                btH10.Text = "H102";
                btH10.BackColor = Color.Red;
            }
            else if (btH10.Text == "H102")
            {
                sendByteRobot("H102");
                btH10.Text = "F102";
                btH10.BackColor = Color.DodgerBlue;
            }
        }

        private void btH11_Click(object sender, EventArgs e)
        {
            if (btH11.Text == "F112")
            {
                sendByteRobot("F112");
                btH11.Text = "H112";
                btH11.BackColor = Color.Red;
            }
            else if (btH11.Text == "H112")
            {
                sendByteRobot("H112");
                btH11.Text = "F112";
                btH11.BackColor = Color.DodgerBlue;
            }
        }

        private void btH12_Click(object sender, EventArgs e)
        {
            if (btH12.Text == "F122")
            {
                sendByteRobot("F122");
                btH12.Text = "H122";
                btH12.BackColor = Color.Red;
            }
            else if (btH12.Text == "H122")
            {
                sendByteRobot("H122");
                btH12.Text = "F122";
                btH12.BackColor = Color.DodgerBlue;
            }
        }

        private void btH13_Click(object sender, EventArgs e)
        {
            if (btH13.Text == "F132")
            {
                sendByteRobot("F132");
                btH13.Text = "H132";
                btH13.BackColor = Color.Red;
            }
            else if (btH13.Text == "H132")
            {
                sendByteRobot("H132");
                btH13.Text = "F132";
                btH13.BackColor = Color.DodgerBlue;
            }
        }

        private void btH14_Click(object sender, EventArgs e)
        {
            if (btH14.Text == "F142")
            {
                sendByteRobot("F142");
                btH14.Text = "H142";
                btH14.BackColor = Color.Red;
            }
            else if (btH14.Text == "H142")
            {
                sendByteRobot("H142");
                btH14.Text = "F142";
                btH14.BackColor = Color.DodgerBlue;
            }
        }

        private void btH15_Click(object sender, EventArgs e)
        {
            if (btH15.Text == "F152")
            {
                sendByteRobot("F152");
                btH15.Text = "H152";
                btH15.BackColor = Color.Red;
            }
            else if (btH15.Text == "H152")
            {
                sendByteRobot("H152");
                btH15.Text = "F152";
                btH15.BackColor = Color.DodgerBlue;
            }
        }

        private void btH16_Click(object sender, EventArgs e)
        {
            if (btH16.Text == "F162")
            {
                sendByteRobot("F162");
                btH16.Text = "H162";
                btH16.BackColor = Color.Red;
            }
            else if (btH16.Text == "H162")
            {
                sendByteRobot("H162");
                btH16.Text = "F162";
                btH16.BackColor = Color.DodgerBlue;
            }
        }

        private void btH17_Click(object sender, EventArgs e)
        {
            if (btH17.Text == "F172")
            {
                sendByteRobot("F172");
                btH17.Text = "H172";
                btH17.BackColor = Color.Red;
            }
            else if (btH17.Text == "H172")
            {
                sendByteRobot("H172");
                btH17.Text = "F172";
                btH17.BackColor = Color.DodgerBlue;
            }
        }

        private void btH18_Click(object sender, EventArgs e)
        {
            if (btH18.Text == "F182")
            {
                sendByteRobot("F182");
                btH18.Text = "H182";
                btH18.BackColor = Color.Red;
            }
            else if (btH18.Text == "H182")
            {
                sendByteRobot("H182");
                btH18.Text = "F182";
                btH18.BackColor = Color.DodgerBlue;
            }
        }

        private void btH19_Click(object sender, EventArgs e)
        {
            if (btH19.Text == "F192")
            {
                sendByteRobot("F192");
                btH19.Text = "H192";
                btH19.BackColor = Color.Red;
            }
            else if (btH19.Text == "H192")
            {
                sendByteRobot("H192");
                btH19.Text = "F192";
                btH19.BackColor = Color.DodgerBlue;
            }
        }

        private void btH20_Click(object sender, EventArgs e)
        {
            if (btH20.Text == "F202")
            {
                sendByteRobot("F202");
                btH20.Text = "H202";
                btH20.BackColor = Color.Red;
            }
            else if (btH20.Text == "H202")
            {
                sendByteRobot("H202");
                btH20.Text = "F202";
                btH20.BackColor = Color.DodgerBlue;
            }
        }

        private void btP1_Click(object sender, EventArgs e)
        {
            if (btF20.Text == "F201")
            {
                sendByteRobot("F201");
                btF20.Text = "H201";
            }
            else if (btF20.Text == "H201")
            {
                sendByteRobot("H201");
                btF20.Text = "F201";
            }
        }
        private void btSend_Click(object sender, EventArgs e)
        {
            sendByteRobot(tbSend1.Text);
        }
        private void btSend2_Click(object sender, EventArgs e)
        {
            sendByteRobot(tbSend2.Text);
        }
        private void btSend3_Click(object sender, EventArgs e)
        {
            sendByteRobot(tbSend3.Text);
        }

        private void btP11_Click(object sender, EventArgs e)
        {
            if (btP11.Text == "P11")
            {
                sendByteRobot("P11");
                btP11.Text = "HP11";
            }
            else if (btP11.Text == "HP11")
            {
                sendByteRobot("HP11");
                btP11.Text = "P11";
            }
        }

        private void btP12_Click(object sender, EventArgs e)
        {
            if (btP12.Text == "P12")
            {
                sendByteRobot("P12");
                btP12.Text = "HP12";
            }
            else if (btP12.Text == "HP12")
            {
                sendByteRobot("HP12");
                btP12.Text = "P12";
            }
        }

        private void btP21_Click(object sender, EventArgs e)
        {
            if (btP21.Text == "P21")
            {
                sendByteRobot("P21");
                btP21.Text = "HP21";
            }
            else if (btP21.Text == "HP21")
            {
                sendByteRobot("HP21");
                btP21.Text = "P21";
            }
        }

        private void btP22_Click(object sender, EventArgs e)
        {
            if (btP22.Text == "P22")
            {
                sendByteRobot("P22");
                btP22.Text = "HP22";
            }
            else if (btP22.Text == "HP22")
            {
                sendByteRobot("HP22");
                btP22.Text = "P22";
            }
        }

        private void btW11_Click(object sender, EventArgs e)
        {
            if (btW11.Text == "W11")
            {
                sendByteRobot("W11");
                btW11.Text = "HW11";
            }
            else if (btW11.Text == "HW11")
            {
                sendByteRobot("HW11");
                btW11.Text = "W11";
            }
        }

        private void btW12_Click(object sender, EventArgs e)
        {
            if (btW12.Text == "W12")
            {
                sendByteRobot("W12");
                btW12.Text = "HW12";
            }
            else if (btW12.Text == "HW12")
            {
                sendByteRobot("HW12");
                btW12.Text = "W12";
            }
        }

        private void btW21_Click(object sender, EventArgs e)
        {
            if (btW21.Text == "W21")
            {
                sendByteRobot("W21");
                btW21.Text = "HW21";
            }
            else if (btW21.Text == "HW21")
            {
                sendByteRobot("HW21");
                btW21.Text = "W21";
            }
        }

        private void btW22_Click(object sender, EventArgs e)
        {
            if (btW22.Text == "W22")
            {
                sendByteRobot("W22");
                btW22.Text = "HW22";
            }
            else if (btW22.Text == "HW22")
            {
                sendByteRobot("HW22");
                btW22.Text = "W22";
            }
        }

        private void btXL1_Click(object sender, EventArgs e)
        {
            if (btXL1.BackColor == Color.White)
            {
                sendByteRobot("ON_XL1");
                btXL1.BackColor = Color.MediumSpringGreen;
            }
            else if (btXL1.BackColor == Color.MediumSpringGreen)
            {
                sendByteRobot("OFF_XL1");
                btXL1.BackColor = Color.White;
            }
        }

        private void btHU1_Click(object sender, EventArgs e)
        {
            if (btHU1.BackColor == Color.White)
            {
                sendByteRobot("ON_HU1");
                btHU1.BackColor = Color.MediumSpringGreen;
            }
            else if (btHU1.BackColor == Color.MediumSpringGreen)
            {
                sendByteRobot("OFF_HU1");
                btHU1.BackColor = Color.White;
            }
        }

        private void btXL2_Click(object sender, EventArgs e)
        {
            if (btXL2.BackColor == Color.White)
            {
                sendByteRobot("ON_XL2");
                btXL2.BackColor = Color.MediumSpringGreen;
            }
            else if (btXL2.BackColor == Color.MediumSpringGreen)
            {
                sendByteRobot("OFF_XL2");
                btXL2.BackColor = Color.White;
            }
        }

        private void btHU2_Click(object sender, EventArgs e)
        {
            if (btHU2.BackColor == Color.White)
            {
                sendByteRobot("ON_HU2");
                btHU2.BackColor = Color.MediumSpringGreen;
            }
            else if (btHU2.BackColor == Color.MediumSpringGreen)
            {
                sendByteRobot("OFF_HU2");
                btHU2.BackColor = Color.White;
            }
        }
        private void EnableOneFix(string btt)
        {
            btF1.Enabled = false;
            btF2.Enabled = false;
            btF3.Enabled = false;
            btF4.Enabled = false;
            btF5.Enabled = false;
            btF6.Enabled = false;
            btF7.Enabled = false;
            btF8.Enabled = false;
            btF9.Enabled = false;
            btF10.Enabled = false;
            btF11.Enabled = false;
            btF12.Enabled = false;
            btF13.Enabled = false;
            btF14.Enabled = false;
            btF15.Enabled = false;
            btF16.Enabled = false;
            btF17.Enabled = false;
            btF18.Enabled = false;
            btF19.Enabled = false;
            btF20.Enabled = false;

            btH1.Enabled = false;
            btH2.Enabled = false;
            btH3.Enabled = false;
            btH4.Enabled = false;
            btH5.Enabled = false;
            btH6.Enabled = false;
            btH7.Enabled = false;
            btH8.Enabled = false;
            btH9.Enabled = false;
            btH10.Enabled = false;
            btH11.Enabled = false;
            btH12.Enabled = false;
            btH13.Enabled = false;
            btH14.Enabled = false;
            btH15.Enabled = false;
            btH16.Enabled = false;
            btH17.Enabled = false;
            btH18.Enabled = false;
            btH19.Enabled = false;
            btH20.Enabled = false;

            btW11.Enabled = false;
            btW21.Enabled = false;            
            btW12.Enabled = false;
            btW22.Enabled = false;
            btP11.Enabled = false;
            btP21.Enabled = false;
            btP12.Enabled = false;
            btP22.Enabled = false;
            if (btt == "F1")
                btF1.Enabled = true;
            else if (btt == "F2")
                btF2.Enabled = true;
            else if (btt == "F3")
                btF3.Enabled = true;
            else if (btt == "F4")
                btF4.Enabled = true;
            else if (btt == "F5")
                btF5.Enabled = true;
            else if (btt == "F6")
                btF6.Enabled = true;
            else if (btt == "F7")
                btF7.Enabled = true;
            else if (btt == "F8")
                btF8.Enabled = true;
            else if (btt == "F9")
                btF9.Enabled = true;
            else if (btt == "F10")
                btF10.Enabled = true;
            else if (btt == "F11")
                btF11.Enabled = true;
            else if (btt == "F12")
                btF12.Enabled = true;
            else if (btt == "F13")
                btF13.Enabled = true;
            else if (btt == "F14")
                btF14.Enabled = true;
            else if (btt == "F15")
                btF15.Enabled = true;
            else if (btt == "F16")
                btF16.Enabled = true;
            else if (btt == "F17")
                btF17.Enabled = true;
            else if (btt == "F18")
                btF18.Enabled = true;
            else if (btt == "F19")
                btF19.Enabled = true;
            else if (btt == "F20")
                btF20.Enabled = true;

            if (btt == "H1")
                btH1.Enabled = true;
            else if (btt == "H2")
                btH2.Enabled = true;
            else if (btt == "H3")
                btH3.Enabled = true;
            else if (btt == "H4")
                btH4.Enabled = true;
            else if (btt == "H5")
                btH5.Enabled = true;
            else if (btt == "H6")
                btH6.Enabled = true;
            else if (btt == "H7")
                btH7.Enabled = true;
            else if (btt == "H8")
                btH8.Enabled = true;
            else if (btt == "H9")
                btH9.Enabled = true;
            else if (btt == "H10")
                btH10.Enabled = true;
            else if (btt == "H11")
                btH11.Enabled = true;
            else if (btt == "H12")
                btH12.Enabled = true;
            else if (btt == "H13")
                btH13.Enabled = true;
            else if (btt == "H14")
                btH14.Enabled = true;
            else if (btt == "H15")
                btH15.Enabled = true;
            else if (btt == "H16")
                btH16.Enabled = true;
            else if (btt == "H17")
                btH17.Enabled = true;
            else if (btt == "H18")
                btH18.Enabled = true;
            else if (btt == "H19")
                btH19.Enabled = true;
            else if (btt == "H20")
                btH20.Enabled = true;

            if (btt == "W11")
                btW11.Enabled = true;
            else if (btt == "W21")
                btW21.Enabled = true;
            else if (btt == "W12")
                btW12.Enabled = true;
            else if (btt == "W22")
                btW22.Enabled = true;
            else if (btt == "P11")
                btP11.Enabled = true;
            else if (btt == "P21")
                btP21.Enabled = true;
            else if (btt == "P12")
                btP12.Enabled = true;
            else if (btt == "P22")
                btP22.Enabled = true;

        }
        private void EnableAll()
        {
            btF1.Enabled = true;
            btF2.Enabled = true;
            btF3.Enabled = true;
            btF4.Enabled = true;
            btF5.Enabled = true;
            btF6.Enabled = true;
            btF7.Enabled = true;
            btF8.Enabled = true;
            btF9.Enabled = true;
            btF10.Enabled = true;
            btF11.Enabled = true;
            btF12.Enabled = true;
            btF13.Enabled = true;
            btF14.Enabled = true;
            btF15.Enabled = true;
            btF16.Enabled = true;
            btF17.Enabled = true;
            btF18.Enabled = true;
            btF19.Enabled = true;
            btF20.Enabled = true;

            btH1.Enabled = true;
            btH2.Enabled = true;
            btH3.Enabled = true;
            btH4.Enabled = true;
            btH5.Enabled = true;
            btH6.Enabled = true;
            btH7.Enabled = true;
            btH8.Enabled = true;
            btH9.Enabled = true;
            btH10.Enabled = true;
            btH11.Enabled = true;
            btH12.Enabled = true;
            btH13.Enabled = true;
            btH14.Enabled = true;
            btH15.Enabled = true;
            btH16.Enabled = true;
            btH17.Enabled = true;
            btH18.Enabled = true;
            btH19.Enabled = true;
            btH20.Enabled = true;

            btW11.Enabled = true;
            btW21.Enabled = true;
            btW12.Enabled = true;
            btW22.Enabled = true;
            btP11.Enabled = true;
            btP21.Enabled = true;
            btP12.Enabled = true;
            btP22.Enabled = true;
        }

        private void btRst_Click(object sender, EventArgs e)
        {
            if (btRst.Text == "RST")
            {
                btRst.Text = "Set P.";
                btF1.Text = "H11";
                btF1.BackColor = Color.Red;
                btF2.Text = "H21";
                btF2.BackColor = Color.Red;
                btF3.Text = "H31";
                btF3.BackColor = Color.Red;
                btF4.Text = "H41";
                btF4.BackColor = Color.Red;
                btF5.Text = "H51";
                btF5.BackColor = Color.Red;
                btF6.Text = "H61";
                btF6.BackColor = Color.Red;
                btF7.Text = "H71";
                btF7.BackColor = Color.Red;
                btF8.Text = "H81";
                btF8.BackColor = Color.Red;
                btF9.Text = "H91";
                btF9.BackColor = Color.Red;
                btF10.Text = "H101";
                btF10.BackColor = Color.Red;
                btF11.Text = "H111";
                btF11.BackColor = Color.Red;
                btF12.Text = "H121";
                btF12.BackColor = Color.Red;
                btF13.Text = "H131";
                btF13.BackColor = Color.Red;
                btF14.Text = "H141";
                btF14.BackColor = Color.Red;
                btF15.Text = "H151";
                btF15.BackColor = Color.Red;
                btF16.Text = "H161";
                btF16.BackColor = Color.Red;
                btF17.Text = "H171";
                btF17.BackColor = Color.Red;
                btF18.Text = "H181";
                btF18.BackColor = Color.Red;
                btF19.Text = "H191";
                btF19.BackColor = Color.Red;
                btF20.Text = "H201";
                btF20.BackColor = Color.Red;

                btH1.Text = "H12";
                btH1.BackColor = Color.Red;
                btH2.Text = "H22";
                btH2.BackColor = Color.Red;
                btH3.Text = "H32";
                btH3.BackColor = Color.Red;
                btH4.Text = "H42";
                btH4.BackColor = Color.Red;
                btH5.Text = "H52";
                btH5.BackColor = Color.Red;
                btH6.Text = "H62";
                btH6.BackColor = Color.Red;
                btH7.Text = "H72";
                btH7.BackColor = Color.Red;
                btH8.Text = "H82";
                btH8.BackColor = Color.Red;
                btH9.Text = "H92";
                btH9.BackColor = Color.Red;
                btH10.Text = "H102";
                btH10.BackColor = Color.Red;
                btH11.Text = "H112";
                btH11.BackColor = Color.Red;
                btH12.Text = "H122";
                btH12.BackColor = Color.Red;
                btH13.Text = "H132";
                btH13.BackColor = Color.Red;
                btH14.Text = "H142";
                btH14.BackColor = Color.Red;
                btH15.Text = "H152";
                btH15.BackColor = Color.Red;
                btH16.Text = "H162";
                btH16.BackColor = Color.Red;
                btH17.Text = "H172";
                btH17.BackColor = Color.Red;
                btH18.Text = "H182";
                btH18.BackColor = Color.Red;
                btH19.Text = "H192";
                btH19.BackColor = Color.Red;
                btH20.Text = "H202";
                btH20.BackColor = Color.Red;
                btW11.Text = "HW11";
                btW12.Text = "HW12";
                btW21.Text = "HW21";
                btW22.Text = "HW22";
                btP11.Text = "HP11";
                btP12.Text = "HP12";
                btP21.Text = "HP21";
                btP22.Text = "HP22";
            }
            else if (btRst.Text == "Set P.")
            {
                btRst.Text = "RST";
                btF1.Text = "F11";
                btF1.BackColor = Color.MediumSpringGreen;
                btF2.Text = "F21";
                btF2.BackColor = Color.MediumSpringGreen;
                btF3.Text = "F31";
                btF3.BackColor = Color.MediumSpringGreen;
                btF4.Text = "F41";
                btF4.BackColor = Color.MediumSpringGreen;
                btF5.Text = "F51";
                btF5.BackColor = Color.MediumSpringGreen;
                btF6.Text = "F61";
                btF6.BackColor = Color.MediumSpringGreen;
                btF7.Text = "F71";
                btF7.BackColor = Color.MediumSpringGreen;
                btF8.Text = "F81";
                btF8.BackColor = Color.MediumSpringGreen;
                btF9.Text = "F91";
                btF9.BackColor = Color.MediumSpringGreen;
                btF10.Text = "F101";
                btF10.BackColor = Color.MediumSpringGreen;
                btF11.Text = "F111";
                btF11.BackColor = Color.MediumSpringGreen;
                btF12.Text = "F121";
                btF12.BackColor = Color.MediumSpringGreen;
                btF13.Text = "F131";
                btF13.BackColor = Color.MediumSpringGreen;
                btF14.Text = "F141";
                btF14.BackColor = Color.MediumSpringGreen;
                btF15.Text = "F151";
                btF15.BackColor = Color.MediumSpringGreen;
                btF16.Text = "F161";
                btF16.BackColor = Color.MediumSpringGreen;
                btF17.Text = "F171";
                btF17.BackColor = Color.MediumSpringGreen;
                btF18.Text = "F181";
                btF18.BackColor = Color.MediumSpringGreen;
                btF19.Text = "F191";
                btF19.BackColor = Color.MediumSpringGreen;
                btF20.Text = "F201";
                btF20.BackColor = Color.MediumSpringGreen;

                btH1.Text = "F12";
                btH1.BackColor = Color.DodgerBlue;
                btH2.Text = "F22";
                btH2.BackColor = Color.DodgerBlue;
                btH3.Text = "F32";
                btH3.BackColor = Color.DodgerBlue;
                btH4.Text = "F42";
                btH4.BackColor = Color.DodgerBlue;
                btH5.Text = "F52";
                btH5.BackColor = Color.DodgerBlue;
                btH6.Text = "F62";
                btH6.BackColor = Color.DodgerBlue;
                btH7.Text = "F72";
                btH7.BackColor = Color.DodgerBlue;
                btH8.Text = "F82";
                btH8.BackColor = Color.DodgerBlue;
                btH9.Text = "F92";
                btH9.BackColor = Color.DodgerBlue;
                btH10.Text = "F102";
                btH10.BackColor = Color.DodgerBlue;
                btH11.Text = "F112";
                btH11.BackColor = Color.DodgerBlue;
                btH12.Text = "F122";
                btH12.BackColor = Color.DodgerBlue;
                btH13.Text = "F132";
                btH13.BackColor = Color.DodgerBlue;
                btH14.Text = "F142";
                btH14.BackColor = Color.DodgerBlue;
                btH15.Text = "F152";
                btH15.BackColor = Color.DodgerBlue;
                btH16.Text = "F162";
                btH16.BackColor = Color.DodgerBlue;
                btH17.Text = "F172";
                btH17.BackColor = Color.DodgerBlue;
                btH18.Text = "F182";
                btH18.BackColor = Color.DodgerBlue;
                btH19.Text = "F192";
                btH19.BackColor = Color.DodgerBlue;
                btH20.Text = "F202";
                btH20.BackColor = Color.DodgerBlue;
                btW11.Text = "W11";
                btW12.Text = "W12";
                btW21.Text = "W21";
                btW22.Text = "W22";
                btP11.Text = "P11";
                btP12.Text = "P12";
                btP21.Text = "P21";
                btP22.Text = "P22";
            }
        }

        private void btJog_Click(object sender, EventArgs e)
        {
            if (btJog.Text == "JOG")
            {
                btJog.Text = "XYZ";
                btXplus.Visible = false;
                btXsub.Visible = false;
                btYplus.Visible = false;
                btYsub.Visible = false;
                btZplus.Visible = false;
                btZsub.Visible = false;

                btAplus.Visible = false;
                btAsub.Visible = false;
                btBplus.Visible = false;
                btBsub.Visible = false;
                btCplus.Visible = false;
                btCsub.Visible = false;
                btL2plus.Visible = false;
                btL2sub.Visible = false;
                bt100.Visible = false;
                btJog.BackColor = Color.Red;
                btJ1plus.Visible = true;
                btJ1sub.Visible = true;
                btJ2plus.Visible = true;
                btJ2sub.Visible = true;
                btJ3plus.Visible = true;
                btJ3sub.Visible = true;
                btJ4plus.Visible = true;
                btJ4sub.Visible = true;
                btJ5plus.Visible = true;
                btJ5sub.Visible = true;
                btJ6plus.Visible = true;
                btJ6sub.Visible = true;
            }
            else if (btJog.Text == "XYZ")
            {
                btJog.Text = "JOG";
                btXplus.Visible = true;
                btXsub.Visible = true;
                btYplus.Visible = true;
                btYsub.Visible = true;
                btZplus.Visible = true;
                btZsub.Visible = true;

                btAplus.Visible = true;
                btAsub.Visible = true;
                btBplus.Visible = true;
                btBsub.Visible = true;
                btCplus.Visible = true;
                btCsub.Visible = true;
                btL2plus.Visible = true;
                btL2sub.Visible = true;
                bt100.Visible = true;
                btJog.BackColor = Color.MediumSpringGreen;
                btJ1plus.Visible = false;
                btJ1sub.Visible = false;
                btJ2plus.Visible = false;
                btJ2sub.Visible = false;
                btJ3plus.Visible = false;
                btJ3sub.Visible = false;
                btJ4plus.Visible = false;
                btJ4sub.Visible = false;
                btJ5plus.Visible = false;
                btJ5sub.Visible = false;
                btJ6plus.Visible = false;
                btJ6sub.Visible = false;
            }           
        }

        private void bt135_Click(object sender, EventArgs e)
        {
            if (bt100.Text == "135")
            {
                bt100.Text = "100";
                btL2plus.Visible = true;
                btL2sub.Visible = true;
                btL2plus.Enabled = true;
                btL2sub.Enabled = true;
            }
            else if (bt100.Text == "100")
            {
                bt100.Text = "135";
                btL2plus.Visible = false;
                btL2sub.Visible = false;
                btL2plus.Enabled = false;
                btL2sub.Enabled = false;
            }           
        }

        private void tbShow_TextChanged(object sender, EventArgs e)
        {
            tbShow.SelectionStart = tbShow.Text.Length;
            tbShow.ScrollToCaret();
        }

        private void tbPing_TextChanged(object sender, EventArgs e)
        {
            tbPing.SelectionStart = tbPing.Text.Length;
            tbPing.ScrollToCaret();
        }
    }
}

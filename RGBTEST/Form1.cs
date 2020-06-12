using RGBTEST.SDK_Wrappers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RGBTEST
{
    public partial class Form1 : Form
    {
        int maxDivision;
        enum LedTypes { NA, A_LED, D_LED_TYPE1, D_LED_TYPE2 };
        List<LedTypes> LedLayout = new List<LedTypes>();

        bool fusionMotherboardInitialised = false;
        bool fusionPeripheralsInitialised = false;

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] buf = new byte[16];
                FusionMotherboardWrapper.GetSdkVersion(buf, buf.Length);
                textBox2.Text += "SDK Version: " + System.Text.Encoding.Unicode.GetString(buf).ToString() + Environment.NewLine;
                textBox1.Text += "API Init: " + (FusionMotherboardWrapper.InitAPI() == 0 ? "Success" : "Failure") + Environment.NewLine;

                maxDivision = FusionMotherboardWrapper.GetMaxDivision();
                textBox1.Text += "Max No. of LED Zones: " + maxDivision.ToString() + Environment.NewLine;

                buf = new byte[maxDivision];
                FusionMotherboardWrapper.GetLedLayout(buf, maxDivision);
                foreach (byte b in buf)
                {
                    switch (b)
                    {
                        case 0:
                            LedLayout.Add(LedTypes.NA);
                            break;
                        case 1:
                            LedLayout.Add(LedTypes.A_LED);
                            break;
                        case 2:
                            LedLayout.Add(LedTypes.D_LED_TYPE1);
                            break;
                        case 3:
                            LedLayout.Add(LedTypes.D_LED_TYPE2);
                            break;
                    }
                }
                for (int i = 0; i < LedLayout.Count; i++)
                {
                    textBox1.Text += "LED Zone " + i + ": " + LedLayout.ElementAt(i) + Environment.NewLine;
                }

                fusionMotherboardInitialised = true;
            }
            catch (Exception exc) { }

            try
            {
                int deviceCount;
                int[] deviceIdArray = new int[10];
                FusionPeripheralsWrapper.InitAPI(out deviceCount, deviceIdArray);

                for (int i = 0; i < deviceCount; i++)
                {
                    if (deviceIdArray[i] > 0 && deviceIdArray[i] < 20481)
                    {
                        textBox1.Text += "Peripheral " + i + ": " + (deviceIdArray[i] == 4097 ? "VGA" : "OTHER");
                    }
                }

                //Program.dllexp_GvLedSet(-1, new GVLED_CFG(1, 0, 0, 0, 0, 0, 10, 100/*colour*/, 0, 1, 1));

                //THIS FUNCTION IS BROKEN
                //byte[] vgaName;
                //Program.dllexp_GvLedGetVgaModelName(out vgaName);
                //textBox6.Text = System.Text.Encoding.ASCII.GetString(vgaName);

                fusionPeripheralsInitialised = true;
            }
            catch (Exception exc) { }

        }

        private GVLED_CFG constructPeripArray(byte R, byte G, byte B)
        {
            byte[] buf = new byte[] { B, G, R, 0 };
            uint colour = BitConverter.ToUInt32(buf, 0);
            return new GVLED_CFG(1, 0, 0, 0, 0, 0, 10, colour, 0, 1, 1);
        }

        private byte[] constructArray()
        {
            MemoryStream buffer = new MemoryStream(new Byte[maxDivision * 16]);

            using (BinaryWriter writer = new BinaryWriter(buffer))
            {
                for (int i = 0; i < maxDivision; i++)
                {
                    writer.Write((byte)0);          //reserve
                    writer.Write((byte)4);          //mode
                    writer.Write((byte)100);        //maxbrightness
                    writer.Write((byte)0);          //minbrightness
                    writer.Write((byte)int.Parse(textBox4.Text));          //BB
                    writer.Write((byte)int.Parse(textBox5.Text));        //GG    (0-128)
                    writer.Write((byte)int.Parse(textBox3.Text));          //RR
                    writer.Write((byte)0);          //WW
                    writer.Write((ushort)0);
                    writer.Write((ushort)0);
                    writer.Write((ushort)0);
                    writer.Write((byte)0);
                    writer.Write((byte)0);
                }
            }


            /*byte[] buf = new byte[16]
            {
                0x00,       //Byte Reserve0
                0x04,       //Byte LedMode (0 null, 1 pulse, 2 music, 3 color cycle, 4 static, 5 flash, 8 transition...)
                0x64,       //Byte MaxBrightness (0-100)
                0x64,       //Byte MinBrightness (0-100)
                0xFF,           //DWord dwColor   
                0xFF,           //
                0xFF,           //
                0x00,           //
                0x00,       //Word wTime0   (0-65535)
                0x00,
                0x00,       //Word wTime1
                0x00,
                0x00,       //Word wTime2
                0x00,
                0x00,       //CtrlVal0      (0-255)
                0x00,       //CtrlVal1
            };*/

            return buffer.ToArray();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (fusionMotherboardInitialised)
            {
                Byte[] settingBuffer = constructArray();
                textBox6.Text = "Update LED Settings: " + (FusionMotherboardWrapper.SetLedData(settingBuffer, settingBuffer.Length) == 0 ? "Success" : "Failure") + Environment.NewLine;
                textBox6.Text += "Apply Settings: " + (FusionMotherboardWrapper.Apply(-1) == 0 ? "Success" : "Failure") + Environment.NewLine;
            }
            if (fusionPeripheralsInitialised)
            {
                textBox6.Text += "Apply Peripheral Settings: " + (FusionPeripheralsWrapper.SetLed(-1, constructPeripArray((byte)int.Parse(textBox3.Text), (byte)int.Parse(textBox5.Text), (byte)int.Parse(textBox4.Text))) == 0 ? "Success" : "Failure");
            }
        }
    }
}

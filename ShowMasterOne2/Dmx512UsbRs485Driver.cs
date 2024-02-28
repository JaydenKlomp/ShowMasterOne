using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

/// <summary>
/// test
/// </summary>
namespace Dmx512UsbRs485
{
    public class Dmx512UsbRs485Driver
    {
        #region Version
        int dmxDriverVersion = 1;//this version is the latest and set as default
        #endregion

        #region USB and DMX settings
        static SerialPort USB_RS485;
        byte port_open = 0;
        int buffSize = 12;
        byte[] buff = new byte[513];
        #endregion

        #region portInfo
        List<string> portName = new List<string>();
        int nrOfPorts = 0;
        #endregion

        #region DMX parameters assigned to default, for future use
        //int parity = 0;
        //int baudrate = 250000;
        //StopBits stopbits = StopBits.Two;
        //int databits = 8;
        //int readtimeout = 500;
        //int writetimeout = 500;
        #endregion

        /// <summary>
        /// constructor: initialize the USB serial port
        /// <para>find number of ports and store them in the getter "NrOfPorts"</para>
        /// <para>and initialize the DMX buffer</para>
        /// </summary>
        public Dmx512UsbRs485Driver()
        {
            int m_index;

            USB_RS485 = new SerialPort();

            nrOfPorts = 0;
            foreach (string s in SerialPort.GetPortNames())
            {
                //Ports.Items.Add(s);
                portName.Add(s);
                nrOfPorts++;
            }

            //Ports.Text = "Select Port";

            for (m_index = 0; m_index <= 512; m_index++)
            {
                buff[m_index] = 0;
            }
        }

        /// <summary>
        /// this gettersetter provides a possiblity to change the version, default is the latest; check out the about
        /// </summary>
        public int GetSetDmxDriverVersion
        {
            get
            {
                return dmxDriverVersion;
            }

            set
            {
                dmxDriverVersion = value;
            }
        }

        public string PortNameAt(int a_index)
        {
            string m_portName = "";

            m_portName = portName[a_index];

            return m_portName;
        }

        /// <summary>
        /// get the number of USB ports found in the constructor
        /// </summary>
        public int NrOfPorts
        {
            get { return nrOfPorts; }
        }

        /// <summary>
        /// call the method with the COM port name
        /// </summary>
        /// <param name="a_portName"></param>
        public void DmxToDefault(string a_portName)
        {
            if (port_open != 0)
            {
                USB_RS485.Close();
                port_open = 0;
            }

            //Ports.Text;                         //Ports.Text from combo box i.e. COM3
            USB_RS485.PortName = a_portName;
            USB_RS485.Parity = 0;
            USB_RS485.BaudRate = 250000;// 38400;

            //USB_RS485.StopBits = StopBits.Two;// set in method : DmxStopBits
            DmxStopBits(2);
            USB_RS485.DataBits = 8;
            USB_RS485.ReadTimeout = 500;
            USB_RS485.WriteTimeout = 500;
            try
            {
                USB_RS485.Open();
            }
            catch
            {

            }

            if (USB_RS485.IsOpen == true)
            {
                port_open = 1;
            }
        }

        /// <summary>
        /// set the DMX parameters as required, exception handling only for USB-RS485 types, erroneous values are returned as argument number
        /// </summary>
        /// <param name="a_portName"></param>
        /// <param name="a_parity"></param>
        /// <param name="a_baudRate"></param>
        /// <param name="a_stopBits"></param>
        /// <param name="a_dataBits"></param>
        /// <param name="a_readTimeOut"></param>
        /// <param name="a_writeTimeOut"></param>
        public string DmxUseModifiedValues(string a_portName, int a_parity, int a_baudRate, int a_stopBits,
                                           int a_dataBits, int a_readTimeOut, int a_writeTimeOut)
        {
            string m_message = "";
            int m_argNumber = 1;

            try
            {
                if (port_open != 0)
                {
                    USB_RS485.Close();
                    port_open = 0;
                }

                //Ports.Text;                         //Ports.Text from combo box i.e. COM3
                USB_RS485.PortName = a_portName;
                m_argNumber++;
                //USB_RS485.Parity = 0;
                DmxParity(0);
                m_argNumber++;
                USB_RS485.BaudRate = a_baudRate;// 38400;
                m_argNumber++;

                //USB_RS485.StopBits = StopBits.Two;// set in method : DmxStopBits
                DmxStopBits(a_stopBits);
                m_argNumber++;
                USB_RS485.DataBits = a_dataBits;
                m_argNumber++;
                USB_RS485.ReadTimeout = a_readTimeOut;
                m_argNumber++;
                USB_RS485.WriteTimeout = a_writeTimeOut;
                m_argNumber++;

                USB_RS485.Open();
                if (USB_RS485.IsOpen == true)
                {
                    port_open = 1;
                }
            }
            catch
            {
                m_message = "ERROR: on argument number: " + Convert.ToString(m_argNumber);
            }

            return m_message;
        }

        /// <summary>
        /// set number of stopbits
        /// <para>0: none; 1: one; 2: two; 15: onePointFive; else: two</para>
        /// <para>returns the result of the change</para>
        /// </summary>
        private string DmxStopBits(int a_stopBits)
        {
            string m_message = "";

            switch (a_stopBits)
            {
                case 0:
                    USB_RS485.StopBits = StopBits.None;
                    m_message = "stopbits: 0";
                    break;
                case 1:
                    USB_RS485.StopBits = StopBits.One;
                    m_message = "stopbits: 1";
                    break;
                case 2:
                    USB_RS485.StopBits = StopBits.Two;
                    m_message = "stopbits: 2";
                    break;
                case 15:
                    USB_RS485.StopBits = StopBits.OnePointFive;
                    m_message = "stopbits: 1.5";
                    break;
                default:
                    USB_RS485.StopBits = StopBits.Two;
                    m_message = "ERROR: no value recognized, set to default, stopbits: 2";
                    break;
            }
            return m_message;
        }

        /// <summary>
        /// DO NOT MODIFY
        /// </summary>
        /// <param name="a_parity"></param>
        /// <returns></returns>
        private string DmxParity(int a_parity)
        {
            string m_message = "";

            if (a_parity == 0)
            {
                USB_RS485.Parity = 0;
                m_message = "OK";
            }
            else
            {
                //USB_RS485.Parity = 1;
                USB_RS485.Parity = 0;
                m_message = "ERROR: parity not recognized and set to zero";
            }

            return m_message;
        }

        /// <summary>
        /// Sets the PAR56 to random values: Red 100%; Green 50%; Blue 25%; test purposes only
        /// </summary>
        public void DmxTestForPar56()
        {
            DmxLoadBuffer(1, 255, 8);
            DmxLoadBuffer(2, 128, 8);
            DmxLoadBuffer(3, 63, 8);
            DmxLoadBuffer(4, 0, 8);
            DmxLoadBuffer(5, 0, 8);
            DmxLoadBuffer(6, 0, 8);

            DmxSendCommand(6);
        }

        /// <summary>
        /// Sets the PAR56 values in arguments for test purposes only
        /// </summary>
        public void DmxTestForPar56RgbValues(byte a_red, byte a_green, byte a_blue)
        {
            DmxLoadBuffer(1, a_red, 8);
            DmxLoadBuffer(2, a_green, 8);
            DmxLoadBuffer(3, a_blue, 8);
            DmxLoadBuffer(4, 0, 8);
            DmxLoadBuffer(5, 0, 8);
            DmxLoadBuffer(6, 0, 8);

            DmxSendCommand(6);
        }

        /// <summary>
        /// Store values in buffer from the armature start address, after loading buffer call DmxSendCommand
        /// <para>use for every address + next value of armature</para>
        /// <para>PAR56 example</para>
        /// <para>DmxLoadBuffer(1, 128, 512) </para>
        /// <para>DmxLoadBuffer(2, 64, 512) </para>
        /// <para>etc..</para>
        /// <para>set buffsize to 512</para>
        /// </summary>
        /// <param name="a_index"></param>
        /// <param name="a_value"></param>
        /// <param name="a_buffSize"></param>
        /// <returns></returns>
        public string DmxLoadBuffer(int a_index, byte a_value, int a_buffSize)
        {
            string m_message = "";

            if (a_index <= 512 && a_index >= 1)
            {
                buff[a_index] = a_value;
                buffSize = a_buffSize;
                m_message = "OK";
            }
            else
            {
                m_message = "ERROR: index out of range";
            }

            return m_message;
        }

        /// <summary>
        /// Sends the defined number of DMX bytes to the USB-RS485 converter
        /// <para>512 DMX packets take approximately 35ms, keep timer interval at 50ms</para>
        /// <para>limit number of "a_nrOfBytes" if higher update rates are required</para>
        /// </summary>
        /// <param name="a_nrOfBytes"></param>
        /// <returns></returns>
        public string DmxSendCommand(int a_nrOfBytes)
        {
            string m_message = "";

            if (port_open == 0)
            {
                m_message = "ERROR: port closed";
                //return;
            }
            else
            {
                //first byte MUST be zero and cannot be written
                a_nrOfBytes++;
                buff[0] = 0;

                #region original code form 2012
                //break needed for DMX512
                if (dmxDriverVersion == 0)
                {
                    USB_RS485.BreakState = true;
                    Thread.Sleep(20);//20
                    USB_RS485.BreakState = false;
                }
                #endregion

                #region code from Devantech (Chris) 27feb2023
                //discussion with Chris: DMX protocol not fully followed in my opinion, but it works..
                if (dmxDriverVersion == 1)
                {
                    USB_RS485.BaudRate = 80000; //slow baud rate to generate break
                    USB_RS485.Write(buff, 0, 1);
                    USB_RS485.BaudRate = 250000; //back to 250K baud normal DMX rate
                }
                #endregion

                //USB_RS485.Write(buff, 0, 12);//test purposes
                USB_RS485.Write(buff, 0, a_nrOfBytes);

                #region modified v1 code from Devantech (Chris) 27feb2023
                //function not quite clear but advised by Chris of Devantech
                if (dmxDriverVersion == 1)
                {
                    while (USB_RS485.BytesToWrite != 0) ;
                    Thread.Sleep(20);
                }
                #endregion

                #region original v0 code from 2012
                //breakstates no longer supported in new FTDI devices
                if (dmxDriverVersion == 0)
                {
                    Thread.Sleep(1);//
                    USB_RS485.BreakState = true;//
                    Thread.Sleep(1);
                    USB_RS485.BreakState = false;//
                }
                #endregion
            }

            return m_message;
        }

        /// <summary>
        /// Version: 08okt2022, created by Dick van Kalsbeek
        /// </summary>
        /// <returns></returns>
        public string About()
        {
           string m_about = "Driver DLL for RobotElectronics USB-RS485\n" +
                      "This DLL configures the device for DMX512\n" +
                      "Created by: Dick van Kalsbeek, ROC ter Aa, Helmond\n" +
                      "Initial date: vA - 12mar2012\n" +
                      "Update: v0 - 08okt2022\n\n" +
                      "Changed v0: DmxTestForPar56: RGB all on\n" +
                      "Update: v1 - 07mar2023\n\n" +
                      "Changed v1: breakstates remove and replaced\n" +
                      "Added: DMXTestForPar56RGBvalues";

            return m_about;
        }

        public string Help()
        {
            string m_help = "";

            //m_help = ;

            return m_help;
        }
    }
}

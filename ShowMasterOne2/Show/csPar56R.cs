using Dmx512UsbRs485;
using ShowMasterOne2.Show;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace ShowMasterOne2.Show
{
    public class csPar56R
    {
        public string armatureName = "";
        public string nameOrLocation = "TBD";
        public int DmxDeviceNr = 0; //needed for the automation
        public int sceneWaitTime = 500;
        public byte DmxAddress = 0;
        public byte DmxWidth = 0;

        public byte _1_redValue;
        public byte _2_greenValue;
        public byte _3_blueValue;
        public byte _4_macroSettingValue;
        public byte _5_programSpeedValue;
        public byte _6_strobeValue;


        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="a_name">name or physical room location</param>
        /// <param name="a_nr">there can be more devices used in a show</param>
        public csPar56R(string a_name, int a_nr)
        {
            armatureName = a_name + "_" + a_nr.ToString();
        }

        public string About()
        {
            return "Class for use with Ayra Ero FX\n " +
                    "created: Dick van Kalsbeek\n " +
                    "Initial 11feb2023; version 28feb2023";
        }

        /// <summary>
        /// full string of set values for armature; needed for show scene data; 		
        /// <para>example: Scene 000:PAR56_2:NAME_LOCATION;128;000;128;000;000;000</para>
        /// </summary>
        /// <returns>returns: full string with values formatted in 3 digits</returns>
        public string GetShowDataAsText()
        {
            string m_getShowDataAsText = "";

            m_getShowDataAsText =
                armatureName + "_" + DmxDeviceNr.ToString() + ":" +
                nameOrLocation + ":" +
                this._1_redValue.ToString("000") + ";" +
                this._2_greenValue.ToString("000") + ";" +
                this._3_blueValue.ToString("000") + ";" +
                this._4_macroSettingValue.ToString("000") + ";" +
                this._5_programSpeedValue.ToString("000") + ";" +
            this._6_strobeValue.ToString("000") + ";";

            return m_getShowDataAsText;
        }
    }
}
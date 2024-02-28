using Dmx512UsbRs485;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowMasterOne2.Show
{
    public class csAyraFx
    {
        public string armatureName = "";
        public string nameOrLocation = "";
        public int DmxDeviceNr = 0; //needed for the automation
        public byte DmxAddress = 0;
        public byte DmxWidth = 0;

        public byte _8_masterDimmer;
        public byte _9_strobeFastToSlow;
        public byte _10_pan;
        public byte _11_panFine;
        public byte _12_tilt;
        public byte _13_tiltFine;
        public byte _14_panTiltSpeed;
        public byte _15_redBeam;
        public byte _16_greenBeam;
        public byte _17_blueBeam;
        public byte _18_white;
        public byte _19_redRing;
        public byte _20_greenRing;
        public byte _21_blueRing;
        public byte _22_DmxMode;
        public byte _23_autoShow;

        /// <summary>
		/// constructor
		/// </summary>
		/// <param name="a_name">name or physical room location</param>
		/// <param name="a_nr">there can be more devices used in a show</param>
		public csAyraFx(string a_name, int a_nr)
        {
            armatureName = a_name + "_" + a_nr.ToString();
        }

        public csAyraFx()
        {
        }

        /// <summary>
        /// creator and version information
        /// </summary>
        /// <returns></returns>
        public string About()
        {
            return "Class for use with Ayra Ero FX\n " +
                    "created: Dick van Kalsbeek\n " +
                    "Initial 11feb2023; version 28feb2023";
        }

        /// <summary>
        /// full string of set values for armature; needed for show scene data; 		
        /// <para>example: Scene 000:AyraFx_1:NAME_LOCATION;000;000 etc until 14 values</para>
        /// </summary>
        /// <returns>returns: full string with values formatted in 3 digits</returns>
        public string GetShowDataAsText()
        {
            string m_getShowDataAsText = "";

            m_getShowDataAsText =
                armatureName + "_" + DmxDeviceNr.ToString() + ":" +
                nameOrLocation + ":" +
                _8_masterDimmer.ToString("000") + ";" +
                _9_strobeFastToSlow.ToString("000") + ";" +
                _10_pan.ToString("000") + ";" +
                _11_panFine.ToString("000") + ";" +
                _12_tilt.ToString("000") + ";" +
                _13_tiltFine.ToString("000") + ";" +
                _14_panTiltSpeed.ToString("000") + ";" +
                _15_redBeam.ToString("000") + ";" +
                _16_greenBeam.ToString("000") + ";" +
                _17_blueBeam.ToString("000") + ";" +
                _18_white.ToString("000") + ";" +
                _19_redRing.ToString("000") + ";" +
                _20_greenRing.ToString("000") + ";" +
                _21_blueRing.ToString("000") + ";" +
                _22_DmxMode.ToString("000") + ";" +
                _23_autoShow.ToString("000") + ";";

            return m_getShowDataAsText;
        }
    }
}

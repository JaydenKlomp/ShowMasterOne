using APPR_ShowMasterOne_22SD_AssignmentPrep;
//extra
using Dmx512UsbRs485;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ShowMasterOne2
{
        #region Globals
    public partial class Form1 : Form
    {
        Dmx512UsbRs485Driver dmxControl = new Dmx512UsbRs485Driver();
        csArmatureSettingsInScene obDirectArm = new csArmatureSettingsInScene();

        csArmatureSettingsInScene obColorDirect = new csArmatureSettingsInScene();
        csArmatureSettingsInScene[] arColorShow = new csArmatureSettingsInScene[100];

        int storedSceneNrPar = 0;
        int playedSceneNrPar = 0;
        int storedSceneNrAyra = 1;
        int playedSceneNrAyra = 0;

        string relativeFileDirectoryPar = "";
        string storedFileNamePar = "";
        string relativeFileDirectoryAyra = "";
        string storedFileNameAyra = "";

        #endregion

        #region FormHandler

        public Form1()
        {
            InitializeComponent();
            //Centered the form to the screen
            this.CenterToScreen();
            this.Size = new Size(608, 627);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Disappeared the tabs in the tabcontrole
            tcShowMasterOneJKLO.Appearance = TabAppearance.FlatButtons;
            tcShowMasterOneJKLO.ItemSize = new Size(0, 1);
            tcShowMasterOneJKLO.SizeMode = TabSizeMode.Fixed;
        }


        private void tsmHome_Click(object sender, EventArgs e)
        {
            //Changed the size of the form
            this.Size = new Size(608, 627);

            //Center the form to the middle of the screen
            this.CenterToScreen();

            //Opens home page
            tcShowMasterOneJKLO.SelectedTab = tpHomeJKLO;
        }

        private void tsmQuit_Click(object sender, EventArgs e)
        {
            //Checked if 
            if (DialogResult.Yes == MessageBox.Show("Are you sure that you want to leave this application?", "Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                Application.Exit();
            }
        }
        private void tsmConnectionJKLO_Click(object sender, EventArgs e)
        {
            //Changed the size of the form
            this.Size = new Size(281, 256);

            //Center the form to the middle of the screen
            this.CenterToScreen();

            //Opens the The Race page
            tcShowMasterOneJKLO.SelectedTab = tpConnectionJKLO;
        }
        private void tsmParJKLO_Click(object sender, EventArgs e)
        {
            //Changed the size of the form
            this.Size = new Size(384, 474);

            //Center the form to the middle of the screen
            this.CenterToScreen();

            //Opens the The Race page
            tcShowMasterOneJKLO.SelectedTab = tpPar56RJKLO;
        }

        private void tsmAyraJKLO_Click(object sender, EventArgs e)
        {
            //Changed the size of the form
            this.Size = new Size(608, 627);

            //Center the form to the middle of the screen
            this.CenterToScreen();

            //Opens the The Race page
            tcShowMasterOneJKLO.SelectedTab = tpAyraJKLO;
        }

        private void tmrGlobal_Tick(object sender, EventArgs e)
        {
            dmxControl.DmxSendCommand(512);
        }

        #endregion

        #region USBHandler

        private void btnCheck_Click_1(object sender, EventArgs e)
        {
            int m_UsbPortsCount;
            int m_UsbPortNr;

            m_UsbPortsCount = dmxControl.NrOfPorts;
            lblPortsCountJKLO.Text = m_UsbPortsCount.ToString();

            for (m_UsbPortNr = 0; m_UsbPortNr < m_UsbPortsCount; m_UsbPortNr++)
            {
                cbbUsbPortsJKLO.Items.Add(dmxControl.PortNameAt(m_UsbPortNr));
            }
        }

        private void cbbDmxUpdate_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            dmxControl.DmxToDefault(cbbUsbPortsJKLO.Text);
            tmrGlobalJKLO.Start();
        }

        #endregion

        #region ParHandler

        private void handleParControl()
        {
            int m_nr;
            m_nr = Convert.ToInt32(nudAdressValuePar.Value);

            obDirectArm.obPar56[m_nr]._1_redValue = Convert.ToByte(trbRedParJKLO.Value);
            obDirectArm.obPar56[m_nr]._2_greenValue = Convert.ToByte(trbGreenParJKLO.Value);
            obDirectArm.obPar56[m_nr]._3_blueValue = Convert.ToByte(trbBlueParJKLO.Value);
            obDirectArm.obPar56[m_nr]._4_macroSettingValue = Convert.ToByte(trbMacroParJKLO.Value);
            obDirectArm.obPar56[m_nr]._5_programSpeedValue = Convert.ToByte(trbSpeedParJKLO.Value);
            obDirectArm.obPar56[m_nr]._6_strobeValue = Convert.ToByte(trbStrobeParJKLO.Value);
        }

        private void handleParView()
        {
            int m_nr;
            m_nr = Convert.ToInt32(nudAdressValuePar.Value);

            lblRedValueParJKLO.Text = obDirectArm.obPar56[m_nr]._1_redValue.ToString();
            lblGreenValueParJKLO.Text = obDirectArm.obPar56[m_nr]._2_greenValue.ToString();
            lblBlueValueParJKLO.Text = obDirectArm.obPar56[m_nr]._3_blueValue.ToString();
            lblMacroValueParJKLO.Text = obDirectArm.obPar56[m_nr]._4_macroSettingValue.ToString();
            lblSpeedValueParJKLO.Text = obDirectArm.obPar56[m_nr]._5_programSpeedValue.ToString();
            lblStrobeValueParJKLO.Text = obDirectArm.obPar56[m_nr]._6_strobeValue.ToString();
        }

        private void handleParModel()
        {
            int m_nr;
            m_nr = Convert.ToInt32(nudAdressValuePar.Value);

            dmxControl.DmxLoadBuffer(1, obDirectArm.obPar56[m_nr]._1_redValue, 512);
            dmxControl.DmxLoadBuffer(2, obDirectArm.obPar56[m_nr]._2_greenValue, 512);
            dmxControl.DmxLoadBuffer(3, obDirectArm.obPar56[m_nr]._3_blueValue, 512);
            dmxControl.DmxLoadBuffer(4, obDirectArm.obPar56[m_nr]._4_macroSettingValue, 512);
            dmxControl.DmxLoadBuffer(5, obDirectArm.obPar56[m_nr]._5_programSpeedValue, 512);
            dmxControl.DmxLoadBuffer(6, obDirectArm.obPar56[m_nr]._6_strobeValue, 512);
        }

        private void handleColorModelGuiPar()
        {
            pnlColorParJKLO.BackColor = Color.FromArgb(obDirectArm.obPar56[0]._1_redValue,
                                                       obDirectArm.obPar56[0]._2_greenValue,
                                                       obDirectArm.obPar56[0]._3_blueValue);
        }

        private void trbRPar_ValueChanged(object sender, EventArgs e)
        {
            handleParControl();
            handleParView();
            handleParModel();
            handleColorModelGuiPar();

            handleColorControlPar();
            handleColorViewPar();
        }

        #endregion

        #region ParShowHandler

        private void btnSceneParJKLO_Click(object sender, EventArgs e)
        {
            arColorShow[storedSceneNrPar] = new csArmatureSettingsInScene();

            handleShowControlPar();
            handleShowViewPar();
            handleShowProgramLinesPar();

            storedSceneNrPar++;
        }

        public void handleShowControlPar()
        {
            arColorShow[storedSceneNrPar].obPar56[0].sceneWaitTime = Convert.ToInt32(tbWaitToNextSceneParJKLO.Text);

            arColorShow[storedSceneNrPar].obPar56[0]._1_redValue = obColorDirect.obPar56[0]._1_redValue;
            arColorShow[storedSceneNrPar].obPar56[0]._2_greenValue = obColorDirect.obPar56[0]._2_greenValue;
            arColorShow[storedSceneNrPar].obPar56[0]._3_blueValue = obColorDirect.obPar56[0]._3_blueValue;
            arColorShow[storedSceneNrPar].obPar56[0]._4_macroSettingValue = obColorDirect.obPar56[0]._4_macroSettingValue;
            arColorShow[storedSceneNrPar].obPar56[0]._5_programSpeedValue = obColorDirect.obPar56[0]._5_programSpeedValue;
            arColorShow[storedSceneNrPar].obPar56[0]._6_strobeValue = obColorDirect.obPar56[0]._6_strobeValue;
        }

        public void handleShowViewPar()
        {
            lblRedValueParJKLO.Text = obColorDirect.obPar56[0]._1_redValue.ToString("000");
            lblGreenValueParJKLO.Text = obColorDirect.obPar56[0]._2_greenValue.ToString("000");
            lblBlueValueParJKLO.Text = obColorDirect.obPar56[0]._3_blueValue.ToString("000");
            lblMacroValueParJKLO.Text = obColorDirect.obPar56[0]._4_macroSettingValue.ToString("000");
            lblSpeedValueParJKLO.Text = obColorDirect.obPar56[0]._5_programSpeedValue.ToString("000");
            lblStrobeValueParJKLO.Text = obColorDirect.obPar56[0]._6_strobeValue.ToString("000");

            trbRedParJKLO.Value = obColorDirect.obPar56[0]._1_redValue;
            trbGreenParJKLO.Value = obColorDirect.obPar56[0]._2_greenValue;
            trbBlueParJKLO.Value = obColorDirect.obPar56[0]._3_blueValue;
            trbMacroParJKLO.Value = obColorDirect.obPar56[0]._4_macroSettingValue;
            trbSpeedParJKLO.Value = obColorDirect.obPar56[0]._5_programSpeedValue;
            trbStrobeParJKLO.Value = obColorDirect.obPar56[0]._6_strobeValue;
        }

        public void handleShowProgramLinesPar()
        {
            rtbSceneLogParJKLO.AppendText(storedSceneNrPar.ToString("000") +
                    ";" + arColorShow[storedSceneNrPar].obPar56[0].sceneWaitTime.ToString("0000") +";" +
                    arColorShow[storedSceneNrPar].obPar56[0]._1_redValue.ToString("000") + ";" +
                    arColorShow[storedSceneNrPar].obPar56[0]._2_greenValue.ToString("000") + ";" +
                    arColorShow[storedSceneNrPar].obPar56[0]._3_blueValue.ToString("000") + ";" +
                    arColorShow[storedSceneNrPar].obPar56[0]._4_macroSettingValue.ToString("000") + ";" +
                    arColorShow[storedSceneNrPar].obPar56[0]._5_programSpeedValue.ToString("000") + ";" +
                    arColorShow[storedSceneNrPar].obPar56[0]._6_strobeValue.ToString("000") + "\n");
        }

        public void handleColorControlPar()
        {
            obColorDirect.obPar56[0]._1_redValue = Convert.ToByte(trbRedParJKLO.Value);
            obColorDirect.obPar56[0]._2_greenValue = Convert.ToByte(trbGreenParJKLO.Value);
            obColorDirect.obPar56[0]._3_blueValue = Convert.ToByte(trbBlueParJKLO.Value);
            obColorDirect.obPar56[0]._4_macroSettingValue = Convert.ToByte(trbMacroParJKLO.Value);
            obColorDirect.obPar56[0]._5_programSpeedValue = Convert.ToByte(trbSpeedParJKLO.Value);
            obColorDirect.obPar56[0]._6_strobeValue = Convert.ToByte(trbStrobeParJKLO.Value);
        }

        public void handleColorViewPar()
        {
            lblRedValueParJKLO.Text = obColorDirect.obPar56[0]._1_redValue.ToString("000");
            lblGreenValueParJKLO.Text = obColorDirect.obPar56[0]._2_greenValue.ToString("000");
            lblBlueValueParJKLO.Text = obColorDirect.obPar56[0]._3_blueValue.ToString("000");
            lblMacroValueParJKLO.Text = obColorDirect.obPar56[0]._4_macroSettingValue.ToString("000");
            lblSpeedValueParJKLO.Text = obColorDirect.obPar56[0]._5_programSpeedValue.ToString("000");
            lblStrobeValueParJKLO.Text = obColorDirect.obPar56[0]._6_strobeValue.ToString("000");
        }

        public void handleShowModelPar()
        {
            tmrShowParJKLO.Interval = arColorShow[playedSceneNrPar].obPar56[0].sceneWaitTime;

            obColorDirect.obPar56[0]._1_redValue = arColorShow[playedSceneNrPar].obPar56[0]._1_redValue;
            obColorDirect.obPar56[0]._2_greenValue = arColorShow[playedSceneNrPar].obPar56[0]._2_greenValue;
            obColorDirect.obPar56[0]._3_blueValue = arColorShow[playedSceneNrPar].obPar56[0]._3_blueValue;
            obColorDirect.obPar56[0]._4_macroSettingValue = arColorShow[playedSceneNrPar].obPar56[0]._4_macroSettingValue;
            obColorDirect.obPar56[0]._5_programSpeedValue = arColorShow[playedSceneNrPar].obPar56[0]._5_programSpeedValue;
            obColorDirect.obPar56[0]._6_strobeValue = arColorShow[playedSceneNrPar].obPar56[0]._6_strobeValue;
        }

        private void btnRunParJKLO_Click_1(object sender, EventArgs e)
        {
            tmrShowParJKLO.Start();
        }

        #endregion

        #region AyraHandler

        private void handleAyraControl()
        {
            int m_nr;
            m_nr = Convert.ToInt32(nudAdressValueAyraJKLO.Value);

            obDirectArm.obAyraFx[m_nr]._8_masterDimmer = Convert.ToByte(trbMasterDimmerAyraJKLO.Value);
            obDirectArm.obAyraFx[m_nr]._9_strobeFastToSlow = Convert.ToByte(trbStrobeAyraJKLO.Value);
            obDirectArm.obAyraFx[m_nr]._10_pan = Convert.ToByte(trbPanAyraJKLO.Value);
            obDirectArm.obAyraFx[m_nr]._11_panFine = Convert.ToByte(trbPanFineAyraJKLO.Value);
            obDirectArm.obAyraFx[m_nr]._12_tilt = Convert.ToByte(trbTiltAyraJKLO.Value);
            obDirectArm.obAyraFx[m_nr]._13_tiltFine = Convert.ToByte(trbTiltFineAyraJKLO.Value);
            obDirectArm.obAyraFx[m_nr]._14_panTiltSpeed = Convert.ToByte(trbPanTiltSpeedAyraJKLO.Value);
            obDirectArm.obAyraFx[m_nr]._15_redBeam = Convert.ToByte(trbRedBeamAyraJKLO.Value);
            obDirectArm.obAyraFx[m_nr]._16_greenBeam = Convert.ToByte(trbGreenBeamAyraJKLO.Value);
            obDirectArm.obAyraFx[m_nr]._17_blueBeam = Convert.ToByte(trbBlueBeamAyraJKLO.Value);
            obDirectArm.obAyraFx[m_nr]._18_white = Convert.ToByte(trbWhiteBeamAyraJKLO.Value);
            obDirectArm.obAyraFx[m_nr]._19_redRing = Convert.ToByte(trbRedRingAyraJKLO.Value);
            obDirectArm.obAyraFx[m_nr]._20_greenRing = Convert.ToByte(trbGreenRingAyraJKLO.Value);
            obDirectArm.obAyraFx[m_nr]._21_blueRing = Convert.ToByte(trbBlueRingAyraJKLO.Value);
            obDirectArm.obAyraFx[m_nr]._22_DmxMode = Convert.ToByte(trbDmxModeAyraJKLO.Value);
            obDirectArm.obAyraFx[m_nr]._23_autoShow = Convert.ToByte(trbAutoShowAyraJKLO.Value);
        }

        private void handleAyraView()
        {
            int m_nr;
            m_nr = Convert.ToInt32(nudAdressValueAyraJKLO.Value);

            lblMasterDimmerValueAyraJKLO.Text = obDirectArm.obAyraFx[m_nr]._8_masterDimmer.ToString();
            lblStrobeValueAyraJKLO.Text = obDirectArm.obAyraFx[m_nr]._9_strobeFastToSlow.ToString();
            lblPanValueAyraJKLO.Text = obDirectArm.obAyraFx[m_nr]._10_pan.ToString();
            lblPanFineValueAyraJKLO.Text = obDirectArm.obAyraFx[m_nr]._11_panFine.ToString();
            lblTiltValueAyraJKLO.Text = obDirectArm.obAyraFx[m_nr]._12_tilt.ToString();
            lblTiltFineValueAyraJKLO.Text = obDirectArm.obAyraFx[m_nr]._13_tiltFine.ToString();
            lblPanTiltSpeedValueAyraJKLO.Text = obDirectArm.obAyraFx[m_nr]._14_panTiltSpeed.ToString();
            lblRedBeamValueAyraJKLO.Text = obDirectArm.obAyraFx[m_nr]._15_redBeam.ToString();
            lblGreenBeamValueAyraJKLO.Text = obDirectArm.obAyraFx[m_nr]._16_greenBeam.ToString();
            lblBlueBeamValueAyraJKLO.Text = obDirectArm.obAyraFx[m_nr]._17_blueBeam.ToString();
            lblWhiteBeamValueAyraJKLO.Text = obDirectArm.obAyraFx[m_nr]._18_white.ToString();
            lblRedRingValueAyraJKLO.Text = obDirectArm.obAyraFx[m_nr]._19_redRing.ToString();
            lblGreenRingValueAyraJKLO.Text = obDirectArm.obAyraFx[m_nr]._20_greenRing.ToString();
            lblBlueRingValueAyraJKLO.Text = obDirectArm.obAyraFx[m_nr]._21_blueRing.ToString();
            lblDMXModeValueAyraJKLO.Text = obDirectArm.obAyraFx[m_nr]._22_DmxMode.ToString();
            lblAutoShowValueAyraJKLO.Text = obDirectArm.obAyraFx[m_nr]._23_autoShow.ToString();
        }

        private void handleAyraModel()
        {
            int m_nr;
            m_nr = Convert.ToInt32(nudAdressValueAyraJKLO.Value);

            dmxControl.DmxLoadBuffer(8, obDirectArm.obAyraFx[m_nr]._8_masterDimmer, 512);
            dmxControl.DmxLoadBuffer(9, obDirectArm.obAyraFx[m_nr]._9_strobeFastToSlow, 512);
            dmxControl.DmxLoadBuffer(10, obDirectArm.obAyraFx[m_nr]._10_pan, 512);
            dmxControl.DmxLoadBuffer(11, obDirectArm.obAyraFx[m_nr]._11_panFine, 512);
            dmxControl.DmxLoadBuffer(12, obDirectArm.obAyraFx[m_nr]._12_tilt, 512);
            dmxControl.DmxLoadBuffer(13, obDirectArm.obAyraFx[m_nr]._13_tiltFine, 512);
            dmxControl.DmxLoadBuffer(14, obDirectArm.obAyraFx[m_nr]._14_panTiltSpeed, 512);
            dmxControl.DmxLoadBuffer(15, obDirectArm.obAyraFx[m_nr]._15_redBeam, 512);
            dmxControl.DmxLoadBuffer(16, obDirectArm.obAyraFx[m_nr]._16_greenBeam, 512);
            dmxControl.DmxLoadBuffer(17, obDirectArm.obAyraFx[m_nr]._17_blueBeam, 512);
            dmxControl.DmxLoadBuffer(18, obDirectArm.obAyraFx[m_nr]._18_white, 512);
            dmxControl.DmxLoadBuffer(19, obDirectArm.obAyraFx[m_nr]._19_redRing, 512);
            dmxControl.DmxLoadBuffer(20, obDirectArm.obAyraFx[m_nr]._20_greenRing, 512);
            dmxControl.DmxLoadBuffer(21, obDirectArm.obAyraFx[m_nr]._21_blueRing, 512);
            dmxControl.DmxLoadBuffer(22, obDirectArm.obAyraFx[m_nr]._22_DmxMode, 512);
            dmxControl.DmxLoadBuffer(23, obDirectArm.obAyraFx[m_nr]._23_autoShow, 512);
        }

        private void handleColorModelGuiAyra()
        {
            pnlRingAyraJKLO.BackColor = Color.FromArgb(obDirectArm.obAyraFx[0]._19_redRing,
                                                       obDirectArm.obAyraFx[0]._20_greenRing,
                                                       obDirectArm.obAyraFx[0]._21_blueRing);

            pnlBeamAyraJKLO.BackColor = Color.FromArgb(obDirectArm.obAyraFx[0]._15_redBeam,
                                                       obDirectArm.obAyraFx[0]._16_greenBeam,
                                                       obDirectArm.obAyraFx[0]._17_blueBeam);
        }

        private void trbMasterDimmerAyraJKLO_ValueChanged(object sender, EventArgs e)
        {
            handleAyraControl();
            handleAyraView();
            handleAyraModel();
            handleColorModelGuiAyra();

            handleColorControlAyra();
            handleColorViewAyra();
        }

        private void btnResetAyraJKLO_Click(object sender, EventArgs e)
        {
            trbMasterDimmerAyraJKLO.Value = 0;
            trbRedRingAyraJKLO.Value = 0;
            trbGreenRingAyraJKLO.Value = 0;
            trbBlueRingAyraJKLO.Value = 0;
            trbWhiteBeamAyraJKLO.Value = 0;
            trbRedBeamAyraJKLO.Value = 0;
            trbGreenBeamAyraJKLO.Value = 0;
            trbBlueBeamAyraJKLO.Value = 0;
            trbPanAyraJKLO.Value = 0;
            trbPanFineAyraJKLO.Value = 0;
            trbPanTiltSpeedAyraJKLO.Value = 0;
            trbTiltAyraJKLO.Value = 0;
            trbTiltFineAyraJKLO.Value = 0;
            trbStrobeAyraJKLO.Value = 0;
            trbDmxModeAyraJKLO.Value = 0;
            trbAutoShowAyraJKLO.Value = 0;

            rtbSceneLogAyraJKLO.Clear();
            pnlBeamAyraJKLO.BackColor = Color.White;
            pnlRingAyraJKLO.BackColor = Color.White;
        }

        private void btnResetParJKLO_Click(object sender, EventArgs e)
        {
            trbRedParJKLO.Value = 0;
            trbGreenParJKLO.Value = 0;
            trbBlueParJKLO.Value = 0;
            trbMacroParJKLO.Value = 0;
            trbSpeedParJKLO.Value = 0;
            trbStrobeParJKLO.Value = 0;

            rtbSceneLogParJKLO.Clear();
        }

        private void tmrShowJKLO_Tick(object sender, EventArgs e)
        {
            handleShowModelPar();
            handleShowViewPar();

            playedSceneNrPar++;

            if (playedSceneNrPar >= storedSceneNrPar)
            {
                playedSceneNrPar = 0;

                if (cbxLoopParJKLO.Checked == false)
                {
                    tmrShowParJKLO.Stop();
                }
            }
        }
        #endregion

        #region AyraShowHandler

        private void btnSceneAyraJKLO_Click(object sender, EventArgs e)
        {
            arColorShow[storedSceneNrAyra] = new csArmatureSettingsInScene();

            handleShowControlAyra();
            handleShowViewAyra();
            handleShowProgramLinesAyra();

            storedSceneNrAyra++;
        }

        public void handleShowControlAyra()
        {
            arColorShow[storedSceneNrAyra].sceneWaitTime = Convert.ToInt32(tbWaitToNextSceneAyraJKLO.Text);

            arColorShow[storedSceneNrAyra].obAyraFx[0]._8_masterDimmer = obColorDirect.obAyraFx[0]._8_masterDimmer;
            arColorShow[storedSceneNrAyra].obAyraFx[0]._9_strobeFastToSlow = obColorDirect.obAyraFx[0]._9_strobeFastToSlow;
            arColorShow[storedSceneNrAyra].obAyraFx[0]._10_pan = obColorDirect.obAyraFx[0]._10_pan;
            arColorShow[storedSceneNrAyra].obAyraFx[0]._11_panFine = obColorDirect.obAyraFx[0]._11_panFine;
            arColorShow[storedSceneNrAyra].obAyraFx[0]._12_tilt = obColorDirect.obAyraFx[0]._12_tilt;
            arColorShow[storedSceneNrAyra].obAyraFx[0]._13_tiltFine = obColorDirect.obAyraFx[0]._13_tiltFine;
            arColorShow[storedSceneNrAyra].obAyraFx[0]._14_panTiltSpeed = obColorDirect.obAyraFx[0]._14_panTiltSpeed;
            arColorShow[storedSceneNrAyra].obAyraFx[0]._15_redBeam = obColorDirect.obAyraFx[0]._15_redBeam;
            arColorShow[storedSceneNrAyra].obAyraFx[0]._16_greenBeam = obColorDirect.obAyraFx[0]._16_greenBeam;
            arColorShow[storedSceneNrAyra].obAyraFx[0]._17_blueBeam = obColorDirect.obAyraFx[0]._17_blueBeam;
            arColorShow[storedSceneNrAyra].obAyraFx[0]._18_white = obColorDirect.obAyraFx[0]._18_white;
            arColorShow[storedSceneNrAyra].obAyraFx[0]._19_redRing = obColorDirect.obAyraFx[0]._19_redRing;
            arColorShow[storedSceneNrAyra].obAyraFx[0]._20_greenRing = obColorDirect.obAyraFx[0]._20_greenRing;
            arColorShow[storedSceneNrAyra].obAyraFx[0]._21_blueRing = obColorDirect.obAyraFx[0]._21_blueRing;
            arColorShow[storedSceneNrAyra].obAyraFx[0]._22_DmxMode = obColorDirect.obAyraFx[0]._22_DmxMode;
            arColorShow[storedSceneNrAyra].obAyraFx[0]._23_autoShow = obColorDirect.obAyraFx[0]._23_autoShow;
        }

        public void handleShowViewAyra()
        {
            lblMasterDimmerValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._8_masterDimmer.ToString("000");
            lblRedRingValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._19_redRing.ToString("000");
            lblGreenRingValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._20_greenRing.ToString("000");
            lblBlueRingValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._21_blueRing.ToString("000");
            lblWhiteBeamValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._18_white.ToString("000");
            lblRedBeamValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._15_redBeam.ToString("000");
            lblGreenBeamValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._16_greenBeam.ToString("000");
            lblBlueBeamValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._17_blueBeam.ToString("000");
            lblPanValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._10_pan.ToString("000");
            lblPanFineValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._11_panFine.ToString("000");
            lblPanTiltSpeedValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._14_panTiltSpeed.ToString("000");
            lblTiltValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._12_tilt.ToString("000");
            lblTiltFineValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._13_tiltFine.ToString("000");
            lblStrobeValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._9_strobeFastToSlow.ToString("000");
            lblDMXModeValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._22_DmxMode.ToString("000");
            lblAutoShowValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._23_autoShow.ToString("000");

            trbMasterDimmerAyraJKLO.Value = obColorDirect.obAyraFx[0]._8_masterDimmer;
            trbRedRingAyraJKLO.Value = obColorDirect.obAyraFx[0]._19_redRing;
            trbGreenRingAyraJKLO.Value = obColorDirect.obAyraFx[0]._20_greenRing;
            trbBlueRingAyraJKLO.Value = obColorDirect.obAyraFx[0]._21_blueRing;
            trbWhiteBeamAyraJKLO.Value = obColorDirect.obAyraFx[0]._18_white;
            trbRedBeamAyraJKLO.Value = obColorDirect.obAyraFx[0]._15_redBeam;
            trbGreenBeamAyraJKLO.Value = obColorDirect.obAyraFx[0]._16_greenBeam;
            trbBlueBeamAyraJKLO.Value = obColorDirect.obAyraFx[0]._17_blueBeam;
            trbPanAyraJKLO.Value = obColorDirect.obAyraFx[0]._10_pan;
            trbPanFineAyraJKLO.Value = obColorDirect.obAyraFx[0]._11_panFine;
            trbPanTiltSpeedAyraJKLO.Value = obColorDirect.obAyraFx[0]._14_panTiltSpeed;
            trbTiltAyraJKLO.Value = obColorDirect.obAyraFx[0]._12_tilt;
            trbTiltFineAyraJKLO.Value = obColorDirect.obAyraFx[0]._13_tiltFine;
            trbStrobeAyraJKLO.Value = obColorDirect.obAyraFx[0]._9_strobeFastToSlow;
            trbDmxModeAyraJKLO.Value = obColorDirect.obAyraFx[0]._22_DmxMode;
            trbAutoShowAyraJKLO.Value = obColorDirect.obAyraFx[0]._23_autoShow;
        }

        public void handleShowProgramLinesAyra()
        {
            rtbSceneLogAyraJKLO.AppendText("Scene: " + storedSceneNrAyra.ToString("000") + " > " +
                    "next after: " + arColorShow[storedSceneNrAyra].sceneWaitTime.ToString("0000") + "ms > " +
                    arColorShow[storedSceneNrAyra].obAyraFx[0]._8_masterDimmer.ToString("000") + " > " +
                    arColorShow[storedSceneNrAyra].obAyraFx[0]._19_redRing.ToString("000") + " > " +
                    arColorShow[storedSceneNrAyra].obAyraFx[0]._20_greenRing.ToString("000") + " > " +
                    arColorShow[storedSceneNrAyra].obAyraFx[0]._21_blueRing.ToString("000") + " > " +
                    arColorShow[storedSceneNrAyra].obAyraFx[0]._18_white.ToString("000") + " > " +
                    arColorShow[storedSceneNrAyra].obAyraFx[0]._15_redBeam.ToString("000") + " > " +
                    arColorShow[storedSceneNrAyra].obAyraFx[0]._16_greenBeam.ToString("000") + " > " +
                    arColorShow[storedSceneNrAyra].obAyraFx[0]._17_blueBeam.ToString("000") + " > " +
                    arColorShow[storedSceneNrAyra].obAyraFx[0]._10_pan.ToString("000") + " > " +
                    arColorShow[storedSceneNrAyra].obAyraFx[0]._11_panFine.ToString("000") + " > " +
                    arColorShow[storedSceneNrAyra].obAyraFx[0]._14_panTiltSpeed.ToString("000") + " > " +
                    arColorShow[storedSceneNrAyra].obAyraFx[0]._12_tilt.ToString("000") + " > " +
                    arColorShow[storedSceneNrAyra].obAyraFx[0]._13_tiltFine.ToString("000") + " > " +
                    arColorShow[storedSceneNrAyra].obAyraFx[0]._9_strobeFastToSlow.ToString("000") + " > " +
                    arColorShow[storedSceneNrAyra].obAyraFx[0]._22_DmxMode.ToString("000") + " > " +
                    arColorShow[storedSceneNrAyra].obAyraFx[0]._23_autoShow.ToString("000") + "\n");
        }

        public void handleColorControlAyra()
        {
            obColorDirect.obAyraFx[0]._8_masterDimmer = Convert.ToByte(trbMasterDimmerAyraJKLO.Value);
            obColorDirect.obAyraFx[0]._9_strobeFastToSlow = Convert.ToByte(trbStrobeAyraJKLO.Value);
            obColorDirect.obAyraFx[0]._10_pan = Convert.ToByte(trbPanAyraJKLO.Value);
            obColorDirect.obAyraFx[0]._11_panFine = Convert.ToByte(trbPanFineAyraJKLO.Value);
            obColorDirect.obAyraFx[0]._12_tilt = Convert.ToByte(trbTiltAyraJKLO.Value);
            obColorDirect.obAyraFx[0]._13_tiltFine = Convert.ToByte(trbTiltFineAyraJKLO.Value);
            obColorDirect.obAyraFx[0]._14_panTiltSpeed = Convert.ToByte(trbPanTiltSpeedAyraJKLO.Value);
            obColorDirect.obAyraFx[0]._15_redBeam = Convert.ToByte(trbRedBeamAyraJKLO.Value);
            obColorDirect.obAyraFx[0]._16_greenBeam = Convert.ToByte(trbGreenBeamAyraJKLO.Value);
            obColorDirect.obAyraFx[0]._17_blueBeam = Convert.ToByte(trbBlueBeamAyraJKLO.Value);
            obColorDirect.obAyraFx[0]._18_white = Convert.ToByte(trbWhiteBeamAyraJKLO.Value);
            obColorDirect.obAyraFx[0]._19_redRing = Convert.ToByte(trbRedRingAyraJKLO.Value);
            obColorDirect.obAyraFx[0]._20_greenRing = Convert.ToByte(trbGreenRingAyraJKLO.Value);
            obColorDirect.obAyraFx[0]._21_blueRing = Convert.ToByte(trbBlueRingAyraJKLO.Value);
            obColorDirect.obAyraFx[0]._22_DmxMode = Convert.ToByte(trbDmxModeAyraJKLO.Value);
            obColorDirect.obAyraFx[0]._23_autoShow = Convert.ToByte(trbAutoShowAyraJKLO.Value);
        }

        public void handleColorViewAyra()
        {
            lblMasterDimmerValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._8_masterDimmer.ToString("000");
            lblRedRingValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._19_redRing.ToString("000");
            lblGreenRingValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._20_greenRing.ToString("000");
            lblBlueRingValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._21_blueRing.ToString("000");
            lblWhiteBeamValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._18_white.ToString("000");
            lblRedBeamValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._15_redBeam.ToString("000");
            lblGreenBeamValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._16_greenBeam.ToString("000");
            lblBlueBeamValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._17_blueBeam.ToString("000");
            lblPanValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._10_pan.ToString("000");
            lblPanFineValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._11_panFine.ToString("000");
            lblPanTiltSpeedValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._14_panTiltSpeed.ToString("000");
            lblTiltValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._12_tilt.ToString("000");
            lblTiltFineValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._13_tiltFine.ToString("000");
            lblStrobeValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._9_strobeFastToSlow.ToString("000");
            lblDMXModeValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._22_DmxMode.ToString("000");
            lblAutoShowValueAyraJKLO.Text = obColorDirect.obAyraFx[0]._23_autoShow.ToString("000");
        }

        public void handleShowModelAyra()
        {
            tmrShowAyraJKLO.Interval = arColorShow[playedSceneNrAyra].sceneWaitTime;

            obColorDirect.obAyraFx[0]._8_masterDimmer = arColorShow[playedSceneNrAyra].obAyraFx[0]._8_masterDimmer;
            obColorDirect.obAyraFx[0]._19_redRing = arColorShow[playedSceneNrAyra].obAyraFx[0]._19_redRing;
            obColorDirect.obAyraFx[0]._20_greenRing = arColorShow[playedSceneNrAyra].obAyraFx[0]._20_greenRing;
            obColorDirect.obAyraFx[0]._21_blueRing = arColorShow[playedSceneNrAyra].obAyraFx[0]._21_blueRing;
            obColorDirect.obAyraFx[0]._18_white = arColorShow[playedSceneNrAyra].obAyraFx[0]._18_white;
            obColorDirect.obAyraFx[0]._15_redBeam = arColorShow[playedSceneNrAyra].obAyraFx[0]._15_redBeam;
            obColorDirect.obAyraFx[0]._16_greenBeam = arColorShow[playedSceneNrAyra].obAyraFx[0]._16_greenBeam;
            obColorDirect.obAyraFx[0]._17_blueBeam = arColorShow[playedSceneNrAyra].obAyraFx[0]._17_blueBeam;
            obColorDirect.obAyraFx[0]._10_pan = arColorShow[playedSceneNrAyra].obAyraFx[0]._10_pan;
            obColorDirect.obAyraFx[0]._11_panFine = arColorShow[playedSceneNrAyra].obAyraFx[0]._11_panFine;
            obColorDirect.obAyraFx[0]._14_panTiltSpeed = arColorShow[playedSceneNrAyra].obAyraFx[0]._14_panTiltSpeed;
            obColorDirect.obAyraFx[0]._12_tilt = arColorShow[playedSceneNrAyra].obAyraFx[0]._12_tilt;
            obColorDirect.obAyraFx[0]._13_tiltFine = arColorShow[playedSceneNrAyra].obAyraFx[0]._13_tiltFine;
            obColorDirect.obAyraFx[0]._9_strobeFastToSlow = arColorShow[playedSceneNrAyra].obAyraFx[0]._9_strobeFastToSlow;
            obColorDirect.obAyraFx[0]._22_DmxMode = arColorShow[playedSceneNrAyra].obAyraFx[0]._22_DmxMode;
            obColorDirect.obAyraFx[0]._23_autoShow = arColorShow[playedSceneNrAyra].obAyraFx[0]._23_autoShow;
        }

        private void btnRunAyraJKLO_Click(object sender, EventArgs e)
        {
            tmrShowAyraJKLO.Start();
        }

        private void tmrShowAyraJKLO_Tick(object sender, EventArgs e)
        {
            handleShowModelAyra();
            handleShowViewAyra();

            playedSceneNrAyra++;

            if (playedSceneNrAyra >= storedSceneNrAyra)
            {
                playedSceneNrAyra = 0;

                if (cbxLoopAyraJKLO.Checked == false)
                {
                    tmrShowAyraJKLO.Stop();
                }
            }
        }

        #endregion

        #region FileHandlerPar

        private void btnSaveParJKLO_Click(object sender, EventArgs e)
        {
            diaFileSaveJKLO.FileName = "MasterShowOne - Show.txt";
            diaFileSaveJKLO.Filter = "Text file|*.txt";
            diaFileSaveJKLO.ShowDialog();
            relativeFileDirectoryPar = diaFileSaveJKLO.FileName;

            storedFileNamePar = Path.GetFileName(relativeFileDirectoryPar);

            writeScenesPar();
        }

        private void writeScenesPar()
        {
            StreamWriter SceneRecorder = new StreamWriter(relativeFileDirectoryPar, false);

            SceneRecorder.WriteLine(rtbSceneLogParJKLO.Text);    

            SceneRecorder.Close();

        }

        private void btnLoadScenePar_Click(object sender, EventArgs e)
        {
            string m_filenameAndLocationJKLO = "";

            diaFileOpenJKLO.Filter = "Excel import |*.csv |Text file | *.txt | All files(*.*)|*.*";
            diaFileOpenJKLO.ShowDialog();
            m_filenameAndLocationJKLO = diaFileOpenJKLO.FileName;

            ReadFilePar(m_filenameAndLocationJKLO);
        }

        private void ReadFilePar(string a_relativeFileDirectory)
        {
            string csvData = "";
            StreamReader showProgram = new StreamReader(a_relativeFileDirectory);

            showProgram.ReadLine();

            rtbSceneLogParJKLO.Clear();
            storedSceneNrPar = 0;

            while(showProgram.EndOfStream == false)
            {
                arColorShow[storedSceneNrPar] = new csArmatureSettingsInScene();

                csvData = showProgram.ReadLine();
                AddCsvDataToShowScenesPar(csvData);

                /*HandleShowProgramLines();*/

                storedSceneNrPar++;
            }

            showProgram.Close();
        }

        private void AddCsvDataToShowScenesPar(string a_csvData)
        {
            String[] m_csvDataMember;

            m_csvDataMember = a_csvData.Split(';');

                arColorShow[storedSceneNrPar].obPar56[0].DmxAddress = Convert.ToByte(m_csvDataMember[0]);
                arColorShow[storedSceneNrPar].obPar56[0].sceneWaitTime = Convert.ToInt32(m_csvDataMember[1]);
                arColorShow[storedSceneNrPar].obPar56[0]._1_redValue = Convert.ToByte(m_csvDataMember[2]);
                arColorShow[storedSceneNrPar].obPar56[0]._2_greenValue = Convert.ToByte(m_csvDataMember[3]);
                arColorShow[storedSceneNrPar].obPar56[0]._3_blueValue = Convert.ToByte(m_csvDataMember[4]);
                arColorShow[storedSceneNrPar].obPar56[0]._4_macroSettingValue = Convert.ToByte(m_csvDataMember[5]);
                arColorShow[storedSceneNrPar].obPar56[0]._5_programSpeedValue = Convert.ToByte(m_csvDataMember[6]);
                arColorShow[storedSceneNrPar].obPar56[0]._6_strobeValue = Convert.ToByte(m_csvDataMember[7]);
        }

        #endregion

        #region FileHandlerAyra

        private void btnSaveAyraJKLO_Click(object sender, EventArgs e)
        {
            diaFileSaveJKLO.FileName = "MasterShowOne - Show.txt";
            diaFileSaveJKLO.Filter = "Text file|*.txt";
            diaFileSaveJKLO.ShowDialog();
            relativeFileDirectoryAyra = diaFileSaveJKLO.FileName;

            storedFileNameAyra = Path.GetFileName(relativeFileDirectoryAyra);

            writeScenesAyra();
        }

        private void writeScenesAyra()
        {
            StreamWriter SceneRecorder = new StreamWriter(relativeFileDirectoryAyra, false);

            SceneRecorder.WriteLine(rtbSceneLogAyraJKLO.Text);

            SceneRecorder.Close();

        }

        private void btnLoadSceneAyra_Click(object sender, EventArgs e)
        {
            string m_filenameAndLocationJKLO = "";

            diaFileOpenJKLO.Filter = "Excel import |*.csv |Text file | *.txt | All files(*.*)|*.*";
            diaFileOpenJKLO.ShowDialog();
            m_filenameAndLocationJKLO = diaFileOpenJKLO.FileName;

            ReadFileAyra(m_filenameAndLocationJKLO);
        }

        private void ReadFileAyra(string a_relativeFileDirectory)
        {
            string csvData = "";
            StreamReader showProgram = new StreamReader(a_relativeFileDirectory);

            showProgram.ReadLine();

            rtbSceneLogAyraJKLO.Clear();
            storedSceneNrAyra = 0;

            while (showProgram.EndOfStream == false)
            {
                arColorShow[storedSceneNrAyra] = new csArmatureSettingsInScene();

                csvData = showProgram.ReadLine();
                AddCsvDataToShowScenesAyra(csvData);

                /*HandleShowProgramLines();*/

                storedSceneNrAyra++;
            }

            showProgram.Close();
        }

        private void AddCsvDataToShowScenesAyra(string a_csvData)
        {
            String[] m_csvDataMember;

            m_csvDataMember = a_csvData.Split(';');

            arColorShow[storedSceneNrPar].obAyraFx[0].DmxAddress = Convert.ToByte(m_csvDataMember[0]);
            arColorShow[storedSceneNrPar].obAyraFx[0]._8_masterDimmer = Convert.ToByte(m_csvDataMember[1]);
            arColorShow[storedSceneNrPar].obAyraFx[0]._9_strobeFastToSlow = Convert.ToByte(m_csvDataMember[2]);
            arColorShow[storedSceneNrPar].obAyraFx[0]._10_pan = Convert.ToByte(m_csvDataMember[3]);
            arColorShow[storedSceneNrPar].obAyraFx[0]._11_panFine = Convert.ToByte(m_csvDataMember[4]);
            arColorShow[storedSceneNrPar].obAyraFx[0]._12_tilt = Convert.ToByte(m_csvDataMember[5]);
            arColorShow[storedSceneNrPar].obAyraFx[0]._13_tiltFine = Convert.ToByte(m_csvDataMember[6]);
            arColorShow[storedSceneNrPar].obAyraFx[0]._14_panTiltSpeed = Convert.ToByte(m_csvDataMember[7]);
            arColorShow[storedSceneNrPar].obAyraFx[0]._15_redBeam = Convert.ToByte(m_csvDataMember[7]);
            arColorShow[storedSceneNrPar].obAyraFx[0]._16_greenBeam = Convert.ToByte(m_csvDataMember[7]);
            arColorShow[storedSceneNrPar].obAyraFx[0]._17_blueBeam = Convert.ToByte(m_csvDataMember[7]);
            arColorShow[storedSceneNrPar].obAyraFx[0]._18_white = Convert.ToByte(m_csvDataMember[7]);
            arColorShow[storedSceneNrPar].obAyraFx[0]._19_redRing = Convert.ToByte(m_csvDataMember[7]);
            arColorShow[storedSceneNrPar].obAyraFx[0]._20_greenRing = Convert.ToByte(m_csvDataMember[7]);
            arColorShow[storedSceneNrPar].obAyraFx[0]._21_blueRing = Convert.ToByte(m_csvDataMember[7]);
            arColorShow[storedSceneNrPar].obAyraFx[0]._22_DmxMode = Convert.ToByte(m_csvDataMember[7]);
            arColorShow[storedSceneNrPar].obAyraFx[0]._23_autoShow = Convert.ToByte(m_csvDataMember[7]);
        }

        #endregion

       
    }
}
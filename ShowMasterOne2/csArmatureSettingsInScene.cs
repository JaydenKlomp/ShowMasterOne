using ShowMasterOne2.Show;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPR_ShowMasterOne_22SD_AssignmentPrep
{
    internal class csArmatureSettingsInScene
    {
        public int sceneWaitTime;
        public csPar56R[] obPar56 = new csPar56R[10];
        public csAyraFx[] obAyraFx = new csAyraFx[10];

        public csArmatureSettingsInScene()
        {
            int m_par56Count;
            int m_AyraFxCount;

            m_par56Count = obPar56.Length;

            for (int i = 0; i < m_par56Count; i++)
            {
                obPar56[i] = new csPar56R("PAR56", i);
            }

            m_AyraFxCount = obAyraFx.Length;

            for (int i = 0; i < m_AyraFxCount; i++)
            {
                obAyraFx[i] = new csAyraFx("AyraFx", i);
            }
        }


    }
}

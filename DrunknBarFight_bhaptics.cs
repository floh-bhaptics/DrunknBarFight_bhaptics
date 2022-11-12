using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MelonLoader;
using HarmonyLib;
using MyBhapticsTactsuit;

namespace DrunknBarFight_bhaptics
{
    public class DrunknBarFight_bhaptics : MelonMod
    {
        public static TactsuitVR tactsuitVr;

        public override void OnInitializeMelon()
        {
            tactsuitVr = new TactsuitVR();
            tactsuitVr.PlaybackHaptics("HeartBeat");
        }

        [HarmonyPatch(typeof(GameManager), "OnBossDefeat", new Type[] { })]
        public class bhaptics_BossDefeat
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.StopThreads();
            }
        }

        [HarmonyPatch(typeof(PlayerBip), "startPlayHeartBeat", new Type[] { typeof(int) })]
        public class bhaptics_StartHeartBeat
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.StopThreads();
            }
        }


        [HarmonyPatch(typeof(PlayerBip), "startHurt", new Type[] { typeof(CartWheelCore.Control.HitFeedback), typeof(bool), typeof(UnityEngine.GameObject), typeof(bool) })]
        public class bhaptics_StartHurt
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.StopThreads();
            }
        }

        [HarmonyPatch(typeof(PlayerBip), "endHurt", new Type[] {  })]
        public class bhaptics_EndHurt
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.StopThreads();
            }
        }

    }
}

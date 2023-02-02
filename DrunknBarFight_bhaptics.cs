using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MelonLoader;
using HarmonyLib;
using MyBhapticsTactsuit;
using Photon.Bolt;

[assembly: MelonInfo(typeof(DrunknBarFight_bhaptics.DrunknBarFight_bhaptics), "DrunknBarFight_bhaptics", "2.0.0", "Florian Fahrenberger")]
[assembly: MelonGame("KeithKurby", "Drunkn Bar Fight")]

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
                tactsuitVr.PlaybackHaptics("BossDefeat");
            }
        }

        [HarmonyPatch(typeof(GameManager), "EndGame", new Type[] { typeof(bool) })]
        public class bhaptics_EndGame
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.StopThreads();
            }
        }


        [HarmonyPatch(typeof(GameManager), "OnPlayerKnockOut", new Type[] { typeof(PlayerBip) })]
        public class bhaptics_PlayerKnockout
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.PlaybackHaptics("Impact");
            }
        }

        [HarmonyPatch(typeof(GameManager), "OnPlayerHitFemaleEnemy", new Type[] {  })]
        public class bhaptics_HitFemale
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.PlaybackHaptics("HitFemale");
            }
        }

        [HarmonyPatch(typeof(GameManager), "playerMakeDrunkEvent", new Type[] { typeof(PlayerBip) })]
        public class bhaptics_MakeDrunk
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.PlaybackHaptics("GetDrunk");
            }
        }

        [HarmonyPatch(typeof(GameManager), "StartCountdownPlayerKO", new Type[] { })]
        public class bhaptics_PlayerKO
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.StartHeartBeat();
            }
        }

        [HarmonyPatch(typeof(GameManager), "stopCountdown", new Type[] { })]
        public class bhaptics_StopCountdown
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.StopHeartBeat();
            }
        }

        [HarmonyPatch(typeof(GameManager), "SendPlayerBarf", new Type[] { typeof(PlayerBip), typeof(UnityEngine.Vector3), typeof(int) })]
        public class bhaptics_PlayerBarf
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.PlaybackHaptics("Vomit");
            }
        }

        [HarmonyPatch(typeof(GameManager), "startPlayerHurt", new Type[] { typeof(PlayerBip), typeof(string), typeof(float), typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3) })]
        public class bhaptics_PlayerHurt
        {
            [HarmonyPostfix]
            public static void Postfix(UnityEngine.Vector3 point)
            {
                tactsuitVr.LOG("Point: " + point.x.ToString() + " " + point.y.ToString() + " " + point.z.ToString());
                tactsuitVr.PlaybackHaptics("Impact");
            }
        }


    }
}

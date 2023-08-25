﻿using HarmonyLib;
using Kitchen;
using Kitchen.Modules;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace ReadyCheckNames {

    [HarmonyPatch(typeof(ConsentElement), "UpdateTicks")]
    class ConsentElement_UpdateTicks_Patch {

        public static TMP_FontAsset overriddenFontAsset;

        public static void Postfix(TextMeshPro ___Ticks, Dictionary<int, bool> ___Consents) {
            List<string> names = new List<string>();
            foreach (KeyValuePair<int, bool> consent in ___Consents) {
                if (consent.Value) {
                    names.Add(ReadyCheckNamesUtil.createPlayerNameString(consent.Key));
                }
            }
            ___Ticks.text = string.Join(", ", names.ToArray());
            ___Ticks.font = overriddenFontAsset;
        }
    }

    [HarmonyPatch(typeof(EndPracticeView), "UpdateData")]
    class EndPracticeView_OnUpdate_Patch {

        public static TMP_FontAsset overriddenFontAsset;

        public static void Postfix(TextMeshPro ___ContinueTicks, HashSet<int> ___Consents) {
            ___ContinueTicks.text = string.Join(", ", ___Consents.Select(ReadyCheckNamesUtil.createPlayerNameString).ToArray());
            ___ContinueTicks.font = overriddenFontAsset;
        }
    }

    class ReadyCheckNamesUtil {

        public static string createPlayerNameString(int playerId) {
            var player = Players.Main.Get(playerId);
            string color = ColorUtility.ToHtmlStringRGB(player.Profile.Colour);
            return $"<color=#{color}>{getName(player)}</color>";
        }

        private static string getName(PlayerInfo player) {
            if (ReadyCheckPreferences.isSteamNameSelected()) {
                return player.Username.Substring(0, 2);
            } else if (player.Profile.Name == null || "New chef".Equals(player.Profile.Name) || ReadyCheckPreferences.getDisplayType() == ReadyCheckPreferences.NUMBERS_ONLY) {
                return (player.Index + 1).ToString();
            }
            return player.Profile.Name.Substring(0, 2);
        }
    }
}

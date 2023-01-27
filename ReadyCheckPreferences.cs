using Kitchen;

namespace ReadyCheckNames {
    public class ReadyCheckPreferences {

        public static readonly int STEAM_NAME = 0;
        public static readonly int PROFILE_NAME = 1;

        public static readonly Pref DisplayType = new Pref(ReadyCheckNamesMod.MOD_ID, nameof(DisplayType));

        public static void registerPreferences() {
            Preferences.AddPreference<int>(new Kitchen.IntPreference(DisplayType, STEAM_NAME));
            Preferences.Load();
        }

        public static int getDisplayType() {
            return Preferences.Get<int>(DisplayType);
        }

        public static void setDisplayType(int value) {
            Preferences.Set<int>(DisplayType, value);
        }
        
        public static bool isSteamNameSelected() {
            return getDisplayType() == STEAM_NAME;
        }
    }
}
using Kitchen;
using KitchenData;
using KitchenLib;
using KitchenLib.Colorblind;
using KitchenLib.Event;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Reflection;

namespace ReadyCheckNames {

    public class ReadyCheckNamesMod : BaseMod {

        public const string MOD_ID = "blargle.ReadyCheckNames";
        public const string MOD_NAME = "Ready Check Names";
        public const string MOD_AUTHOR = "blargle";
        public const string MOD_VERSION = "0.0.6";

        public ReadyCheckNamesMod() : base(MOD_ID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, ">=1.2.0", Assembly.GetExecutingAssembly()) { }

        protected override void OnInitialise() {
            setupPatchFonts();
            ReadyCheckPreferences.registerPreferences();
            initMenu();
        }

        private void setupPatchFonts() {
            Item item = GameData.Main.Get<Item>(ItemReferences.PieMeatCooked);
            var existingColourblindChild = GameObjectUtils.GetChildObject(item.Prefab, "Colour Blind");
            TMPro.TMP_FontAsset font = ColorblindUtils.getTextMeshProFromClonedObject(existingColourblindChild).font;
            //ConsentElement_UpdateTicks_Patch.overriddenFontAsset = font;
            //EndPracticeView_OnUpdate_Patch.overriddenFontAsset = font;
        }

        private void initMenu() {
            ModsPreferencesMenu<MenuAction>.RegisterMenu(MOD_NAME, typeof(MainMenu<MenuAction>), typeof(MenuAction));
            Events.PlayerPauseView_SetupMenusEvent += (s, args) => {
                args.addMenu.Invoke(args.instance, new object[] { typeof(MainMenu<MenuAction>), new MainMenu<MenuAction>(args.instance.ButtonContainer, args.module_list) });
            };
        }
    }
}
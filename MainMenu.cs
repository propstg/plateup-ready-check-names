using Kitchen;
using Kitchen.Modules;
using KitchenLib;
using System.Collections.Generic;
using UnityEngine;

namespace ReadyCheckNames {
    public class MainMenu<T> : KLMenu<T> {

        private static readonly List<int> displayTypeValues = new List<int> { 0, 1, 2 };
        private static readonly List<string> displayTypeLabels = new List<string> { "Steam Name", "Profile Name", "Numbers Only" };

        public MainMenu(Transform container, ModuleList module_list) : base(container, module_list) { }

        public override void Setup(int _) {
            Option<int> option = new Option<int>(displayTypeValues, ReadyCheckPreferences.getDisplayType(), displayTypeLabels);
            AddLabel("Display Type");
            AddSelect(option);
            New<SpacerElement>();
            New<SpacerElement>();
            AddButton(Localisation["MENU_BACK_SETTINGS"], delegate { RequestPreviousMenu(); });

            option.OnChanged += delegate (object _, int value) {
                ReadyCheckPreferences.setDisplayType(value);
            };
        }
    }
}
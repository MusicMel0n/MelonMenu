using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using GTA;
using GTA.Native;
using GTA.Math;
using NativeUI;

namespace ModMenu
{
    public class Class1 : Script
    {
        MenuPool modMenuPool;
        UIMenu mainMenu;

        UIMenuColoredItem resetWantedLevel;


        public Class1()
        {
            Setup();
            Banner();
            WantedLevels();

            Tick += onTick;
            KeyDown += onKeyDown;
        }

        void Setup()
        {
            modMenuPool = new MenuPool();
            mainMenu = new UIMenu("Melon Menu", "SELECT AN OPTION");
            modMenuPool.Add(mainMenu);

            mainMenu.OnItemSelect += onMainMenuItemSelect;
        }

        void WantedLevels()
        {
            List<dynamic> WantedLevels = new List<dynamic>();

            WantedLevels.Add("0");
            WantedLevels.Add("1");
            WantedLevels.Add("2");
            WantedLevels.Add("3");
            WantedLevels.Add("4");
            WantedLevels.Add("5");

            resetWantedLevel = new UIMenuColoredItem("RESET WANTED LEVEL", Color.Black, Color.LimeGreen);
            mainMenu.AddItem(resetWantedLevel);

            UIMenuListItem WantedList = new UIMenuListItem("Wanted Level: ", WantedLevels, 0);
            mainMenu.AddItem(WantedList);
        }

        void Banner()
        {
            var banner = new UIResRectangle();
            banner.Color = Color.FromKnownColor(KnownColor.LimeGreen);
            mainMenu.SetBannerType(banner);
        }

        void onMainMenuItemSelect(UIMenu sender, UIMenuItem item, int index)
        {
            if(item == resetWantedLevel)
            {
                if(Game.Player.WantedLevel == 0)
                {
                    UI.ShowSubtitle("You are not wanted!");
                }
                else
                {
                    Game.Player.WantedLevel = 0;
                }
            }
        }

        void onTick(object sender, EventArgs e)
        {
            if (modMenuPool != null)
                modMenuPool.ProcessMenus();
        }

        void onKeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F10 && !modMenuPool.IsAnyMenuOpen())
            {
                mainMenu.Visible = !mainMenu.Visible;
            }
        }
    }
}

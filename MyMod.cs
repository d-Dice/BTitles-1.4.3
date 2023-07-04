using Terraria;
using Terraria.ModLoader;

namespace BTitles
{
    public class MyMod : Mod
    {
        private ZoneDisplayUI _zoneDisplayUI;
        public MyModConfig Config { get; private set; }
        public override void Load()
        {
            if (!Main.dedServ)
            {
                _zoneDisplayUI = new ZoneDisplayUI();
                _zoneDisplayUI.Activate(); // Call Activate here
                On.Terraria.Main.DrawInterface_30_Hotbar += Main_DrawInterface_30_Hotbar;
            }

            Config = ModContent.GetInstance<MyModConfig>();
        }

        public override void Unload()
        {
            if (!Main.dedServ)
            {
                On.Terraria.Main.DrawInterface_30_Hotbar -= Main_DrawInterface_30_Hotbar;
                _zoneDisplayUI = null;
            }
        }

        private void Main_DrawInterface_30_Hotbar(On.Terraria.Main.orig_DrawInterface_30_Hotbar orig, Terraria.Main self)
        {
            if (!Terraria.Main.gameMenu)
            {
                _zoneDisplayUI.Draw(Terraria.Main.spriteBatch);
            }

            orig(self);
        }
    }
}
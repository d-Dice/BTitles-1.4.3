using System;
using System.Collections.Generic;
using System.Linq;
using BTitles.BuiltinModSupport;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace BTitles
{
    public class BiomeTitlesMod : Mod
    {
        private ZoneDisplayUI _zoneDisplayUi;
        public GeneralConfig Config { get; private set; }

        private HashSet<Mod> _implementedMods = new HashSet<Mod>();

        public override void Load()
        {
            if (!Main.dedServ)
            {
                _zoneDisplayUi = new ZoneDisplayUI();
                _zoneDisplayUi.Activate(); // Call Activate here
                On.Terraria.Main.DrawInterface_30_Hotbar += Draw;
                On.Terraria.Main.Update += Update;

                ImplementVanillaBiomes();
                ScanBiomesFromOtherMods();
                ImplementBuiltinSupport();
                
            }

            Config = ModContent.GetInstance<GeneralConfig>();
        }

        private void ImplementVanillaBiomes()
        {
            Biomes info = new Biomes
            {
                BiomeEntries = new Dictionary<string, BiomeEntry>(),
                
                MiniBiomeChecker = player =>
                {
                    if (player.ZoneDungeon) return "Dungeon";
                    if (player.ZoneLihzhardTemple) return "Lihzhard Temple";
                    if (player.ZoneGranite) return "Granite";
                    if (player.ZoneMarble) return "Marble";
                    if (player.ZoneBeach) return "Ocean";
                    if (player.ZoneMeteor) return "Meteor Crash Site";
                    if (player.ZoneTowerNebula) return "Nebula Pillar";
                    if (player.ZoneTowerSolar) return "Solar Pillar";
                    if (player.ZoneTowerStardust) return "Stardust Pillar";
                    if (player.ZoneTowerVortex) return "Vortex Pillar";
                    if (player.ZoneGraveyard) return "Graveyard";
                    if (player.ZoneHive) return "Hive";

                    if (player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight)
                    {
                        if (player.ZoneGlowshroom) return "Underground Glowing Mushroom";
                    }

                    if (player.ZoneGlowshroom) return "Glowing Mushroom";

                    return "";
                },
                
                BiomeChecker = player =>
                {
                    if (player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight)
                    {
                        if (player.ZoneCorrupt) return "Underground Corruption";
                        if (player.ZoneCrimson) return "Underground Crimson";
                        if (player.ZoneHallow) return "Underground Hallow";
                        
                        if (player.ZoneDesert) return "Underground Desert";
                        if (player.ZoneSnow) return "Underground Tundra";
                        if (player.ZoneJungle) return "Underground Jungle";
                    }

                    if (player.ZoneSkyHeight) return "Sky";
                    if (player.ZoneDirtLayerHeight) return "Underground";
                    if (player.ZoneRockLayerHeight) return "Caverns";
                    if (player.ZoneUnderworldHeight) return "Hell";
                    
                    if (player.ZoneCorrupt) return "Corruption";
                    if (player.ZoneCrimson) return "Crimson";
                    if (player.ZoneHallow) return "Hallow";

                    if (player.ZoneDesert) return "Desert";
                    if (player.ZoneSnow) return "Tundra";
                    if (player.ZoneJungle) return "Jungle";
                    if (player.ZoneForest) return "Forest";
                    // ... other Terraria biome checks ...

                    return "Forest";
                }
            };
            
            var registerBiome = (string title, Color titleColor, Color strokeColor) =>
            {
                string iconPath = "BTitles/Resources/Textures/BiomeIcons/" + title.Replace(" ", "_");
            
                info.BiomeEntries.Add(title, new BiomeEntry
                {
                    Title = title,
                    SubTitle = "Terraria",
                    Icon = ModContent.HasAsset(iconPath) ? ModContent.Request<Texture2D>(iconPath, AssetRequestMode.ImmediateLoad).Value : null,
                    TitleColor = titleColor,
                    StrokeColor = strokeColor
                });
            };
            
            registerBiome("Dungeon",                      Color.DarkBlue,       Color.Black);
            registerBiome("Lihzhard Temple",              Color.OrangeRed,      Color.Black);
            registerBiome("Granite",                      Color.DarkSlateBlue,  Color.Black);
            registerBiome("Marble",                       Color.LightGray,      Color.Black);
            registerBiome("Ocean",                        Color.DeepSkyBlue,    Color.Black);
            registerBiome("Meteor Crash Site",            Color.OrangeRed,      Color.Black);
            registerBiome("Nebula Pillar",                Color.Magenta,        Color.Black);
            registerBiome("Solar Pillar",                 Color.Orange,         Color.Black);
            registerBiome("Stardust Pillar",              Color.Yellow,         Color.Black);
            registerBiome("Vortex Pillar",                Color.LightBlue,      Color.Black);
            registerBiome("Graveyard",                    Color.Gray,           Color.Black);
            registerBiome("Hive",                         Color.Orange,         Color.Black);

            registerBiome("Underground Desert",           Color.Yellow,         Color.Black);
            registerBiome("Underground Jungle",           Color.LimeGreen,      Color.Black);
            registerBiome("Underground Tundra",           Color.LightCyan,      Color.Black);
            registerBiome("Underground Corruption",       Color.Purple,         Color.Black);
            registerBiome("Underground Crimson",          Color.Red,            Color.Black);
            registerBiome("Underground Hallow",           Color.LightBlue,      Color.Black);
            registerBiome("Underground Glowing Mushroom", Color.MediumBlue,     Color.Black);
            
            registerBiome("Sky",                          Color.CornflowerBlue, Color.Black);
            registerBiome("Caverns",                      Color.DarkSlateGray,  Color.Black);
            registerBiome("Underground",                  Color.SaddleBrown,    Color.Black);
            registerBiome("Hell",                         Color.Red,            Color.Black);
            
            registerBiome("Corruption",                   Color.Purple,         Color.Black);
            registerBiome("Crimson",                      Color.Red,            Color.Black);
            registerBiome("Hallow",                       Color.LightBlue,      Color.Black);
            
            registerBiome("Desert",                       Color.Yellow,         Color.Black);
            registerBiome("Tundra",                       Color.LightCyan,      Color.Black);
            registerBiome("Jungle",                       Color.LimeGreen,      Color.Black);
            registerBiome("Glowing Mushroom",             Color.Blue,           Color.Black);
            registerBiome("Forest",                       Color.Green,          Color.Black);

            RegisterBiomes(info);
        }
        
        private void ScanBiomesFromOtherMods()
        {
            var BTitlesHook_GetBiome_Example1 = (int index, out string key, out string title, out string subTitle, out Color titleColor, out Color titleStroke, out Texture2D icon) =>
            {
                key = "";
                title = "";
                subTitle = "";
                titleColor = Color.White;
                titleStroke = Color.Black;
                icon = null;
        
                return false;
            };
            
            var BTitlesHook_GetBiome_Example2 = (int index, out string key, out string title, out string subTitle, out Color titleColor, out Color titleStroke, out Texture2D icon, out BiomeTitle titleWidget, out BiomeTitle subTitleWidget) =>
            {
                key = "";
                title = "";
                subTitle = "";
                titleColor = Color.White;
                titleStroke = Color.Black;
                icon = null;
                titleWidget = null;
                subTitleWidget = null;
        
                return false;
            };

            var BTitlesHook_SetupBiomeCheckers_Example = (out Func<Player, string> miniBiomeChecker, out Func<Player, string> biomeChecker) =>
            {
                miniBiomeChecker = null;
                biomeChecker = null;
            };

            foreach (Mod mod in ModLoader.Mods)
            {
                Biomes biomes = new Biomes();

                var setupBiomeCheckersFunc = mod.GetType().GetMethod("BTitlesHook_SetupBiomeCheckers");
                var getBiomeFunc = mod.GetType().GetMethod("BTitlesHook_GetBiome");

                if (setupBiomeCheckersFunc != null)
                {
                    if (setupBiomeCheckersFunc.CompareSignature(BTitlesHook_SetupBiomeCheckers_Example.Method))
                    {
                        object[] parameters = new object[] { null, null };
                        setupBiomeCheckersFunc.Invoke(mod, parameters);

                        Func<Player, string> miniBiomeChecker = (Func<Player, string>)parameters[0];
                        Func<Player, string> biomeChecker = (Func<Player, string>)parameters[1];

                        biomes.MiniBiomeChecker = miniBiomeChecker;
                        biomes.BiomeChecker = biomeChecker;
                    }
                }
                
                if (getBiomeFunc != null)
                {
                    if (getBiomeFunc.CompareSignature(BTitlesHook_GetBiome_Example1.Method))
                    {
                        biomes.BiomeEntries = new Dictionary<string, BiomeEntry>();
                        
                        int biomeIndex = 0;
                        while (true)
                        {
                            object[] parameters = new object[] { biomeIndex, null, null, null, null, null, null };
                            if (!(bool)getBiomeFunc.Invoke(mod, parameters)) break;
                            
                            string key = (string)parameters[1];
                            string title = (string)parameters[2];
                            string subTitle = (string)parameters[3];
                            Color titleColor = (Color)parameters[4];
                            Color titleStroke = (Color)parameters[5];
                            Texture2D icon = (Texture2D)parameters[6];
                                
                            biomes.BiomeEntries.Add(key, new BiomeEntry
                            {
                                Title = title,
                                SubTitle = subTitle,
                                TitleColor = titleColor,
                                StrokeColor = titleStroke, 
                                Icon = icon, 
                                TitleWidget = null,
                                SubTitleWidget = null
                            });

                            biomeIndex++;
                        }
                    }
                    else if (getBiomeFunc.CompareSignature(BTitlesHook_GetBiome_Example2.Method))
                    {
                        biomes.BiomeEntries = new Dictionary<string, BiomeEntry>();
                        
                        int biomeIndex = 0;
                        while (true)
                        {
                            object[] parameters = new object[] { biomeIndex, null, null, null, null, null, null, null, null };
                            if (!(bool)getBiomeFunc.Invoke(mod, parameters)) break;
                            
                            string key = (string)parameters[1];
                            string title = (string)parameters[2];
                            string subTitle = (string)parameters[3];
                            Color titleColor = (Color)parameters[4];
                            Color titleStroke = (Color)parameters[5];
                            Texture2D icon = (Texture2D)parameters[6];
                            BiomeTitle titleWidget = (BiomeTitle)parameters[7];
                            BiomeTitle subTitleWidget = (BiomeTitle)parameters[8];
                                
                            biomes.BiomeEntries.Add(key, new BiomeEntry
                            {
                                Title = title, 
                                SubTitle = subTitle,
                                TitleColor = titleColor,
                                StrokeColor = titleStroke,
                                Icon = icon,
                                TitleWidget = titleWidget,
                                SubTitleWidget = subTitleWidget
                            });
                                
                            biomeIndex++;
                        }
                    }
                }
                
                if (biomes.MiniBiomeChecker == null && biomes.BiomeChecker == null && (biomes.BiomeEntries?.Count ?? 0) == 0) continue;

                RegisterBiomes(biomes);
                _implementedMods.Add(mod);
            }
        }

        private void ImplementBuiltinSupport()
        {
            GetType().Assembly.GetTypes()
                .Where(type => type.IsSubclassOf(typeof(ModSupport)) && !type.IsAbstract)
                .Select(type => Activator.CreateInstance(type) as ModSupport)
                .Where(support => support != null && support.GetTargetMod() != null && !_implementedMods.Contains(support.GetTargetMod()))
                .Select(support => support.Implement())
                .Where(modInfo => modInfo != null)
                .ToList()
                .ForEach(RegisterBiomes);
        }

        private void RegisterBiomes(Biomes info)
        {
            if (info.BiomeEntries != null)
            {
                foreach (var entry in info.BiomeEntries)
                {
                    _zoneDisplayUi.BiomeDictionary.Add(entry.Key, entry.Value);
                }
            }

            if (info.MiniBiomeChecker != null)
            {
                _zoneDisplayUi.MiniBiomeCheckFunctions.Insert(0, info.MiniBiomeChecker);
            }
            
            if (info.BiomeChecker != null)
            {
                _zoneDisplayUi.BiomeCheckFunctions.Insert(0, info.BiomeChecker);
            }
        }

        public override void Unload()
        {
            if (!Main.dedServ)
            {
                On.Terraria.Main.DrawInterface_30_Hotbar -= Draw;
                On.Terraria.Main.Update -= Update;
                _zoneDisplayUi = null;
            }
        }

        private void Draw(On.Terraria.Main.orig_DrawInterface_30_Hotbar orig, Terraria.Main self)
        {
            if (!Terraria.Main.gameMenu)
            {
                _zoneDisplayUi.Draw(Terraria.Main.spriteBatch);
            }

            orig(self);
        }

        private void Update(On.Terraria.Main.orig_Update orig, Terraria.Main self, GameTime gameTime)
        {
            if (!Terraria.Main.gameMenu)
            {
                _zoneDisplayUi.Update(gameTime);
            }
            else
            {
                _zoneDisplayUi.ResetZone();
            }

            orig(self, gameTime);
        }

        public override object Call(params object[] args)
        {
            if (args.Length < 1) return null;

            string method = args[0] as string;
            switch (method)
            {
                case "RegisterBiome":

                    break;
            }

            return null;
        }

        // Example of weak inter-mod implementation
        /*
        public bool BTitlesHook_GetBiome(int index, out string key, out string title, out string subTitle, out Color titleColor, out Color titleStroke, out Texture2D icon)
        {
            switch (index)
            {
                case 0:
                    key = "biome1";
                    title = "Biome 1";
                    subTitle = "Terraria";
                    titleColor = Color.White;
                    titleStroke = Color.Black;
                    icon = null;
                    return true;
                case 1:
                    key = "biome2";
                    title = "Biome 2";
                    subTitle = "Terraria";
                    titleColor = Color.White;
                    titleStroke = Color.Black;
                    icon = null;
                    return true;
                default:
                    key = "";
                    title = "";
                    subTitle = "";
                    titleColor = Color.White;
                    titleStroke = Color.Black;
                    icon = null;
                    return false;
            }
        }

        public void BTitlesHook_SetupBiomeCheckers(out Func<Player, string> miniBiomeChecker, out Func<Player, string> biomeChecker)
        {
            miniBiomeChecker = player => "";
            biomeChecker = player => "";
        }
        */
    }
}
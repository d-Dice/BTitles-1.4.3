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
            Biomes biomes = new Biomes
            {
                BiomeEntries = new Dictionary<string, BiomeEntry>(),
                
                MiniBiomeChecker = player =>
                {
                    Point playerTilePosition = player.Center.ToTileCoordinates();
                    var tileAtPlayeCenter = Main.tile[playerTilePosition.X, playerTilePosition.Y];
                    
                    // Extra-small
                    if (player.ZoneMeteor) return "Meteor Crash Site";
                    if (player.ZoneGraveyard) return "Graveyard";
                    if (player.ZoneHive) return "Hive";
                    
                    // Small
                    if (player.ZoneDungeon) return "Dungeon";
                    if (player.ZoneLihzhardTemple || tileAtPlayeCenter.WallType == 87) return "Lihzhard Temple"; // 87 is natural temple wall
                    
                    // Rare
                    if (player.ZoneTowerNebula) return "Nebula Pillar";
                    if (player.ZoneTowerSolar) return "Solar Pillar";
                    if (player.ZoneTowerStardust) return "Stardust Pillar";
                    if (player.ZoneTowerVortex) return "Vortex Pillar";
                    
                    // Misc
                    if (player.ZoneGranite) return "Granite Cave";
                    if (player.ZoneMarble) return "Marble Cave";

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
                        // Underground infection-independent
                        if (player.ZoneJungle) return "Underground Jungle";
                    
                        // Underground infectable biomes
                        if (player.ZoneDesert)
                        {
                            if (player.ZoneCorrupt) return "Corrupt Cave Desert";
                            if (player.ZoneCrimson) return "Crimson Cave Desert";
                            if (player.ZoneHallow) return "Hallow Cave Desert";
                            return "Cave Desert";
                        }
                        else if (player.ZoneSnow)
                        {
                            if (player.ZoneCorrupt) return "Corrupt Ice Caves";
                            if (player.ZoneCrimson) return "Crimson Ice Caves";
                            if (player.ZoneHallow) return "Hallow Ice Caves";
                            return "Ice Caves";
                        }
                        else
                        {
                            if (player.ZoneCorrupt) return "Underground Corruption";
                            if (player.ZoneCrimson) return "Underground Crimson";
                            if (player.ZoneHallow) return "Underground Hallow";
                        }
                    }
                    
                    // Layer-independent
                    if (player.ZoneBeach) return "Ocean";

                    // Layers
                    if (player.ZoneSkyHeight) return "Sky";
                    if (player.ZoneDirtLayerHeight) return "Underground";
                    if (player.ZoneRockLayerHeight) return "Caverns";
                    if (player.ZoneUnderworldHeight) return "Hell";
                    
                    // Non-underground infection-independent
                    if (player.ZoneJungle) return "Jungle";
                    
                    // Non-underground infectable biomes
                    if (player.ZoneDesert)
                    {
                        if (player.ZoneCorrupt) return "Corrupt Desert";
                        if (player.ZoneCrimson) return "Crimson Desert";
                        if (player.ZoneHallow) return "Hallow Desert";
                        return "Desert";
                    }
                    else if (player.ZoneSnow)
                    {
                        return "Tundra";
                    }
                    else
                    {
                        if (player.ZoneCorrupt) return "Corruption";
                        if (player.ZoneCrimson) return "Crimson";
                        if (player.ZoneHallow) return "Hallow";
                    }

                    // ... other Terraria biome checks ...

                    return "Forest";
                }
            };
            
            var registerBiome = (string title, Color titleColor, Color strokeColor) =>
            {
                string iconPath = "BTitles/Resources/Textures/BiomeIcons/" + title.Replace(" ", "_");
            
                biomes.BiomeEntries.Add(title, new BiomeEntry
                {
                    Title = title,
                    SubTitle = "Terraria",
                    Icon = ModContent.HasAsset(iconPath) ? ModContent.Request<Texture2D>(iconPath, AssetRequestMode.ImmediateLoad).Value : null,
                    TitleColor = titleColor,
                    StrokeColor = strokeColor
                });

                if (!ModContent.HasAsset(iconPath))
                {
                    BiomeTitlesMod.Log("Fail", "Icons", $"Failed to find icon for biome {title}");
                }
            };
            
            registerBiome("Meteor Crash Site",            Color.OrangeRed,      Color.Black);
            registerBiome("Graveyard",                    Color.Gray,           Color.Black);
            registerBiome("Hive",                         Color.Orange,         Color.Black);
            
            registerBiome("Dungeon",                      Color.DarkBlue,       Color.Black);
            registerBiome("Lihzhard Temple",              Color.OrangeRed,      Color.Black);
            
            registerBiome("Nebula Pillar",                Color.Magenta,        Color.Black);
            registerBiome("Solar Pillar",                 Color.Orange,         Color.Black);
            registerBiome("Stardust Pillar",              Color.Yellow,         Color.Black);
            registerBiome("Vortex Pillar",                Color.LightBlue,      Color.Black);
            
            registerBiome("Granite Cave",                 Color.DarkSlateBlue,  Color.Black);
            registerBiome("Marble Cave",                  Color.LightGray,      Color.Black);
            
            registerBiome("Underground Glowing Mushroom", Color.MediumBlue,     Color.Black);
            registerBiome("Glowing Mushroom",             Color.Blue,           Color.Black);



            registerBiome("Underground Jungle",           Color.LimeGreen,      Color.Black);
            
            registerBiome("Corrupt Cave Desert",          Color.Peru,           Color.Black);
            registerBiome("Crimson Cave Desert",          Color.OrangeRed,      Color.Black);
            registerBiome("Hallow Cave Desert",           Color.PapayaWhip,     Color.Black);
            registerBiome("Cave Desert",                  Color.Yellow,         Color.Black);
            
            registerBiome("Corrupt Ice Caves",            Color.Orchid,         Color.Black);
            registerBiome("Crimson Ice Caves",            Color.LightCoral,     Color.Black);
            registerBiome("Hallow Ice Caves",             Color.PowderBlue,     Color.Black);
            registerBiome("Ice Caves",                    Color.LightCyan,      Color.Black);
            
            registerBiome("Underground Corruption",       Color.Purple,         Color.Black);
            registerBiome("Underground Crimson",          Color.Red,            Color.Black);
            registerBiome("Underground Hallow",           Color.LightBlue,      Color.Black);
            
            registerBiome("Ocean",                        Color.DeepSkyBlue,    Color.Black);

            registerBiome("Sky",                          Color.CornflowerBlue, Color.Black);
            registerBiome("Underground",                  Color.SaddleBrown,    Color.Black);
            registerBiome("Caverns",                      Color.DarkSlateGray,  Color.Black);
            registerBiome("Hell",                         Color.Red,            Color.Black);
            
            registerBiome("Jungle",                       Color.LimeGreen,      Color.Black);
            
            registerBiome("Corrupt Desert",               Color.Peru,           Color.Black);
            registerBiome("Crimson Desert",               Color.OrangeRed,      Color.Black);
            registerBiome("Hallow Desert",                Color.PapayaWhip,     Color.Black);
            registerBiome("Desert",                       Color.Yellow,         Color.Black);
            
            registerBiome("Tundra",                       Color.LightCyan,      Color.Black);
            
            registerBiome("Corruption",                   Color.Purple,         Color.Black);
            registerBiome("Crimson",                      Color.Red,            Color.Black);
            registerBiome("Hallow",                       Color.LightBlue,      Color.Black);

            registerBiome("Forest",                       Color.Green,          Color.Black);

            BiomeTitlesMod.Log("Log", "Vanilla Support", $"Registering biomes for vanilla");
            RegisterBiomes(biomes);
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

                BiomeTitlesMod.Log("Log", "Native Support", $"Registering biomes for mod {mod.Name}");
                RegisterBiomes(biomes);
                _implementedMods.Add(mod);
            }
        }

        private void ImplementBuiltinSupport()
        {
            foreach (Type type in GetType().Assembly.GetTypes())
            {
                if (!type.IsSubclassOf(typeof(ModSupport)) || type.IsAbstract) continue;

                ModSupport supportInstance = (ModSupport)Activator.CreateInstance(type);

                var targetMod = supportInstance?.GetTargetMod();
                
                if (targetMod == null || _implementedMods.Contains(targetMod)) continue;

                Biomes biomes = supportInstance.Implement();
                
                if (biomes == null) continue;
                
                BiomeTitlesMod.Log("Log", "Builtin Support", $"Registering biomes for mod {targetMod.Name}");
                RegisterBiomes(biomes);
            }
        }

        private void RegisterBiomes(Biomes biomes)
        {
            if ((biomes.BiomeEntries?.Count ?? 0) == 0 && biomes.MiniBiomeChecker == null && biomes.BiomeChecker == null)
            {
                BiomeTitlesMod.Log("Fail", "Register Biomes", $"Nothing to register");
                return;
            }
            
            if (biomes.BiomeEntries != null)
            {
                foreach (var entry in biomes.BiomeEntries)
                {
                    bool overriding = _zoneDisplayUi.BiomeDictionary.ContainsKey(entry.Key);
                    BiomeTitlesMod.Log("Log", "Register Biomes", $"{(overriding ? "Overriding" : "Registering")} biome {entry.Key}");
                    _zoneDisplayUi.BiomeDictionary[entry.Key] = entry.Value;
                }
            }

            if (biomes.MiniBiomeChecker != null)
            {
                BiomeTitlesMod.Log("Log", "Register Biomes", $"Registering mini-biome check function");
                _zoneDisplayUi.MiniBiomeCheckFunctions.Insert(0, biomes.MiniBiomeChecker);
            }
            
            if (biomes.BiomeChecker != null)
            {
                BiomeTitlesMod.Log("Log", "Register Biomes", $"Registering biome check function");
                _zoneDisplayUi.BiomeCheckFunctions.Insert(0, biomes.BiomeChecker);
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
            if (!Main.gameMenu)
            {
                _zoneDisplayUi.Draw(Terraria.Main.spriteBatch);
            }

            orig(self);
        }

        private void Update(On.Terraria.Main.orig_Update orig, Terraria.Main self, GameTime gameTime)
        {
            if (!Main.gameMenu)
            {
                _zoneDisplayUi.Update(gameTime);
            }
            else
            {
                _zoneDisplayUi.ResetZone();
            }

            orig(self, gameTime);
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
        
        internal static void Log(string type, string category, object message)
        {
            Console.WriteLine($"[BiomeTitles] [{type}] [{category}] {message}");
        }
    }
}
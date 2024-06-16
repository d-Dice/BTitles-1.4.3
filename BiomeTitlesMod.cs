using System;
using System.Collections.Generic;
using BTitles.BuiltinModSupport;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace BTitles
{
    internal enum LogType
    {
        Log,
        Warning,
        Fail
    }
    
    public class BiomeTitlesMod : Mod
    {
        internal static BiomeTitlesMod Instance = null;
        
        private BiomeTitlesUI _biomeTitlesUi;
        public GeneralConfig Config { get; private set; }

        private HashSet<Mod> _implementedMods = new HashSet<Mod>();
        
        internal Dictionary<string, BiomeEntry> BiomeDictionary = new Dictionary<string, BiomeEntry>();
        internal List<Func<string, dynamic>> DynamicBiomeProviders = new List<Func<string, dynamic>>();
        internal List<Func<Player, string>> DynamicBiomeCheckerFunctions = new List<Func<Player, string>>();
        internal List<Func<Player, string>> MiniBiomeCheckFunctions = new List<Func<Player, string>>();
        internal List<Func<Player, string>> BiomeCheckFunctions = new List<Func<Player, string>>();
        
        public override void Load()
        {
            Instance = this;
            
            if (!Main.dedServ)
            {
                _biomeTitlesUi = new BiomeTitlesUI();
                _biomeTitlesUi.DynamicBiomeProviders = DynamicBiomeProviders;
                _biomeTitlesUi.DynamicBiomeCheckFunctions = DynamicBiomeCheckerFunctions;
                _biomeTitlesUi.BiomeDictionary = BiomeDictionary;
                _biomeTitlesUi.MiniBiomeCheckFunctions = MiniBiomeCheckFunctions;
                _biomeTitlesUi.BiomeCheckFunctions = BiomeCheckFunctions;
                
                _biomeTitlesUi.Activate();
                
                Terraria.On_Main.DrawInterface_30_Hotbar += Draw;
                Terraria.On_Main.Update += Update;

                ImplementVanillaBiomes();
                ScanBiomesFromOtherMods();
                ImplementBuiltinSupport();
            }

            Config = ModContent.GetInstance<GeneralConfig>();
        }
        
        public override void Unload()
        {
            Instance = null;
            
            if (!Main.dedServ)
            {
                Terraria.On_Main.DrawInterface_30_Hotbar -= Draw;
                Terraria.On_Main.Update -= Update;
                
                _biomeTitlesUi = null;
                _implementedMods.Clear();
                BiomeDictionary.Clear();
                MiniBiomeCheckFunctions.Clear();
                BiomeCheckFunctions.Clear();
            }
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
                    if (player.ZoneShimmer) return "Aether";
                    
                    // Small
                    if (player.ZoneDungeon) return "The Dungeon";
                    if (player.ZoneLihzhardTemple || tileAtPlayeCenter.WallType == 87) return "The Temple"; // 87 is natural temple wall
                    
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
                    StrokeColor = strokeColor,
                    LocalizationScope = "Terraria"
                });

                if (!ModContent.HasAsset(iconPath))
                {
                    BiomeTitlesMod.Log(LogType.Fail, "Icons", $"Failed to find icon for biome {title}");
                }
            };
            
            registerBiome("Meteor Crash Site",            Color.OrangeRed,      Color.Black);
            registerBiome("Graveyard",                    Color.Gray,           Color.Black);
            registerBiome("Hive",                         Color.Orange,         Color.Black);
            registerBiome("Aether",                       Color.Violet,         Color.Black);
            
            registerBiome("The Dungeon",                  Color.DarkBlue,       Color.Black);
            registerBiome("The Temple",                   Color.OrangeRed,      Color.Black);
            
            registerBiome("Nebula Pillar",                Color.Magenta,        Color.Black);
            registerBiome("Solar Pillar",                 Color.Orange,         Color.Black);
            registerBiome("Stardust Pillar",              Color.Yellow,         Color.Black);
            registerBiome("Vortex Pillar",                Color.LightBlue,      Color.Black);
            
            registerBiome("Granite Cave",                 Color.DarkSlateBlue,  Color.Black);
            registerBiome("Marble Cave",                  Color.LightGray,      Color.Black);
            
            registerBiome("Underground Glowing Mushroom", Color.LightBlue,      Color.Black);
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

            BiomeTitlesMod.Log(LogType.Log, "Vanilla Support", $"Registering biomes for vanilla");
            RegisterBiomes(biomes);
        }

        private void ScanBiomesFromOtherMods()
        {
            foreach (Mod mod in ModLoader.Mods)
            {
                var biomes = Integration.IntegrateMod(mod);

                if (biomes.DynamicBiomeProvider == null && biomes.DynamicBiomeChecker == null && biomes.MiniBiomeChecker == null && biomes.BiomeChecker == null && (biomes.BiomeEntries?.Count ?? 0) == 0) continue;

                BiomeTitlesMod.Log(LogType.Log, "Native Support", $"Registering biomes for mod {mod.Name}");
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
                
                BiomeTitlesMod.Log(LogType.Log, "Builtin Support", $"Registering biomes for mod {targetMod.Name}");
                RegisterBiomes(biomes);
            }
        }

        private void RegisterBiomes(Biomes biomes)
        {
            if ((biomes.BiomeEntries?.Count ?? 0) == 0 && biomes.DynamicBiomeProvider == null && biomes.DynamicBiomeChecker == null && biomes.MiniBiomeChecker == null && biomes.BiomeChecker == null)
            {
                BiomeTitlesMod.Log(LogType.Fail, "Register Biomes", $"Nothing to register");
                return;
            }
            
            if (biomes.BiomeEntries != null)
            {
                foreach (var entry in biomes.BiomeEntries)
                {
                    bool overriding = BiomeDictionary.ContainsKey(entry.Key);
                    BiomeTitlesMod.Log(LogType.Log, "Register Biomes", $"{(overriding ? "Overriding" : "Registering")} biome {entry.Key}");
                    entry.Value.Key = entry.Key;
                    BiomeDictionary[entry.Key] = entry.Value;
                }
            }

            if (biomes.DynamicBiomeProvider != null)
            {
                BiomeTitlesMod.Log(LogType.Log, "Register Biomes", $"Registering dynamic biome provider");
                DynamicBiomeProviders.Insert(0, biomes.DynamicBiomeProvider);
            }

            if (biomes.DynamicBiomeChecker != null)
            {
                BiomeTitlesMod.Log(LogType.Log, "Register Biomes", $"Registering dynamic biome check function");
                DynamicBiomeCheckerFunctions.Insert(0, biomes.DynamicBiomeChecker);
            }

            if (biomes.MiniBiomeChecker != null)
            {
                BiomeTitlesMod.Log(LogType.Log, "Register Biomes", $"Registering mini-biome check function");
                MiniBiomeCheckFunctions.Insert(0, biomes.MiniBiomeChecker);
            }
            
            if (biomes.BiomeChecker != null)
            {
                BiomeTitlesMod.Log(LogType.Log, "Register Biomes", $"Registering biome check function");
                BiomeCheckFunctions.Insert(0, biomes.BiomeChecker);
            }
        }

        private void Draw(Terraria.On_Main.orig_DrawInterface_30_Hotbar orig, Terraria.Main self)
        {
            if (!Main.gameMenu)
            {
                _biomeTitlesUi.Draw(Terraria.Main.spriteBatch);
            }

            orig(self);
        }

        private void Update(Terraria.On_Main.orig_Update orig, Terraria.Main self, GameTime gameTime)
        {
            if (!Main.gameMenu)
            {
                _biomeTitlesUi.Update(gameTime);
            }
            else
            {
                _biomeTitlesUi.ResetBiome();
            }

            orig(self, gameTime);
        }

        internal static void Log(LogType type, string category, object message)
        {
            if (Instance != null)
            {
                switch (type)
                {
                    case LogType.Log:
                        Instance.Logger.Info($"[{category}] {message}");
                        break;
                    case LogType.Warning:
                        Instance.Logger.Warn($"[{category}] {message}");
                        break;
                    case LogType.Fail:
                        Instance.Logger.Error($"[{category}] {message}");
                        break;
                }
            }
            else
            {
                Console.WriteLine($"[BTitles] [{type}] [{category}] {message}");
            }
        }
    }
}
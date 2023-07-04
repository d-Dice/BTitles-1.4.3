using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using System.Linq;
using System.Collections.Generic;

namespace BTitles
{
    public class ZoneDisplayUI : UIState
    {
        private bool _calamityModInstalled;
        private float _biomeCheckTimer;
        private Dictionary<string, Texture2D> _biomeIcons;
        private CustomUIText _zoneText;
        private string _currentZone;
        private float _fadeTime;
        private bool _displayText;
        private float _displayTimer;
        private const float DisplayTime = 7f; // 7 seconds
        private const float FadeTime = 3f; // 3 seconds for fade in/out
        private Mod _calamityMod;
        private bool _isDragging;
        public ZoneDisplayUI()
        {
            LoadBiomeIcons();
        }
        private string AddZone(string currentZone, string newZone)
        {
            if (string.IsNullOrEmpty(currentZone))
            {
                return newZone;
            }
            else
            {
                return currentZone;
            }
        }
        private void LoadBiomeIcons()
        {
            _biomeIcons = new Dictionary<string, Texture2D>
    {
        {"Forest", ModContent.Request<Texture2D>("BTitles/BiomeIcons/Bestiary_Forest").Value},
        // ... add the rest of the biome icons here ...
    };
        }
        private Vector2 GetPositionFromConfig(PositionOption positionOption)
        {
            int screenWidth = Main.screenWidth;
            int screenHeight = Main.screenHeight;
            MyModConfig config = ModContent.GetInstance<MyModConfig>();

            if (config.EnableDraggableTitle && positionOption == PositionOption.CustomDraggable)
            {
                return new Vector2(config.CustomTitlePosition.X, config.CustomTitlePosition.Y);
            }

            switch (positionOption)
            {
                case PositionOption.Top:
                    return new Vector2(screenWidth / 2, 80);
                case PositionOption.Bottom:
                    return new Vector2(screenWidth / 2, screenHeight - 80);
                case PositionOption.RPGStyle:
                    return new Vector2(220, screenHeight - 80);
                default:
                    return Vector2.Zero;
            }
        }
        private Texture2D GetBiomeIcon(string biomeName)
        {
            if (_biomeIcons.TryGetValue(biomeName, out Texture2D icon))
            {
                return icon;
            }

            return null;
        }

        private void UpdateDragging()
        {
            MyModConfig config = ModContent.GetInstance<MyModConfig>();
            if (!config.EnableDraggableTitle) return;

            var mousePosition = new Vector2(Main.mouseX, Main.mouseY);
            var titleDimensions = _zoneText.GetDimensions();
            int hitboxOffset = 30; // Increase this value to make the hitbox larger
            var titleRectangle = new Rectangle((int)titleDimensions.X - hitboxOffset, (int)titleDimensions.Y - hitboxOffset, (int)titleDimensions.Width + 2 * hitboxOffset, (int)titleDimensions.Height + 2 * hitboxOffset);
            var mousePoint = new Point((int)mousePosition.X, (int)mousePosition.Y);

            if (!_isDragging && Main.mouseLeft && titleRectangle.Contains(mousePoint))
            {
                _isDragging = true;
            }

            if (_isDragging)
            {
                if (Main.mouseLeft)
                {
                    config.CustomTitlePosition.X = mousePosition.X;
                    config.CustomTitlePosition.Y = mousePosition.Y;
                }
                else
                {
                    _isDragging = false;
                }
            }
        }
        private bool GetInCalamityZone(Player player, string zone)
        {
            Mod calamityMod = ModLoader.GetMod("CalamityMod");
            if (calamityMod == null)
            {
                return false;
            }

            try
            {
                return (bool)calamityMod.Call("GetInZone", player, zone);
            }
            catch
            {
                return false;
            }
        }
        private bool CheckModBiome(Mod mod, string biomeName, string zoneName, ref string customZoneName)
        {
            if (mod.TryFind<ModBiome>(biomeName, out ModBiome modBiome) && Main.LocalPlayer.InModBiome(modBiome))
            {
                customZoneName = GetCustomZoneName(zoneName);
                return true;
            }
            return false;
        }
        private string GetCustomZoneName(string zone)
        {
            MyModConfig config = ModContent.GetInstance<MyModConfig>();

            // Find the custom name for the current zone, if available
            string customName = config.CustomBiomeNames.FirstOrDefault(mapping => mapping.CurrentName == zone)?.NewName;

            return string.IsNullOrEmpty(customName) ? zone : customName;
        }
        private float GetTextScaleFromConfig(TextScaleOption textScaleOption)
        {
            switch (textScaleOption)
            {
                case TextScaleOption.Small:
                    return 0.75f;
                case TextScaleOption.Normal:
                    return 1f;
                case TextScaleOption.Big:
                    return 1.2f;
                default:
                    return 1f;
            }
        }
        public override void OnInitialize()
        {
            _calamityModInstalled = ModLoader.TryGetMod("CalamityMod", out _calamityMod);
            MyModConfig config = ModContent.GetInstance<MyModConfig>();
            float textScale = GetTextScaleFromConfig(config.TextScale);
            _zoneText = new CustomUIText("", textScale);
            Vector2 position = GetPositionFromConfig(config.Position);
            _zoneText.Left.Set(position.X, 0f);
            _zoneText.Top.Set(position.Y, 0f);
            _zoneText.VAlign = 0f;
            _zoneText.HAlign = 0f;
            Append(_zoneText);


        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            MyModConfig config = ModContent.GetInstance<MyModConfig>();

            // Update the biome check timer
            _biomeCheckTimer += (float)Main._drawInterfaceGameTime.ElapsedGameTime.TotalSeconds;

            if (_biomeCheckTimer >= config.BiomeCheckDelay)
            {
                // Reset the timer
                _biomeCheckTimer = 0;

                string newZone = GetCurrentZone();

                if (!Main.playerInventory && newZone != _currentZone)
                {
                    _displayText = true;
                    if (_displayTimer > FadeTime)
                    {
                        // Immediately start fading out when entering a new zone
                        _displayTimer = FadeTime;
                    }
                    else
                    {
                        // Change the zone name only after the text has faded out completely
                        _currentZone = newZone;
                        _displayTimer = DisplayTime;
                    }
                }
            }

            if (_displayText)
            {
                Vector2 position = GetPositionFromConfig(config.Position);
                _zoneText.Left.Set(position.X, 0f);
                _zoneText.Top.Set(position.Y, 0f);
                _zoneText.SetText(_currentZone);

                // Update the text scale
                float textScale = GetTextScaleFromConfig(config.TextScale);
                _zoneText.UpdateTextScale(textScale);

                // Fade in/out and display time logic
                float opacity;
                if (_displayTimer > DisplayTime - FadeTime)
                {
                    opacity = MathHelper.Lerp(0f, 1f, (DisplayTime - _displayTimer) / FadeTime);
                }
                else if (_displayTimer < FadeTime)
                {
                    opacity = MathHelper.Lerp(0f, 1f, _displayTimer / FadeTime);
                }
                else
                {
                    opacity = 1f;
                }

                _zoneText.Opacity = opacity;

                _displayTimer -= (float)Main._drawInterfaceGameTime.ElapsedGameTime.TotalSeconds;
                if (_displayTimer <= 0 && opacity <= 0)
                {
                    _displayText = false;
                }
            }

            // Only call base.DrawSelf if the text should be visible
            if (_displayText)
            {
                System.Diagnostics.Debug.WriteLine($"Current zone: {_currentZone}");
                Texture2D icon = GetBiomeIcon(_currentZone);
                if (icon != null)
                {
                    Vector2 iconPosition = new Vector2(_zoneText.Left.Pixels - icon.Width - 5, _zoneText.Top.Pixels);
                    spriteBatch.Draw(icon, iconPosition, Color.White * _zoneText.Opacity);
                }

                base.DrawSelf(spriteBatch);
            }
        }
        private string GetCurrentZone()
        {
            Player player = Main.LocalPlayer;
            string zone = "";
            Mod targetmod = null;
            ModBiome modbiome = null;

            // Check if the Calamity mod is loaded
            if (_calamityModInstalled)
            {
                if (GetInCalamityZone(player, "crags")) zone = AddZone(zone, GetCustomZoneName("Brimstone Crag"));
                if (GetInCalamityZone(player, "astral")) zone = AddZone(zone, GetCustomZoneName ("Astral Infection"));
                if (GetInCalamityZone(player, "sunkensea")) zone = AddZone(zone, GetCustomZoneName("Sunken Sea"));
                if (GetInCalamityZone(player, "sulphursea")) zone = AddZone(zone, GetCustomZoneName("Sulphurous Sea"));
                if (GetInCalamityZone(player, "abyss")) zone = AddZone(zone, GetCustomZoneName("Abyss"));
                if (GetInCalamityZone(player, "layer1")) zone = AddZone(zone, GetCustomZoneName("Layer 1"));
                if (GetInCalamityZone(player, "layer2")) zone = AddZone(zone, GetCustomZoneName("Layer 2"));
                if (GetInCalamityZone(player, "layer3")) zone = AddZone(zone, GetCustomZoneName("Layer 3"));
                if (GetInCalamityZone(player, "layer4")) zone = AddZone(zone, GetCustomZoneName("Layer 4"));
            }

            // If no Calamity biomes are found, check for Terraria biomes
            if (string.IsNullOrEmpty(zone))
                // Thorium Mod biome checks
                if (ModLoader.TryGetMod("ThoriumMod", out targetmod))
                {
                    if (targetmod.TryFind<ModBiome>("DepthsBiome", out modbiome) && player.InModBiome(modbiome) && player.position.Y > Main.worldSurface * 16)
                    {
                        return GetCustomZoneName("Aquatic Depths");
                    }
                    if (ModLoader.TryGetMod("ThoriumMod", out Mod targetMod))
                    {
                        if (targetMod.Call("GetBloodChamberBounds") is Rectangle rectangle)
                        {
                            var point = player.Center.ToTileCoordinates();
                            if (rectangle.Contains(point))
                            {
                                return GetCustomZoneName("Blood Chamber");
                            }
                        }
                    }
                }
            //Spooky biome checks
            if (ModLoader.TryGetMod("Spooky", out targetmod))
            {
                if (targetmod.TryFind<ModBiome>("CatacombBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return "Catacomb";
                }
                if (targetmod.TryFind<ModBiome>("SpookyBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return "Spooky Forest";
                }
                if (targetmod.TryFind<ModBiome>("SpookyBiomeUg", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return "Underground Spooky";
                }
                if (targetmod.TryFind<ModBiome>("SpookyHellBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return "Spooky Hell";
                }
            }
            // FFF biome checks
            if (ModLoader.TryGetMod("CosmeticVariety", out targetmod))
            {
                if (targetmod.TryFind<ModBiome>("CelestialSurfaceBiome", out modbiome) && player.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Celestial Surface");
                }
                if (targetmod.TryFind<ModBiome>("GardenBiome", out modbiome) && player.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Garden");
                }
                if (targetmod.TryFind<ModBiome>("GardenSurfaceBiome", out modbiome) && player.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Garden Surface");
                }
            }
            // Aequus biome checks
            if (ModLoader.TryGetMod("Aequus", out targetmod))
            {
                if (targetmod.TryFind<ModBiome>("FakeUnderworldBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Fake Underworld");
                }
                if (targetmod.TryFind<ModBiome>("GlowingMossBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Glowing Moss");
                }
                if (targetmod.TryFind<ModBiome>("GoreNestBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Gore Nest");
                }
                if (targetmod.TryFind<ModBiome>("RadonMossBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Radon Moss");
                }
                if (targetmod.TryFind<ModBiome>("CrabCreviceBiome", out modbiome) && player.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Crab Crevice");
                }
            }
            // Arbour biome checks
            if (ModLoader.TryGetMod("Arbour", out targetmod))
            {
                if (targetmod.TryFind<ModBiome>("ArborBiome", out modbiome) && player.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Arbour Island");
                }
            }
                // Verdant biomes checks
                if (ModLoader.TryGetMod("Verdant", out targetmod))
            {
                if (targetmod.TryFind<ModBiome>("VerdantBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Verdant");
                }
                if (targetmod.TryFind<ModBiome>("VerdantUndergroundBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Verdant Underground");
                }
            }
            // Remnants biomes checks
            if (ModLoader.TryGetMod("Remnants", out targetmod))
            {
                if (targetmod.TryFind<ModBiome>("GoldenCity", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Golden City");
                }
                if (targetmod.TryFind<ModBiome>("Growth", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Growth");
                }
                if (targetmod.TryFind<ModBiome>("OceanCave", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Ocean Cave");
                }
                if (targetmod.TryFind<ModBiome>("Pyramid", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Pyramid");
                }
                if (targetmod.TryFind<ModBiome>("Hive", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Giant Hive");
                }
                if (targetmod.TryFind<ModBiome>("JungleTemple", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Jungle Temple");
                }
                if (targetmod.TryFind<ModBiome>("MarbleCave", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Marble Cave");
                }
                if (targetmod.TryFind<ModBiome>("Vault", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Vault");
                }
            }
            // 
            if (ModLoader.TryGetMod("SpiritMod", out targetmod))
            {
                if (targetmod.TryFind<ModBiome>("AsteroidBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Asteroid");
                }
                if (targetmod.TryFind<ModBiome>("BriarSurfaceBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Briar Surface");
                }
                if (targetmod.TryFind<ModBiome>("BriarUndergroundBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Briar Underground");
                }
                if (targetmod.TryFind<ModBiome>("JellyDelugeBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Jelly Deluge");
                }
                if (targetmod.TryFind<ModBiome>("MysticMoonBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Mystic Moon");
                }
                if (targetmod.TryFind<ModBiome>("SpiritSurfaceBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Spirit Surface");
                }
                if (targetmod.TryFind<ModBiome>("SpiritUndergroundBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Spirit Underground");
                }
                if (targetmod.TryFind<ModBiome>("SynthwaveSurfaceBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Synthwave");
                }
                if (targetmod.TryFind<ModBiome>("TideBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Tide");
                }
            }
            // StarsAbove biomes checks
            if (ModLoader.TryGetMod("StarsAbove", out targetmod))
            {
                if (targetmod.TryFind<ModBiome>("AstarteDriverBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Astarte Driver");
                }
                if (targetmod.TryFind<ModBiome>("BleachedWorldBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Bleached World");
                }
                if (targetmod.TryFind<ModBiome>("CorvusBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Corvus");
                }
                if (targetmod.TryFind<ModBiome>("LyraBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Lyra");
                }
                if (targetmod.TryFind<ModBiome>("MoonBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Moon");
                }
                if (targetmod.TryFind<ModBiome>("ObservatoryBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Observatory");
                }
                if (targetmod.TryFind<ModBiome>("FriendlySpaceBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Pyxis");
                }
                if (targetmod.TryFind<ModBiome>("SeaOfStarsBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Sea of Stars");
                }
            }
            // Bob Blender checks
            if (ModLoader.TryGetMod("BobBlender2", out targetmod))
            {
                if (targetmod.TryFind<ModBiome>("TreeBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Tree");
                }
                if (targetmod.TryFind<ModBiome>("VoidBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Void");
                }
                if (targetmod.TryFind<ModBiome>("BobBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Bob Biome");
                }
                if (targetmod.TryFind<ModBiome>("BobsBobBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Bobs Bob Island");
                }
                if (targetmod.TryFind<ModBiome>("BobTowerBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Bob Tower");
                }
                if (targetmod.TryFind<ModBiome>("CorrodedBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Corroded");
                }
                if (targetmod.TryFind<ModBiome>("CursedBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Cursed");
                }
                if (targetmod.TryFind<ModBiome>("DarkmatterBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Darkmatter");
                }
                if (targetmod.TryFind<ModBiome>("MemeBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Meme");
                }
                if (targetmod.TryFind<ModBiome>("StrangeBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Strange Biome");
                }
                if (targetmod.TryFind<ModBiome>("StrangeDungeonBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Strange Dungeon");
                }
                if (targetmod.TryFind<ModBiome>("StrangeUndergroundBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Strange Undergrond");
                }
                if (targetmod.TryFind<ModBiome>("SurfaceBiome", out modbiome) && Main.LocalPlayer.InModBiome(modbiome))
                {
                    return GetCustomZoneName("Strange Surface");
                }
            }
            // Mod of Redemption biomes checks
            if (ModLoader.TryGetMod("Redemption", out targetmod))
            {
                string playerbiome = "";

                CheckModBiome(targetmod, "BlazingBastionBiome", "Blazing Bastion", ref playerbiome);
                CheckModBiome(targetmod, "FowlMorningBiome", "Fowl Morning", ref playerbiome);
                CheckModBiome(targetmod, "LabBiome", "Abandoned Laboratory", ref playerbiome);
                CheckModBiome(targetmod, "LidenBiome", "Liden", ref playerbiome);
                CheckModBiome(targetmod, "LidenBiomeAlpha", "Liden Alpha", ref playerbiome);
                CheckModBiome(targetmod, "LidenBiomeOmega", "Liden Omega", ref playerbiome);
                CheckModBiome(targetmod, "SlayerShipBiome", "Slayer Ship", ref playerbiome);
                CheckModBiome(targetmod, "SoullessBiome", "Soulless", ref playerbiome);
                CheckModBiome(targetmod, "WastelandCorruptionBiome", "Wasteland Corruption", ref playerbiome);
                CheckModBiome(targetmod, "WastelandCrimsonBiome", "Wasteland Crimson", ref playerbiome);
                CheckModBiome(targetmod, "WastelandDesertBiome", "Wasteland Desert", ref playerbiome);
                CheckModBiome(targetmod, "WastelandPurityBiome", "Wasteland Purity", ref playerbiome);
                CheckModBiome(targetmod, "WastelandSnowBiome", "Wasteland Snow", ref playerbiome);

                if (!string.IsNullOrEmpty(playerbiome))
                {
                    return playerbiome;
                }
            }
            // Check for "TheDepths" biome
            if (ModLoader.TryGetMod("TheDepths", out Mod theDepthsMod))
            {
                if (CheckModBiome(theDepthsMod, "DepthsBiome", "Depths", ref zone))
                {
                    return zone;
                }
            }


            // Check for "TheConfectionRebirth" biomes
            if (ModLoader.TryGetMod("TheConfectionRebirth", out Mod theConfectionRebirthMod))
            {
                if (CheckModBiome(theConfectionRebirthMod, "IceConfectionUndergroundBiome", "Underground Tundra C", ref zone) ||
                    CheckModBiome(theConfectionRebirthMod, "SandConfectionUndergroundBiome", "Underground Desert C", ref zone) ||
                    CheckModBiome(theConfectionRebirthMod, "ConfectionUndergroundBiome", "Underground Confection", ref zone) ||
                    CheckModBiome(theConfectionRebirthMod, "IceConfectionSurfaceBiome", "Tundra Confection", ref zone) ||
                    CheckModBiome(theConfectionRebirthMod, "SandConfectionSurfaceBiome", "Desert Confection", ref zone) ||
                    CheckModBiome(theConfectionRebirthMod, "ConfectionBiomeSurface", "Confection", ref zone))
                {
                    return zone;
                }
            }

            {
                // Terraria biome checks
                if (player.ZoneGlowshroom) zone = AddZone(zone, "Glowing Mushroom");
                if (player.ZoneDungeon) zone = AddZone(zone, "Dungeon");
                if (player.ZoneGranite) zone = AddZone(zone, "Granite");
                if (player.ZoneMarble) zone = AddZone(zone, "Marble");
                if (player.ZoneBeach) zone = AddZone(zone, "Ocean");
                if (player.ZoneDesert && (player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight)) zone = AddZone(zone, "Underground Desert");

                if (player.ZoneSkyHeight) zone = AddZone(zone, "Space");
                if (player.ZoneUnderworldHeight) zone = AddZone(zone, "Hell");

                if (player.ZoneJungle && player.ZoneRockLayerHeight) zone = AddZone(zone, "Underground Jungle");
                if (player.ZoneJungle && player.ZoneDirtLayerHeight) zone = AddZone(zone, "Underground Jungle");
                if (player.ZoneSnow && player.ZoneDirtLayerHeight) zone = AddZone(zone, "Underground Tundra");
                if (player.ZoneSnow && player.ZoneRockLayerHeight) zone = AddZone(zone, "Underground Tundra");

                if (player.ZoneCorrupt && (player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight)) zone = AddZone(zone, "Underground Corruption");
                if (player.ZoneCorrupt) zone = AddZone(zone, "Corruption");
                if (player.ZoneCrimson && (player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight)) zone = AddZone(zone, "Underground Crimson");
                if (player.ZoneCrimson) zone = AddZone(zone, "Crimson");
                if (player.ZoneDesert) zone = AddZone(zone, "Desert");
                if (player.ZoneHallow) zone = AddZone(zone, "Hallow");
                if (player.ZoneMeteor) zone = AddZone(zone, "Meteor");
                if (player.ZoneOldOneArmy) zone = AddZone(zone, "Old One's Army");
                if (player.ZoneSnow) zone = AddZone(zone, "Tundra");
                if (player.ZoneTowerNebula) zone = AddZone(zone, "Nebula Pillar");
                if (player.ZoneTowerSolar) zone = AddZone(zone, "Solar Pillar");
                if (player.ZoneTowerStardust) zone = AddZone(zone, "Stardust Pillar");
                if (player.ZoneTowerVortex) zone = AddZone(zone, "Vortex Pillar");
                if (player.ZoneJungle) zone = AddZone(zone, "Jungle");
                if (player.ZoneRockLayerHeight) zone = AddZone(zone, "Caverns");
                if (player.ZoneDirtLayerHeight) zone = AddZone(zone, "Underground");
                // ... other Terraria biome checks ...

                // Forest biome check
                if (!player.ZoneBeach && !player.ZoneCorrupt && !player.ZoneCrimson && !player.ZoneDesert &&
                    !player.ZoneDungeon && !player.ZoneGlowshroom && !player.ZoneGranite && !player.ZoneHallow &&
                    !player.ZoneJungle && !player.ZoneMarble && !player.ZoneMeteor && !player.ZoneOldOneArmy &&
                    !player.ZoneSnow && !player.ZoneTowerNebula && !player.ZoneTowerSolar &&
                    !player.ZoneTowerStardust && !player.ZoneTowerVortex && !player.ZoneUndergroundDesert)
                {
                    zone = AddZone(zone, "Forest");
                }
            }

            MyModConfig config = ModContent.GetInstance<MyModConfig>();

            // Find the custom name for the current zone, if available
            string customName = config.CustomBiomeNames.FirstOrDefault(mapping => mapping.CurrentName == zone)?.NewName;

            if (!string.IsNullOrEmpty(customName))
            {
                zone = customName;
            }

            return zone;
        }
    }
}
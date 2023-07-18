using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace BTitles
{
    public class CustomUIText : UIText
    {
        protected float _customTextScale = 2f;
        protected float _strokeScale = 2f; // Set equal to _customTextScale
        protected float _opacity;
        protected DynamicSpriteFont _customFont;

        public CustomUIText(string text, float textScale = 1f) : base(text, textScale)
        {
            _customTextScale = textScale;
            _customFont = CreateCustomFont();
            _opacity = 1f;
        }

        public float Opacity
        {
            get => _opacity;
            set => _opacity = MathHelper.Clamp(value, 0f, 1f);
        }
        private (Color textColor, Color strokeColor) GetColorsForZone(string zone)
        {
            MyModConfig config = ModContent.GetInstance<MyModConfig>();

            // Use the original biome name as the key to look up custom colors
            string originalZoneName = config.CustomBiomeNames.FirstOrDefault(mapping => mapping.NewName == zone)?.CurrentName ?? zone;

            if (!config.UseCustomTextColors)
            {
                return (Color.White, Color.Black);
            }

            switch (originalZoneName)
            {
                case "Glowing Mushroom":
                    return (Color.Blue, Color.Black);
                case "Dungeon":
                    return (Color.DarkBlue, Color.Black);
                case "Granite":
                    return (Color.DarkSlateBlue, Color.Black);
                case "Space":
                    return (Color.CornflowerBlue, Color.Black);
                case "Hell":
                    return (Color.Red, Color.Black);
                case "Forest":
                    return (Color.Green, Color.Black);
                case "Desert":
                    return (Color.Yellow, Color.Black);
                case "Celestial Surface":
                    return (Color.Magenta, Color.Black);
                case "Garden":
                    return (Color.MediumSeaGreen, Color.Black);
                case "Garden Surface":
                    return (Color.ForestGreen, Color.Black);
                case "Fake Underworld":
                    return (Color.DarkRed, Color.Black);
                case "Glowing Moss":
                    return (Color.LimeGreen, Color.Black);
                case "Gore Nest":
                    return (Color.IndianRed, Color.Black);
                case "Radon Moss":
                    return (Color.Green, Color.Black);
                case "Crab Crevice":
                    return (Color.LightSeaGreen, Color.Black);
                case "Depths":
                    return (Color.Gray, Color.Black);
                case "Confection":
                    return (Color.Beige, Color.Black);
                case "Underground Confection":
                    return (Color.Beige, Color.Black);
                case "Desert Confection":
                    return (Color.LightYellow, Color.Black);
                case "Tundra Confection":
                    return (Color.Beige, Color.Black);
                case "Tundra Underground C":
                    return (Color.Beige, Color.Black);
                case "Desert Underground C":
                    return (Color.LightYellow, Color.Black);
                case "Catacomb":
                    return (Color.DarkOliveGreen, Color.Black);
                case "Spooky Forest":
                    return (Color.Orange, Color.Black);
                case "Underground Spooky":
                    return (Color.OrangeRed, Color.Black);
                case "Spooky Hell":
                    return (Color.DarkViolet, Color.Black);
                case "Asteroid":
                    return (Color.Black, Color.DarkGray);
                case "Briar Surface":
                    return (Color.DarkGreen, Color.Black);
                case "Briar Underground":
                    return (Color.ForestGreen, Color.Black);
                case "Jelly Deluge":
                    return (Color.LightBlue, Color.Black);
                case "Mystic Moon":
                    return (Color.Cyan, Color.Black);
                case "Spirit Surface":
                    return (Color.DeepSkyBlue, Color.Black);
                case "Spirit Underground":
                    return (Color.SkyBlue, Color.Black);
                case "Synthwave":
                    return (Color.DarkViolet, Color.Black);
                case "Tide":
                    return (Color.BlueViolet, Color.Black);
                case "Aquatic Depths":
                    return (Color.Cyan, Color.Black);
                case "Blood Chamber":
                    return (Color.Red, Color.Black);
                case "Marble Cave":
                    return (Color.LightGray, Color.Black);
                case "Vault":
                    return (Color.Gold, Color.Black);
                case "Astarte Driver":
                    return (Color.Magenta, Color.Black);
                case "Bleached World":
                    return (Color.BlanchedAlmond, Color.Black);
                case "Corvus":
                    return (Color.DarkSlateGray, Color.Black);
                case "Lyra":
                    return (Color.Orchid, Color.Black);
                case "Moon":
                    return (Color.SlateGray, Color.Black);
                case "Observatory":
                    return (Color.LightYellow, Color.Black);
                case "Pyxis":
                    return (Color.RosyBrown, Color.Black);
                case "Sea of Stars":
                    return (Color.Navy, Color.Black);
                case "Brimstone Crag":
                    return (Color.OrangeRed, Color.Black);
                case "Astral Infection":
                    return (Color.DarkViolet, Color.Black);
                case "Sunken Sea":
                    return (Color.Teal, Color.Black);
                case "Blazing Bastion":
                    return (Color.Orange, Color.Black);
                case "Fowl Morning":
                    return (Color.LightYellow, Color.Black);
                case "Abandoned Laboratory":
                    return (Color.Gray, Color.Black);
                case "Liden":
                    return (Color.LightGreen, Color.Black);
                case "Liden Alpha":
                    return (Color.LightCoral, Color.Black);
                case "Liden Omega":
                    return (Color.PaleVioletRed, Color.Black);
                case "Slayer Ship":
                    return (Color.MediumPurple, Color.Black);
                case "Soulless":
                    return (Color.DarkGreen, Color.Black);
                case "Verdant":
                    return (Color.YellowGreen, Color.Black);
                case "Verdant Underground":
                    return (Color.LightPink, Color.Black);
                case "Wasteland Corruption":
                    return (Color.Purple, Color.Black);
                case "Wasteland Crimson":
                    return (Color.DarkRed, Color.Black);
                case "Wasteland Desert":
                    return (Color.Khaki, Color.Black);
                case "Wasteland Purity":
                    return (Color.LightBlue, Color.Black);
                case "Wasteland Snow":
                    return (Color.White, Color.Black);
                case "Sulphurous Sea":
                    return (Color.LightSeaGreen, Color.Black);
                case "Arbour Island":
                    return (Color.Orange, Color.Black);
                case "Abyss":
                    return (Color.MediumBlue, Color.Black);
                case "Layer 1":
                case "Layer 2":
                case "Layer 3":
                case "Layer 4":
                    return (Color.DarkSlateBlue, Color.Black);
                case "Golden City":
                    return (Color.Gold, Color.Black);
                case "Growth":
                    return (Color.ForestGreen, Color.Black);
                case "Ocean Cave":
                    return (Color.HotPink, Color.Black);
                case "Pyramid":
                    return (Color.Goldenrod, Color.Black);
                case "Giant Hive":
                    return (Color.Yellow, Color.Black);
                case "Jungle Temple":
                    return (Color.DarkGreen, Color.Black);
                case "Vitric Desert":
                    return (Color.Tan, Color.Black);
                case "Hotspring":
                    return (Color.Orange, Color.Black);
                case "Moonstone":
                    return (Color.Silver, Color.Black);
                case "Lost Colosseum":
                    return (Color.DarkGoldenrod, Color.Black);
                case "Profaned Temple":
                    return (Color.IndianRed, Color.Black);
                case "Overgrow":
                    return (Color.DarkGreen, Color.Black);
                case "Permafrost Temple":
                    return (Color.LightCyan, Color.Black);
                case "Vitric Temple":
                    return (Color.Turquoise, Color.Black);
                case "Marble":
                    return (Color.LightGray, Color.Black);
                case "Ocean":
                    return (Color.DeepSkyBlue, Color.Black);
                case "Underground Desert":
                    return (Color.Yellow, Color.Black);
                case "Underground Jungle":
                    return (Color.LimeGreen, Color.Black);
                case "Caverns":
                    return (Color.DarkSlateGray, Color.Black);
                case "Underground":
                    return (Color.SaddleBrown, Color.Black);
                case "Corruption":
                    return (Color.Purple, Color.Black);
                case "Crimson":
                    return (Color.Red, Color.Black);
                case "Underground Crimson":
                    return (Color.Red, Color.Black);
                case "Underground Corruption":
                    return (Color.Purple, Color.Black);
                case "Underground Hallow":
                    return (Color.LightBlue, Color.Black);
                case "Underground Tundra":
                    return (Color.LightCyan, Color.Black);
                case "Hallow":
                    return (Color.LightBlue, Color.Black);
                case "Meteor":
                    return (Color.OrangeRed, Color.Black);
                case "Old One's Army":
                    return (Color.Magenta, Color.Black);
                case "Tundra":
                    return (Color.LightCyan, Color.Black);
                case "Nebula Pillar":
                    return (Color.Magenta, Color.Black);
                case "Solar Pillar":
                    return (Color.Orange, Color.Black);
                case "Stardust Pillar":
                    return (Color.Yellow, Color.Black);
                case "Vortex Pillar":
                    return (Color.LightBlue, Color.Black);
                case "Jungle":
                    return (Color.LimeGreen, Color.Black);
                case "Tree":
                    return (Color.ForestGreen, Color.Black);
                case "Void":
                    return (Color.Purple, Color.Black);
                case "Bob Biome":
                    return (Color.Brown, Color.Black);
                case "Bobs Bob Island":
                    return (Color.Yellow, Color.Black);
                case "Bob Tower":
                    return (Color.SlateGray, Color.Black);
                case "Corroded":
                    return (Color.DarkOliveGreen, Color.Black);
                case "Cursed":
                    return (Color.DarkOrange, Color.Black);
                case "Darkmatter":
                    return (Color.DarkSlateBlue, Color.Black);
                case "Meme":
                    return (Color.MediumPurple, Color.Black);
                case "Strange Biome":
                    return (Color.Purple, Color.Black);
                case "Strange Dungeon":
                    return (Color.DarkViolet, Color.Black);
                case "Strange Undergrond":
                    return (Color.DarkViolet, Color.Black);
                case "Strange Surface":
                    return (Color.Purple, Color.Black);
                default:
                    return (Color.White, Color.Black);

            }
        }
        private string GetSubtitleForZone(string zone)
        {
            MyModConfig config = ModContent.GetInstance<MyModConfig>();

            // Use the original biome name as the key to look up the subtitle
            string originalZoneName = config.CustomBiomeNames.FirstOrDefault(mapping => mapping.NewName == zone)?.CurrentName ?? zone;

            switch (originalZoneName)
            {
                // Thorium Mod biomes
                case "Aquatic Depths":
                case "Blood Chamber":
                    return "Thorium";
                // Bob Blender biomes
                case "Tree":
                case "Void":
                case "Bob Biome":
                case "Bobs Bob Island":
                case "Bob Tower":
                case "Corroded":
                case "Cursed":
                case "Darkmatter":
                case "Meme":
                case "Strange Biome":
                case "Strange Dungeon":
                case "Strange Undergrond":
                case "Strange Surface":
                    return "Bob Blender";
                // Mod of Redemption biomes
                case "Blazing Bastion":
                case "Fowl Morning":
                case "Abandoned Laboratory":
                case "Liden":
                case "Liden Alpha":
                case "Liden Omega":
                case "Slayer Ship":
                case "Soulless":
                case "Wasteland Corruption":
                case "Wasteland Crimson":
                case "Wasteland Desert":
                case "Wasteland Purity":
                case "Wasteland Snow":
                    return "Redemption";
                // FFF biomes
                case "Celestial Surface":
                case "Garden":
                case "Garden Surface":
                    return "Furniture, Food, and Fun";
                // Aequus biomes
                case "Fake Underworld":
                case "Glowing Moss":
                case "Gore Nest":
                case "Radon Moss":
                case "Crab Crevice":
                    return "Aequus";
                // Depths biome
                case "Depths":
                    return "Depths";

                // Confection Rebaked
                case "Confection":
                case "Underground Confection":
                case "Desert Confection":
                case "Tundra Confection":
                case "Tundra Underground C":
                case "Desert Underground C":
                    return "Confection Rebaked";

                // Verdant biomes
                case "Verdant":
                case "Verdant Underground":
                    return "Verdant";

                // Arbour biome
                case "Arbour Island":
                    return "Arbour";

                // Remnants biomes
                case "Golden City":
                case "Growth":
                case "Ocean Cave":
                case "Pyramid":
                case "Giant Hive":
                case "Jungle Temple":
                case "Marble Cave":
                case "Vault":
                    return "Remnants";

                // Spooky biomes
                case "Catacomb":
                case "Spooky Forest":
                case "Underground Spooky":
                case "Spooky Hell":
                    return "Spooky";

                // SpiritMod biomes
                case "Asteroid":
                case "Briar Surface":
                case "Briar Underground":
                case "Jelly Deluge":
                case "Mystic Moon":
                case "Spirit Surface":
                case "Spirit Underground":
                case "Synthwave":
                case "Tide":
                    return "Spirit Mod";

                // StarsAbove biomes
                case "Astarte Driver":
                case "Bleached World":
                case "Corvus":
                case "Lyra":
                case "Moon":
                case "Observatory":
                case "Pyxis":
                case "Sea of Stars":
                    return "The Stars Above";

                // Calamity Mod biomes
                case "Brimstone Crag":
                case "Astral Infection":
                case "Sunken Sea":
                case "Sulphurous Sea":
                case "Abyss":
                case "Layer 1":
                case "Layer 2":
                case "Layer 3":
                case "Layer 4":
                    return "Calamity";

                // StarlightRiver biomes
                case "Vitric Desert":
                case "Hotspring":
                case "Moonstone":
                case "Overgrow":
                case "Permafrost Temple":
                case "Vitric Temple":
                    return "Starlight River";

                // Infernum biomes
                case "Lost Colosseum":
                case "Profaned Temple":
                    return "Calamity Infernum";

                // Terraria biomes
                case "Glowing Mushroom":
                case "Dungeon":
                case "Forest":
                case "Desert":
                case "Ocean":
                case "Tundra":
                case "Jungle":
                case "Corruption":
                case "Granite":
                case "Marble":
                case "Crimson":
                case "Hallow":
                case "Underground":
                case "Underground Desert":
                case "Underground Jungle":
                case "Underground Snow":
                case "Underground Corruption":
                case "Underground Crimson":
                case "Underground Hallow":
                case "Underground Tundra":
                case "Underworld":
                case "Space":
                case "Sky":
                case "Hell":
                case "Caverns":
                    return "Terraria";


                // Add more cases for other mods and biomes here

                default:
                    return "";
            }
        }
        private DynamicSpriteFont CreateCustomFont()
        {
            return ModContent.Request<DynamicSpriteFont>("Terraria/Fonts/Death_Text").Value;
        }

        public void UpdateTextScale(float scale)
        {
            _customTextScale = scale;
            _strokeScale = scale; // Update to match
            Recalculate();
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle innerDimensions = GetInnerDimensions();
            Vector2 position = innerDimensions.Position();
            Vector2 origin = new Vector2((float)_customFont.MeasureString(Text).X / 2f, (float)_customFont.LineSpacing / 2f); // Center the text

            (Color textColor, Color strokeColor) = GetColorsForZone(Text);
            textColor *= _opacity;
            strokeColor *= _opacity;

            // Draw the black stroke around the text
            for (int offsetX = -1; offsetX <= 1; offsetX++)
            {
                for (int offsetY = -1; offsetY <= 1; offsetY++)
                {
                    spriteBatch.DrawString(_customFont, Text, position + new Vector2(offsetX, offsetY), strokeColor, 0f, origin, _strokeScale, SpriteEffects.None, 0f);
                }
            }

            // Draw the colored text on top
            spriteBatch.DrawString(_customFont, Text, position, textColor, 0f, origin, _customTextScale, SpriteEffects.None, 0f);

            // Draw the subtitle
            if (ModContent.GetInstance<MyModConfig>().DisplaySubtitle)
            {
                string subtitle = GetSubtitleForZone(Text);
                if (!string.IsNullOrEmpty(subtitle))
                {
                    float subtitleScale = _customTextScale * 0.5f;
                    Vector2 subtitlePosition = position + new Vector2(0, _customFont.LineSpacing * _customTextScale - _customFont.LineSpacing * subtitleScale / 2 - 24);
                    Vector2 subtitleOrigin = new Vector2((float)_customFont.MeasureString(subtitle).X / 2f, (float)_customFont.LineSpacing / 2f); // Center the subtitle

                    spriteBatch.DrawString(_customFont, subtitle, subtitlePosition, Color.DarkGray * _opacity, 0f, subtitleOrigin, subtitleScale, SpriteEffects.None, 0f);
                }
            }
        }
    }
}
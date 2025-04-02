using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BTitles.BuiltinModSupport;

public class BuiltinLunarVeil : AutoImplementedModSupport
{
    protected override string GetTargetModName() => "Stellamod";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = null;

        biomes = new Dictionary<string, BiomeEntry>
        {
            {"AbyssBiome", new BiomeEntry{ Title = "Abyss", SubTitle = "Lunar Veil", TitleColor = Color.DarkBlue, StrokeColor = Color.Black }},
            {"AcidBiome", new BiomeEntry{ Title = "Acid Plains", SubTitle = "Lunar Veil", TitleColor = Color.LimeGreen, StrokeColor = Color.Black }},
            {"IshtarBiome", new BiomeEntry{ Title = "Ishtar", SubTitle = "Lunar Veil", TitleColor = Color.MediumPurple, StrokeColor = Color.Black }},
            {"MorrowUndergroundBiome", new BiomeEntry{ Title = "Morrow Underground", SubTitle = "Lunar Veil", TitleColor = Color.ForestGreen, StrokeColor = Color.Black }},
            {"VeriplantUndergroundBiome", new BiomeEntry{ Title = "Veriplant Underground", SubTitle = "Lunar Veil", TitleColor = Color.Green, StrokeColor = Color.Black }},
            
            {"AlcadziaBiome", new BiomeEntry{ Title = "Alcadzia", SubTitle = "Lunar Veil", TitleColor = Color.Gold, StrokeColor = Color.Black }},
            {"CathedralBiome", new BiomeEntry{ Title = "Cathedral", SubTitle = "Lunar Veil", TitleColor = Color.LightSkyBlue, StrokeColor = Color.Black }},
            {"FableBiome", new BiomeEntry{ Title = "Fabled Wilds", SubTitle = "Lunar Veil", TitleColor = Color.MediumSpringGreen, StrokeColor = Color.Black }},
            {"IlluriaBiome", new BiomeEntry{ Title = "Illuria", SubTitle = "Lunar Veil", TitleColor = Color.LightGoldenrodYellow, StrokeColor = Color.Black }},
            {"MarrowSurfaceBiome", new BiomeEntry{ Title = "Marrow Surface", SubTitle = "Lunar Veil", TitleColor = Color.LemonChiffon, StrokeColor = Color.Black }},
            {"StarbloomBiome", new BiomeEntry{ Title = "Starbloom", SubTitle = "Lunar Veil", TitleColor = Color.LightYellow, StrokeColor = Color.Black }},
            {"VeilBiome", new BiomeEntry{ Title = "Veil", SubTitle = "Lunar Veil", TitleColor = Color.MediumPurple, StrokeColor = Color.Black }},
            {"XixVillage", new BiomeEntry{ Title = "Xix Village", SubTitle = "Lunar Veil", TitleColor = Color.LightGreen, StrokeColor = Color.Black }},
            
            {"AurelusBiome", new BiomeEntry{ Title = "Aurelus Temple", SubTitle = "Lunar Veil", TitleColor = Color.Gold, StrokeColor = Color.Black }},
            {"CatacombFlames", new BiomeEntry{ Title = "Catacombs - Flames", SubTitle = "Lunar Veil", TitleColor = Color.OrangeRed, StrokeColor = Color.Black }},
            {"CatacombTrap", new BiomeEntry{ Title = "Catacombs - Trap", SubTitle = "Lunar Veil", TitleColor = Color.Brown, StrokeColor = Color.Black }},
            {"CatacombWater", new BiomeEntry{ Title = "Catacombs - Water", SubTitle = "Lunar Veil", TitleColor = Color.Blue, StrokeColor = Color.Black }},
            {"CindersparkBiome", new BiomeEntry{ Title = "Cinderspark", SubTitle = "Lunar Veil", TitleColor = Color.Firebrick, StrokeColor = Color.Black }},
            {"DrakonicManor", new BiomeEntry{ Title = "Drakonic Manor", SubTitle = "Lunar Veil", TitleColor = Color.DarkRed, StrokeColor = Color.Black }},
            {"EngineerShop", new BiomeEntry{ Title = "Engineer's Shop", SubTitle = "Lunar Veil", TitleColor = Color.Silver, StrokeColor = Color.Black }},
            {"GovheilCastle", new BiomeEntry{ Title = "Govheil Castle", SubTitle = "Lunar Veil", TitleColor = Color.Goldenrod, StrokeColor = Color.Black }},
            {"Laboratory", new BiomeEntry{ Title = "Laboratory", SubTitle = "Lunar Veil", TitleColor = Color.LightGreen, StrokeColor = Color.Black }},
            {"NaxtrinTemple", new BiomeEntry{ Title = "Naxtrin Temple", SubTitle = "Lunar Veil", TitleColor = Color.DarkGoldenrod, StrokeColor = Color.Black }},
            {"SeaTemple", new BiomeEntry{ Title = "Sea Temple", SubTitle = "Lunar Veil", TitleColor = Color.Aqua, StrokeColor = Color.Black }},
        };
    }
}

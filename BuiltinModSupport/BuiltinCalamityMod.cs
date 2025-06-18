using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BTitles.BuiltinModSupport;

public class BuiltinCalamityMod : AutoImplementedModSupport
{
    protected override string GetTargetModName() => "CalamityMod";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = null;

        biomes = new Dictionary<string, BiomeEntry>
        {
            {"AstralInfectionBiome",         new BiomeEntry{ Title = "Astral Infection",   SubTitle = "Calamity Mod", TitleColor = Color.DarkViolet,    StrokeColor = Color.Black }},
            {"BrimstoneCragsBiome",          new BiomeEntry{ Title = "Brimstone Crags",    SubTitle = "Calamity Mod", TitleColor = Color.OrangeRed,     StrokeColor = Color.Black }},
            {"SulphurousSeaBiome",           new BiomeEntry{ Title = "Sulphurous Sea",     SubTitle = "Calamity Mod", TitleColor = Color.LightSeaGreen, StrokeColor = Color.Black }},
            {"SunkenSeaBiome",               new BiomeEntry{ Title = "Sunken Sea",         SubTitle = "Calamity Mod", TitleColor = Color.Teal,          StrokeColor = Color.Black }},
            {"AbyssLayer1Biome",             new BiomeEntry{ Title = "Sulphuric Depths",   SubTitle = "Calamity Mod", TitleColor = Color.DarkSlateBlue, StrokeColor = Color.Black }},
            {"AbyssLayer2Biome",             new BiomeEntry{ Title = "Murky Waters",       SubTitle = "Calamity Mod", TitleColor = Color.DarkSlateBlue, StrokeColor = Color.Black }},
            {"AbyssLayer3Biome",             new BiomeEntry{ Title = "Thermal Vents",      SubTitle = "Calamity Mod", TitleColor = Color.DarkSlateBlue, StrokeColor = Color.Black }},
            {"AbyssLayer4Biome",             new BiomeEntry{ Title = "The Void",           SubTitle = "Calamity Mod", TitleColor = Color.DarkSlateBlue, StrokeColor = Color.Black }},
        };
    }
}
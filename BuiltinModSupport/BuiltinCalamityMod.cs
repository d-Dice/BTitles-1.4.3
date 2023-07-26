using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace BTitles.BuiltinModSupport;

public class BuiltinCalamityMod : AutoImplementedModSupport
{
    protected override string GetTargetModName() => "CalamityMod";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = null;

        biomes = new Dictionary<string, BiomeEntry>
        {
            {"UndergroundAstralBiome",       new BiomeEntry{ Title = "Underground Astral Infection", SubTitle = "Calamity Mod", TitleColor = Color.Indigo,        StrokeColor = Color.Black }},
            {"AbovegroundAstralDesertBiome", new BiomeEntry{ Title = "Astral Infection Desert",      SubTitle = "Calamity Mod", TitleColor = Color.BlueViolet,    StrokeColor = Color.Black }},
            {"AbovegroundAstralSnowBiome",   new BiomeEntry{ Title = "Astral Infection Tundra",      SubTitle = "Calamity Mod", TitleColor = Color.Orchid,        StrokeColor = Color.Black }},
            {"AbovegroundAstralBiome",       new BiomeEntry{ Title = "Astral Infection",             SubTitle = "Calamity Mod", TitleColor = Color.DarkViolet,    StrokeColor = Color.Black }},
            {"BrimstoneCragsBiome",          new BiomeEntry{ Title = "Brimstone Crags",              SubTitle = "Calamity Mod", TitleColor = Color.OrangeRed,     StrokeColor = Color.Black }},
            {"SulphurousSeaBiome",           new BiomeEntry{ Title = "Sulphurous Sea",               SubTitle = "Calamity Mod", TitleColor = Color.LightSeaGreen, StrokeColor = Color.Black }},
            {"SunkenSeaBiome",               new BiomeEntry{ Title = "Sunken Sea",                   SubTitle = "Calamity Mod", TitleColor = Color.Teal,          StrokeColor = Color.Black }},
            {"AbyssLayer1Biome",             new BiomeEntry{ Title = "Sulphuric Depths",             SubTitle = "Calamity Mod", TitleColor = Color.DarkSlateBlue, StrokeColor = Color.Black }},
            {"AbyssLayer2Biome",             new BiomeEntry{ Title = "Murky Waters",                 SubTitle = "Calamity Mod", TitleColor = Color.DarkSlateBlue, StrokeColor = Color.Black }},
            {"AbyssLayer3Biome",             new BiomeEntry{ Title = "Thermal Vents",                SubTitle = "Calamity Mod", TitleColor = Color.DarkSlateBlue, StrokeColor = Color.Black }},
            {"AbyssLayer4Biome",             new BiomeEntry{ Title = "The Void",                     SubTitle = "Calamity Mod", TitleColor = Color.DarkSlateBlue, StrokeColor = Color.Black }},
        };
    }
}
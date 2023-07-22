using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace BTitles.BuiltinModSupport;

public class BuiltinAequus : AutoImplementedModSupport
{
    protected override string GetTargetModName() => "Aequus";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = new Dictionary<string, BiomeEntry>
        {
            {"GlowingMossBiome", new BiomeEntry{ Title = "Glowing Moss", SubTitle = "Aequus", TitleColor = Color.LimeGreen,     StrokeColor = Color.Black }},
            {"GoreNestBiome",    new BiomeEntry{ Title = "Gore Nest",    SubTitle = "Aequus", TitleColor = Color.IndianRed,     StrokeColor = Color.Black }},
            {"RadonMossBiome",   new BiomeEntry{ Title = "Radon Moss",   SubTitle = "Aequus", TitleColor = Color.Green,         StrokeColor = Color.Black }},
        };

        biomes = new Dictionary<string, BiomeEntry>
        {
            {"CrabCreviceBiome",    new BiomeEntry{ Title = "Crab Crevice",    SubTitle = "Aequus", TitleColor = Color.LightSeaGreen, StrokeColor = Color.Black }},
            {"FakeUnderworldBiome", new BiomeEntry{ Title = "Fake Underworld", SubTitle = "Aequus", TitleColor = Color.DarkRed,       StrokeColor = Color.Black }},
        };
    }
}
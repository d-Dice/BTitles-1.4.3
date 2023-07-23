using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BTitles.BuiltinModSupport;

public class BuiltinVerdant : AutoImplementedModSupport
{
    protected override string GetTargetModName() => "Verdant";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = new Dictionary<string, BiomeEntry>
        {
            {"VerdantBiome",            new BiomeEntry{ Title = "Verdant",             SubTitle = "Verdant", TitleColor = Color.YellowGreen, StrokeColor = Color.Black }},
            {"VerdantUndergroundBiome", new BiomeEntry{ Title = "Verdant Underground", SubTitle = "Verdant", TitleColor = Color.LightPink,   StrokeColor = Color.Black }},
        };

        biomes = null;
    }
}
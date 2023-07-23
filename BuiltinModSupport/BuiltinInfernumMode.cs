using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BTitles.BuiltinModSupport;

public class BuiltinInfernumMode : AutoImplementedModSupport
{
    protected override string GetTargetModName() => "InfernumMode";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = new Dictionary<string, BiomeEntry>
        {
            {"LostColosseumBiome",  new BiomeEntry{ Title = "Lost Colosseum",  SubTitle = "Infernum Mode", TitleColor = Color.DarkGoldenrod, StrokeColor = Color.Black }},
            {"ProfanedTempleBiome", new BiomeEntry{ Title = "Profaned Temple", SubTitle = "Infernum Mode", TitleColor = Color.IndianRed,     StrokeColor = Color.Black }},
        };

        biomes = null;
    }
}
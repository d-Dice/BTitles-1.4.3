using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BTitles.BuiltinModSupport;

public class BuiltinEverjade : AutoImplementedModSupport
{
    protected override string GetTargetModName() => "JadeFables";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = null;

        biomes = new Dictionary<string, BiomeEntry>
        {
            {"JadeLakeBiome",       new BiomeEntry{ Title = "Jade Lake", SubTitle = "Awakened Light", TitleColor = Color.Cyan,        StrokeColor = Color.Black }},
        };
    }
}
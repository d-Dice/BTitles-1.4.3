using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BTitles.BuiltinModSupport;

public class BuiltinArbour : AutoImplementedModSupport
{
    protected override string GetTargetModName() => "Arbour";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = new Dictionary<string, BiomeEntry>
        {
            {"ArborBiome", new BiomeEntry{ Title = "Arbour Island", SubTitle = "Arbour", TitleColor = Color.Orange, StrokeColor = Color.Black }},
        };

        biomes = null;
    }
}
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BTitles.BuiltinModSupport;

public class BuiltinTheDepths : AutoImplementedModSupport
{
    protected override string GetTargetModName() => "TheDepths";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = null;

        biomes = new Dictionary<string, BiomeEntry>
        {
            {"DepthsBiome", new BiomeEntry{ Title = "Depths", SubTitle = "The Depths", TitleColor = Color.Gray, StrokeColor = Color.Black }},
        };
    }
}
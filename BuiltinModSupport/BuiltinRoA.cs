using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BTitles.BuiltinModSupport;

public class BuiltinRoA : AutoImplementedModSupport
{
    protected override string GetTargetModName() => "RoA";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = new Dictionary<string, BiomeEntry>
        {
            {"BackwoodsBiome", new BiomeEntry{ Title = "Backwoods", SubTitle = "Rise of Ages", TitleColor = Color.DarkGreen, StrokeColor = Color.Black }},
        };

        biomes = new Dictionary<string, BiomeEntry>();
    }
}
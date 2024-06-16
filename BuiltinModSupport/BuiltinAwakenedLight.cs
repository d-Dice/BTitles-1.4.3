using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BTitles.BuiltinModSupport;

public class BuiltinAwakenedLight : AutoImplementedModSupport
{
    protected override string GetTargetModName() => "MultidimensionMod";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = null;

        biomes = new Dictionary<string, BiomeEntry>
        {
            {"FrozenUnderworld",       new BiomeEntry{ Title = "Frozen Underworld", SubTitle = "Awakened Light", TitleColor = Color.LightGray,        StrokeColor = Color.Cyan }},
            {"ShroomForest", new BiomeEntry{ Title = "Shroom Forest",      SubTitle = "Awakened Light", TitleColor = Color.Orange,    StrokeColor = Color.Black }},
        };
    }
}
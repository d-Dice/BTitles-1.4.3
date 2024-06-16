using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BTitles.BuiltinModSupport;

public class BuiltinITD : AutoImplementedModSupport
{
    protected override string GetTargetModName() => "ITD";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = null;

        biomes = new Dictionary<string, BiomeEntry>
        {
            { "BlueshroomGrovesBiome",  new BiomeEntry { Title = "Blueshroom Groves", SubTitle = "Into The Depths", TitleColor = Color.Cyan, StrokeColor = Color.Black }},
            { "BlueshroomGrovesSurfaceBiome", new BiomeEntry { Title = "Blueshroom Groves", SubTitle = "Into The Depths", TitleColor = Color.Cyan, StrokeColor = Color.Black }},
        };
    }
}
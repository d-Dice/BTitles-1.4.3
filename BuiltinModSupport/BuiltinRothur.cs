using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BTitles.BuiltinModSupport;

public class BuiltinRothur : AutoImplementedModSupport
{
    protected override string GetTargetModName() => "RothurModRedux";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = null;

        biomes = new Dictionary<string, BiomeEntry>
        {
            {"CrystalSurfaceBiome",       new BiomeEntry{ Title = "Crystal", SubTitle = "Rothur", TitleColor = Color.HotPink,        StrokeColor = Color.Black }},
            {"CrystalUndergroundBiome", new BiomeEntry{ Title = "Crystal Underground",      SubTitle = "Rothur", TitleColor = Color.DeepPink,    StrokeColor = Color.Black }},
        };
    }
}
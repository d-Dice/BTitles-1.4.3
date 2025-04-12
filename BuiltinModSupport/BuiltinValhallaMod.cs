using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BTitles.BuiltinModSupport;

public class BuiltinValhallaMod : AutoImplementedModSupport
{
    protected override string GetTargetModName() => "ValhallaMod";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = null;

        biomes = new Dictionary<string, BiomeEntry>
        {
            {"TarBiome",            new BiomeEntry{ Title = "Tar Cave", SubTitle = "Valhalla Mod", TitleColor = Color.DarkGray, StrokeColor = Color.Black }},
        };
    }
}
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BTitles.BuiltinModSupport;

public class BuiltinSpiritReforged : AutoImplementedModSupport
{
    protected override string GetTargetModName() => "SpiritReforged";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = new Dictionary<string, BiomeEntry>
        {
            {"HallowSavanna", new BiomeEntry{ Title = "Hallow Savanna", SubTitle = "Spirit Reforged", TitleColor = Color.HotPink, StrokeColor = Color.Black }},
            {"SavannaBiome", new BiomeEntry{ Title = "Savanna", SubTitle = "Spirit Reforged", TitleColor = Color.Goldenrod, StrokeColor = Color.Black }}
        };

        biomes = new Dictionary<string, BiomeEntry>();
    }
}
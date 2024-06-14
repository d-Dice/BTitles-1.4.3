using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BTitles.BuiltinModSupport;

public class BuiltinContinentOfJourney : AutoImplementedModSupport
{
    protected override string GetTargetModName() => "ContinentOfJourney";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = null;

        biomes = new Dictionary<string, BiomeEntry>
        {
            {"AbyssBiome",  new BiomeEntry{ Title = "Abyss",         SubTitle = "Homeward Journey", TitleColor = Color.Blue,       StrokeColor = Color.Black }},
        };
    }
}
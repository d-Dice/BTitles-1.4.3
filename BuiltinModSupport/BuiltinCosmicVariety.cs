using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BTitles.BuiltinModSupport;

public class BuiltinCosmicVariety : AutoImplementedModSupport
{
    protected override string GetTargetModName() => "CosmeticVariety";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = null;

        biomes = new Dictionary<string, BiomeEntry>
        {
            {"CelestialSurfaceBiome", new BiomeEntry{ Title = "Celestial Surface", SubTitle = "Cosmetic Variety", TitleColor = Color.Magenta,        StrokeColor = Color.Black }},
            {"GardenBiome",           new BiomeEntry{ Title = "Garden",            SubTitle = "Cosmetic Variety", TitleColor = Color.MediumSeaGreen, StrokeColor = Color.Black }},
            {"GardenSurfaceBiome",    new BiomeEntry{ Title = "Garden Surface",    SubTitle = "Cosmetic Variety", TitleColor = Color.ForestGreen,    StrokeColor = Color.Black }}
        };
    }
}
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BTitles.BuiltinModSupport;

public class BuiltinCosmeticVariety : AutoImplementedModSupport
{
    protected override string GetTargetModName() => "CosmeticVariety";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = null;

        biomes = new Dictionary<string, BiomeEntry>
        {
            {"CelestialSurfaceBiome", new BiomeEntry{ Title = "Celestial Surface", SubTitle = "Details of Furniture, Food, and Fun", TitleColor = Color.Magenta,        StrokeColor = Color.Black }},
            {"GardenBiome",           new BiomeEntry{ Title = "Garden",            SubTitle = "Details of Furniture, Food, and Fun", TitleColor = Color.MediumSeaGreen, StrokeColor = Color.Black }},
            {"GardenSurfaceBiome",    new BiomeEntry{ Title = "Garden Surface",    SubTitle = "Details of Furniture, Food, and Fun", TitleColor = Color.ForestGreen,    StrokeColor = Color.Black }}
        };
    }
}
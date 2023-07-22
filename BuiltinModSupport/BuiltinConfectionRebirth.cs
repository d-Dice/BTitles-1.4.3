using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BTitles.BuiltinModSupport;

public class BuiltinConfectionRebirth : AutoImplementedModSupport
{
    protected override string GetTargetModName() => "TheConfectionRebirth";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = null;

        biomes = new Dictionary<string, BiomeEntry>
        {
            {"IceConfectionUndergroundBiome",  new BiomeEntry{ Title = "Underground Tundra Confection", SubTitle = "Confection Rebaked", TitleColor = Color.Beige,       StrokeColor = Color.Black }},
            {"SandConfectionUndergroundBiome", new BiomeEntry{ Title = "Underground Desert Confection", SubTitle = "Confection Rebaked", TitleColor = Color.Beige,       StrokeColor = Color.Black }},
            {"ConfectionUndergroundBiome",     new BiomeEntry{ Title = "Underground Confection",        SubTitle = "Confection Rebaked", TitleColor = Color.Beige,       StrokeColor = Color.Black }},
            {"IceConfectionSurfaceBiome",      new BiomeEntry{ Title = "Tundra Confection",             SubTitle = "Confection Rebaked", TitleColor = Color.Beige,       StrokeColor = Color.Black }},
            {"SandConfectionSurfaceBiome",     new BiomeEntry{ Title = "Desert Confection",             SubTitle = "Confection Rebaked", TitleColor = Color.LightYellow, StrokeColor = Color.Black }},
            {"ConfectionBiomeSurface",         new BiomeEntry{ Title = "Confection",                    SubTitle = "Confection Rebaked", TitleColor = Color.Beige,       StrokeColor = Color.Black }},
        };
    }
}
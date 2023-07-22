using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BTitles.BuiltinModSupport;

public class BuiltinRemnants : AutoImplementedModSupport
{
    protected override string GetTargetModName() => "Remnants";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = new Dictionary<string, BiomeEntry>
        {
            {"GoldenCity",   new BiomeEntry{ Title = "Golden City",   SubTitle = "Remnants", TitleColor = Color.Gold,        StrokeColor = Color.Black }},
            {"Growth",       new BiomeEntry{ Title = "Growth",        SubTitle = "Remnants", TitleColor = Color.ForestGreen, StrokeColor = Color.Black }},
            {"OceanCave",    new BiomeEntry{ Title = "Ocean Cave",    SubTitle = "Remnants", TitleColor = Color.HotPink,     StrokeColor = Color.Black }},
            {"Pyramid",      new BiomeEntry{ Title = "Pyramid",       SubTitle = "Remnants", TitleColor = Color.Goldenrod,   StrokeColor = Color.Black }},
            {"Hive",         new BiomeEntry{ Title = "Giant Hive",    SubTitle = "Remnants", TitleColor = Color.Yellow,      StrokeColor = Color.Black }},
            {"JungleTemple", new BiomeEntry{ Title = "Jungle Temple", SubTitle = "Remnants", TitleColor = Color.DarkGreen,   StrokeColor = Color.Black }},
            {"MarbleCave",   new BiomeEntry{ Title = "Marble Cave",   SubTitle = "Remnants", TitleColor = Color.LightGray,   StrokeColor = Color.Black }},
            {"Vault",        new BiomeEntry{ Title = "Vault",         SubTitle = "Remnants", TitleColor = Color.Gold,        StrokeColor = Color.Black }},
        };

        biomes = null;
    }
}
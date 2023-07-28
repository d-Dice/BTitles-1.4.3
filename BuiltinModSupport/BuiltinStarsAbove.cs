using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BTitles.BuiltinModSupport;

public class BuiltinStarsAbove : AutoImplementedModSupport
{
    protected override string GetTargetModName() => "StarsAbove";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = null;

        biomes = new Dictionary<string, BiomeEntry>
        {
            {"ObservatoryBiome",   new BiomeEntry{ Title = "Observatory Hyperborea", SubTitle = "Stars Above", TitleColor = Color.LightYellow,    StrokeColor = Color.Black }},
            {"AstarteDriverBiome", new BiomeEntry{ Title = "Astarte Driver",         SubTitle = "Stars Above", TitleColor = Color.Magenta,        StrokeColor = Color.Black }},
            {"BleachedWorldBiome", new BiomeEntry{ Title = "Bleached World",         SubTitle = "Stars Above", TitleColor = Color.BlanchedAlmond, StrokeColor = Color.Black }},
            {"CorvusBiome",        new BiomeEntry{ Title = "Corvus",                 SubTitle = "Stars Above", TitleColor = Color.DarkSlateGray,  StrokeColor = Color.Black }},
            {"LyraBiome",          new BiomeEntry{ Title = "Lyra",                   SubTitle = "Stars Above", TitleColor = Color.Orchid,         StrokeColor = Color.Black }},
            {"FriendlySpaceBiome", new BiomeEntry{ Title = "Pyxis",                  SubTitle = "Stars Above", TitleColor = Color.RosyBrown,      StrokeColor = Color.Black }},
            {"MoonBiome",          new BiomeEntry{ Title = "Moon",                   SubTitle = "Stars Above", TitleColor = Color.SlateGray,      StrokeColor = Color.Black }},
            {"SeaOfStarsBiome",    new BiomeEntry{ Title = "Sea of Stars",           SubTitle = "Stars Above", TitleColor = Color.Navy,           StrokeColor = Color.Black }},
        };
    }
}
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BTitles.BuiltinModSupport;

public class BuiltinRedemption : AutoImplementedModSupport
{
    protected override string GetTargetModName() => "Redemption";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = new Dictionary<string, BiomeEntry>
        {
            {"BlazingBastionBiome", new BiomeEntry{ Title = "Blazing Bastion", SubTitle = "Redemption", TitleColor = Color.Orange,        StrokeColor = Color.Black }},
            {"LidenBiome",          new BiomeEntry{ Title = "Liden",           SubTitle = "Redemption", TitleColor = Color.LightGreen,    StrokeColor = Color.Black }},
            {"LidenBiomeAlpha",     new BiomeEntry{ Title = "The Alpha",       SubTitle = "Redemption", TitleColor = Color.LightCoral,    StrokeColor = Color.Black }},
            {"LidenBiomeOmega",     new BiomeEntry{ Title = "The Omega",       SubTitle = "Redemption", TitleColor = Color.PaleVioletRed, StrokeColor = Color.Black }},
            {"SlayerShipBiome",     new BiomeEntry{ Title = "Slayer Ship",     SubTitle = "Redemption", TitleColor = Color.MediumPurple,  StrokeColor = Color.Black }},
        };

        biomes = new Dictionary<string, BiomeEntry>
        {
            {"LabBiome",                 new BiomeEntry{ Title = "Abandoned Laboratory", SubTitle = "Redemption", TitleColor = Color.Gray,      StrokeColor = Color.Black }},
            {"SoullessBiome",            new BiomeEntry{ Title = "Soulless Caverns",     SubTitle = "Redemption", TitleColor = Color.DarkGreen, StrokeColor = Color.Black }},
            {"WastelandCorruptionBiome", new BiomeEntry{ Title = "Wasteland Corruption", SubTitle = "Redemption", TitleColor = Color.Purple,    StrokeColor = Color.Black }},
            {"WastelandCrimsonBiome",    new BiomeEntry{ Title = "Wasteland Crimson",    SubTitle = "Redemption", TitleColor = Color.DarkRed,   StrokeColor = Color.Black }},
            {"WastelandDesertBiome",     new BiomeEntry{ Title = "Wasteland Desert",     SubTitle = "Redemption", TitleColor = Color.Khaki,     StrokeColor = Color.Black }},
            {"WastelandPurityBiome",     new BiomeEntry{ Title = "Wasteland Purity",     SubTitle = "Redemption", TitleColor = Color.LightBlue, StrokeColor = Color.Black }},
            {"WastelandSnowBiome",       new BiomeEntry{ Title = "Wasteland Snow",       SubTitle = "Redemption", TitleColor = Color.White,     StrokeColor = Color.Black }},
        };
    }
}
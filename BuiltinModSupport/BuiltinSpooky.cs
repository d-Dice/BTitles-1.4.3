using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace BTitles.BuiltinModSupport;

public class BuiltinSpooky : AutoImplementedModSupport
{
    protected override string GetTargetModName() => "Spooky";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = new Dictionary<string, BiomeEntry>
        {
            {"CatacombBiome", new BiomeEntry{ Title = "Catacomb", SubTitle = "Spooky", TitleColor = Color.DarkOliveGreen, StrokeColor = Color.Black }}
        };

        biomes = new Dictionary<string, BiomeEntry>
        {
            {"SpookyBiome",     new BiomeEntry{ Title = "Spooky Forest",      SubTitle = "Spooky", TitleColor = Color.Orange,     StrokeColor = Color.Black }},
            {"SpookyBiomeUg",   new BiomeEntry{ Title = "Underground Spooky", SubTitle = "Spooky", TitleColor = Color.OrangeRed,  StrokeColor = Color.Black }},
            {"SpookyHellBiome", new BiomeEntry{ Title = "Spooky Hell",        SubTitle = "Spooky", TitleColor = Color.DarkViolet, StrokeColor = Color.Black }},
        };
    }
}
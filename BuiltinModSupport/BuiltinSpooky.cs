using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace BTitles.BuiltinModSupport;

public class BuiltinSpooky : AutoImplementedModSupport
{
    protected override string GetTargetModName() => "Spooky";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = null;

        biomes = new Dictionary<string, BiomeEntry>
        {
            {"CatacombBiome",   new BiomeEntry{ Title = "Catacomb",           SubTitle = "Spooky", TitleColor = Color.DarkOliveGreen, StrokeColor = Color.Black }},
            {"CatacombBiome2",   new BiomeEntry{ Title = "Catacomb",           SubTitle = "Spooky", TitleColor = Color.DarkOliveGreen, StrokeColor = Color.Black }},
            {"CemeteryBiome",   new BiomeEntry{ Title = "Cemetery",           SubTitle = "Spooky", TitleColor = Color.Gray, StrokeColor = Color.Black }},
            {"HallucinationZone",   new BiomeEntry{ Title = "Hallucination Zone",           SubTitle = "Spooky", TitleColor = Color.BlueViolet, StrokeColor = Color.Black }},
            {"SpiderCaveBiome",   new BiomeEntry{ Title = "Spider Cave",           SubTitle = "Spooky", TitleColor = Color.MediumPurple, StrokeColor = Color.Black }},
            {"RaveyardBiome",   new BiomeEntry{ Title = "Raveyard",           SubTitle = "Spooky", TitleColor = Color.LimeGreen, StrokeColor = Color.Black }},
            {"SpookyBiome",     new BiomeEntry{ Title = "Spooky Forest",      SubTitle = "Spooky", TitleColor = Color.Orange,         StrokeColor = Color.Black }},
            {"PandoraBoxBiome",     new BiomeEntry{ Title = "Pandora Box",      SubTitle = "Spooky", TitleColor = Color.Orange,         StrokeColor = Color.Black }},
            {"SpookyBiomeUg",   new BiomeEntry{ Title = "Underground Spooky", SubTitle = "Spooky", TitleColor = Color.OrangeRed,      StrokeColor = Color.Black }},
            {"SpookyHellBiome", new BiomeEntry{ Title = "Spooky Hell",        SubTitle = "Spooky", TitleColor = Color.DarkViolet,     StrokeColor = Color.Black }},
        };
    }
}
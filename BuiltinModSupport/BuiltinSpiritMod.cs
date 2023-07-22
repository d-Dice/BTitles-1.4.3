using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BTitles.BuiltinModSupport;

public class BuiltinSpiritMod : AutoImplementedModSupport
{
    protected override string GetTargetModName() => "SpiritMod";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = new Dictionary<string, BiomeEntry>
        {
            {"AsteroidBiome",    new BiomeEntry{ Title = "Asteroid",     SubTitle = "Spirit Mod", TitleColor = Color.Black,     StrokeColor = Color.DarkGray }},
            {"JellyDelugeBiome", new BiomeEntry{ Title = "Jelly Deluge", SubTitle = "Spirit Mod", TitleColor = Color.LightBlue, StrokeColor = Color.Black    }},
            {"MysticMoonBiome",  new BiomeEntry{ Title = "Mystic Moon",  SubTitle = "Spirit Mod", TitleColor = Color.Cyan,      StrokeColor = Color.Black    }},
        };

        biomes = new Dictionary<string, BiomeEntry>
        {
            {"BriarSurfaceBiome",      new BiomeEntry{ Title = "Briar Surface",      SubTitle = "Spirit Mod", TitleColor = Color.DarkGreen,   StrokeColor = Color.Black }},
            {"BriarUndergroundBiome",  new BiomeEntry{ Title = "Briar Underground",  SubTitle = "Spirit Mod", TitleColor = Color.ForestGreen, StrokeColor = Color.Black }},
            {"SpiritSurfaceBiome",     new BiomeEntry{ Title = "Spirit Surface",     SubTitle = "Spirit Mod", TitleColor = Color.DeepSkyBlue, StrokeColor = Color.Black }},
            {"SpiritUndergroundBiome", new BiomeEntry{ Title = "Spirit Underground", SubTitle = "Spirit Mod", TitleColor = Color.SkyBlue,     StrokeColor = Color.Black }},
            {"SynthwaveSurfaceBiome",  new BiomeEntry{ Title = "Synthwave",          SubTitle = "Spirit Mod", TitleColor = Color.DarkViolet,  StrokeColor = Color.Black }},
            {"TideBiome",              new BiomeEntry{ Title = "Tide",               SubTitle = "Spirit Mod", TitleColor = Color.BlueViolet,  StrokeColor = Color.Black }},
        };
    }
}
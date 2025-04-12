using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BTitles.BuiltinModSupport;

public class BuiltinPolarities : AutoImplementedModSupport
{
    protected override string GetTargetModName() => "Polarities";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = null;

        biomes = new Dictionary<string, BiomeEntry>
        {
            {"LavaOcean",            new BiomeEntry{ Title = "Lava Ocean", SubTitle = "Polarities", TitleColor = Color.OrangeRed, StrokeColor = Color.Black }},
            {"LimestoneCave",            new BiomeEntry{ Title = "Limestone Cave", SubTitle = "Polarities", TitleColor = Color.DarkGray, StrokeColor = Color.Black }},
            {"SaltCave",            new BiomeEntry{ Title = "Salt Cave", SubTitle = "Polarities", TitleColor = Color.LightPink, StrokeColor = Color.Black }},
            {"FractalBiome",            new BiomeEntry{ Title = "Fractal", SubTitle = "Polarities", TitleColor = Color.LightSkyBlue, StrokeColor = Color.Black }},
            {"FractalOceanBiome",            new BiomeEntry{ Title = "Fractal Ocean", SubTitle = "Polarities", TitleColor = Color.SkyBlue, StrokeColor = Color.Black }},
            {"FractalSkyBiome",            new BiomeEntry{ Title = "Fractal Sky", SubTitle = "Polarities", TitleColor = Color.LightSkyBlue, StrokeColor = Color.Black }},
            {"FractalSubworldSky",            new BiomeEntry{ Title = "Fractal Sky", SubTitle = "Polarities", TitleColor = Color.LightSkyBlue, StrokeColor = Color.Black }},
            
        };
    }
}
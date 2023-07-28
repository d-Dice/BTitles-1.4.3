using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BTitles.BuiltinModSupport;

public class BuiltinBobBlender2 : AutoImplementedModSupport
{
    protected override string GetTargetModName() => "BobBlender2";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = new Dictionary<string, BiomeEntry>
        {
            {"TreeBiome",               new BiomeEntry{ Title = "Tree",               SubTitle = "Bob Blender", TitleColor = Color.ForestGreen,    StrokeColor = Color.Black }},
            {"VoidBiome",               new BiomeEntry{ Title = "Void",               SubTitle = "Bob Blender", TitleColor = Color.Purple,         StrokeColor = Color.Black }},
            {"BobBiome",                new BiomeEntry{ Title = "Bob Biome",          SubTitle = "Bob Blender", TitleColor = Color.Brown,          StrokeColor = Color.Black }},
            {"BobsBobBiome",            new BiomeEntry{ Title = "Bobs Bob Island",    SubTitle = "Bob Blender", TitleColor = Color.Yellow,         StrokeColor = Color.Black }},
            {"BobTowerBiome",           new BiomeEntry{ Title = "Bob Tower",          SubTitle = "Bob Blender", TitleColor = Color.SlateGray,      StrokeColor = Color.Black }},
            {"CorrodedBiome",           new BiomeEntry{ Title = "Corroded",           SubTitle = "Bob Blender", TitleColor = Color.DarkOliveGreen, StrokeColor = Color.Black }},
            {"CursedBiome",             new BiomeEntry{ Title = "Cursed",             SubTitle = "Bob Blender", TitleColor = Color.DarkOrange,     StrokeColor = Color.Black }},
            {"DarkmatterBiome",         new BiomeEntry{ Title = "Darkmatter",         SubTitle = "Bob Blender", TitleColor = Color.DarkSlateBlue,  StrokeColor = Color.Black }},
            {"MemeBiome",               new BiomeEntry{ Title = "Meme",               SubTitle = "Bob Blender", TitleColor = Color.MediumPurple,   StrokeColor = Color.Black }},
            {"StrangeBiome",            new BiomeEntry{ Title = "Strange Biome",      SubTitle = "Bob Blender", TitleColor = Color.Purple,         StrokeColor = Color.Black }},
            {"StrangeDungeonBiome",     new BiomeEntry{ Title = "Strange Dungeon",    SubTitle = "Bob Blender", TitleColor = Color.DarkViolet,     StrokeColor = Color.Black }},
            {"StrangeUndergroundBiome", new BiomeEntry{ Title = "Strange Undergrond", SubTitle = "Bob Blender", TitleColor = Color.DarkViolet,     StrokeColor = Color.Black }},
            {"SurfaceBiome",            new BiomeEntry{ Title = "Strange Surface",    SubTitle = "Bob Blender", TitleColor = Color.Purple,         StrokeColor = Color.Black }},
        };

        biomes = null;
    }
}
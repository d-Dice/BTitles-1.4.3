using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace BTitles;

/**
 * Class that contains info about biomes in mod
 */
public class Biomes
{
    // Information for biome title rendering
    public Dictionary<string, BiomeEntry> BiomeEntries;
    
    // Check for biomes that can be inside other biomes, sucha as meteor or mushroom cave
    public Func<Player, string> MiniBiomeChecker;
    
    // Check for biomes
    public Func<Player, string> BiomeChecker;
}
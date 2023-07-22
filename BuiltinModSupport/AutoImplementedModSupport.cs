using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;

namespace BTitles.BuiltinModSupport;

public abstract class AutoImplementedModSupport : ModSupport
{
    private class FoundBiome
    {
        public ModBiome Biome;
        public string Name;
    }
    
    protected abstract string GetTargetModName();
    
    protected abstract void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes);

    public override Mod GetTargetMod()
    {
        if (ModLoader.TryGetMod(GetTargetModName(), out Mod mod)) return mod;
        return null;
    }

    public override Biomes Implement()
    {
        if (ModLoader.TryGetMod(GetTargetModName(), out Mod mod))
        {
            GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes);
            
            List<FoundBiome> miniBiomesFound = miniBiomes?.Count > 0 ? miniBiomes.Select(pair => new FoundBiome{ Biome = mod.FindOrDefault<ModBiome>(pair.Key), Name = pair.Key}).Where(biome => biome.Biome != null).ToList() : null;
            List<FoundBiome> biomesFound = biomes?.Count > 0 ? biomes.Select(pair => new FoundBiome{ Biome = mod.FindOrDefault<ModBiome>(pair.Key), Name = pair.Key}).Where(biome => biome.Biome != null).ToList() : null;;

            return new Biomes
            {
                BiomeEntries = new Dictionary<string, BiomeEntry>().Union(miniBiomes ?? new Dictionary<string, BiomeEntry>()).Union(biomes ?? new Dictionary<string, BiomeEntry>()).ToDictionary(entry => entry.Key, entry => entry.Value),
                
                MiniBiomeChecker = miniBiomesFound?.Count > 0 ? player =>
                {
                    foreach (var miniBiome in miniBiomesFound)
                    {
                        if (player.InModBiome(miniBiome.Biome)) return miniBiome.Name;
                    }
                    
                    return "";
                } : null,
                
                BiomeChecker = biomesFound?.Count > 0 ? player =>
                {
                    foreach (var biome in biomesFound)
                    {
                        if (player.InModBiome(biome.Biome)) return biome.Name;
                    }
                    
                    return "";
                } : null,
            };
        }
        
        return null;
    }
}
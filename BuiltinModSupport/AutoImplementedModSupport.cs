using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;

namespace BTitles.BuiltinModSupport;

public abstract class AutoImplementedModSupport : ModSupport
{
    private class ConfirmedBiomeEntry
    {
        public ModBiome Biome;
        public string Name;
        public BiomeEntry BiomeEntry;
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
            
            List<ConfirmedBiomeEntry> miniBiomesFound = FetchBiomes(miniBiomes, mod).ToList();
            List<ConfirmedBiomeEntry> biomesFound = FetchBiomes(biomes, mod).ToList();

            return new Biomes
            {
                BiomeEntries = miniBiomesFound.Union(biomesFound).ToDictionary(item => item.Name, item => item.BiomeEntry),
                
                MiniBiomeChecker = miniBiomesFound.Count > 0 ? player =>
                {
                    foreach (var miniBiome in miniBiomesFound)
                    {
                        if (player.InModBiome(miniBiome.Biome)) return miniBiome.Name;
                    }
                    
                    return "";
                } : null,
                
                BiomeChecker = biomesFound.Count > 0 ? player =>
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

    private IEnumerable<ConfirmedBiomeEntry> FetchBiomes(Dictionary<string, BiomeEntry> data, Mod mod)
    {
        if (data != null && mod != null)
        {
            foreach (var pair in data)
            {
                var biome = mod.FindOrDefault<ModBiome>(pair.Key);
                if (biome == null)
                {
                    BiomeTitlesMod.Log("Fail", "Auto-Implement", $"Biome {pair.Key} was not found in mod {mod.Name}");
                    continue;
                }

                yield return new ConfirmedBiomeEntry
                {
                    Biome = biome,
                    Name = pair.Key,
                    BiomeEntry = pair.Value
                };
            }
        }
    }
}
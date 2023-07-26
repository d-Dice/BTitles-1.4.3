using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace BTitles.BuiltinModSupport;

public class BuiltinThoriumMod : ModSupport
{
    public static readonly string ModName = "ThoriumMod";
    
    public override Mod GetTargetMod()
    {
        if (ModLoader.TryGetMod(ModName, out Mod mod)) return mod;
        return null;
    }

    public override Biomes Implement()
    {
        if (ModLoader.TryGetMod(ModName, out Mod mod))
        {
            var test = mod.GetContent<ModBiome>();
            
            var depthsBiome = mod.FindOrDefault<ModBiome>("DepthsBiome");
            
            return new Biomes
            {
                BiomeEntries = new Dictionary<string, BiomeEntry>
                {
                    {"DepthsBiome",  new BiomeEntry{ Title = "Aquatic Depths", SubTitle = "Thorium Mod", TitleColor = Color.Cyan, StrokeColor = Color.Black }},
                    {"BloodChamber", new BiomeEntry{ Title = "Blood Chamber",  SubTitle = "Thorium Mod", TitleColor = Color.Red,  StrokeColor = Color.Black }},
                },
                
                MiniBiomeChecker = player =>
                {
                    if (mod.Call("GetBloodChamberBounds") is Rectangle rectangle)
                    {
                        var point = player.Center.ToTileCoordinates();
                        if (rectangle.Contains(point))
                        {
                            return "BloodChamber";
                        }
                    }

                    return "";
                },
                
                BiomeChecker = depthsBiome != null ? player =>
                {
                    if (player.InModBiome(depthsBiome) && player.position.Y > Main.worldSurface * 16) return "DepthsBiome";

                    return "";
                } : null
            };
        }
        
        return null;
    }
}
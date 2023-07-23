using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace BTitles.BuiltinModSupport;

public class BuiltinCalamityMod : ModSupport
{
    public static readonly string ModName = "CalamityMod";
    
    public override Mod GetTargetMod()
    {
        if (ModLoader.TryGetMod(ModName, out Mod mod)) return mod;
        return null;
    }

    public override Biomes Implement()
    {
        if (ModLoader.TryGetMod(ModName, out Mod mod))
        {
            return new Biomes
            {
                BiomeEntries = new Dictionary<string, BiomeEntry>
                {
                    {"crags",      new BiomeEntry{ Title = "Brimstone Crags",  SubTitle = "Calamity Mod", TitleColor = Color.OrangeRed,     StrokeColor = Color.Black }},
                    {"astral",     new BiomeEntry{ Title = "Astral Infection", SubTitle = "Calamity Mod", TitleColor = Color.DarkViolet,    StrokeColor = Color.Black }},
                    {"sunkensea",  new BiomeEntry{ Title = "Sunken Sea",       SubTitle = "Calamity Mod", TitleColor = Color.Teal,          StrokeColor = Color.Black }},
                    {"sulphursea", new BiomeEntry{ Title = "Sulphurous Sea",   SubTitle = "Calamity Mod", TitleColor = Color.LightSeaGreen, StrokeColor = Color.Black }},
                    {"abyss1",     new BiomeEntry{ Title = "Abyss - Layer 1",  SubTitle = "Calamity Mod", TitleColor = Color.DarkSlateBlue, StrokeColor = Color.Black }},
                    {"abyss2",     new BiomeEntry{ Title = "Abyss - Layer 2",  SubTitle = "Calamity Mod", TitleColor = Color.DarkSlateBlue, StrokeColor = Color.Black }},
                    {"abyss3",     new BiomeEntry{ Title = "Abyss - Layer 3",  SubTitle = "Calamity Mod", TitleColor = Color.DarkSlateBlue, StrokeColor = Color.Black }},
                    {"abyss4",     new BiomeEntry{ Title = "Abyss - Layer 4",  SubTitle = "Calamity Mod", TitleColor = Color.DarkSlateBlue, StrokeColor = Color.Black }},
                },
                
                MiniBiomeChecker = player =>
                {
                    if ((bool)mod.Call("GetInZone", player, "sulphursea")) return "sulphursea";
                    
                    if ((bool)mod.Call("GetInZone", player, "abyss"))
                    {
                        if ((bool)mod.Call("GetInZone", player, "layer1")) return "abyss1";
                        if ((bool)mod.Call("GetInZone", player, "layer2")) return "abyss2";
                        if ((bool)mod.Call("GetInZone", player, "layer3")) return "abyss3";
                        if ((bool)mod.Call("GetInZone", player, "layer4")) return "abyss4";
                    }

                    return "";
                },
                
                BiomeChecker = player =>
                {
                    if ((bool)mod.Call("GetInZone", player, "crags")) return "crags";
                    if ((bool)mod.Call("GetInZone", player, "astral")) return "astral";
                    if ((bool)mod.Call("GetInZone", player, "sunkensea")) return "sunkensea";

                    return "";
                }
            };
        }
        
        return null;
    }
}
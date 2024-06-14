using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using Terraria;

namespace BTitles.BuiltinModSupport;

public class BuiltinStarlightRiver : AutoImplementedModSupport
{
    private PropertyInfo _glassweaverArenaPosProperty;
    
    protected override string GetTargetModName() => "StarlightRiver";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = new Dictionary<string, BiomeEntry>
        {
            {"MoonstoneBiome", new BiomeEntry{ Title = "Moonstone", SubTitle = "Starlight River", TitleColor = Color.DarkViolet }},
            {"PermafrostTempleBiome", new BiomeEntry{ Title = "Permafrost Temple", SubTitle = "Starlight River", TitleColor = Color.Aqua }},
            {"VitricTempleBiome", new BiomeEntry{ Title = "Vitric Temple", SubTitle = "Starlight River", TitleColor = Color.DarkGoldenrod }},
        };
        
        biomes = new Dictionary<string, BiomeEntry>
        {
            {"VitricDesertBiome", new BiomeEntry{ Title = "Vitric Desert", SubTitle = "Starlight River", TitleColor = Color.Bisque }},
        };

        _glassweaverArenaPosProperty = GetTargetMod().GetType().Assembly.GetType("StarlightRiver.Core.StarlightWorld")?.GetProperty("GlassweaverArenaPos", BindingFlags.Static | BindingFlags.NonPublic);
    }

    public override Biomes Implement()
    {
        var baseImpl = base.Implement();

        baseImpl.BiomeEntries.Add("GlassweaverArena", new BiomeEntry{ Title = "Glassweaver Arena", SubTitle = "Starlight River", TitleColor = Color.BurlyWood});
        
        var baseMiniBiomeChecker = baseImpl.MiniBiomeChecker;
        
        baseImpl.MiniBiomeChecker = (player) =>
        {
            Vector2? gwArenaPos = (Vector2?)_glassweaverArenaPosProperty?.GetValue(null);
            if (gwArenaPos.HasValue && new Rectangle((int)gwArenaPos.Value.X - 35 * 16, (int)gwArenaPos.Value.Y - 30 * 16, 70 * 16, 60 * 16).Contains(player.Center.ToPoint()))
            {
                return "GlassweaverArena";
            }
            else
            {
                return baseMiniBiomeChecker?.Invoke(player) ?? "";
            }
        };
        
        return baseImpl;
    }
}
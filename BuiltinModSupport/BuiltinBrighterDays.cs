using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BTitles.BuiltinModSupport;

public class BuiltinBrighterDays : AutoImplementedModSupport
{
    protected override string GetTargetModName() => "BrighterDays";

    protected override void GetData(out Dictionary<string, BiomeEntry> miniBiomes, out Dictionary<string, BiomeEntry> biomes)
    {
        miniBiomes = null;

        biomes = new Dictionary<string, BiomeEntry>
        {
            {"DreamWorldBiome", new BiomeEntry{ 
                Title = "Nightmare Realm", 
                SubTitle = "Brighter Days", 
                TitleColor = new Color(255, 165, 0),
                StrokeColor = new Color(128, 0, 128)
            }},

            {"SpaceDreamBiome", new BiomeEntry{
                Title = "Sweetest of Dreams", 
                SubTitle = "Brighter Days", 
                TitleColor = new Color(173, 216, 230),
                StrokeColor = new Color(75, 0, 130)
            }}
        };
    }
} 
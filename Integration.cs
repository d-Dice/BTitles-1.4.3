using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace BTitles;

public static class Integration
{
    public static Biomes IntegrateMod(Mod mod)
    {
        Biomes biomes = new Biomes();

        var GetBiomes = mod.GetType().GetMethod("BTitlesHook_GetBiomes")?.ToMethod<Func<IEnumerable<dynamic>>, Mod>(mod);
        var GetBiome = mod.GetType().GetMethod("BTitlesHook_GetBiome")?.ToMethod<Func<int, dynamic>, Mod>(mod);
        var DynamicBiomeProvider = mod.GetType().GetMethod("BTitlesHook_DynamicBiomeProvider")?.ToMethod<Func<string, dynamic>, Mod>(mod);

        var DynamicBiomeChecker = mod.GetType().GetMethod("BTitlesHook_DynamicBiomeChecker")?.ToMethod<Func<Player, string>, Mod>(mod);
        var MiniBiomeChecker = mod.GetType().GetMethod("BTitlesHook_MiniBiomeChecker")?.ToMethod<Func<Player, string>, Mod>(mod);
        var BiomeChecker = mod.GetType().GetMethod("BTitlesHook_BiomeChecker")?.ToMethod<Func<Player, string>, Mod>(mod);

        IEnumerable<dynamic> biomeCollection = null;

        if (GetBiomes != null)
        {
            biomeCollection = GetBiomes();
        }
        else if (GetBiome != null)
        {
            var biomeList = new List<dynamic>();
            int i = 0;

            while (GetBiome(i++) is { } biome)
            {
                biomeList.Add(biome);
            }

            biomeCollection = biomeList;
        }

        if (biomeCollection != null)
        {
            biomes.BiomeEntries = new Dictionary<string, BiomeEntry>();

            foreach (var biome in biomeCollection)
            {
                if (IntegrateBiome(biome, mod) is KeyValuePair<string, BiomeEntry> biomeIntegration)
                {
                    biomes.BiomeEntries.Add(biomeIntegration.Key, biomeIntegration.Value);
                }
            }
        }

        biomes.DynamicBiomeProvider = DynamicBiomeProvider;

        biomes.DynamicBiomeChecker = DynamicBiomeChecker;
        biomes.MiniBiomeChecker = MiniBiomeChecker;
        biomes.BiomeChecker = BiomeChecker;

        return biomes;
    }

    public static KeyValuePair<string, BiomeEntry>? IntegrateBiome(dynamic biome, Mod mod = null, bool ignoreKey = false)
    {
        if (biome is null) return null;

        string key = null;
        string title = Extensions.GetProperty<string>(biome, "Title", null);

        if (!ignoreKey)
        {
            key = Extensions.GetProperty<string>(biome, "Key", null);
            if (key == null && title == null)
            {
                BiomeTitlesMod.Log(LogType.Fail, "Native Integration", "Returned biome object must have at least Key or Title defined!");
                return null;
            }

            if (key == null) key = title;
            else if (title == null) title = key;
        }

        string subTitle = Extensions.GetProperty<string>(biome, "SubTitle", mod?.DisplayName ?? "");
        Color titleColor = Extensions.GetProperty<Color>(biome, "TitleColor", Color.White);
        Color titleStroke = Extensions.GetProperty<Color>(biome, "TitleStroke", Color.Black);
        Texture2D icon = Extensions.GetProperty<Texture2D>(biome, "Icon", null);
        BiomeTitle titleBackground = GetBackground(biome, "titleBackground");
        BiomeTitle subTitleBackground = GetBackground(biome, "SubTitleBackground");

        return new KeyValuePair<string, BiomeEntry>(key, new BiomeEntry
        {
            Title = title,
            SubTitle = subTitle,
            TitleColor = titleColor,
            StrokeColor = titleStroke,
            Icon = icon,
            TitleWidget = titleBackground,
            SubTitleWidget = subTitleBackground,
            LocalizationScope = mod?.Name ?? null
        });
    }

    public static BiomeTitle GetBackground(dynamic target, string propertyName)
    {
        if (Extensions.GetProperty<object>(target, "TitleBackground", null) is { } property)
        {
            if (property is BiomeTitle biomeTitle)
            {
                return biomeTitle;
            }
            else
            {
                bool textureOnly = false;
                Texture2D texture = null;
                if (property is Texture2D textureValueOnly)
                {
                    texture = textureValueOnly;
                    textureOnly = true;
                }
                else if (property.GetProperty<Texture2D>("Texture") is Texture2D textureInObject)
                {
                    texture = textureInObject;
                }

                if (texture is not null)
                {
                    return new BasicBiomeTitle
                    {
                        Background = new SegmentedHorizontalPanel
                        {
                            Texture = texture,
                            LeftSegmentSize = textureOnly ? 44 / 514f : Extensions.GetProperty<float>(property, "LeftSegmentSize"),
                            RightSegmentSize = textureOnly ? 44 / 514f : Extensions.GetProperty<float>(property, "RightSegmentSize"),
                            MiddleSegmentSize = textureOnly ? 46 / 514f : Extensions.GetProperty<float>(property, "MiddleSegmentSize"),
                            Width = { Percent = 1 },
                            Height = { Percent = 1 },
                            LeftContentPadding = textureOnly ? 44 : Extensions.GetProperty<float>(property, "LeftContentPadding"),
                            RightContentPadding = textureOnly ? 44 : Extensions.GetProperty<float>(property, "RightContentPadding")
                        },
                        CustomReset = self => { self.ContentOffset = new Vector2(0, 1); }
                    };
                }
            }
        }

        return null;
    }
}
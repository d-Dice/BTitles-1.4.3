# Integration guide
The process of adding titles for biomes in your own mod is quite simple.

Your main mod class must implement some of these functions.
## Biome check
```csharp
public string BTitlesHook_DynamicBiomeChecker(Player player)
public string BTitlesHook_MiniBiomeChecker(Player player)
public string BTitlesHook_BiomeChecker(Player player)
```

These functions are being called to check if player in any biome. Dynamic biome check starts first. If no dynamic biome detected, mini-biome check starts and then goes to biome check if mini-biome check returned nothing. Keep in mind that these functions are executed quite often, so **THEY SHOULD BE AS OPTIMIZED AS POSSIBLE!**

* **Dynamic Biome** is a biome that may be added at runtime. That should be used for biomes that can't be determined at load stage. This function should return key string which will later be fed into `DynamicBiomeProvider` to get actual biome info.

* **Mini-biome** is a biome that can be located inside other modded biomes. Some example is a [meteor crash site](https://terraria.fandom.com/wiki/Meteorite_(biome)) mini-biome that can be inside [astral infection](https://calamitymod.wiki.gg/wiki/Astral_Infection).

* **Biome** is any other regular biome.

## Biome data
```csharp
public dynamic BTitlesHook_GetBiome(int index)
public IEnumerable<dynamic> BTitlesHook_GetBiomes()
public dynamic BTitlesHook_DynamicBiomeProvider(string key)
```

`GetBiome` and `GetBiomes` are called during mod load stage. `DynamicBiomeProvider` is called at runtime whenever `DynamicBiomeChecker` returns non-null value.

`GetBiome` and `GetBiomes` are interchangable. If `GetBiomes` present, then `GetBiome` is ignored.

* For **`GetBiome`**, index parameter starts from 0  and increments on each call. You should return [`BiomeData`](DataModelsReference.md#biomedata) or return null if provided index is not valid.

* With **`GetBiomes`** you simply have to return collection of [`BiomeData`](DataModelsReference.md#biomedata).

* **`DynamicBiomeProvider`** must return valid [`BiomeData`](DataModelsReference.md#biomedata) object or null if key is not valid.

## Localization
Biome titles can be localized if localization with following key exists: `Mods.BiomeTitles.Title.{ModName}.{BiomeKey}` where `ModName` is mod internal name and `BiomeKey` is a value of `Key` property on biome entry.

## Example
```csharp
public string BTitlesHook_BiomeChecker(Player player)
{
    if (player.IsInBiome1()) return "biome1";
    
    return "";
}

public string BTitlesHook_MiniBiomeChecker(Player player)
{
    if (player.IsInBiome2()) return "biome2";
    if (player.IsInBiome3()) return "biome3";
    
    return "";
}

public IEnumerable<dynamic> BTitlesHook_GetBiomes()
{
    yield return new
        {
            Key = "biome1",
            Title = "Biome 1",
            SubTitle = "My Mod",
            TitleColor = Color.White,
            TitleStroke = Color.Black,
        };
    yield return new
        {
            Key = "biome2",
            Title = "Biome 2",
            TitleColor = Color.Red
        };
    yield return new
        {
            Key = "biome3",
            Title = "Biome 3"
        }
}
```
# Integration guide
The process of adding titles for biomes in your own mod is quite simple

Your main mod class must implement some of these functions
## Biome check
```csharp
public void BTitlesHook_SetupBiomeCheckers(out Func<Player, string> miniBiomeChecker, out Func<Player, string> biomeChecker)
```

This function will be called on load stage. It should return two functions (one of them can be null). First function is used to check if the player inside mini-biome. Second is for checking if player is inside a regular biome. Keep in mind that these functions are executed quite often, so **THEY SHOULD BE AS OPTIMIZED AS POSSIBLE!**

Mini-biomes are biomes that can be located inside other modded biomes. Some example is a [meteor crash site](https://terraria.fandom.com/wiki/Meteorite_(biome)) mini-biome that can be inside [astral infection](https://calamitymod.wiki.gg/wiki/Astral_Infection) biome. Mini-biome check functions are executed before biome check. Biome check is executed only if none mini-biome was detected.

## Biome data
```csharp
public dynamic BTitlesHook_GetBiome(int index)
public IEnumerable<dynamic> BTitlesHook_GetBiomes()
```
Returned must be valid single [`BiomeData`](DataModelsReference.md#biomedata) object or collection of them.

Biome data is requested during mod load stage. Your mod have to implement one of these two functions.

In first case, index parameter starts from 0  and increments on each call. You should return [`BiomeData`](DataModelsReference.md#biomedata) or return null if provided index is not valid.

In second case you simply have to return collection of biomes.

## Localization
Biome titles can be localized if localization with following key exists: `Mods.BiomeTitles.Title.{ModName}.{BiomeKey}` where `ModName` is mod internal name and `BiomeKey` is a value of `Key` property on biome entry.

## Example
```csharp
public void BTitlesHook_SetupBiomeCheckers(out Func<Player, string> miniBiomeChecker, out Func<Player, string> biomeChecker)
{
    miniBiomeChecker = player => {
        if (player.IsInBiome1()) return "biome1";
        
        return "";
    };
    biomeChecker = player => {
        if (player.IsInBiome2()) return "biome2";
        if (player.IsInBiome3()) return "biome3";
        
        return "";
    };
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
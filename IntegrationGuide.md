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
```
Returned object may be instance of [`ExpandoObject`](https://learn.microsoft.com/en-us/dotnet/api/system.dynamic.expandoobject?view=net-6.0), [anonymous type](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/anonymous-types) or of any other regular C# class.
<br>This function is called during load stage. Index parameter starts from 0  and increments on each call. You should put biome data into returning object or return null if provided index is not valid.

Following properties may be specified:

| Data type    | Property name    | Description                                             | Default          |
|--------------|------------------|---------------------------------------------------------|------------------|
| `string`     | `Key`            | Unique biome identifier                                 | Title            |
| `string`     | `Title`          | Displayed biome name in english                         | Key              |
| `string`     | `SubTitle`       | Displayed subtitle in english, usually mod display name | Mod display name |
| `Color`      | `TitleColor`     | Title color                                             | `Color.White`    |
| `Color`      | `TitleStroke`    | Title outline color                                     | `Color.Black`    |
| `Texture2D`  | `Icon`           | Icon to be displayed near title                         | No icon          |
| `BiomeTitle` | `TitleWidget`    | Title widget                                            | Golden plate     |
| `BiomeTitle` | `SubTitleWidget` | Subtitle widget                                         | Silver plate     |

At least `Key` or `Title` must be specified.

For `BiomeTitle` see [biome title customization](BiomeTitleCustomization.md).

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

public dynamic BTitlesHook_GetBiome(int index)
{
    switch (index)
    {
        case 0:
            return new
            {
                Key = "biome1",
                Title = "Biome 1",
                SubTitle = "My Mod",
                TitleColor = Color.White,
                TitleStroke = Color.Black,
            };
        case 1:
            return new
            {
                Key = "biome2",
                Title = "Biome 2",
                TitleColor = Color.Red
            };
        case 3:
            return new
            {
                Key = "biome3",
                Title = "Biome 3"
            }
        default:
            return null;
    }
}
```
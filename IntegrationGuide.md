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
public bool BTitlesHook_GetBiome(int index, out string key, out string title, out string subTitle, out Color titleColor, out Color titleStroke, out Texture2D icon)
```

This function is called during load stage. Index parameter starts from 0  and increments on each call. You should put biome data into out parameters and return true as long as index is valid.

This function is optional, which means that you can write a plugin that only implement biome checks.

Alternative to this function is
```csharp
public bool BTitlesHook_GetBiome(int index, out string key, out string title, out string subTitle, out Color titleColor, out Color titleStroke, out Texture2D icon, out BiomeTitle titleWidget, out BiomeTitle subTitleWidget)
```
As you can see, it have two new out parameters. If you want to customize title for specific biome, you can construct and provide new widget here. Providing null means that default widget will be used. See [Biome Title customization](BiomeTitleCustomization.md)

## Example
```csharp
public void BTitlesHook_SetupBiomeCheckers(out Func<Player, string> miniBiomeChecker, out Func<Player, string> biomeChecker)
{
    miniBiomeChecker = player => {
        if (player.IsInsideBiome1()) return "biome1";
        
        return "";
    };
    biomeChecker = player => {
        if (player.IsInsideBiome2()) return "biome2";
        
        return "";
    };
}

public bool BTitlesHook_GetBiome(int index, out string key, out string title, out string subTitle, out Color titleColor, out Color titleStroke, out Texture2D icon)
{
    switch (index)
    {
        case 0:
            key = "biome1";
            title = "Biome 1";
            subTitle = "My Mod";
            titleColor = Color.Goldenrod;
            titleStroke = Color.Black;
            icon = LoadIconForBiome1();
            return true;
        case 1:
            key = "biome2";
            title = "Biome 2";
            subTitle = "My Mod";
            titleColor = Color.Cyan;
            titleStroke = Color.Black;
            icon = null;
            return true;
        default:
            key = "";
            title = "";
            subTitle = "";
            titleColor = Color.White;
            titleStroke = Color.Black;
            icon = null;
            return false;
    }
}
```
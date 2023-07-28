# Builtin support checklist
> **Warning**
> We accept builtin support only for popular mods or mods we consider worth adding, because after adding code to this repository we become responsible for it's maintaining. Please, try to implement [integration](IntegrationGuide.md) in your mod yourself, if possible.

Make sure that all of these steps are done when implementing builtin support for other mods.

**Common:** Biomes and checkers must be in a separate class for each mod within [BuiltinModSupport](BuiltinModSupport) directory.

[`AutoImplementedModSupport`](BuiltinModSupport/AutoImplementedModSupport.cs) is recommended if all biomes in mod are `ModBiome`.

We will use `AbovegroundAstralBiome` from [`CalamityMod`](BuiltinModSupport/BuiltinCalamityMod.cs) as an example.

## [`ModSupport`](BuiltinModSupport/ModSupport.cs) approach

1. `GetTargetMod` function is implemented and returns target mod instance if present or null
2. Biome is added to returning `Biomes.BiomeEntries` in `Implement` function, including:
   1. `Key` - some unique biome identifier, example: `AbovegroundAstralBiome`
   2. `Title` - title to be displayed, example: `"Astral Infection"`
   3. `SubTitle` - usually name of this mod in human-readable format, example: `"Calamity Mod"`
   4. `TitleColor`
   5. `StrokeColor` - title outline color
   6. `Icon` *(optional)* - `Texture2D` reference to be used as icon
   7. `LocalizationScope` - usually name of this mod, example: `"CalamityMod"`
2. Biome check is added to returning `Biomes.MiniBiomeChecker` or `Biomes.BiomeChecker` function accordingly
3. Localization is present in all supported localization files
   <br>**OR** team members are asked to add it
   <br>**OR** this is noted for later add and recorded somewhere
4. Mod is added to `sortAfter` list in [build.txt](build.txt)

Localization key format is `Mods.BiomeTitles.Title.{LocalizationScope}.{Key}`
<br>Example: `Mods.BiomeTitles.Title.CalamityMod.AbovegroundAstralBiome`

## [`AutoImplementedModSupport`](BuiltinModSupport/AutoImplementedModSupport.cs) approach
1. `GetTargetModName` function is implemented and returns actual internal mod name
2. Biome is added to `miniBiomes` or `biomes` out dictionary within `GetData` function, including:
   1. `Key` - exact `ModBiome` internal name, example: `AbovegroundAstralBiome`
   2. `Title` - title to be displayed, example: `"Astral Infection"`
   3. `SubTitle` - usually name of this mod in human-readable format, example: `"Calamity Mod"`
   4. `TitleColor`
   5. `StrokeColor` - title outline color
3. Localization is present in all supported localization files
   <br>**OR** team members are asked to add it
   <br>**OR** this is noted for later add and recorded somewhere
4. Mod is added to `sortAfter` list in [build.txt](build.txt)

Localization key format is `Mods.BiomeTitles.Title.{TargetModName}.{Key}`
<br>Example: `Mods.BiomeTitles.Title.CalamityMod.AbovegroundAstralBiome`

## Vanilla biome
1. Biome entry is added using `registerBiome` function within `BiomeTitlesMod.ImplementVanillaBiomes`
2. Check is added to `biomes.MiniBiomeChecker` or `biomes.BiomeChecker` accordingly within `BiomeTitlesMod.ImplementVanillaBiomes`
3. _optional_ Icon with name equal to biome title, but `" "` replaced with `"_"` is placed in folder [Resources/Textures/BiomeIcons](Resources/Textures/BiomeIcons)
4. Localization is present in all supported localization files
   <br>**OR** team members are asked to add it
   <br>**OR** this is noted for later add and recorded somewhere
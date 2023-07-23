# Biome Title Customizatioon
To customize biome title you must reference this project into your CSharp project first. Then you have 3 options of customizing the title.

## Option 1 - Change only background texture
Construct new object of [BasicBiomeTitle](BasicBiomeTitle.cs) class. In `Background` property provide instance of [SegmentedHorizontalPanel](SegmentedHorizontalPanel.cs) class. There you can set any texture to be used and other parameters.

## Option 2 - Change background
Construct new object of [BasicBiomeTitle](BasicBiomeTitle.cs) class. In `Background` property provide instance of any `UIElement`. Inheritance from [IBasicBiomeTitleBackground](BasicBiomeTitle.cs) interface is recommended.

## Option 3 - Change whole title layout
Construct new object of any class that is inherited from [BiomeTitle](BiomeTitle.cs) class. You on your own here.
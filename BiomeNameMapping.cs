using Terraria.ModLoader.Config;

public class BiomeNameMapping
{
    [Label("Current Name")]
    [Tooltip("The current (original) name of the biome.")]
    public string CurrentName { get; set; }

    [Label("New Name")]
    [Tooltip("The new custom name for the biome.")]
    public string NewName { get; set; }
}
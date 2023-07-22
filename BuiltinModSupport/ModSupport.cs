using Terraria.ModLoader;

namespace BTitles.BuiltinModSupport;

/**
 * Base class for built-in supports of other mods
 */
public abstract class ModSupport
{
    public abstract Mod GetTargetMod();
    public abstract Biomes Implement();
}
using System.Collections.Generic;
using System.ComponentModel;
using Terraria.ModLoader.Config;
using Newtonsoft.Json;

namespace BTitles
{
    public enum PositionOption
    {
        [Label("Top")]
        Top,

        [Label("Bottom")]
        Bottom,

        [Label("RPG style")]
        RPGStyle,

        [Label("Custom (Draggable)")]
        CustomDraggable
    }

    public enum TextScaleOption
    {
        [Label("Small")]
        Small,

        [Label("Normal")]
        Normal,

        [Label("Big")]
        Big
    }

    public class MyModConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("Titles config")]

        [Label("Position")]
        [Tooltip("Choose the position of the Title.")]
        [DefaultValue(PositionOption.Top)]
        public PositionOption Position;

        [Label("Text Scale")]
        [Tooltip("Set the scale of the Title.")]
        [DefaultValue(TextScaleOption.Normal)]
        public TextScaleOption TextScale;

        [Label("Display the Titles with their respective color")]
        [Tooltip("Enable or disable custom title colors for each biome.")]
        [DefaultValue(true)]
        public bool UseCustomTextColors;

        [Header("Subtitles config")]

        [Label("Display Subtitle")]
        [Tooltip("Enable or disable title subtitle.")]
        [DefaultValue(true)]
        public bool DisplaySubtitle;

        [Header("Advanced customization")]

        [Label("Custom Biome Names")]
        [Tooltip("Set custom names for the biomes.")]
        public List<BiomeNameMapping> CustomBiomeNames { get; set; } = new List<BiomeNameMapping>();

        [Label("Enable Custom (Draggable) Title")]
        [Tooltip("Enable or disable draggable title position.")]
        [DefaultValue(true)] // Set to true to enable dragging by default
        public bool EnableDraggableTitle { get; set; }

        [Label("Lock Title Position")]
        [Tooltip("Lock the title position to prevent accidental dragging.")]
        [DefaultValue(false)]
        public bool LockTitlePosition { get; set; }
        [Header("Titles delay")]
        [Label("Biome Titles Delay")]
        [Tooltip("Set the delay between titles.")]
        [Range(0f, 5f)] // Set the minimum and maximum values for the range
        [Increment(1f)]
        [DefaultValue(0f)]
        public float BiomeCheckDelay;

        // CustomTitlePosition is stored in a separate class and marked with [JsonIgnore]
        [JsonIgnore]
        public CustomTitlePositionData CustomTitlePosition { get; set; } = new CustomTitlePositionData();
    }

    public class CustomTitlePositionData
    {
        public float X { get; set; } = 200f;
        public float Y { get; set; } = 200f;
    }
}
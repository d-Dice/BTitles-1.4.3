using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using Terraria.ModLoader.Config;

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

        [Label("Custom")]
        Custom
    }

    public enum ScaleOption
    {
        [Label("Small")]
        Small,

        [Label("Normal")]
        Normal,

        [Label("Big")]
        Big
    }

    public enum TitleAnimationType
    {
        [Label("No animation")]
        None,
        
        [Label("Show/Fade")]
        ShowFade,
        
        [Label("Show/Fade with swipe")]
        ShowFadeSwipe
    }

    public class GeneralConfig : ModConfig
    {
        public delegate void PropertyChangedDelegate(string name);

        public event PropertyChangedDelegate OnPropertyChanged;
        
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("Visuals_Config")]
        
        [Label("Position")]
        [Tooltip("Choose the position of the Title")]
        [DefaultValue(PositionOption.Top)]
        [DrawTicks]
        public PositionOption Position;
        
        [Label("Custom Position X")]
        [Range(0, 1)]
        [DefaultValue(0.5)]
        public float CustomPositionX;
        
        [Label("Custom Position Y")]
        [Range(0, 1)]
        [DefaultValue(0.5)]
        public float CustomPositionY;

        [Label("Scale")]
        [Tooltip("Set the scale of the title")]
        [DefaultValue(ScaleOption.Normal)]
        [DrawTicks]
        public ScaleOption Scale;

        [Label("Colorize Titles")]
        [Tooltip("Enable or disable custom title colors for each biome")]
        [DefaultValue(true)]
        public bool UseCustomTextColors;
        
        [Label("Biome Icons")]
        [Tooltip("Enable or disable custom icon for each biome, if available")]
        [DefaultValue(true)]
        public bool ShowIcons;

        [Label("Display Subtitle")]
        [Tooltip("Enable or disable title subtitle")]
        [DefaultValue(true)]
        public bool DisplaySubtitle;

        [Label("Title Backgrounds")]
        [Tooltip("Enable visibility for title and subtitle backgrounds")]
        [DefaultValue(true)]
        public bool EnableBackgrounds;
        
        [Label("Hide While Inventory Open")]
        [Tooltip("Should titles be hidden when the player inventory is open")]
        [DefaultValue(true)]
        public bool HideWhileInventoryOpen;

        [Label("Hide While Boss Is Alive")]
        [Tooltip("Should titles be hidden while a boss is alive")]
        [DefaultValue(true)]
        public bool HideWhileBossIsAlive;


        [Header("Advanced_Customization")]
        
        [Label("Custom Biome Names")]
        [Tooltip("Set custom names for the biomes")]
        public List<BiomeNameMapping> CustomBiomeNames = new List<BiomeNameMapping>();

        // TODO (draco): it's hard to implement dragging properly due to stupid terraria UI system, I removed this property so we could solve that later
        [Label("Enable Draggable Title")]
        [Tooltip("Enable or disable draggable title if position is set to Custom")]
        [DefaultValue(false)]
        public bool EnableDraggableTitle;

        [Label("Biome Check Interval")]
        [Tooltip("Interval between biome checks in seconds")]
        [Range(0f, 5f)] // Set the minimum and maximum values for the range
        [Increment(0.1f)]
        [DefaultValue(0.1f)]
        public float BiomeCheckDelay;

        [Header("Animation")]
        
        [Label("Animation Type")]
        [Tooltip("Title appear and disappear animation")]
        [DefaultValue(TitleAnimationType.ShowFade)]
        [DrawTicks]
        public TitleAnimationType TitleAnimation;

        [Label("Visibility Duration")]
        [Tooltip("Time for titles to be displayed on screen in seconds, 0 means infinite")]
        [Range(0f, 10f)]
        [Increment(0.1f)]
        [DefaultValue(5f)]
        public float VisibilityDuration;

        [Label("Transition In Duration")]
        [Tooltip("Time for titles to appear on screen in seconds")]
        [Range(0f, 5f)]
        [Increment(0.1f)]
        [DefaultValue(0.2f)]
        public float TransitionInDuration;

        [Label("Transition Out Duration")]
        [Tooltip("Time for titles to disappear from screen in seconds")]
        [Range(0f, 5f)]
        [Increment(0.1f)]
        [DefaultValue(0.2f)]
        public float TransitionOutDuration;

        [JsonIgnore]
        private Dictionary<string, int> _cachedDataHashes = new Dictionary<string, int>();

        public override void OnChanged()
        {
            GetType()
                .GetFields()
                .Where(field => field.GetCustomAttribute(typeof(LabelAttribute)) != null)
                .Select(field => new { Name = field.Name, Value = field.GetValue(this) })
                .Where(field => !_cachedDataHashes.TryGetValue(field.Name, out int hash) || hash != field.Value.GetHashCode())
                .ToList()
                .ForEach(field =>
                {
                    _cachedDataHashes[field.Name] = field.Value.GetHashCode();
                    if (OnPropertyChanged != null) 
                        OnPropertyChanged(field.Name);
                });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using Terraria.ModLoader;

namespace BTitles;

public static class Extensions
{
    public static T FindOrDefault<T>(this Mod mod, string name, T defaultValue = default) where T : IModType
    {
        if (mod.TryFind(name, out T found)) return found;
        return defaultValue;
    }

    public static float ToFloat(this ScaleOption scale)
    {
        switch (scale)
        {
            case ScaleOption.Small:
                return 0.75f;
            case ScaleOption.Normal:
                return 1.0f;
            case ScaleOption.Big:
                return 1.2f;
        }

        return 1.0f;
    }

    public static T GetProperty<T>(object obj, string propertyName, T defaultValue = default)
    {
        if (obj is ExpandoObject)
        {
            if (((IDictionary<string, object>)obj).TryGetValue(propertyName, out object value))
                if (value is T castedDictValue)
                    return castedDictValue;

            return defaultValue;
        }

        if (obj.GetType().GetField(propertyName)?.GetValue(obj) is T castedFieldValue) return castedFieldValue;
        if (obj.GetType().GetProperty(propertyName)?.GetValue(obj) is T castedPropertyValue) return castedPropertyValue;

        return defaultValue;
    }

    public static MethodType ToMethod<MethodType, ThisType>(this MethodInfo methodInfo, ThisType This) where MethodType : Delegate
    {
        try
        {
            return (MethodType) Delegate.CreateDelegate(typeof(MethodType), This, methodInfo);
        }
        catch (Exception)
        {
            BiomeTitlesMod.Log(LogType.Fail, "Reflection", $"Failed to get method from MethodInfo {methodInfo}");
            return null;
        }
    }
    
    public static MethodType ToMethod<MethodType>(this MethodInfo methodInfo) where MethodType : Delegate
    {
        try
        {
            return (MethodType) Delegate.CreateDelegate(typeof(MethodType), methodInfo);
        }
        catch (Exception)
        {
            BiomeTitlesMod.Log(LogType.Fail, "Reflection", $"Failed to get method from MethodInfo {methodInfo}");
            return null;
        }
    }
}
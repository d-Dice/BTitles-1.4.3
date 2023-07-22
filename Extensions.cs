using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria.ModLoader;

namespace BTitles;

public static class Extensions
{
    public class ParameterSignatureComparer : IEqualityComparer<ParameterInfo>
    {
        public bool Equals(ParameterInfo p1, ParameterInfo p2)
        {
            if (ReferenceEquals(p1, p2))
                return true;

            if (p2 is null || p1 is null)
                return false;

            return p1.CompareSignature(p2);
        }

        public int GetHashCode(ParameterInfo p) => p.GetHashCode();
    }
    
    public static bool CompareSignature(this MethodInfo self, MethodInfo otherMethod)
    {
        return self.ReturnParameter.CompareSignature(otherMethod.ReturnParameter) &&
               self.GetParameters().SequenceEqual(otherMethod.GetParameters(), new ParameterSignatureComparer());
    }

    public static bool CompareSignature(this ParameterInfo self, ParameterInfo otherParameter)
    {
        return self.ParameterType == otherParameter.ParameterType &&
               self.IsIn == otherParameter.IsIn &&
               self.IsOut == otherParameter.IsOut &&
               self.IsRetval == otherParameter.IsRetval;
    }

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
}
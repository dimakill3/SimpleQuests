using System;
using System.Linq;

namespace _Assets.Scripts.Core.Infrastructure.Utils
{
    public static class EnumUtils
    {
        public static T GetRandomEnumValue<T>(params T[] exceptValues)
        {
            var allValues = Enum.GetValues(typeof(T)).Cast<T>();
            var filtered = allValues.Except(exceptValues).ToArray();
            
            if (filtered.Length == 0)
                throw new InvalidOperationException($"Нет доступных значений для enum {typeof(T)} после исключения.");
            
            var index = UnityEngine.Random.Range(0, filtered.Length);
            return filtered[index];
        }
    }
}
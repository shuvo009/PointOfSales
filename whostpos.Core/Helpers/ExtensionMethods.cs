using System;
using System.Linq;

namespace whostpos.Core.Helpers
{
    public static class ExtensionMethods
    {
        public static void ClearAllProperty<T>(this T property)
        {
            var propertyInfos = typeof(T).GetProperties();
            propertyInfos.Where(x => typeof(string).IsAssignableFrom(x.PropertyType) && x.GetSetMethod() != null && x.GetSetMethod()!=null).ToList().ForEach(pro => pro.SetValue(property, string.Empty, null));
            propertyInfos.Where(x => typeof(int).IsAssignableFrom(x.PropertyType)).ToList().ForEach(pro => pro.SetValue(property, 0, null));
            propertyInfos.Where(x => typeof(double).IsAssignableFrom(x.PropertyType)).ToList().ForEach(pro => pro.SetValue(property, 0, null));
            propertyInfos.Where(x => typeof(DateTime).IsAssignableFrom(x.PropertyType)).ToList().ForEach(pro => pro.SetValue(property, DateTime.Today, null));
            propertyInfos.Where(x => typeof(byte[]).IsAssignableFrom(x.PropertyType)).ToList().ForEach(pro => pro.SetValue(property, null, null));
        }

    }
}

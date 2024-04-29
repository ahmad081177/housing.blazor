using System;

using System.Reflection;
using System.Text.Json;


namespace SqlGPT
{
    internal class SqlGPTUtils
    {
        public static string GenerateClassString(Type type)
        {
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var classString = $"class {type.Name}:\n";

            foreach (var property in properties)
            {
                var typeName = property.PropertyType.Name;
                if (property.PropertyType.IsGenericType)
                {
                    typeName = $"{property.PropertyType.GetGenericArguments().First().Name}<{property.PropertyType.GetGenericArguments().Last().Name}>";
                }
                classString += $"   {typeName} {property.Name};\n";
            }

            return classString;
        }

        internal static T? ReadFromConfig<T>(string configFile)
        {
            try
            {
                using (var reader = File.OpenRead(configFile))
                {
                    var jj = JsonSerializer.Deserialize<T>(reader);
                    return jj;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return default;
            }
        }
    }
}

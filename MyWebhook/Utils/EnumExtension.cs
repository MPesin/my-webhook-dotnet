using System.ComponentModel;
using System.Reflection;

namespace MyWebhook.Utils;

public static class EnumExtension
{
    public static TEnum ParseEnumFromDescription<TEnum>(this string description) where TEnum : struct, IConvertible
    {
        ValidateIsEnum<TEnum>();
        FieldInfo? enumField = null;
        foreach (var field in typeof(TEnum).GetFields())
        {
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute
                && attribute.Description == description)
            {
                enumField = field;
            }
        }

        if (enumField == null)
        {
            throw new ArgumentException($"No such value or description '{description}'", nameof(description));
        }

        return (TEnum) enumField.GetValue(null)!;
    }

    private static void ValidateIsEnum<TEnum>() where TEnum : struct, IConvertible
    {
        var type = typeof(TEnum);
        if (!type.IsEnum)
        {
            throw new ArgumentException($"{nameof(TEnum)} must be of Enum type");
        }
    }
}
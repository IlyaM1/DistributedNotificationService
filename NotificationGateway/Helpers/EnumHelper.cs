namespace NotificationGateway.Helpers;

public static class EnumHelper
{
    public static TEnum ParseFromString<TEnum>(string value, bool ignoreCase = true)
            where TEnum : struct, Enum
    {
        if (Enum.TryParse<TEnum>(value, ignoreCase, out var result))
            return result;

        throw new ArgumentException($"Cannot convert '{value}' to enum {typeof(TEnum).Name}");
    }

}

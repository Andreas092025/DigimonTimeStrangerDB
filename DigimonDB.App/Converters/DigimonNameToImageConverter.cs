using System.Globalization;
using System.Text;
using Microsoft.Maui.Controls;

namespace DigimonDB.App.Converters;

public class DigimonNameToImageConverter : IValueConverter
{
    private static readonly object LockObj = new();
    private static Dictionary<string, string>? _normalizedPathLookup;

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        const string fallback = "dotnet_bot.png";

        if (value is not string name || string.IsNullOrWhiteSpace(name))
        {
            return fallback;
        }

        var normalized = Normalize(name);
        if (string.IsNullOrWhiteSpace(normalized))
        {
            return fallback;
        }

        var lookup = GetLookup();
        if (lookup.TryGetValue(normalized, out var filePath))
        {
            return ImageSource.FromFile(filePath);
        }

        return fallback;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }

    private static string Normalize(string input)
    {
        var sb = new StringBuilder(input.Length);
        var previousUnderscore = false;

        foreach (var c in input.Trim().ToLowerInvariant())
        {
            if (char.IsLetterOrDigit(c))
            {
                sb.Append(c);
                previousUnderscore = false;
                continue;
            }

            if (c is ' ' or '-' or '(' or ')' or '.' or '\'' or '/' or ':')
            {
                if (!previousUnderscore)
                {
                    sb.Append('_');
                    previousUnderscore = true;
                }
            }
        }

        return sb.ToString().Trim('_');
    }

    private static Dictionary<string, string> GetLookup()
    {
        if (_normalizedPathLookup is not null)
        {
            return _normalizedPathLookup;
        }

        lock (LockObj)
        {
            if (_normalizedPathLookup is not null)
            {
                return _normalizedPathLookup;
            }

            var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var baseDir = Path.Combine(AppContext.BaseDirectory, "DigimonImages");

            if (Directory.Exists(baseDir))
            {
                foreach (var file in Directory.EnumerateFiles(baseDir, "*.png", SearchOption.AllDirectories))
                {
                    var filename = Path.GetFileNameWithoutExtension(file);
                    var key = Normalize(filename);

                    if (string.IsNullOrWhiteSpace(key) || map.ContainsKey(key))
                    {
                        continue;
                    }

                    map[key] = file;
                }
            }

            _normalizedPathLookup = map;
            return map;
        }
    }
}

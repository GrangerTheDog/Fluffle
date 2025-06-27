namespace Fluffle.Search.Api.IdGeneration;

public static class ShortUuidDateTime
{
    private const string Alphabet = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz23456789";

    private static readonly Dictionary<char, int> AlphabetReverse = Alphabet
        .Select((character, index) => (character, index))
        .ToDictionary(x => x.character, x => x.index);

    private const int YearOffset = 2020;
    private const int MinuteMultiplier = 2;

    public static string ToString(DateTime value)
    {
        var year = Alphabet[value.Year - YearOffset];
        var month = Alphabet[value.Month];
        var day = Alphabet[value.Day];
        var hour = Alphabet[value.Hour];
        var minute = Alphabet[value.Minute / MinuteMultiplier];

        return $"{year}{month}{day}{hour}{minute}";
    }

    public static DateTime FromString(string value)
    {
        if (value.Length < 5)
            throw new ArgumentException($"A {nameof(ShortUuidDateTime)} consists out of at least 5 characters.", nameof(value));

        var year = AlphabetReverse[value[0]] + YearOffset;
        var month = AlphabetReverse[value[1]];
        var day = AlphabetReverse[value[2]];
        var hour = AlphabetReverse[value[3]];
        var minute = AlphabetReverse[value[4]] * MinuteMultiplier;

        return new DateTime(year, month, day, hour, minute, 0);
    }
}

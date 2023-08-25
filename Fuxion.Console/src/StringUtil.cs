
namespace Fuxion.Utilities;

public static class StringUtil 
{
    public static string[] ToList(string input) 
    {
        return input.Split(',');
    }

    public static string[] Shift(string[] strings, int offset = 0) 
    {
        List<string> listing = new List<string>();
        for(var i=offset; i<strings.Length; i++) 
        {
            listing.Add(strings[i]);
        }

        return listing.ToArray();
    }
}
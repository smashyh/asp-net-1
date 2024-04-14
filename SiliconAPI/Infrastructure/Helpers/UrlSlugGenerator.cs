using System.Text.RegularExpressions;

namespace Infrastructure.Helpers;

public static class UrlSlugGenerator
{
    public static string RemoveAccent(this string txt)
    {
        byte[] bytes = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(txt);
        return System.Text.Encoding.ASCII.GetString(bytes);
    }

    public static string GenerateSlug(this string phrase)
    {
        string str = phrase.RemoveAccent().ToLower();

        str = Regex.Replace(str, @"[^a-z0-9\s-]", ""); // invalid chars           
        str = Regex.Replace(str, @"\s+", " ").Trim(); // convert multiple spaces into one space   
        str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim(); // cut and trim it   
        str = Regex.Replace(str, @"\s", "-"); // hyphens   

        return str;
    }


}

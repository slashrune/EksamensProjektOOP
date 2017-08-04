using System.Linq;
using System.Text.RegularExpressions;

namespace Eksamensopgave2017
{
    public static class StringCleaner
    {
        public static string[] RemoveCharacterFromStringArray(string [] stringArray, char charToRemove)
        {
            return stringArray.Select(x => x.Trim(charToRemove)).ToArray();
        }

        public static string[] RemoveHtmlTagsFromStringArray(string[] stringArray)
        {
            return stringArray.Select(s => Regex.Replace(s, "<.*?>", string.Empty)).ToArray();
        }
    }
}

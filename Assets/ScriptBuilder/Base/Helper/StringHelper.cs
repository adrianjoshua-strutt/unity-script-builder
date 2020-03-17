using UnityEngine;
using UnityEditor;

public class StringHelper
{

    //https://weblogs.asp.net/jongalloway/426087
    public static string SplitCamelCase(string input)
    {
        return System.Text.RegularExpressions.Regex.Replace(input, "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
    }

}
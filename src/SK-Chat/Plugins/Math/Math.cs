using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace SK_Chat.Plugins.Math;

public class Math
{
    [SKFunction, Description("Count the number of characters generated.")]
    public static string CountCharacters([Description("The generated input.")]string input)
    {
        return string.Concat(input, "\r\n\r\nThe number of generated characters is: ", input.Length.ToString());
    }
}

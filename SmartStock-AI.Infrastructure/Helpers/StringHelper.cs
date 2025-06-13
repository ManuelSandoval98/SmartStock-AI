namespace SmartStock_AI.Infrastructure.Helpers;

public class StringHelper
{
    public static string RemoveSpecialCharacters(string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        return text
            .Replace("á", "a")
            .Replace("é", "e")
            .Replace("í", "i")
            .Replace("ó", "o")
            .Replace("ú", "u")
            .Replace("ñ", "n")
            .Replace("Á", "A")
            .Replace("É", "E")
            .Replace("Í", "I")
            .Replace("Ó", "O")
            .Replace("Ú", "U")
            .Replace("Ñ", "N");
    }
}
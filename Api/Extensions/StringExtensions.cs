namespace Api.Extensions
{
    public static class StringExtensions
    {
        public static string StripPostalCodeFormatting(this string input)
        {
            input = input.Replace(" ", "");
            return input;
        }
    }
}

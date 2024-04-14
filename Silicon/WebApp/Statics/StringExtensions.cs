namespace WebApp.Statics
{
    public static class StringExtensions
    {
        public static string ToStringWithSuffix(this int number)
        {
            decimal numberPortion = Math.Abs(number);
            if (numberPortion < 1000)
            {
                return numberPortion.ToString();
            }

            string suffix;
            if (numberPortion >= 1000000000)
            {
                numberPortion /= 1000000000;
                suffix = "B";
            }
            else if (numberPortion >= 1000000)
            {
                numberPortion /= 1000000;
                suffix = "M";
            }
            else
            {
                numberPortion /= 1000;
                suffix = "K";
            }

            if (numberPortion % 1.0m < 0.1m)
            {
                return numberPortion.ToString() + suffix;
            }

            string result = string.Format("{0:0.0}", numberPortion * Math.Sign(number)) + suffix;
            return result;
        }

        /// <summary>
        /// Adds a parameter to a URI for use with HttpClient.Get/PostAsync.
        /// Automatically decides if prefix symbol should be '?' or '&'.
        /// </summary>
        public static string AddParamToUri(this string uri, string param)
        {
            char paramSymbol = uri.Contains('?') ? '&' : '?';
            return $"{uri}{paramSymbol}{param}";
        }
    }
}

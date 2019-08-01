namespace NumberIntoWords
{
    public static class Converter
    {
        #region en_US
        public static ConvertResult en_US_Result(string num, string levelSeparator = " ") 
            => new Converters.en_US().NumToWords(num, levelSeparator);
        public static ConvertResult en_US_Result(long num, string levelSeparator = " ") 
            => new Converters.en_US().NumToWords(num, levelSeparator);
        public static ConvertResult en_US_Result(double num, string levelSeparator = " ")
            => new Converters.en_US().NumToWords(num, levelSeparator);
        public static ConvertResult en_US_Result(decimal num, string levelSeparator = " ")
            => new Converters.en_US().NumToWords(num, levelSeparator);
        #endregion

        #region ar_SA
        public static ConvertResult ar_SA_Result(string num, string levelSeparator = " و")
            => new Converters.ar_SA().NumToWords(num, levelSeparator);
        public static ConvertResult ar_SA_Result(long num, string levelSeparator = " و")
            => new Converters.ar_SA().NumToWords(num, levelSeparator);
        public static ConvertResult ar_SA_Result(double num, string levelSeparator = " و")
            => new Converters.ar_SA().NumToWords(num, levelSeparator);
        public static ConvertResult ar_SA_Result(decimal num, string levelSeparator = " و")
            => new Converters.ar_SA().NumToWords(num, levelSeparator);
        #endregion
    }
}

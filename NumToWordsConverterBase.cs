namespace NumberIntoWords
{
    internal abstract class NumToWordsConverterBase
    {
        protected NumAnalyzer analyzer;

        /// <summary>
        /// levels is to store the words of digits levels: billion, million, thousand, null, zero and negative.
        /// </summary>
        protected string[] levels;

        /// <summary>
        /// To insert values into levels of digits.<para/>Max index for levels is 3, 4 is null, 5 for zero, 6 for negative.
        /// </summary>
        /// <param name="billion"></param>
        /// <param name="million"></param>
        /// <param name="thousand"></param>
        /// <param name="zero"></param>
        /// <param name="negative"></param>
        protected NumToWordsConverterBase(string billion, string million, string thousand, string zero, string negative)
        {
            levels = new string[] { billion, million, thousand, "", zero, negative };
        }

        /// <summary>
        /// For Mathmatics converters.
        /// </summary>
        /// <param name="billion"></param>
        /// <param name="million"></param>
        /// <param name="thousand"></param>
        /// <param name="zero"></param>
        /// <param name="negative"></param>
        protected NumToWordsConverterBase(string billion, string million, string thousand, string hundred, string ten, string single, string zero, string negative)
        {
            levels = new string[] { billion, million, thousand, hundred, ten, single, zero, negative };
        }

        #region Num To Words Result

        /// <summary>
        /// Reading Convert Result from value.
        /// </summary>
        /// <param name="val">Value to convert.</param>
        /// <param name="levelsSeparator">Chars that separate between levels in result.</param>
        /// <returns></returns>
        public ConvertResult NumToWords(long val,  string levelsSeparator)
        {
            analyzer = new NumAnalyzer(val);
            return new ConvertResult(GetInteger(levelsSeparator), "", analyzer.IsNegative ? levels[5] : "");
        }
        /// <summary>
        /// Reading Convert Result from value.
        /// </summary>
        /// <param name="val">Value to convert.</param>
        /// <param name="levelsSeparator">Chars that separate between levels in result.</param>
        /// <returns></returns>
        public ConvertResult NumToWords(string val, string levelsSeparator)
        {
            analyzer = new NumAnalyzer(val);
            return new ConvertResult(GetInteger(levelsSeparator), GetFraction(levelsSeparator), analyzer.IsNegative ? levels[5] : "");
        }
        /// <summary>
        /// Reading Convert Result from value.
        /// </summary>
        /// <param name="val">Value to convert.</param>
        /// <param name="levelsSeparator">Chars that separate between levels in result.</param>
        /// <returns></returns>
        public ConvertResult NumToWords(double val, string levelsSeparator) =>
                NumToWords(val.ToString(), levelsSeparator);
        /// <summary>
        /// Reading Convert Result from value.
        /// </summary>
        /// <param name="val">Value to convert.</param>
        /// <param name="levelsSeparator">Chars that separate between levels in result.</param>
        /// <returns></returns>
        public ConvertResult NumToWords(decimal val, string levelsSeparator) =>
                NumToWords(val.ToString(), levelsSeparator);

        #endregion

        internal string GetFraction(string levelsSeparator)
        {
            if (analyzer.HasFractions)
            {
                string temp = NumToWords(analyzer.FractionsThrees, levelsSeparator);
                return temp.Substring(0, temp.Length - levelsSeparator.Length);
            }
            else return "";
        }
        internal string GetInteger(string levelsSeparator)
        {
            string words = "";
            if (analyzer.Integer == 0)
            {
                return levels[4];
            }
            words += NumToWords(analyzer.IntThrees, levelsSeparator);
            return words.Substring(0, words.Length - levelsSeparator.Length);
        }

        #region Abstract Methods for Converters

        /// <summary>
        /// The main method to combine reading of digits values with their levels. This will need reading each hundred block alone. 
        /// </summary>
        /// <param name="blocks"></param>
        /// <param name="levelsSeparator"></param>
        /// <returns></returns>
        protected abstract string NumToWords(short[] blocks, string levelsSeparator);
        /// <summary>
        /// To convert a hundred block of level into string. This should combine reading GetHundred and GetRest
        /// </summary>
        /// <param name="val">a hundred block value</param>
        /// <param name="ind">For the current block index</param>
        /// <param name="managed">You can use 'managed' to prevent NumToWords from adding the level name if it is managed from inside.</param>
        /// <returns></returns>
        protected abstract string HundrendBlock(short val, int ind, out bool managed);
        /// <summary>
        /// To convert the hundred level alone #--.
        /// </summary>
        /// <param name="val">a hundred block value</param>
        /// <param name="ind">For the current block index</param>
        /// <param name="managed">You can use 'managed' to prevent NumToWords from adding the level name if it is managed from inside.</param>
        /// <returns></returns>
        protected abstract string GetHundred(short val, int ind, out bool managed);
        /// <summary>
        /// To convert the rest of hundred block into words-##.
        /// </summary>
        /// <param name="val">a hundred block value</param>
        /// <param name="ind">For the current block index</param>
        /// <param name="managed">You can use 'managed' to prevent NumToWords from adding the level name if it is managed from inside.</param>
        /// <returns></returns>
        protected abstract string GetRest(short val, int ind, out bool managed);

        #endregion

    }
}

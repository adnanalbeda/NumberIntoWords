namespace NumberIntoWords
{
    /// <summary>
    /// Convert Result is to store the results of conversion from number into words.
    /// </summary>
    public class ConvertResult
    {

        public string Integer { get; }
        public string Fractions { get; }
        public string Negative { get; }
        public bool IsNegative { get; }

        /// <summary>
        /// To include conversion values which need to be stored.
        /// </summary>
        /// <param name="integer"></param>
        /// <param name="fractions"></param>
        /// <param name="negative"></param>
        public ConvertResult(string integer, string fractions, string negative)
        {
            Integer = integer;
            Fractions = fractions;
            Negative = negative;
            IsNegative = !string.IsNullOrEmpty(Negative);
        }
    }
}

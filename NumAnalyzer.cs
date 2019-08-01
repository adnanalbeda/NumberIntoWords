using System;

namespace NumberIntoWords
{

    public class NumAnalyzer
    {
        // example: 10020.123456789123
        #region Variables

        public long Integer { get; } // 10020
        public long? Fractions { get; } // 123456789123

        public bool IsNegative { get; } // false
        public bool HasFractions { get; } // true

        public short[] IntThrees { get; } //[0, 0, 10, 20]
        public short[] FractionsThrees { get; } // [123, 456, 789, 123]

        #endregion

        #region Constructors

        public NumAnalyzer(string numAsString)
        {
            string[] parts = numAsString.Split('.');

            IsNegative = '-' == numAsString[0];

            Integer = NumOnly(parts[0]);
            if (Integer > 999999999999 || Integer < -999999999999)
            {
                throw new OverflowException("Number must be in range of ( (999 999 999 999) : (-999 999 999 999) ).");
            }
            IntThrees = DivideToBlocksOfThrees(Integer);

            try
            {
                Fractions = NumOnly(parts[1]);
                if (Fractions > 999999999999 || Fractions < -999999999999)
                {
                    throw new OverflowException("Fractions must be in range of ( (9 999 999 999) : (-9 999 999 999) ).");
                }
                FractionsThrees = DivideToBlocksOfThrees(Fractions.Value);
                HasFractions = true;
            }
            catch
            {
                Fractions = null;
                FractionsThrees = new short[0];
                HasFractions = false;
            }
        }

        public NumAnalyzer(long val)
        {
            if (val > 999999999999 || val < -999999999999)
            {
                throw new OverflowException("Number must be in range of ( (999 999 999 999) : (-999 999 999 999) ).");
            }
            IsNegative = val < 0;
            Integer = IsNegative ? val * -1 : val;
            IntThrees = DivideToBlocksOfThrees(Integer);
            Fractions = null;
            FractionsThrees = new short[0];
            HasFractions = false;
        }

        public NumAnalyzer(double val) : this(val.ToString())
        {
        }

        public NumAnalyzer(decimal val) : this(val.ToString())
        {
        }
        #endregion


        private long NumOnly(string num)
        {
            string temp = num;
            for (int i = 0; i < temp.Length; i++)
            {
                try
                {
                    while (!char.IsNumber(temp[i]))
                        temp = temp.Replace(temp[i].ToString(), "");
                }
                catch (Exception) { }
            }
            if (temp.Length == 0)
                return 0;
            return Convert.ToInt64(temp);
        }

        private short[] DivideToBlocksOfThrees(long v)
        {
            string NumbersOnly = v.ToString();
            byte threesLength = (byte)Math.Ceiling(NumbersOnly.Length / 3.0);
            byte blockRemaining = (byte)(NumbersOnly.Length % 3);
            short[] temp = new short[threesLength];
            if (blockRemaining == 0)
                blockRemaining = 3;
            temp[0] = Convert.ToInt16(NumbersOnly.Substring(0, blockRemaining));
            for (int i = 1; i < threesLength; i++)
            {
                temp[i] = Convert.ToInt16
                    (NumbersOnly.Substring
                        (blockRemaining + ((i - 1) * 3), 3));
            }
            return threesLength == 4 ? temp : FixLevelsOfBlocks(temp);
        }

        private short[] FixLevelsOfBlocks(short[] val)
        {
            if (val.Length < 4 && val.Length > 0)
            {
                short[] temp = new short[4];
                temp[0] = 0;
                if (val.Length == 3)
                {
                    temp[1] = val[0];
                    temp[2] = val[1];
                    temp[3] = val[2];
                }
                else
                {
                    temp[1] = 0;
                    if (val.Length == 2)
                    {
                        temp[2] = val[0];
                        temp[3] = val[1];
                    }
                    else
                    {
                        temp[2] = 0;
                        temp[3] = val[0];
                    }
                }
                return temp;
            }
            else
            {
                throw new OverflowException();
            }
        }
    }
}

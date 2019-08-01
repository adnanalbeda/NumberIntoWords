namespace NumberIntoWords.Converters
{
    internal class en_US : NumberIntoWords.NumToWordsConverterBase
    {

        enum SinglesTensSpecial
        {
            One = 1,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Eleven,
            Twelve,
            Thriteen,
            Fourteen,
            Fifteen,
            Sixteen,
            Seventeen,
            Eighteen,
            Nineteen,
            Twenty,
            Thirty = 30,
            Fourty = 40,
            Fifty = 50,
            Sixty = 60,
            Seventy = 70,
            Eighty = 80,
            Ninety = 90
        }

        public en_US() : base("Billions", "Millions", "Thousands", "Zero", "Negative")
        { }


        protected override string NumToWords(short[] blocks, string levelsSeparator = " ")
        {
            string words = "";
            for (int i = 0; i < 4; i++)
            {
                short val = blocks[i];
                if (val > 0)
                {

                    if (val == 1)
                    {
                        if (i == 0)
                            words += "One Billion" + levelsSeparator;
                        else if (i == 1)
                            words += "One Million" + levelsSeparator;
                        else if (i == 2)
                            words += "One Thousand" + levelsSeparator;
                        else if (i == 3)
                            words += "One" + levelsSeparator;
                    }
                    else
                        words += HundrendBlock(blocks[i], i, out bool m) +
                                (m ? levelsSeparator : " " + levels[i] + levelsSeparator);
                }
            }
            return words;
        }
        protected override string HundrendBlock(short val, int ind, out bool managed)
        {
            string temp = GetHundred(val, ind, out bool m1) + GetRest(val, ind, out bool m2);
            managed = m1 || m2;
            return temp;
        }
        protected override string GetHundred(short val, int ind, out bool managed)
        {
            byte temp = (byte)(val / 100);
            managed = false;
            if (temp > 0)
            {
                return ((SinglesTensSpecial)temp).ToString() + " Hundered ";
            }
            else return "";
        }
        protected override string GetRest(short val, int ind, out bool managed)
        {
            byte temp = (byte)(val - (val / 100 * 100));
            byte tens = (byte)(temp / 10 * 10);
            byte single = (byte)(temp - tens);
            managed = false;
            if (temp == 0)
            {
                return "";
            }
            if (temp > 0 && temp <= 20)
            {
                return ((SinglesTensSpecial)temp).ToString();
            }
            else
            {
                if (single == 0)
                {
                    return ((SinglesTensSpecial)tens).ToString();
                }
                else
                {
                    return ((SinglesTensSpecial)tens).ToString() +
                    "-" + ((SinglesTensSpecial)single).ToString();
                }
            }
        }
    }
}


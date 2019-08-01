using System.Collections.Generic;

namespace NumberIntoWords.Converters
{
    internal class ar_SA : NumToWordsConverterBase
    {
        static Dictionary<long, string> basicValues;

        public ar_SA() : base("مليار", "مليونا", "ألفا", "صفر", "سالب")
        {
            if (basicValues is null)
            {
                basicValues = new Dictionary<long, string>();
                basicValues.Add(1, "واحد");
                basicValues.Add(2, "إثنان");
                basicValues.Add(3, "ثلاثة");
                basicValues.Add(4, "أربعة");
                basicValues.Add(5, "خمسة");
                basicValues.Add(6, "ستة");
                basicValues.Add(7, "سبعة");
                basicValues.Add(8, "ثمانية");
                basicValues.Add(9, "تسعة");
                basicValues.Add(10, "عشرة");
                basicValues.Add(11, "إحدى عشر");
                basicValues.Add(12, "إثنا عشر");
                basicValues.Add(13, "ثلاثة عشر");
                basicValues.Add(14, "أربعة عشر");
                basicValues.Add(15, "خمسة عشر");
                basicValues.Add(16, "ستة عشر");
                basicValues.Add(17, "سبعة عشر");
                basicValues.Add(18, "ثمانية عشر");
                basicValues.Add(19, "تسعة عشر");
                basicValues.Add(20, "عشرون");
                basicValues.Add(30, "ثلاثون");
                basicValues.Add(40, "أربعون");
                basicValues.Add(50, "خمسون");
                basicValues.Add(60, "ستون");
                basicValues.Add(70, "سبعون");
                basicValues.Add(80, "ثمانون");
                basicValues.Add(90, "تسعون");
                basicValues.Add(1000, "ألف");
                basicValues.Add(2000, "ألفان");
                basicValues.Add(3000, "ثلاثة آلاف");
                basicValues.Add(4000, "أربعة آلاف");
                basicValues.Add(5000, "خمسة آلاف");
                basicValues.Add(6000, "ستة آلاف");
                basicValues.Add(7000, "سبعة آلاف");
                basicValues.Add(8000, "ثمانية آلاف");
                basicValues.Add(9000, "تسعة آلاف");
                basicValues.Add(10000, "عشرة آلاف");
                basicValues.Add(1000000, "مليون");
                basicValues.Add(2000000, "مليونان");
                basicValues.Add(3000000, "ثلاثة ملايين");
                basicValues.Add(4000000, "أربعة ملايين");
                basicValues.Add(5000000, "خمسة ملايين");
                basicValues.Add(6000000, "ستة ملايين");
                basicValues.Add(7000000, "سبعة ملايين");
                basicValues.Add(8000000, "ثمانية ملايين");
                basicValues.Add(9000000, "تسعة ملايين");
                basicValues.Add(10000000, "عشرة ملايين");
                basicValues.Add(1000000000, "مليار");
                basicValues.Add(2000000000, "ملياران");
                basicValues.Add(3000000000, "ثلاثة مليارات");
                basicValues.Add(4000000000, "أربعة مليارات");
                basicValues.Add(5000000000, "خمسة مليارات");
                basicValues.Add(6000000000, "ستة مليارات");
                basicValues.Add(7000000000, "سبعة مليارات");
                basicValues.Add(8000000000, "ثمانية مليارات");
                basicValues.Add(9000000000, "تسعة مليارات");
                basicValues.Add(10000000000, "عشرة مليارات");
            }
        }

        #region Base Members

        protected override string NumToWords(short[] blocks, string levelsSeparator = " و")
        {
            string words = "";
            for (int i = 0; i < 4; i++)
            {
                short val = blocks[i];
                if (val > 0)
                {
                    words += HundrendBlock(val, i, out bool m) +
                            (m ? levelsSeparator : " " + levels[i] + levelsSeparator);
                }
            }
            return words;
        }

        protected override string HundrendBlock(short val, int ind, out bool managed)
        {
            string h = GetHundred(val, ind, out bool m1);
            string r = GetRest(val, ind, out bool m2);
            managed = m1 || m2;
            if (!string.IsNullOrEmpty(h) && !string.IsNullOrEmpty(r))
                return h + " و" + r;
            return h + r;
        }

        protected override string GetHundred(short val, int ind, out bool managed)
        {
            byte temp = (byte)(val / 100);
            managed = false;
            if (temp > 0)
            {
                if (temp == 1)
                {
                    return "مئة";
                }
                else if (temp == 2)
                {
                    return "مئتان";
                }
                else
                    return basicValues[temp].ToString().Replace("ة", "") + " مئة ";
            }
            else return "";
        }

        protected override string GetRest(short val, int ind, out bool managed)
        {
            byte temp = (byte)(val - (val / 100 * 100));
            if (temp == 0)
            {
                managed = false;
                return "";
            }
            byte tens = (byte)(temp / 10 * 10);
            byte single = (byte)(temp - tens);
            if (ind == 0)
            {
                string th = ManageLevel(val, temp, 1000000000, out managed);
                if (managed)
                    return th;
            }
            else if (ind == 1)
            {
                string th = ManageLevel(val, temp, 1000000, out managed);
                if (managed)
                    return th;
            }
            else if (ind == 2)
            {
                string th = ManageLevel(val, temp, 1000, out managed);
                if (managed)
                    return th;
            }
            managed = false;
            if (temp > 0 && temp <= 20)
            {
                return basicValues[temp].ToString();
            }
            else if (single == 0)
            {
                return basicValues[tens].ToString();
            }
            else
            {
                return basicValues[single].ToString() +
                " و" + basicValues[tens].ToString();
            }
        }

        #endregion

        private string ManageLevel(short val, byte temp, long zeros, out bool managed)
        {
            if (temp > 2 && temp < 11)
            {
                managed = true;
                return basicValues[temp * zeros];
            }
            if (val < 100)
            {
                try
                {
                    managed = true;
                    return basicValues[val * zeros];
                }
                catch
                { }
            }
            managed = false;
            return "";
        }
    }
}


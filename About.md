# Number Into Words Converter

---

## About

Number Into Words Converter is a **standard** library that turns digits into words.
It provides a three simple rules allowing developers to add their language.

## NumToWordsConverterBase

NumToWordsConverterBase is an abstract class to be used for any language.
It has four main abstract functions for conversion:

``` CS
        /// <summary>
        /// The main method to combine reading of digits values with their levels. This will need reading each hundred block alone. 
        /// </summary>
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
```

By these four methods, breakdown the conversion become easier.

Another thing, there is no need to worry about how the number will be inserted.
Because of NumAnalyzer class, all to care of is finding the best way for recognizing numbers and their level.

By default, this base class has three methods you don't need to care about:

``` CS
    string GetInteger();
    string GetFractions();
    ConvertResult NumToWords(val, levelSeparatir);
```

The first two relies on `string NumToWords()`, While the third returns both of their results into `new ConvertResult(int, frac, negative)`;

## NumAnalyzer

NumAnalyzer class is to break every number within the range of 999999999999 into two four blocks of levels, one for the integeral value and the other is for fraction.
So a number like this: 10020.123456789123
will be stored as: [0 0 10 20][123 456 789 123].
It also ignores all kind of formatting char, in other words, it accepts all type of string but only numbers will survive.

```CS
        // example: 10020.123456789123
        #region Variables

        public long Integer { get; } // 10020
        public long? Fractions { get; } // 123456789123

        public bool IsNegative { get; } // false
        public bool HasFractions { get; } // true

        public short[] IntThrees { get; } //[0, 0, 10, 20]
        public short[] FractionsThrees { get; } // [123, 456, 789, 123]

        #endregion
```

## ConvertResult

It is a very simple class with no operation at all. 
It just stores results, so you combine them or suffix them with whatever you want like currencies, celesius, etc...

```CS
        public string Integer { get; }
        public string Fractions { get; }
        public string Negative { get; }
        public bool IsNegative { get; }
```

## Documnetation

This library provides simple Documnetation to help in building convertors.

### Who can participate in this library?

If you feel you like this library and you want to add a converter for a non-provided language,
then you can fork the program, inherit NumToWordConverterBase to your converter and apply its abstract with extra methods if needed,
then finally to place your code file into converters folder.

## Licesne

MIT License

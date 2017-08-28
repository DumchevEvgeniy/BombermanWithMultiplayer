using System;

public static class NumberExtensions {
    public static Boolean IsEven(this Int32 number) {
        return number % 2 == 0;
    }
    public static Boolean IsUneven(this Int32 number) {
        return !number.IsEven();
    }
    public static Int32 Normalize(this Int32 number) {
        return number == 0 ? 0 : Math.Sign(number);
    }

    public static Boolean IsEven(this Single number) {
        return number.ToRoundInt32().IsEven();
    }
    public static Boolean IsUneven(this Single number) {
        return !number.IsEven();
    }

    public static Int32 ToRoundInt32(this Single number) {
        return (Int32)Math.Round(number);
    }
}
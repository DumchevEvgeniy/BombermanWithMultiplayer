using System;
using UnityEngine;

public static class VectorExtensions {
    public static Vector3 Set(this Vector3 vector, Single xValue, Single zValue) {
        return new Vector3(xValue, vector.y, zValue);
    }
    public static Vector3 Set(this Vector3 vector, Coordinate coordinate, Single value) {
        var x = vector.x;
        var y = vector.y;
        var z = vector.z;
        switch(coordinate) {
            case Coordinate.X:
                vector.Set(value, y, z);
                break;
            case Coordinate.Y:
                vector.Set(x, value, z);
                break;
            case Coordinate.Z:
                vector.Set(x, y, value);
                break;
        }
        return vector;
    }

    public static Vector3 ToRoundInt32(this Vector3 vector) {
        return new Vector3(vector.x.ToRoundInt32(), vector.y.ToRoundInt32(), vector.z.ToRoundInt32());
    }

    public static Int32 Sign(this Vector3 vector) {
        Int32 sign = Math.Sign(vector.x);
        if(sign != 0)
            return sign;
        sign = Math.Sign(vector.y);
        if(sign != 0)
            return sign;
        return Math.Sign(vector.z);
    }

    public static Cell ToCell(this Vector3 vector) {
        return new Cell(vector.x.ToRoundInt32(), vector.z.ToRoundInt32());
    }
}
using System;
using System.Numerics;

namespace FiniteFieldsLib;

public class IntegerModuloN : 
    IEqualityOperators<IntegerModuloN, IntegerModuloN, bool>, 
    IUnaryNegationOperators<IntegerModuloN, IntegerModuloN>,
    IAdditionOperators<IntegerModuloN, IntegerModuloN, IntegerModuloN>, 
    ISubtractionOperators<IntegerModuloN, IntegerModuloN, IntegerModuloN>, 
    IMultiplyOperators<IntegerModuloN, IntegerModuloN, IntegerModuloN>, 
    IDivisionOperators<IntegerModuloN, IntegerModuloN, IntegerModuloN>
{
    public int Value { get; }
    public int N { get; }
    public IntegerModuloN Inverse => GetInverse();

    public IntegerModuloN(int value, int n)
    {
        Value = (n + value % n) % n;
        N = n;
    }

    public static bool operator ==(IntegerModuloN? left, IntegerModuloN? right)
    {
        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
        {
            return ReferenceEquals(left, null) && ReferenceEquals(right, null);
        }
        
        return left.Equals(right);
    }

    public static bool operator !=(IntegerModuloN? left, IntegerModuloN? right)
    {
        return !(left == right);
    }

    public static IntegerModuloN operator -(IntegerModuloN integer)
    {
        return new IntegerModuloN(-integer.Value, integer.N);
    }

    public static IntegerModuloN operator +(IntegerModuloN left, IntegerModuloN right)
    {
        if (left.N != right.N)
            throw new ArgumentException("Modules are being taken from different Ns");
        
        return new IntegerModuloN(left.Value + right.Value, left.N);
    }

    public static IntegerModuloN operator -(IntegerModuloN left, IntegerModuloN right)
    {
        if (left.N != right.N)
            throw new ArgumentException("Modules are being taken from different Ns");
        
        return new IntegerModuloN(left.Value - right.Value, left.N);
    }

    public static IntegerModuloN operator *(IntegerModuloN left, IntegerModuloN right)
    {
        if (left.N != right.N)
            throw new ArgumentException("Modules are being taken from different Ns");
        
        return new IntegerModuloN(left.Value * right.Value, left.N);
    }

    public static IntegerModuloN operator /(IntegerModuloN left, IntegerModuloN right)
    {
        if (left.N != right.N)
            throw new ArgumentException("Modules are being taken from different Ns");
        
        return left * right.Inverse;
    }

    public IntegerModuloN GetInverse()
    {
        var gcd = FFMath.Gcd(Value, N, out var x, out var y);

        if (gcd != 1)
            throw new ArithmeticException($"Impossible to find inverse element of {Value} to the modulus of {N}");

        return new IntegerModuloN(x, N);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(obj, null)) return false;
        if (GetType() != obj.GetType()) return false;
        if (ReferenceEquals(this, obj)) return true;

        var other = (IntegerModuloN)obj;
        if (N != other.N)
            throw new ArgumentException("Modules are being taken from different Ns");

        return Value == other.Value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value, N);
    }

    public override string ToString()
    {
        return Value + ", " + N;
    }
}
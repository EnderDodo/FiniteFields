using System.Numerics;

namespace FiniteFieldsLib;

public class Polynomial<T> where T :
    IEqualityOperators<T, T, bool>,
    IUnaryNegationOperators<T, T>,
    IAdditionOperators<T, T, T>,
    ISubtractionOperators<T, T, T>,
    IMultiplyOperators<T, T, T>,
    IDivisionOperators<T, T, T>
{
    public T[] Coefficients;
    public int Degree => Coefficients.Length - 1;
    public T this[int i] => Degree >= i ? Coefficients[i] : ZeroElement;
    public T ZeroElement { get; }

    public Polynomial(T[] coefficients, T zeroElement)
    {
        ZeroElement = zeroElement;
        //Coefficients = coefficients;
        Coefficients = new T[coefficients.Length];
        Array.Copy(coefficients, Coefficients, coefficients.Length);
        
        if (Coefficients[^1] == zeroElement)
        {
            DeleteMeaninglessZeros(ref Coefficients);
        }
    }

    private void DeleteMeaninglessZeros(ref T[] coeffs)
    {
        int newDegree = coeffs.Length - 2;
        for (; newDegree >= 0; newDegree--)
        {
            if (coeffs[newDegree] != ZeroElement)
            {
                break;
            }
        }

        Array.Resize(ref coeffs, newDegree + 1);
    }

    public static bool operator ==(Polynomial<T>? left, Polynomial<T>? right)
    {
        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
        {
            return ReferenceEquals(left, null) && ReferenceEquals(right, null);
        }

        return left.Equals(right);
    }

    public static bool operator !=(Polynomial<T>? left, Polynomial<T>? right)
    {
        return !(left == right);
    }

    public static Polynomial<T> operator -(Polynomial<T> polynomial)
    {
        if (polynomial == null)
            throw new ArgumentNullException();

        T[] coeffs = new T[polynomial.Degree + 1];

        for (int i = 0; i <= polynomial.Degree; i++)
        {
            coeffs[i] = -polynomial[i];
        }

        return new Polynomial<T>(coeffs, polynomial.ZeroElement);
    }

    public static Polynomial<T> operator +(Polynomial<T> left, Polynomial<T> right)
    {
        if (left.ZeroElement != right.ZeroElement)
            throw new ArgumentException("Zero elements of the polynomials are different");

        int maxDegree = Math.Max(left.Degree, right.Degree);
        T[] coeffs = new T[maxDegree + 1];

        for (int i = 0; i <= maxDegree; i++)
        {
            coeffs[i] = left[i] + right[i];
        }

        return new Polynomial<T>(coeffs, left.ZeroElement);
    }

    public static Polynomial<T> operator -(Polynomial<T> left, Polynomial<T> right)
    {
        return left + -right;
    }

    public static Polynomial<T> operator *(Polynomial<T> left, Polynomial<T> right)
    {
        if (left.ZeroElement != right.ZeroElement)
            throw new ArgumentException("Zero elements of the polynomials are different");

        T[] coeffs = new T[left.Degree + right.Degree + 1];

        for (int i = 0; i <= left.Degree; i++)
        for (int j = 0; j <= right.Degree; j++)
        {
            coeffs[i + j] += left[i] * right[j];
        }

        return new Polynomial<T>(coeffs, left.ZeroElement);
    }

    public static Polynomial<T> operator /(in Polynomial<T> left, in Polynomial<T> right)
    {
        
        if (left.ZeroElement != right.ZeroElement)
            throw new ArgumentException("Zero elements of the polynomials are different");

        if (left.Degree < right.Degree)
        {
            T[] zeroCoeff = new T[1];
            zeroCoeff[0] = left.ZeroElement;
            return new Polynomial<T>(zeroCoeff, left.ZeroElement);
        }
        
        T[] coeffs = new T[left.Degree - right.Degree + 1];
        T[] leftCoeffsClone = new T[left.Degree + 1];
        Array.Copy(left.Coefficients, leftCoeffsClone, left.Degree + 1);
        var leftClone = new Polynomial<T>(leftCoeffsClone, left.ZeroElement);

        while (leftClone.Degree >= right.Degree)
        {
            coeffs[leftClone.Degree - right.Degree] = leftClone[leftClone.Degree] / right[right.Degree];

            var tempCoeff = new T[leftClone.Degree - right.Degree + 1];
            tempCoeff[leftClone.Degree - right.Degree] = coeffs[leftClone.Degree - right.Degree];
            var tempMultiplier = new Polynomial<T>(tempCoeff, left.ZeroElement);

            leftClone -= right * tempMultiplier;
        }

        return new Polynomial<T>(coeffs, left.ZeroElement);
    }

    public static Polynomial<T> operator %(in Polynomial<T> left, in Polynomial<T> right)
    {
        if (left.ZeroElement != right.ZeroElement)
            throw new ArgumentException("Zero elements of the polynomials are different");

        if (left.Degree < right.Degree)
            return left;

        T[] coeffs = new T[left.Degree - right.Degree + 1];
        T[] leftCoeffsClone = new T[left.Degree + 1];
        Array.Copy(left.Coefficients, leftCoeffsClone, left.Degree + 1);
        var leftClone = new Polynomial<T>(leftCoeffsClone, left.ZeroElement);

        while (leftClone.Degree >= right.Degree)
        {
            coeffs[leftClone.Degree - right.Degree] = leftClone[leftClone.Degree] / right[right.Degree];

            var tempCoeff = new T[leftClone.Degree - right.Degree + 1];
            tempCoeff[leftClone.Degree - right.Degree] = coeffs[leftClone.Degree - right.Degree];
            var tempMultiplier = new Polynomial<T>(tempCoeff, left.ZeroElement);

            leftClone -= right * tempMultiplier;
        }

        return leftClone; //it is a remainder
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(obj, null)) return false;
        if (GetType() != obj.GetType()) return false;
        if (ReferenceEquals(this, obj)) return true;

        var other = (Polynomial<T>)obj;
        if (ZeroElement != other.ZeroElement)
            throw new ArgumentException("The polynomials have different zero elements");

        return Coefficients == other.Coefficients && ZeroElement == other.ZeroElement;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ZeroElement, Coefficients);
    }

    public override string ToString()
    {
        var result = "";

        if (Degree >= 1)
            for (int i = Degree; i > 0; i--)
            {
                result += Coefficients[i] + "x^" + i + " + ";
            }

        result += Coefficients[0];

        return result;
    }

    public static Polynomial<IntegerModuloN> GetPolynomialOverPrimeFieldFromIntArray(int[] coeffs, int p)
    {
        IntegerModuloN[] moduledCoeffs = new IntegerModuloN[coeffs.Length];
        for (int i = 0; i < coeffs.Length; i++)
        {
            moduledCoeffs[i] = new IntegerModuloN(coeffs[i], p);
        }

        return new Polynomial<IntegerModuloN>(moduledCoeffs, new IntegerModuloN(0, p));
    }
}
using System;

namespace FiniteFieldsLib;

public class FiniteField
{
    public int P { get; }
    public int N { get; }
    public int Order { get; }
    public Polynomial<IntegerModuloN> IrreduciblePolynomial { get; }
    
    public FiniteField(int p, int n, Polynomial<IntegerModuloN> irreduciblePolynomial)
    {
        if (!FFMath.IsPrime(p))
            throw new ArithmeticException($"Finite fields contain p^n elements, where p is prime; {p} is not prime");
        if (n != irreduciblePolynomial.Degree)
            throw new ArgumentException($"Degree of the irreducible polynomial is not equal to N({n})");
        
        P = p;
        N = n;
        Order = (int)Math.Pow(p, n);
        IrreduciblePolynomial = irreduciblePolynomial;
    }

    public FiniteField(int p, int n, int[] irreduciblePolynomialCoeffs)
    {
        if (!FFMath.IsPrime(p))
            throw new ArithmeticException($"Finite fields contain p^n elements, where p is prime; {p} is not prime");
        if (n != irreduciblePolynomialCoeffs.Length - 1)
            throw new ArgumentException($"Degree of the irreducible polynomial is not equal to N({n})");

        P = p;
        N = n;
        Order = (int)Math.Pow(p, n);
        IrreduciblePolynomial = Polynomial<IntegerModuloN>
            .GetPolynomialOverPrimeFieldFromIntArray(irreduciblePolynomialCoeffs, p);
    }

    public FiniteFieldElement Zero => GetElement(new[]{0});
    public FiniteFieldElement One => GetElement(new[]{1});
    public FiniteFieldElement GetElement(Polynomial<IntegerModuloN> polynomial)
    {
        return new FiniteFieldElement(polynomial, this);
    }
    
    public FiniteFieldElement GetElement(int[] polynomialCoeffs)
    {
        return new FiniteFieldElement(Polynomial<IntegerModuloN>
            .GetPolynomialOverPrimeFieldFromIntArray(polynomialCoeffs, P), this);
    }
    
    public static bool operator ==(FiniteField? left, FiniteField? right)
    {
        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
        {
            return ReferenceEquals(left, null) && ReferenceEquals(right, null);
        }
        return left.Equals(right);
    }

    public static bool operator !=(FiniteField? left, FiniteField? right)
    {
        return !(left == right);
    }
    
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(obj, null)) return false;
        if (GetType() != obj.GetType()) return false;
        if (ReferenceEquals(this, obj)) return true;

        var other = (FiniteField)obj;
        return Order == other.Order;
    }

    public override int GetHashCode()
    {
        return Order.GetHashCode();
    }

    public override string ToString()
    {
        return $"GF({Order})";
    }
}
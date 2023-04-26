using System.Numerics;

namespace FiniteFieldsLib;

public class FiniteFieldElement : 
    IEqualityOperators<FiniteFieldElement, FiniteFieldElement, bool>, 
    IUnaryNegationOperators<FiniteFieldElement, FiniteFieldElement>,
    IAdditionOperators<FiniteFieldElement, FiniteFieldElement, FiniteFieldElement>, 
    ISubtractionOperators<FiniteFieldElement, FiniteFieldElement, FiniteFieldElement>, 
    IMultiplyOperators<FiniteFieldElement, FiniteFieldElement, FiniteFieldElement>, 
    IDivisionOperators<FiniteFieldElement, FiniteFieldElement, FiniteFieldElement>
{
    public FiniteField Parent { get; }
    public Polynomial<IntegerModuloN> Polynomial { get; }
    public FiniteFieldElement Inverse => Pow(Parent.Order - 2);

    public FiniteFieldElement(Polynomial<IntegerModuloN> polynomial, FiniteField finiteField)
    {
        Polynomial = polynomial;
        Parent = finiteField;
    }
    
    public static bool operator ==(FiniteFieldElement? left, FiniteFieldElement? right)
    {
        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
        {
            return ReferenceEquals(left, null) && ReferenceEquals(right, null);
        }

        return left.Equals(right);
    }

    public static bool operator !=(FiniteFieldElement? left, FiniteFieldElement? right)
    {
        return !(left == right);
    }

    public static FiniteFieldElement operator -(FiniteFieldElement element)
    {
        return element.Parent.GetElement(-element.Polynomial);
    }

    public static FiniteFieldElement operator +(FiniteFieldElement left, FiniteFieldElement right)
    {
        if (left.Parent != right.Parent)
            throw new ArgumentException("The elements are from different fields");

        return left.Parent.GetElement(left.Polynomial + right.Polynomial);
    }

    public static FiniteFieldElement operator -(FiniteFieldElement left, FiniteFieldElement right)
    {
        return left + -right;
    }

    public static FiniteFieldElement operator *(FiniteFieldElement left, FiniteFieldElement right)
    {
        if (left.Parent != right.Parent)
            throw new ArgumentException("The elements are from different fields");

        return left.Parent.GetElement(left.Polynomial * right.Polynomial);
    }

    public static FiniteFieldElement operator /(FiniteFieldElement left, FiniteFieldElement right)
    {
        if (left.Parent != right.Parent)
            throw new ArgumentException("The elements are from different fields");

        if (right == right.Parent.Zero)
            throw new DivideByZeroException("Attempted to divide by zero");

        return left.Parent.GetElement(left.Polynomial * right.Inverse.Polynomial % left.Parent.IrreduciblePolynomial);
    }
    
    public FiniteFieldElement Pow(int degree)
    {
        degree %= Parent.Order - 1;
        switch (degree)
        {
            case 0:
                return Parent.One;
            case 1:
                return this;
        }

        if (degree % 2 == 0)
            return Pow(degree / 2) * Pow(degree / 2);

        return this * Pow(degree - 1);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(obj, null)) return false;
        if (GetType() != obj.GetType()) return false;
        if (ReferenceEquals(this, obj)) return true;

        var other = (FiniteFieldElement)obj;
        if (Parent != other.Parent)
            throw new ArgumentException("The elements are from different finite fields");

        return Polynomial == other.Polynomial && Parent == other.Parent;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Parent, Polynomial);
    }
    
    public override string ToString()
    {
        return $"{Polynomial} of " + Parent;
    }
}
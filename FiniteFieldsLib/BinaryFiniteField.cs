using System;
using System.Linq;

namespace FiniteFieldsLib;

public class BinaryFiniteField : FiniteField
{
    public BinaryFiniteField(int n, Polynomial<IntegerModuloN> irreduciblePolynomial) : base(2, n,
        irreduciblePolynomial)
    {
    }

    public BinaryFiniteField(int n, int[] irreduciblePolynomialCoeffs) : base(2, n, irreduciblePolynomialCoeffs)
    {
    }

    public FiniteFieldElement GetElementFromBytes(byte[] bytes) //big-endian method and *my* system is little-endian
    {
        if (bytes.Length != 4)
            throw new ArgumentException($"Array length of {bytes.Length} is not 4");

        if (N % 8 != 0)
            throw new ArgumentException($"The N of {N} is not a multiple of 8");

        if (N / 8 < bytes.Length)
            throw new ArgumentException($"The field is too small for that amount of bytes - N of {N} is sufficient");

        var number = BitConverter.ToInt32(bytes);

        var binaryString = Convert.ToString(number, 2).Reverse();
        if (BitConverter.IsLittleEndian)
            binaryString = binaryString.Reverse();
        
        var polynomialCoeffs = binaryString.Select(item => item - '0').ToArray();

        return GetElement(polynomialCoeffs);
    }

    public byte[] GetBytesFromElement(FiniteFieldElement element)
    {
        if (N % 8 != 0)
            throw new Exception($"The N of {N} is not a multiple of 8");

        var binaryString = BitConverter.IsLittleEndian
            ? string.Join("", element.Polynomial.Coefficients.Select(item => item.Value)).Reverse().ToArray()
            : string.Join("", element.Polynomial.Coefficients.Select(item => item.Value)).ToArray();
        
        var number = Convert.ToInt32(new string(binaryString), 2);
        return BitConverter.GetBytes(number);
    }
}
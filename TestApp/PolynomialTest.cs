using NUnit.Framework;
using FiniteFieldsLib;

namespace TestApp;

public class PolynomialTest
{
    [Test]
    public void EqualsTest1()
    {
        var polynomial1 = new Polynomial<int>(new[] { 1, 2, 3, 4 }, 0);
        var polynomial2 = new Polynomial<int>(new[] { 1, 2, 3, 4 }, 0);

        Assert.That(polynomial1, Is.EqualTo(polynomial2));
    }

    [Test]
    public void EqualsTest2()
    {
        var polynomial1 = new Polynomial<int>(new[] { 1, 2, 3, 4 }, 0);
        var polynomial2 = new Polynomial<int>(new[] { 1, 0, 3, 4 }, 0);

        Assert.That(polynomial1, Is.Not.EqualTo(polynomial2));
    }

    [Test]
    public void UnaryNegationTest()
    {
        var polynomial1 = new Polynomial<int>(new[] { 1, 2, 3, 4 }, 0);
        var polynomial2 = -polynomial1;
        var expectedResult = new Polynomial<int>(new[] { -1, -2, -3, -4 }, 0);

        Assert.That(polynomial2, Is.EqualTo(expectedResult));
    }

    [Test]
    public void AdditionTest1()
    {
        var polynomial1 = new Polynomial<int>(new[] { 1, 2, 3, 4 }, 0);
        var polynomial2 = new Polynomial<int>(new[] { 1, 2, 3, 4 }, 0);

        var sum = polynomial1 + polynomial2;
        var expectedResult = new Polynomial<int>(new[] { 2, 4, 6, 8 }, 0);

        Assert.That(sum, Is.EqualTo(expectedResult));
    }

    [Test]
    public void AdditionTest2()
    {
        var polynomial1 = new Polynomial<int>(new[] { 1, 2, 3 }, 0);
        var polynomial2 = new Polynomial<int>(new[] { 1, 2, 3, -4, 5 }, 0);

        var sum = polynomial1 + polynomial2;
        var expectedResult = new Polynomial<int>(new[] { 2, 4, 6, -4, 5 }, 0);

        Assert.That(sum, Is.EqualTo(expectedResult));
    }

    [Test]
    public void SubtractionTest1()
    {
        var polynomial1 = new Polynomial<int>(new[] { 1, 2, 8, 5 }, 0);
        var polynomial2 = new Polynomial<int>(new[] { 1, 2, 3, 4 }, 0);

        var diff = polynomial1 - polynomial2;
        var expectedResult = new Polynomial<int>(new[] { 0, 0, 5, 1 }, 0);

        Assert.That(diff, Is.EqualTo(expectedResult));
    }

    [Test]
    public void SubtractionTest2()
    {
        var polynomial1 = new Polynomial<int>(new[] { 1, 2, 0 }, 0);
        var polynomial2 = new Polynomial<int>(new[] { 1, 2, 3, 4, 5 }, 0);

        var diff = polynomial1 - polynomial2;
        var expectedResult = new Polynomial<int>(new[] { 0, 0, -3, -4, -5 }, 0);

        Assert.That(diff, Is.EqualTo(expectedResult));
    }

    [Test]
    public void MultiplicationTest1()
    {
        var polynomial1 = new Polynomial<int>(new[] { 5, -34, -17, 23, 0, 8 }, 0);
        var polynomial2 = new Polynomial<int>(new[] { 14, -7, 10, 0, 3 }, 0);

        var mul = polynomial1 * polynomial2;
        var expectedResult =
            new Polynomial<int>(new[] { 70, -511, 50, 101, -316, 240, -107, 149, 0, 24 }, 0);

        Assert.That(mul, Is.EqualTo(expectedResult));
    }

    [Test]
    public void DivisionTest()
    {
        var polynomial1 = new Polynomial<int>(new[] { 39, -52, -16, 5, -5, 1 }, 0);
        var polynomial2 = new Polynomial<int>(new[] { 8, -5, -2, 1 }, 0);

        var remainder = polynomial1 / polynomial2;
        var expectedResult = new Polynomial<int>(new[] { 4, -3, 1 }, 0);

        Assert.That(remainder, Is.EqualTo(expectedResult));
    }

    [Test]
    public void FindRemainderTest()
    {
        var polynomial1 = new Polynomial<int>(new[] { 39, -52, -16, 5, -5, 1 }, 0);
        var polynomial2 = new Polynomial<int>(new[] { 8, -5, -2, 1 }, 0);

        var remainder = polynomial1 % polynomial2;
        var expectedResult = new Polynomial<int>(new[] { 7, -8, -31 }, 0);

        Assert.That(remainder, Is.EqualTo(expectedResult));
    }
}
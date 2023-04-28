# Библиотека для работы с элементами конечных полей

В библиотеке реализованы конечные поля и полиномы над любым типом, у которого определены стандартные алгебраические операции.

### Пример использования

## 1. Простое поле
```c#
// создание поля порядка 7 и действия над его элементами
var primeField = new FiniteField(7, 1, new[] { 0, 1 });
var element1 = primeField.GetElement(new []{4}); // 4
var element2 = primeField.GetElement(new []{5}); // 5
var minusElement1 = -element1; // 3
var diff = element1 - element2; // 6
var sum = element1 + element2; // 2
var product = element1 * element2; // 6
var div = element1 / element2; // 5
var isEqual = element2 == minusElement1; //false
var isNotEqual = element2 != sum; //true
```
## 2. Полином
```c#
// создание многочленов с коэффициентами из поля F_7 и действия над ними
var polynomial1 =
	Polynomial<IntegerModuloN>.GetPolynomialOverPrimeFieldFromIntArray(new[] { 1, 3, 6 }, primeField.P); //6x^2 + 3x + 1
var polynomial2 =
	Polynomial<IntegerModuloN>.GetPolynomialOverPrimeFieldFromIntArray(new[] { 2, 5 }, primeField.P); //5x + 2
var degree = polynomial1.Degree; // 2
var minusPolynomial1 = -polynomial1; // x^2 + 4x + 6
var diff = polynomial2 - polynomial1; // x^2 + 2x + 1
var sum = polynomial1 + polynomial2; // 6x^2 + x + 3
var mul = polynomial1 * polynomial2; // 2x^3 + 6x^2 + 4x + 2
var div = polynomial1 / polynomial2; // 4x + 6
var remainder = polynomial1 % polynomial2; // 3
var isNotEqual = polynomial1 == polynomial2; // false
```
## 3. Конечные поля
```c#
//создание GF(9)
var field = new FiniteField(3, 2, new[] { 1, 1, 1 });
var primeFieldOrder = field.P; // 3
var one = field.One;
var zero = field.Zero;
var element1 = field.GetElement(new[] { 0, 2 }); // 2x
var element2 = field.GetElement(new[] { 2, 1 }); // x + 2
var minusElement1 = -element1; // x
var inverse = element1.Inverse; // 2x
var diff = element2 - element1; // 2x + 2
var sum = element1 + element2; // 2
var myl = element1 * element2; // 2x + 1
var div = element1 / element2; // 0
var isNotEqual = element1 == element2; // false

//создание GF(2^n)
var binaryField = new BinaryFiniteField(32,
	new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 });
//операции над массивами байтов длины 4
var element = binaryField.GetElementFromBytes(new byte[] { 1, 2, 3, 4 });
var bytes = field.GetBytesFromElement(element);
```

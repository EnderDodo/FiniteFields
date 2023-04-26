using FiniteFieldsLib;

int[] array = new int[] { 0, 1, 2, 0, 0 };
Polynomial<int> X = new Polynomial<int>(array, 0);
// foreach (var n in X.Coefficients)
// {
//     Console.WriteLine(n);
// }

// foreach (var t in array)
// {
//     Console.WriteLine(t);
// }

// int[] array1 = new int[] { 1, 1 };
// Polynomial<int> X1 = new Polynomial<int>(array1, 0);
// var X2 = X % X1;
// Console.WriteLine(X);
// Console.WriteLine();
// Console.WriteLine(X2);
byte[] a = {0, 0, 0, 5};
byte[] b = {5, 0, 0, 0};
byte[] array1 = {2, 1, 0, 0};

Console.WriteLine(BitConverter.ToInt32(a)); //much number, big integer - 83886080
Console.WriteLine(BitConverter.ToInt32(array1)); //258

var number = BitConverter.ToInt32(array1);
Console.WriteLine(number);
var reversedBinaryString = Convert.ToString(number, 2).Reverse().ToArray();
string result = reversedBinaryString.Aggregate("", (current, chara) => current + chara);
Console.WriteLine(result);

foreach (var z in BitConverter.GetBytes(number))
{
    Console.WriteLine(z);
}

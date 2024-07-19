using System;
using System.Collections.Generic;

public class BMICalculator
{
    private Stack<double> bmiHistory = new Stack<double>();

    public double CalculateBMI(double weight, double height)
    {
        if (height <= 0)
            throw new ArgumentException("Height must be greater than zero.");

        double bmi = weight / (height * height);
        bmiHistory.Push(bmi);
        return bmi;
    }

    public string GetBMICategory(double bmi)
    {
        if (bmi < 18.5)
            return "Underweight";
        else if (bmi >= 18.5 && bmi < 24.9)
            return "Normal weight";
        else if (bmi >= 25 && bmi < 29.9)
            return "Overweight";
        else
            return "Obesity";
    }

    public void DisplayHistory()
    {
        Console.WriteLine("BMI History:");
        foreach (var bmi in bmiHistory)
        {
            Console.WriteLine(bmi);
        }
    }
}

public delegate T Operation<T>(T a, T b);

public class Calculator<T> where T : struct, IComparable, IConvertible, IComparable<T>, IEquatable<T>
{
    public T Add(T a, T b, Operation<T> addDelegate)
    {
        return addDelegate(a, b);
    }

    public T Subtract(T a, T b, Operation<T> subtractDelegate)
    {
        return subtractDelegate(a, b);
    }

    public T Multiply(T a, T b, Operation<T> multiplyDelegate)
    {
        return multiplyDelegate(a, b);
    }

    public T Divide(T a, T b, Operation<T> divideDelegate)
    {
        return divideDelegate(a, b);
    }
}

class Program
{
    static void Main()
    {
        BMICalculator bmiCalculator = new BMICalculator();
        Calculator<double> calculator = new Calculator<double>();

        Operation<double> add = (a, b) => a + b;
        Operation<double> subtract = (a, b) => a - b;
        Operation<double> multiply = (a, b) => a * b;
        Operation<double> divide = (a, b) => a / b;

        while (true)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Calculate BMI");
            Console.WriteLine("2. Perform Arithmetic Operation");
            Console.WriteLine("3. View BMI History");
            Console.WriteLine("4. Exit");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter height (meters): ");
                    double height = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Enter weight (kilograms): ");
                    double weight = Convert.ToDouble(Console.ReadLine());

                    double bmi = bmiCalculator.CalculateBMI(weight, height);
                    string category = bmiCalculator.GetBMICategory(bmi);

                    Console.WriteLine($"BMI: {bmi}");
                    Console.WriteLine($"Category: {category}");
                    break;

                case 2:
                    Console.WriteLine("Choose an operation:");
                    Console.WriteLine("1. Add");
                    Console.WriteLine("2. Subtract");
                    Console.WriteLine("3. Multiply");
                    Console.WriteLine("4. Divide");

                    int operationChoice = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter first number: ");
                    double a = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Enter second number: ");
                    double b = Convert.ToDouble(Console.ReadLine());

                    switch (operationChoice)
                    {
                        case 1:
                            Console.WriteLine($"Result: {calculator.Add(a, b, add)}");
                            break;
                        case 2:
                            Console.WriteLine($"Result: {calculator.Subtract(a, b, subtract)}");
                            break;
                        case 3:
                            Console.WriteLine($"Result: {calculator.Multiply(a, b, multiply)}");
                            break;
                        case 4:
                            Console.WriteLine($"Result: {calculator.Divide(a, b, divide)}");
                            break;
                        default:
                            Console.WriteLine("Invalid operation choice.");
                            break;
                    }
                    break;

                case 3:
                    bmiCalculator.DisplayHistory();
                    break;

                case 4:
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
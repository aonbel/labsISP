namespace MauiLabs.Lab3.Handlers;

public static class OperatorHandler
{
    public static double GetResultOfBinaryOperation(double? firstOperand, double secondOperand, string? operation)
    {
        if (firstOperand is null || operation is null)
        {
            return secondOperand;
        }
        
        var firstOperandDouble = firstOperand.Value;
        
        return operation switch
        {
            "+" => firstOperandDouble + secondOperand,
            "-" => firstOperandDouble - secondOperand,
            "*" => firstOperandDouble * secondOperand,
            "/" => firstOperandDouble / secondOperand,
            _ => throw new ArgumentOutOfRangeException(nameof(operation), operation, null)
        };
    }

    public static double GetResultOfUnaryOperation(double? operand, string? operation)
    {
        ArgumentNullException.ThrowIfNull(operand);
        ArgumentNullException.ThrowIfNull(operation);
        
        var operandDouble = operand.Value;

        return operation switch
        {
            "%" => operandDouble * 0.01,
            "1/x" => 1 / operandDouble,
            "x^2" => operandDouble * operandDouble,
            "sqrt(x)" => Math.Sqrt(operandDouble),
            "+/-" => -operandDouble,
            "|x|" => Math.Abs(operandDouble),
            _ => throw new ArgumentOutOfRangeException(nameof(operation), operation, null)
        };
    }
}
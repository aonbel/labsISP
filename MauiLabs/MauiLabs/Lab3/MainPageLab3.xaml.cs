using System.Globalization;
using MauiLabs.Lab3.Handlers;

namespace MauiLabs.Lab3;

public partial class Lab3 : ContentPage
{
    private double? _previousOperand;
    private string? _previousOp;

    public Lab3()
    {
        InitializeComponent();
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        var button = (sender as Button)!;
        var currentText = TextField.Text;

        var tryParseResult = double.TryParse(currentText, out var currentOperand);

        switch (button.Text)
        {
            case "0":
            case "1":
            case "2":
            case "3":
            case "4":
            case "5":
            case "6":
            case "7":
            case "8":
            case "9":
                TextField.Text += button.Text;
                break;
            case ",":
                if (!currentText.Contains(',') && !currentText.Contains('E'))
                {
                    TextField.Text += CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                }
                break;
            case "CE":
                TextField.Text = string.Empty;
                break;
            case "C":
                TextField.Text = string.Empty;
                _previousOperand = null;
                break;
            case "Backspace":
                TextField.Text = currentText.Remove(currentText.Length - 1);
                break;
            case "+":
            case "-":
            case "*":
            case "/":
                _previousOperand =
                    OperatorHandler.GetResultOfBinaryOperation(
                        _previousOperand,
                        currentOperand,
                        _previousOp);
                _previousOp = button.Text;
                TextField.Text = string.Empty;
                break;
            case "%":
            case "1/x":
            case "x^2":
            case "+/-":
            case "sqrt(x)":
            case "|x|":
                _previousOperand =
                    OperatorHandler.GetResultOfBinaryOperation(
                        _previousOperand,
                        currentOperand,
                        _previousOp);
                TextField.Text = OperatorHandler.GetResultOfUnaryOperation(
                        _previousOperand,
                        button.Text)
                    .ToString(CultureInfo.CurrentCulture);
                _previousOperand = null;
                _previousOp = null;
                break;
            case "=":
                TextField.Text =
                    OperatorHandler.GetResultOfBinaryOperation(
                        _previousOperand,
                        currentOperand,
                        _previousOp).ToString(CultureInfo.CurrentCulture);
                _previousOp = null;
                _previousOperand = null;
                break;
        }
    }
}
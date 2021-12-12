using System;
using System.Collections.Generic;
using UnityEngine;

public class Calculator
{
    private double _result;
    private List<double> _operands;
    private List<Operator> _operators;

    public event Action<TypeError> ErrorEvent;
    public event Action<double> ResultEvent;

    public Calculator()
    {
        _operands = new List<double>();
        _operators = new List<Operator>();
    }

    public void Calculate(string expression)
    {
        Parse(expression);
        Calculation();
    }
    
    private void Parse(string expression)
    {
        _result = 0;
        _operands.Clear();
        _operators.Clear();
        
        var subs = expression.Split(':');

        if (double.TryParse(subs[0], out var firstNumber))
        {
            _operands.Add(firstNumber);
        }
        else
        {
            ErrorEvent?.Invoke(TypeError.UnknownOperation);
            return;
        }
        
        for (var i = 1; i < subs.Length; i++)
        {
            var sub = subs[i];

            if (double.TryParse(sub, out var number))
            {
                _operands.Add(number);
            }
            else
            {
                switch (sub)
                {
                    case "+" : _operators.Add(Operator.Addition); break;
                    case "-" : _operators.Add(Operator.Subtraction); break;
                    case "*" : _operators.Add(Operator.Multiplication); break;
                    case "/" : _operators.Add(Operator.Division); break;
                }
                
            }
        }
    }

    private void Calculation()
    {
        var result = _operands[0];

        for (var i = 1; i < _operands.Count; i++)
        {
            switch (_operators[i - 1])
            {
                case Operator.Addition : result += _operands[i]; break;
                case Operator.Subtraction : result -= _operands[i]; break; 
                case Operator.Multiplication : result *= _operands[i]; break;
                case Operator.Division:
                {
                    if (_operands[i] == 0d)
                    {
                        ErrorEvent?.Invoke(TypeError.DivisionByZero);
                        Debug.Log("Error Division");
                        return;
                    }
                    
                    result /= _operands[i]; 
                    break;
                } 
            }
        }

        ResultEvent?.Invoke(result);
    }

    private enum Operator
    {
        None,
        Addition,
        Subtraction,
        Division,
        Multiplication,
    }
}
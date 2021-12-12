using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    [SerializeField] private Text _inputText; 
    [SerializeField] private Text _resultText;

    private string _expression;
    
    private Calculator _calculator;
    private Localization _localization;

    private void Awake()
    {
        _calculator = new Calculator();
        _localization = new Localization();
    }

    private void Start()
    {
        _localization.Start();
    }

    private void OnEnable()
    {
        _calculator.ErrorEvent += OnError;
        _calculator.ResultEvent += OnResult;
    }

    private void OnDisable()
    {
        _calculator.ErrorEvent -= OnError;
        _calculator.ResultEvent -= OnResult;
    }

    private void OnError(TypeError typeError)
    {
        _resultText.text = _localization.GetError((int) typeError);
    }

    private void OnResult(double result)
    {
        _resultText.text = result.ToString(CultureInfo.InvariantCulture);
    }
    
    public void AddSymbol(string value)
    {
        switch (value)
        {
            case "ce":
                _inputText.text = string.Empty;
                _resultText.text = string.Empty;
                _expression = string.Empty;
                break;
            case "=":
                _calculator.Calculate(_expression);
                break;
            default:
            {
                _inputText.text += value;

                if (!char.IsNumber(value[0]) && value != ".")
                {
                    value = string.Concat(":", value, ":");
                }
            
                _expression += value;
                break;
            }
        }
    }
}
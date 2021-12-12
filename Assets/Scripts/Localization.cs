using System;
using System.IO;
using UnityEngine;

public class Localization
{
    private string _json;
    private Language _language;
    private string _currentLanguage;
    
    public Localization()
    {
        _language = new Language();
    }

    public void Start()
    {
        CheckLanguage();   
        LangLoad();
    }

    public string GetError(int indexError)
    {
        Debug.Log(_language.errors[indexError]);
        return _language.errors[indexError];
    }

    private void LangLoad()
    {
        _json = File.ReadAllText(Path.Combine(Application.streamingAssetsPath,_currentLanguage + ".json"));
        _language = JsonUtility.FromJson<Language>(_json);
    }

    private void CheckLanguage()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Russian : _currentLanguage = "ru_RU"; break;
            case SystemLanguage.English : _currentLanguage = "en_US"; break;
        }
    }
}

public class Language
{
    public string[] errors;
}
using System;
using System.Collections.Generic;
using UnityEngine;

public class Autorization : MonoBehaviour
{
    private const string LOGIN = "login";
    private const string PASSWORD = "password";
    private string _login;
    private string _password;

    public Action Error;

    public void SetLogin(string value)
    {
        _login = value;
    }

    public void SetPassword(string value)
    {
        _password = value;
    }

    public void SignIn()
    {
        if ((string.IsNullOrEmpty(_login) || string.IsNullOrEmpty(_password)))
        {
            ErrorMessage("Логин и/или пароль пустые");
            return;
        }

        var uri = UrlLibrary.MAIN + UrlLibrary.AUTHORIZATION;
        var data = new Dictionary<string, string>
        {
            {LOGIN, _login},
            {PASSWORD, _password}
        };
        Network.Instance.Post(uri, data, Success, ErrorMessage);
    }

    private void Success(string data)
    {
        string[] result = data.Split('|');
        if (result.Length < 2 || result[0] != "ok")
        {
            ErrorMessage("Ошибочный ответ сервера: " + data);
            return;
        }
        if (int.TryParse(result[1], out int value))
        {
            Debug.Log("Успешный вход ID = " + value);
            UserInfo.Instance.SetID(value);

        }
        else
        {
            ErrorMessage("Не удалось распарсить " + result[1] + " в INT. Полный ответ: " + data);
        }
    }

    private void ErrorMessage(string error)
    {
        Debug.LogError(error);
        Error?.Invoke();
    }
}

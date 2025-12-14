using System;
using System.Collections.Generic;
using UnityEngine;

public class Registration : MonoBehaviour
{
    private const string LOGIN = "login";
    private const string PASSWORD = "password";
    private string _login;
    private string _password;

    private string _conformPassword;

    public Action Error;
    public Action Success;

    public void SetLogin(string value)
    {
        _login = value;
    }

    public void SetPassword(string value)
    {
        _password = value;
    }

    public void SetConformPassword(string value)
    {
        _conformPassword = value;
    }

    public void SignUp()
    {
        if ((string.IsNullOrEmpty(_login) || string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_conformPassword)))
        {
            ErrorMessage("Логин и/или пароль пустые");
            return;
        }

        if (_password != _conformPassword)
        {
            ErrorMessage("Пароли не совпадают");
            return;
        }

        var uri = UrlLibrary.MAIN + UrlLibrary.REGISTRATION;
        var data = new Dictionary<string, string>
        {
            {LOGIN, _login},
            {PASSWORD, _password}
        };
        Network.Instance.Post(uri, data, SuccessMessage, ErrorMessage);
    }

    private void SuccessMessage(string data)
    {

        if (data != "ok")
        {
            ErrorMessage("Ошибочный ответ сервера: " + data);
            return;
        }
       
    Debug.Log("Успешная регистрация");
    Success?.Invoke();
    }

    private void ErrorMessage(string error)
    {
        Debug.LogError(error);
        Error?.Invoke();
    }
}

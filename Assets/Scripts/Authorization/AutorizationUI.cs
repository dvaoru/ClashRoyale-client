using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutorizationUI : MonoBehaviour
{
    [SerializeField] private Autorization _autorization;
    [SerializeField] private TMP_InputField _login;
    [SerializeField] private TMP_InputField _password;
    [SerializeField] private Button _signIn;
    [SerializeField] private Button _signUp;

    [SerializeField] private GameObject _authorizationCanvas;
    [SerializeField] private GameObject _registrationCanvas;

    private void Awake()
    {
        _login.onEndEdit.AddListener(_autorization.SetLogin);
        _password.onEndEdit.AddListener(_autorization.SetPassword);

        _signIn.onClick.AddListener(SignInClick);
        _signUp.onClick.AddListener(SignUpClick);

        _autorization.Error += () =>
        {
            _signIn.gameObject.SetActive(true);
            _signUp.gameObject.SetActive(true);
        };
    }

    private void SignUpClick()
    {
        _authorizationCanvas.SetActive(false);
        _registrationCanvas.SetActive(true);
    }

    private void SignInClick()
    {
        _signIn.gameObject.SetActive(false);
        _signUp.gameObject.SetActive(false);

        _autorization.SignIn();

    }
}

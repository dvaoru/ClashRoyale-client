using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegistrationUI : MonoBehaviour
{

    [SerializeField] private Registration _registration;
    [SerializeField] private TMP_InputField _login;
    [SerializeField] private TMP_InputField _password;

    [SerializeField] private TMP_InputField _conformPassword;
    [SerializeField] private Button _apply;
    [SerializeField] private Button _signIn;

    [SerializeField] private GameObject _authorizationCanvas;
    [SerializeField] private GameObject _registrationCanvas;

    private void Awake()
    {
        _login.onEndEdit.AddListener(_registration.SetLogin);
        _password.onEndEdit.AddListener(_registration.SetPassword);
        _conformPassword.onEndEdit.AddListener(_registration.SetConformPassword);

        _apply.onClick.AddListener(ApplyClick);
        _signIn.onClick.AddListener(SignInClick);

        _registration.Error += () =>
        {
            _apply.gameObject.SetActive(true);
            _signIn.gameObject.SetActive(true);
        };

        _registration.Success += () =>
        {
            _signIn.gameObject.SetActive(true);
        };
    }

    private void ApplyClick()
    {
        _apply.gameObject.SetActive(false);
        _signIn.gameObject.SetActive(false);

        _registration.SignUp();

    }

    private void SignInClick()
    {
          _registrationCanvas.SetActive(false);
        _authorizationCanvas.SetActive(true);
      
    }
}

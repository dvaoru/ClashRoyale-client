using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private RectTransform _filledImage;
    [SerializeField] private float _defoltWidth = 0f;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TextMeshProUGUI _healthText;

    private Camera _mainCamera;
    private void Awake()
    {
        _mainCamera = Camera.main;
        _defoltWidth = _filledImage.sizeDelta.x;
    }

    public void UpdateHealth(float max, float current)
    {
        float percent = current / max;
        _filledImage.sizeDelta = new Vector2(_defoltWidth * percent, _filledImage.sizeDelta.y);
        _healthText.text = current + " / " + max;
    }


    private void LateUpdate()
    {
        if (_canvas == null || _mainCamera == null) return;
        Vector3 dir = _canvas.transform.position - _mainCamera.transform.position;
        dir.x = 0f;
        if (dir.sqrMagnitude < 0.001f) return;
        _canvas.transform.rotation = Quaternion.LookRotation(dir);
    }
}

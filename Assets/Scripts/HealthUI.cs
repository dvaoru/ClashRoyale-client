using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private RectTransform _filledImage;
    [SerializeField] private float _defoaltWidth = 0f;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TextMeshProUGUI _healthText;

    private Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main;
        _defoaltWidth = _filledImage.sizeDelta.x;
    }

    public void UpdateHealth(float max, float current)
    {
        float percent = current / max;
        _filledImage.sizeDelta = new Vector2(_defoaltWidth * percent, _filledImage.sizeDelta.y);
        _healthText.text = current + " / " + max;
    }


    private void LateUpdate()
    {
        if (_canvas == null || mainCamera == null) return;
        Vector3 dir = _canvas.transform.position - mainCamera.transform.position;
        dir.x = 0f;
        if (dir.sqrMagnitude < 0.001f) return;
        _canvas.transform.rotation = Quaternion.LookRotation(dir);
    }
}

using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private RectTransform _filledImage;
    [SerializeField] private float _defoaltWidth;
    [SerializeField] private Canvas _canvas;
    //[SerializeField] private TextMeshProUGUI _healthText;

    private Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main;
        if (_filledImage == null) _defoaltWidth = 0;
        _defoaltWidth = _filledImage.sizeDelta.x;
    }

    public void UpdateHealth(float max, int current)
    {
        if ((_defoaltWidth == 0)) return;
        float percent = current / max;
        _filledImage.sizeDelta = new Vector2(_defoaltWidth * percent, _filledImage.sizeDelta.y);
    }

    public void SetHealthState(int currentHealth, int maxHealth)
    {
        if (_canvas != null)
        {
            if (currentHealth <= 0) _canvas.enabled = false;
        }
      //  _healthText.text = currentHealth + "/" + maxHealth;
        UpdateHealth(maxHealth, currentHealth);
    }

    private void LateUpdate()
    {
        if (_canvas == null) return;
        _canvas.transform.rotation = Quaternion.LookRotation(
            _canvas.transform.position - mainCamera.transform.position
        );
    }
}

using UnityEngine;

public class TowerUI : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private RectTransform _filledHealthImage;
    [SerializeField] private float _defoltWidth = 0f;

    [SerializeField] private TMPro.TextMeshProUGUI _healthText;

    private float _maxHealth;


    private void Awake()
    {

        _defoltWidth = _filledHealthImage.sizeDelta.x;
    }

    private void Start()
    {
        _healthBar.SetActive(false);
        _maxHealth = _tower.health.max;
        _tower.health.UpdateHealth += UpdateHealth;
    }

    public void OnDestroy()
    {
        _tower.health.UpdateHealth -= UpdateHealth;
    }
    public void UpdateHealth(float current)
    {
        _healthBar.SetActive(true);
        float percent = current / _maxHealth;
        _filledHealthImage.sizeDelta = new Vector2(_defoltWidth * percent, _filledHealthImage.sizeDelta.y);
        _healthText.text = current + " / " + _maxHealth;
    }
}

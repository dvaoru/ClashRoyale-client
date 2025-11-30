using UnityEngine;

[RequireComponent(typeof(Health))]

public class Health : MonoBehaviour
{
    [field: SerializeField] public float max { get; private set; } = 10f;
    [SerializeField] private HealthUI _healthUI;
    private float current;

    private void Start()
    {
        current = max;
        UpdateUI();
    }

    public void ApplyDamage(float value)
    {
        current -= value;
        if (current < 0) current = 0;
        UpdateUI();
        if (current == 0)
        {
            MapInfo.Instance.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    private void UpdateUI()
    {
        if (_healthUI == false) return;
        _healthUI.UpdateHealth(max, current);
    }
}

interface IHealth
{
    Health health {get;}
}

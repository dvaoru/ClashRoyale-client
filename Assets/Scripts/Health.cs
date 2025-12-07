using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]

public class Health : MonoBehaviour
{
    public event Action<float> UpdateHealth;
    [field: SerializeField] public float max { get; private set; } = 10f;
    [SerializeField] private UnitUI _healthUI;
    private float current;

    private void Start()
    {
        current = max;
    }

    public void ApplyDamage(float value)
    {
        current -= value;
        if (current < 0) current = 0;
        UpdateHealth?.Invoke(current);
    }

    public void ApplayDelayDamage(float delay, float damage)
    {
        StartCoroutine(DelayDamageCoroutine(delay, damage));
    }

    private IEnumerator DelayDamageCoroutine(float delay, float damage)
    {
        yield return new WaitForSeconds(delay);
        ApplyDamage(damage);
    }

}

public interface IHealth
{
    Health health { get; }
}

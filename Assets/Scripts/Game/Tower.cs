using System;
using UnityEngine;

public class Tower : MonoBehaviour, IHealth, IDestroyed
{
    public event Action Destroyed;
    [field: SerializeField] public Health health { get; private set; }

    [field: SerializeField] public float radius { get; private set; } = 2f;



    private void Start()
    {
        health.UpdateHealth += CheckDestroy;
    }
    public float GetDistance(in Vector3 position)
    {
        return Vector3.Distance(transform.position, position) - radius;
    }

    private void CheckDestroy(float healthValue)
    {
        if (healthValue > 0) return;
        health.UpdateHealth -= CheckDestroy;
        Destroyed?.Invoke();
        Destroy(gameObject);

    }
}
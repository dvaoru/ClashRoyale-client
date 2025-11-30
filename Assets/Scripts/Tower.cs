using UnityEngine;

public class Tower : MonoBehaviour, IHealth
{
    [field: SerializeField] public Health health { get; private set; }

    [field: SerializeField] public float radius { get; private set; } = 2f;

    public float GetDistance(in Vector3 position)
    {
        return Vector3.Distance(transform.position, position) - radius;
    }
}
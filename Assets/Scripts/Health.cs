using UnityEngine;

[RequireComponent(typeof(Health))]

public class Health : MonoBehaviour
{
    [field: SerializeField] public float max { get; private set; } = 10f;
    private float current;

    private void Start()
    {
        current = max;
    }

    public void ApplyDamage(float value)
    {
        current -= value;
        if (current < 0) current = 0;
        Debug.Log("Объект " + name + "здоровье стало " + current) ;
    }
}

interface IHealth
{
    Health health {get;}
}

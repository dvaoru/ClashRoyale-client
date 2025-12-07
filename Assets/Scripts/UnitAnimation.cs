using UnityEngine;

public class UnitAnimation : MonoBehaviour
{
    private const string State = "State";
    private const string AttackSpeed = "AttackSpeed";
    [SerializeField] private Animator _animator;

    public void Init(Unit unit)
    {
        _animator.SetFloat(AttackSpeed, 1 / unit.parametrs.damageDelay);
    }

    public void SetState(UnitStateType type)
    {
        Debug.Log("Установили анимацию " + (int)type);
        _animator.SetInteger(State, (int) type);
    }
}

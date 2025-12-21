using System;
using UnityEngine;


public abstract class UnitStateAttack : UnitState
{
    [SerializeField] private float _damage = 1.5f;
    private float _delay = 1f;
    private float _time = 0;

    private float _stopAttackDistance = 0f;
    protected bool _targetIsEnemy;
    protected Unit _targetUnit;
    protected Health _target;

    public override void Constructor(Unit unit)
    {
        base.Constructor(unit);
        _targetIsEnemy = _unit.isEnemy == false;
        _delay = _unit.parametrs.damageDelay;

    }

    public override void Init()
    {
        if (TryFindTarget(out _stopAttackDistance) == false)
        {
            _unit.SetState(UnitStateType.Default);
            return;
        }

        _time = 0;
        _unit.transform.LookAt(_target.transform.position);

    }


    public override void Run()
    {

        if (_target == false)
        {
            _unit.SetState(UnitStateType.Default);
            return;
        }

        _time += Time.deltaTime;
        if (_time < _delay) return;
        _time -= _delay;


        var distanceToTarget = Vector3.Distance(_unit.transform.position, _target.transform.position);
        if (distanceToTarget > _stopAttackDistance) _unit.SetState(UnitStateType.Chase);
        //  _unit.transform.LookAt(_target.transform.position);

        Attack(_target, _damage);
    }

    protected virtual void Attack(Health target, float damage)
    {
        target.ApplyDamage(damage);
    }

    public override void Finish()
    {

    }
    protected abstract bool TryFindTarget(out float stopAttackDistance);

}

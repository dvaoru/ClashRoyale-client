using UnityEngine;

[CreateAssetMenu(fileName = "_UsualAttack", menuName = "UnitState/UsualAttack", order = 0)]
public class UsualAttack : UnitState
{
    [SerializeField] private float _damage = 1.5f;
    [SerializeField] private float _delay = 1f;
    private float _time = 0;

    private float _stopAttackDistance = 0f;
    private bool _targetIsEnemy;
    private Unit _targetUnit;
    private Health _target;

    public override void Constructor(Unit unit)
    {
        base.Constructor(unit);
        _targetIsEnemy = _unit.isEnemy == false;

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
        _time += Time.deltaTime;
        if (_time < _delay) return;
        _time -= _delay;

        if (_target == false)
        {
            _unit.SetState(UnitStateType.Default);
            return;
        }

        var distanceToTarget = Vector3.Distance(_unit.transform.position, _target.transform.position);
        if (distanceToTarget > _stopAttackDistance) _unit.SetState(UnitStateType.Chase);
      //  _unit.transform.LookAt(_target.transform.position);
        _target.ApplyDamage(_damage);

    }

    public override void Finish()
    {

    }
    private bool TryFindTarget(out float stopAttackDistance)
    {
        Vector3 unitPosition = _unit.transform.position;
        bool hasEnemy = MapInfo.Instance.TryGetNearestUnit(unitPosition, _targetIsEnemy, out Unit enemy, out float distance);
        if (hasEnemy && distance - enemy.parametrs.modelRadius < _unit.parametrs.startAttackDistance)
        {
            _target = enemy.health;
            stopAttackDistance = _unit.parametrs.stopAttackDistance + enemy.parametrs.modelRadius;
            return true;
        }

        Tower targetTower = MapInfo.Instance.GetNerestTower(unitPosition, _targetIsEnemy);
        if (targetTower.GetDistance(unitPosition) <= _unit.parametrs.startAttackDistance)
        {
            _target = targetTower.health;
            stopAttackDistance = _unit.parametrs.stopAttackDistance + targetTower.radius;
            return true;
        }

        stopAttackDistance = 0f;
        return false;
    }
}

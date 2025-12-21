using UnityEngine;

[CreateAssetMenu(fileName = "_UsualRangeAttack", menuName = "UnitState/UsualRangeAttack", order = 0)]
public class UsualRangeAttack : UnitStateAttack
{
    [SerializeField] private Arrow _arrow;
    protected override bool TryFindTarget(out float stopAttackDistance)
    {
        Vector3 unitPosition = _unit.transform.position;
        bool hasEnemy = MapInfo.Instance.TryGetNearestAnyUnit(unitPosition, _targetIsEnemy, out Unit enemy, out float distance);
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

    protected override void Attack(Health target, float damage)
    {
        var targetPosition = target.transform.position;
        var unitPosition = _unit.transform.position;
        var arrow = Instantiate(_arrow, unitPosition, Quaternion.identity);
        arrow.Init(targetPosition);
        float delay = Vector3.Distance(unitPosition, targetPosition) / arrow.speed;
        _target.ApplayDelayDamage(delay, damage);
    }
}

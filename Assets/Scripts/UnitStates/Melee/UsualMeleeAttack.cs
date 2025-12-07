using UnityEngine;

[CreateAssetMenu(fileName = "_UsualMeleeAttack", menuName = "UnitState/UsualMeleeAttack", order = 0)]
public class UsualMeleeAttack : UnitStateAttack
{
    protected override bool TryFindTarget(out float stopAttackDistance)
    {
        Vector3 unitPosition = _unit.transform.position;
        bool hasEnemy = MapInfo.Instance.TryGetNearestWalkingUnit(unitPosition, _targetIsEnemy, out Unit enemy, out float distance);
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

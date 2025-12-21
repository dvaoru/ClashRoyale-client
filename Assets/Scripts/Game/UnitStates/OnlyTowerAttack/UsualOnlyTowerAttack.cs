using UnityEngine;
[CreateAssetMenu(fileName = "_UsualOnlyTowerAttack", menuName = "UnitState/UsualOnlyTowerAttack")]


public class UsualOnlyTowerAttack : UnitStateAttack
{
    protected override bool TryFindTarget(out float stopAttackDistance)
    {
        Vector3 unitPosition = _unit.transform.position;
      
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

using UnityEngine;

[CreateAssetMenu(fileName = "_NawMeshMeleeMove", menuName = "UnitState/NawMeshMeleeMove")]
public class NawMeshMeleeMove : UnitStateNawMeshMove
{
    protected override bool TryFindTarget(out UnitStateType changeType)
    {
        if (TryAttackTower())
        {
            changeType = UnitStateType.Attack;
            return true;
        }
        if (TryChaseUnit())
        {
            changeType = UnitStateType.Chase;
            return true;
        }
        changeType = UnitStateType.None;
        return false;

    }


    private bool TryAttackTower()
    {
        var distanceToTarget = _nearestTower.GetDistance(_unit.transform.position);
        if (distanceToTarget <= _unit.parametrs.startAttackDistance)
        {
            return true;
        }
        return false;
    }

    private bool TryChaseUnit()
    {
        var hasEnemy = MapInfo.Instance.TryGetNearestWalkingUnit(_unit.transform.position, _targetIsEnemy, out Unit enemy, out float distance);
        if (hasEnemy == false) return false;
        if (_unit.parametrs.startChaseDistance >= distance)
        {
            return true;
        }
        return false;
    }


}

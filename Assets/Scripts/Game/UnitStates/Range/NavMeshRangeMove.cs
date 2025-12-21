using UnityEngine;


[CreateAssetMenu(fileName = "_NawMeshRangeMove", menuName = "UnitState/NawMeshRangeMove", order = 0)]
public class NavMeshRangeMove : UnitStateNawMeshMove
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
        var hasEnemy = MapInfo.Instance.TryGetNearestAnyUnit(_unit.transform.position, _targetIsEnemy, out Unit enemy, out float distance);
        if (hasEnemy == false) return false;
        if (_unit.parametrs.startChaseDistance >= distance)
        {
            return true;
        }
        return false;
    }


}

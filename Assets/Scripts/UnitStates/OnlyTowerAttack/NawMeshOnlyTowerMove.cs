using UnityEngine;


[CreateAssetMenu(fileName = "_NawMeshOnlyTowerMove", menuName = "UnitState/NawMeshOnlyTowerMove", order = 0)]
public class NawMeshOnlyTowerMove : UnitStateNawMeshMove
{

    protected override bool TryFindTarget(out UnitStateType changeType)
    {
        if (TryAttackTower())
        {
            changeType = UnitStateType.Attack;
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




}

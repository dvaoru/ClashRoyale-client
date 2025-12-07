using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(fileName = "_NawMeshRangeChase", menuName = "UnitState/NawMeshRangeChase", order = 1)]

public class NavMeshRangeChase : UnitStateNawMeshChase
{
    protected override void FindTarget(out Unit targetUnit, out float startAttackDistance)
    {
        var hasEnemy = MapInfo.Instance.TryGetNearestAnyUnit(_unit.transform.position, _targetIsEnemy, out targetUnit, out float distance);
        if (hasEnemy)
        {
            startAttackDistance = _unit.parametrs.startAttackDistance + _targetUnit.parametrs.modelRadius;
        }
        else
        {
            startAttackDistance = 0;
        }
    }
}

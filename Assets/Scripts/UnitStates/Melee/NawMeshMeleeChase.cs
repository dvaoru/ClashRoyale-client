using UnityEngine;

[CreateAssetMenu(fileName = "_NawMeshMeleeChase", menuName = "UnitState/NawMeshMeleeChase", order = 0)]
public class NawMeshMeleeChase : UnitStateNawMeshChase
{
    protected override void FindTarget(out Unit targetUnit, out float distance)
    {
        var hasEnemy = MapInfo.Instance.TryGetNearestWalkingUnit(_unit.transform.position, _targetIsEnemy, out targetUnit, out distance);
    }
}

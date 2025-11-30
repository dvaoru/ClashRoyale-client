using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "_NawMeshMove", menuName = "UnitState/NawMeshMove", order = 0)]
public class NavMeshMove : UnitState
{

    private NavMeshAgent _agent;
    private bool _targetIsEnemy;
    private Tower nearestTower;

    public override void Constructor(Unit unit)
    {
        base.Constructor(unit);
        _targetIsEnemy = _unit.isEnemy == false;
        _agent = _unit.GetComponent<NavMeshAgent>();
        if (_agent == null) Debug.LogError($"на персоонаже {unit.name} нет компонента NavMeshAgent");
        _agent.speed = _unit.parametrs.speed;
        _agent.radius = _unit.parametrs.modelRadius;
        _agent.stoppingDistance = _unit.parametrs.startAttackDistance;

    }

    public override void Init()
    {
        Vector3 unitPosition = _unit.transform.position;
        nearestTower = MapInfo.Instance.GetNerestTower(in unitPosition, _targetIsEnemy);
        _agent.SetDestination(nearestTower.transform.position);
    }

    public override void Finish()
    {
        _agent.SetDestination(_unit.transform.position);
    }

    public override void Run()
    {
        if (TryAttackTower()) return;
        if (TryAttackUnit()) return;
    }



    private bool TryAttackTower()
    {
        var distanceToTarget = nearestTower.GetDistance(_unit.transform.position);
        if (distanceToTarget <= _unit.parametrs.startAttackDistance)
        {
            _unit.SetState(UnitStateType.Attack);
            return true;
        }
        return false;
    }

    private bool TryAttackUnit()
    {
        var hasEnemy = MapInfo.Instance.TryGetNearestUnit(_unit.transform.position,_targetIsEnemy,  out Unit enemy, out float distance);
        if (hasEnemy == false) return false;
        if (_unit.parametrs.startChaseDistance >= distance)
        {
            Debug.Log("Chase");
            _unit.SetState(UnitStateType.Chase);
            return true;
        }
        return false;
    }
}

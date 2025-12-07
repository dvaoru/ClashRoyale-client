using UnityEngine;
using UnityEngine.AI;

public abstract class UnitStateNawMeshMove : UnitState
{

    private NavMeshAgent _agent;
    protected bool _targetIsEnemy;
    protected Tower _nearestTower;

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
        _nearestTower = MapInfo.Instance.GetNerestTower(in unitPosition, _targetIsEnemy);
        _agent.SetDestination(_nearestTower.transform.position);
    }

    public override void Finish()
    {
        _agent.SetDestination(_unit.transform.position);
    }

    public override void Run()
    {
        if (TryFindTarget(out UnitStateType changeStateType))
        {
            _unit.SetState(changeStateType);
        }
    }

    protected abstract bool TryFindTarget(out UnitStateType changeStateType);


}

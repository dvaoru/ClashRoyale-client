using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "_NawMeshChase", menuName = "UnitState/NawMeshChase", order = 1)]
public class NavMeshChase : UnitState
{

    private NavMeshAgent _agent;
    private bool _targetIsEnemy;
    private Unit _targetUnit;

    private float _startAttackDistance = 0f; 

    public override void Constructor(Unit unit)
    {
        base.Constructor(unit);
        _targetIsEnemy = _unit.isEnemy == false;
        _agent = _unit.GetComponent<NavMeshAgent>();
        if (_agent == null) Debug.LogError($"на персоонаже {unit.name} нет компонента NavMeshAgent");
    }


    public override void Init()
    {
        var hasEnemy = MapInfo.Instance.TryGetNearestUnit(_unit.transform.position,  _targetIsEnemy, out _targetUnit, out float distance);
        if (hasEnemy)
        {
            _startAttackDistance = _unit.parametrs.startAttackDistance + _targetUnit.parametrs.modelRadius;
        }
    }

    public override void Run()
    {
        if (_targetUnit == null)
        {
            _unit.SetState(UnitStateType.Default);
            return;
        }

        var distanceToTarget = Vector3.Distance(_unit.transform.position, _targetUnit.transform.position);

        if (distanceToTarget > _unit.parametrs.stopChaseDistance) _unit.SetState(UnitStateType.Default);
        if (distanceToTarget <= _startAttackDistance) _unit.SetState(UnitStateType.Attack);
        else
        {
            Debug.Log("Преследует " + _targetUnit.transform.position);
            _agent.SetDestination(_targetUnit.transform.position);
        }
    }

    public override void Finish()
    {
        _agent.SetDestination(_unit.transform.position);
    }

#if UNITY_EDITOR
    public override void DebugDrawDistance(Unit unit)
    {
        base.DebugDrawDistance(unit);
        Handles.color = Color.red;
        Handles.DrawWireDisc(unit.transform.position, Vector3.up, unit.parametrs.startChaseDistance);
        Handles.color = Color.blue;
        Handles.DrawWireDisc(unit.transform.position, Vector3.up, unit.parametrs.stopChaseDistance);
    }
#endif

}

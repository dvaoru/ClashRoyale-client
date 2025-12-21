using System;
using UnityEngine;

[RequireComponent(typeof(UnitParametrs), typeof(Health), typeof(UnitAnimation))]
public class Unit : MonoBehaviour, IHealth, IDestroyed
{

    public event Action Destroyed;


    [field: SerializeField] public Health health { get; private set; }

    [field: SerializeField] public bool isEnemy { get; private set; } = false;
    [field: SerializeField] public UnitParametrs parametrs;
    [SerializeField] private UnitAnimation _unitAnimation;

    [SerializeField] private UnitState _defaultStateSO;
    [SerializeField] private UnitState _chaseStateSO;
    [SerializeField] private UnitState _attackStateSO;

    private UnitState _currentState;


    private UnitState _defaultState;
    private UnitState _chaseState;
    private UnitState _attackState;

    private void Start()
    {
        _unitAnimation.Init(this);
        CreateStates();

        _currentState = _defaultState;
        _currentState.Init();

        health.UpdateHealth += CheckDestroy;

    }

    private void Update()
    {
        _currentState.Run();
    }

    private void CreateStates()
    {
        _defaultState = Instantiate(_defaultStateSO);
        _defaultState.Constructor(this);

        _chaseState = Instantiate(_chaseStateSO);
        _chaseState.Constructor(this);

        _attackState = Instantiate(_attackStateSO);
        _attackState.Constructor(this);
    }

    public void SetState(UnitStateType type)
    {
        _currentState.Finish();
        switch (type)
        {
            case UnitStateType.Default:
                _currentState = _defaultState;
                break;
            case UnitStateType.Chase:
                _currentState = _chaseState;
                break;
            case UnitStateType.Attack:
                _currentState = _attackState;
                break;
            default:
                Debug.LogError("Не обрабатывается состояние " + type);
                break;
        }
        _currentState.Init();
        _unitAnimation.SetState(type);
    }

    private void CheckDestroy(float healthValue)
    {
        if (healthValue > 0) return;
        health.UpdateHealth -= CheckDestroy;
        Destroyed?.Invoke();
        Destroy(gameObject);

    }

#if UNITY_EDITOR
    [Space(24)]
    [SerializeField] private bool _drowDebug = false;


    private void OnDrawGizmos()
    {
        if (_drowDebug == false) return;
        if (_chaseStateSO != null) _chaseStateSO.DebugDrawDistance(this);
    }
#endif
}

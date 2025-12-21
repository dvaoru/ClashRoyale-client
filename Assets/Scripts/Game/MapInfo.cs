using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
    #region OneSceneSingelton
    public static MapInfo Instance;
    public void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    #endregion
    [SerializeField] private List<Tower> _enemyTowers = new List<Tower>();
    [SerializeField] private List<Tower> _playerTowers = new List<Tower>();

    [SerializeField] private List<Unit> _enemyWalkingUnits = new List<Unit>();
    [SerializeField] private List<Unit> _playerWalkingUnits = new List<Unit>();

    [SerializeField] private List<Unit> _enemyFlyingUnits = new List<Unit>();
    [SerializeField] private List<Unit> _playerFlyingUnits = new List<Unit>();

    private void Start()
    {
        SubscribeForDestroy(_enemyTowers);
        SubscribeForDestroy(_playerTowers);
        SubscribeForDestroy(_enemyWalkingUnits);
        SubscribeForDestroy(_playerWalkingUnits);
    }

    private void AddUnit(Unit unit)
    {
        List<Unit> list;
        if (unit.isEnemy) list = unit.parametrs.canFly ? _enemyFlyingUnits : _enemyWalkingUnits;
        else list = unit.parametrs.canFly ? _playerFlyingUnits : _playerWalkingUnits;

        AddObjToList(list, unit);
    }
    public bool TryGetNearestAnyUnit(in Vector3 currentPosition, bool isEnemy, out Unit unit, out float distance)
    {
        TryGetNearestWalkingUnit(in currentPosition, isEnemy, out Unit walkingUnit, out float walkingDistance);
        TryGetNearestFlyingUnit(in currentPosition, isEnemy, out Unit flyingUnit, out float flyingDistance);
        if (walkingDistance <= flyingDistance)
        {
            unit = walkingUnit;
            distance = walkingDistance;
        }
        else
        {
            unit = flyingUnit;
            distance = flyingDistance;
        }
        return unit != null;
    }

    public bool TryGetNearestWalkingUnit(in Vector3 currentPosition, bool isEnemy, out Unit unit, out float distance)
    {
        var units = isEnemy ? _enemyWalkingUnits : _playerWalkingUnits;
        unit = GetNearest<Unit>(currentPosition, units, out distance);
        return unit != null;
    }

    public bool TryGetNearestFlyingUnit(in Vector3 currentPosition, bool isEnemy, out Unit unit, out float distance)
    {
        var units = isEnemy ? _enemyFlyingUnits : _playerFlyingUnits;
        unit = GetNearest<Unit>(currentPosition, units, out distance);
        return unit != null;
    }

    public Tower GetNerestTower(in Vector3 currentPosition, bool isEnemy)
    {
        var towers = isEnemy ? _enemyTowers : _playerTowers;

        return GetNearest<Tower>(currentPosition, towers, out float distance);
    }


    private void SubscribeForDestroy<T>(List<T> list) where T : IDestroyed
    {
        for (int i = 0; i < list.Count; i++)
        {
            T obj = list[i];

            list[i].Destroyed += RemoveAndUnsubscribe;

            void RemoveAndUnsubscribe()
            {
                RemoveObjFromList<T>(list, obj);
                obj.Destroyed -= RemoveAndUnsubscribe;

            }
        }
    }

    public void AddObjToList<T>(List<T> list, T obj) where T : IDestroyed
    {
        if (list.Contains(obj)) return;
        list.Add(obj);
        obj.Destroyed += () => RemoveAndUnsubscribe();

        void RemoveAndUnsubscribe()
        {
            RemoveObjFromList<T>(list, obj);
            obj.Destroyed -= RemoveAndUnsubscribe;
        }
    }
    public void RemoveObjFromList<T>(List<T> list, T obj)
    {
        if (list.Contains(obj)) list.Remove(obj);
    }

    private T GetNearest<T>(in Vector3 position, List<T> objects, out float distance) where T : MonoBehaviour
    {
        distance = float.MaxValue;
        if (objects.Count <= 0) return null;
        var result = objects[0];
        distance = Vector3.Distance(position, objects[0].transform.position);
        for (int i = 1; i < objects.Count; i++)
        {
            var tempDistance = Vector3.Distance(position, objects[i].transform.position);
            if (tempDistance > distance) continue;
            distance = tempDistance;
            result = objects[i];
        }
        return result;
    }
}

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

    [SerializeField] private List<Unit> _enemyUnits = new List<Unit>();
    [SerializeField] private List<Unit> _playerUnits = new List<Unit>();

    public bool TryGetNearestUnit(in Vector3 currentPosition, bool isEnemy, out Unit unit, out float distance)
    {
        var units = isEnemy ? _enemyUnits : _playerUnits;
        unit = GetNearest<Unit>(currentPosition, units, out distance);
        return unit != null;
    }

    public Tower GetNerestTower(in Vector3 currentPosition, bool isEnemy)
    {
        var towers = isEnemy ? _enemyTowers : _playerTowers;

        return GetNearest<Tower>(currentPosition, towers, out float distance);
    }

    public void Remove(GameObject obj) 
    {
        if (obj.TryGetComponent<Tower>(out Tower rmTower))            
            _enemyTowers.Remove(rmTower);
        else if (obj.TryGetComponent<Unit>(out Unit rmUnit))
            _enemyUnits.Remove(rmUnit);
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

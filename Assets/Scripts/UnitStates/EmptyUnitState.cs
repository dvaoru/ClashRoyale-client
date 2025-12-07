using UnityEngine;

[CreateAssetMenu(fileName = "_EmptyUnitSytate", menuName = "UnitState/EmptyUnitSytate")]

public class EmptyUnitSytate : UnitState
{


    public override void Init()
    {
        _unit.SetState(UnitStateType.Default);
    }

    public override void Run()
    {

    }
    public override void Finish()
    {
        Debug.LogWarning($"Юнит {_unit.name} был в пустом состоянии, его перекинуло в дефолтное");
    }
}

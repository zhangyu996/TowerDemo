using System;
using UnityEngine;

internal class TowerIdleState : IState
{
    public Fsm Owner { get; set; }
    public string Name { get ; set ; }

    UnitDelegateData unitDelegate;
    Unit _nearUnit;
    float range = 15f;
    public bool CanChange(IState nextState)
    {
        return true;
    }

    public void Dispose()
    {
    }

    public void Enter(IState preState, object[] data)
    {
        _nearUnit = null;
        unitDelegate = new UnitDelegateData();
        unitDelegate.UnitDel = DelegateFunc;
        Game.UnitManagerCompoment.AddForeachCharacterWithStandPoint(unitDelegate);
    }

    public void Exit(IState nextState)
    {
        Game.UnitManagerCompoment.DelForeachCharacterWithStandPoint(unitDelegate);
    }


    private void DelegateFunc(Unit unit)
    {
        float dif = VectorTool.IsDisLong(unit.GetPos(), Owner.Owner.GetPos(), range, out Vector3 dir);
        if(dif >0 && dif<range)
        {
            if(_nearUnit == null)
            {
                _nearUnit = unit;
            }
        }
    }

   
    public void Update(float deltaTime)
    {
        if(_nearUnit != null)
        {
            object[] obj = { _nearUnit };
            Owner.Owner.AI.ExeCommand(UnitCommand.UC_Attack, obj);
        }
    }
}
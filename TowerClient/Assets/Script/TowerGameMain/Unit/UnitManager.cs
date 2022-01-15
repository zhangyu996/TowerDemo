using System;
using System.Collections.Generic;
using UnityEngine;
public delegate void UnitDelegate(Unit unit);

public class UnitDelegateData
{
    public UnitDelegate UnitDel = null;
    public CampType UnitCamp = CampType.CT_Enemy;
}
public class UnitManager
{
    public Dictionary<CampType, List<Unit>> _troops = new Dictionary<CampType, List<Unit>>(100);
    public List<Unit> _troopsGuid = new List<Unit>(100);
    public List<Unit> _disposeTroops = new List<Unit>(10);

    public List<UnitDelegateData> _unitDelegateList = new List<UnitDelegateData>(100);
    public List<UnitDelegateData> _unitDelRemove = new List<UnitDelegateData>(100);


    public static EnemyUnit CreateEnemy()
    {
        EnemyUnit unit = new EnemyUnit();
        unit.Module = new UnitModule();
        unit.State = UnitState.Normal;
        unit.CampType = CampType.CT_Enemy;
        unit.Parent = GameObject.Find("Start").transform;
        unit.Module.assetName = "Model/Enemy/enemy_ufoGreen";
        unit.Size = Vector3.one * 4;
        unit.Init((go) =>
        {
            Debug.Log("init Enemy success");
        });
        return unit;
    }


    public static TowerUnit CreateTower(Vector3 position)
    {
        TowerUnit unit = new TowerUnit();
        unit.Module = new UnitModule();
        unit.AI = new UnitAI(unit);
        unit.AI.Register("UC_Idle", new TowerIdleState());
        unit.AI.Register("UC_Attack", new TowerAttackState());
        unit.State = UnitState.Normal;
        unit.CampType = CampType.CT_Friend;
        unit.Parent = GameObject.Find("TowerRoot").transform;
        unit.Module.assetName = "Model/Weapon/weapon_cannon";
        unit.Size = Vector3.one * 15;
        unit.Init((go) =>
        {
            Debug.Log("Init Tower success");
            unit.SetPos(position);
            unit.AI.ExeCommand(UnitCommand.UC_Idle);
        });
        return unit;
    }

    public static BulletUnit CreateBullet(Vector3 position)
    {
        BulletUnit unit = new BulletUnit();
        unit.Module = new UnitModule();
        unit.State = UnitState.Normal;
        unit.CampType = CampType.CT_Friend;
        unit.Parent = GameObject.Find("BulletRoot").transform;
        unit.Module.assetName = "Model/Bullet/Bullet_Sphere";
        unit.Size = Vector3.one;
        unit.Init((go)=>
        {
            unit.SetPos(position);
        });
        return unit;
    }



    public void AddUnit(Unit unit)
    {
        if (unit == null)
            return;
        List<Unit> troop = null;
        _troops.TryGetValue(unit.CampType, out troop);
        if (troop == null)
        {
            troop = new List<Unit>();
            _troops.Add(unit.CampType, troop);
        }
        troop.Add(unit);
        _troopsGuid.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        if (unit == null)
            return;
        if (_troops.TryGetValue(unit.CampType, out List<Unit> troop))
        {
            troop.Remove(unit);
        }
        if (_troopsGuid.Contains(unit))
        {
            _troopsGuid.Remove(unit);
        }
    }

    public List<Unit> GetTroops(CampType campType)
    {
        if(_troops.ContainsKey(campType))
        {
            return _troops[campType];
        }
        return null;
    }

    public void Update(float deltaTime)
    {
        for (int i = 0; i < _troopsGuid.Count; i++)
        {
            Unit unit = _troopsGuid[i];
            unit.Update(deltaTime);
            if (unit.State == UnitState.Dispose)
            {
                _disposeTroops = new List<Unit>(10);
                _disposeTroops.Add(unit);
            }
        }

        if (_disposeTroops != null)
        {
            for (int i = 0; i < _disposeTroops.Count; i++)
            {
                Unit unit = _disposeTroops[i];
                unit.DisPose();
                RemoveUnit(unit);
                //_disposeTroops.Remove(unit);
            }
            _disposeTroops.Clear();
            _disposeTroops = null;
        }

        int delCount = _unitDelRemove.Count;
        if (delCount > 0)
        {
            for (int i = 0; i < delCount; i++)
            {
                UnitDelegateData delegateData = _unitDelRemove[i];
                if(delegateData != null)
                {
                    _unitDelegateList.Remove(delegateData);
                }
            }
            _unitDelRemove.Clear();
        }

        if (_unitDelegateList.Count > 0)
        {
            List<Unit> units = GetTroops(CampType.CT_Enemy);
            if(units != null)
            {
                int len = units.Count;
                for(int i=0;i< len; i++)
                {
                    Unit unit = units[i];
                    if (unit != null)
                    {
                        for (int j = 0; j < _unitDelegateList.Count; ++j)
                        {
                            UnitDelegateData deleageData = _unitDelegateList[j];
                            if (deleageData != null && deleageData.UnitDel != null)
                            {
                                deleageData.UnitDel(unit);
                            }
                        }
                    }
                }
            }
        }

    }


    public void AddForeachCharacterWithStandPoint(UnitDelegateData unitDelegateData)
    {
        if (_unitDelegateList.Contains(unitDelegateData) == false)
        {
            _unitDelegateList.Add(unitDelegateData);
        }
    }

    public void DelForeachCharacterWithStandPoint(UnitDelegateData unitDelegateData)
    {
        if (_unitDelRemove.Contains(unitDelegateData) == false)
        {
            _unitDelRemove.Add(unitDelegateData);
        }
    }
}
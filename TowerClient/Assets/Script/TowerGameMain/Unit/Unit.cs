using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitState
{
    Normal,
    Die,
    Dispose,
}

public enum CampType
{
    CT_Unknown = 0,

    /// <summary>
    /// 玩家阵营。
    /// </summary>
    CT_Self
           ,
    CT_Friend
           ,
    /// <summary>
    /// 敌人阵营。
    /// </summary>
    CT_Enemy,

    /// <summary>
    /// 中立阵营。
    /// </summary>
    CT_Neutral,
}
public abstract class Unit
{
    protected UnitState _state = UnitState.Normal;
    private CampType _campType = CampType.CT_Unknown;
    protected Quaternion _curDir; //
    protected Vector3 _curPos;
    protected Vector3 _size = Vector3.one;

    public Vector3 MoveDir { get; set; }
    public UnitModule Module { get; set; }
    public Transform Parent { get; set; }
    public UnitAI AI
    {
        get; set;
    }

    public UnitState State { get => _state; set => _state = value; }
    public CampType CampType { get => _campType; set => _campType = value; }
    public Vector3 Size { get => _size; set => _size = value; }


    public virtual void Init(Action<GameObject> callBack = null)
    {
        if(Module != null)
        {
            if(Module.go == null)
            {
                Module.load(obj =>
                {
                    obj.transform.SetParent(Parent);
                    obj.transform.localScale = Size;
                    obj.transform.localPosition = Vector3.zero;
                    Module.go = obj;
                    if(callBack != null)
                    {
                        callBack(Module.go);
                    }
                });
                
            }
            else
            {
                Module.go.transform.SetParent(Parent);
                if(callBack != null)
                {
                    callBack(Module.go);
                }
            }
        }
    }


    public void SetPos(Vector3 position)
    {
        if(Module != null)
        {
            if(Module.go != null)
            {
                _curPos = position;
                Module.go.transform.position = position;
            }
        }
    }

    public Vector3 CurPos { get => _curPos; set => _curPos = value; }

    public Vector3 GetPos()
    {
        return _curPos;
    }

    public void SetDirection(Quaternion rotate)
    {
        if (Module != null)
        {
            if (Module.go != null)
            {
                Module.go.transform.rotation = rotate;
                _curDir = rotate;
            }
        }
    }

    public Quaternion GetDirection()
    {
        return _curDir;
    }

    public virtual void Die()
    {
        _state = UnitState.Die;
    }

    public virtual void DisPose()
    {
        _state = UnitState.Dispose;
        if(Module != null)
        {
            Module.Dispose();
        }
    }

    public virtual void Update(float detalTime) 
    { 
        if(isDie() == false)
        {
            if(AI != null)
            {
                AI.Update(detalTime);
            }
        }
    }

    public virtual void Hit(Unit unit)
    {
        
    }
    public bool isDie()
    {
        if(_state == UnitState.Die || _state == UnitState.Dispose)
        {
            return true;
        }
        return false;
    }
}



public class UnitAI
{
    private Fsm _fsm;
    public bool IsActive
    {
        set; get;
    }
    public UnitAI(Unit owner)
    {
        _fsm = new Fsm();
        _fsm.Owner = owner;
        IsActive = true;
    }

    public virtual void Register(string stateName,IState state)
    {
        if(_fsm != null)
        {
            _fsm.Register(stateName, state);
        }
    }

    public virtual void ExeCommand(UnitCommand command,object[] data = null)
    {
        string stateName = command.ToString();
        if(_fsm != null)
        {
            _fsm.ChangeState(stateName, data);
        }
    }

    public virtual void Update(float detalTime)
    {
       if(_fsm != null)
        {
            _fsm.Update(detalTime);
        }
    }


    public virtual void Dispose()
    {
        if(_fsm != null)
        {
            _fsm.Dispose();
        }
    }

}

public enum UnitCommand
{
    UC_Idle = 100,
    UC_Attack,
    UC_Die,
}
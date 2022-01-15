using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fsm
{
    public Unit Owner
    {
        get;
        set;
    }

    public Dictionary<string, IState> _states;
    public IState CurrentState;
    public string _enterPoint;

    public Fsm()
    {
        _states = new Dictionary<string, IState>();
    }


    public void Register(string stateName,IState state)
    {
        if (_enterPoint == null) _enterPoint = stateName;
        if(_states.ContainsKey(stateName) == false)
        {
            state.Owner = this;
            _states.Add(stateName, state);
        }
    }

    public bool ChangeState(string stateName,object[] data = null)
    {
        IState newState = GetState(stateName);
        if(newState != null)
        {
            if(newState != CurrentState)
            {
                if(CurrentState != null)
                {
                    CurrentState.Exit(newState);
                }
                newState.Enter(CurrentState,data);
                CurrentState = newState;
            }
            else
            {
                CurrentState.Enter(CurrentState, data);
            }
            return true;
        }
        return false;
    }

    public void Update(float deltaTime)
    {
        if(_states.Count > 0)
        {
            if (CurrentState == null)
            {
                CurrentState = GetState(_enterPoint);
            }
            CurrentState.Update(deltaTime);
        }
    }

    private IState GetState(string stateName)
    {
        if(_states.ContainsKey(stateName))
        {
            return _states[stateName];
        }
        return null;
    }

   public void Dispose()
    {
        foreach (KeyValuePair<string, IState> value in _states)
        {
            if (value.Value != null)
                value.Value.Dispose();
        }
        _states.Clear();
    }
}

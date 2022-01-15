using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{

    Fsm Owner
    {
        get;set;
    }

    string Name
    {
        get;set;
    }


    void Enter(IState preState,object[] data);
    void Update(float deltaTime);
    void Exit(IState nextState);
    bool CanChange(IState nextState);

    void Dispose();

}

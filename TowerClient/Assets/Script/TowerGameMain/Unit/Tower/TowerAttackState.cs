using UnityEngine;

internal class TowerAttackState : IState
{
    public Fsm Owner { get; set; }
    public string Name { get ; set ; }

    public Unit TargetUnit { get; set; }
    public float attackSpeed = 0.5f;
    public float overTime = 0f;
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
        TargetUnit = data[0] as Unit;
        if(TargetUnit == null)
        {
            Owner.Owner.AI.ExeCommand(UnitCommand.UC_Idle);
        }
        overTime = 0f;
    }

    public void Exit(IState nextState)
    {
    }

    public void Update(float deltaTime)
    {
        if(TargetUnit != null && TargetUnit.isDie() == false)
        {
            float dif = VectorTool.IsDisLong(TargetUnit.GetPos(), Owner.Owner.GetPos(), range, out Vector3 dir);
            if (dif > 0 && dif < range)
            {
                Owner.Owner.SetDirection(Quaternion.LookRotation(dir, Vector3.up));
                overTime += deltaTime;
                if(overTime >= attackSpeed)
                {
                    overTime = 0f;
                    BulletUnit unit = UnitManager.CreateBullet(Owner.Owner.GetPos());
                    Game.UnitManagerCompoment.AddUnit(unit);
                    unit.Target = TargetUnit;
                    TargetUnit.Hit(unit);
                }
            }else
            {
                Owner.Owner.AI.ExeCommand(UnitCommand.UC_Idle);
            }
        }
        else
        {
            Owner.Owner.AI.ExeCommand(UnitCommand.UC_Idle);
        }
    }
}
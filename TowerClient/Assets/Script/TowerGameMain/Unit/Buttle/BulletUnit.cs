using UnityEngine;

public class BulletUnit :Unit
{
    public Unit Target { get; set; }
    public float _speed = 30f;

    public override void Update(float detalTime)
    {
        base.Update(detalTime);
        if(Target != null && Target.isDie() == false)
        {
            Vector3 curPos = GetPos();
            Vector3 tarPos = Target.GetPos();
            float v = VectorTool.IsDisLong(tarPos,curPos, 0.1f, out Vector3 dir);
            if(v > 0 && v< 0.1f)
            {
                State = UnitState.Dispose;
                //Target.Hit(null);
            }
            else
            {
                curPos += dir.normalized * _speed * detalTime;
                SetPos(curPos);
            }
        }else
        {
            State = UnitState.Dispose;
        }
    }

   
}
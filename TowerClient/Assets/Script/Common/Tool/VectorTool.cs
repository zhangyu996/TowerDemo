using System.Collections;
using UnityEngine;

public class VectorTool
{
    public static float IsDisLong(Vector3 v1, Vector3 v2, float dis, out Vector3 dif)
    {
        dif = v1 - v2;
        if(dif.magnitude < dis)
        {
            return dif.magnitude;
        }
        return -1;
    }

}

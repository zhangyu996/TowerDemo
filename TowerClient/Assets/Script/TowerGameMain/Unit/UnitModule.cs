using System;
using UnityEngine;

public class UnitModule
{
    public GameObject go;
    public string assetName;

    public void load(Action<GameObject> loadCaallBack)
    {
        if(go == null)
        {
            go = GameObjectPool.Spawn(assetName);
        }
        if(loadCaallBack != null)
        {
            loadCaallBack(go);
        }
    }

    public void Dispose()
    {
        GameObjectPool.UnSpawn(assetName, go);
        go = null;
        assetName = null;
    }
   
}
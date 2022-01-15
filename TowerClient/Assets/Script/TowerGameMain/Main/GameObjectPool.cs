using System.Collections;
using UnityEngine;


public class GameObjectPool : MonoBehaviour
{

    public static GameObject Spawn(string assetName)
    {
        return Instantiate(Resources.Load(assetName)) as GameObject;
    }

    public static void UnSpawn(string assetName, GameObject go)
    {
        GameObject.Destroy(go);
    }


}


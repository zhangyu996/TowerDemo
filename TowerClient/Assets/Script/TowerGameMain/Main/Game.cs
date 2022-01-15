using System.Collections;
using UnityEngine;


public class Game : MonoBehaviour
{
    public static UnitManager UnitManagerCompoment;
    public static GameController Controller;
    private void Awake()
    {
        UnitManagerCompoment = new UnitManager();
        Controller = GameController.Instance;
    }

    private void Start()
    {
        Transform cubeRoot = GameObject.Find("CubeRoot").transform;
        Controller.InitCubes(cubeRoot);
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        if (Controller != null)
        {
            Controller.Update(deltaTime);
        }

        if (UnitManagerCompoment != null)
        {
            UnitManagerCompoment.Update(deltaTime);
        }
    }


}

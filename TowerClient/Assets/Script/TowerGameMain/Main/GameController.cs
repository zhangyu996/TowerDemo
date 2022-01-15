using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : Singleton<GameController>
{
    public Vector3 touchPos;
    public Ray ray;
    public Dictionary<int, CubeData> cubeDictionary = new Dictionary<int, CubeData>(200);


    public void Update(float deltaTime)
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Unit unit = UnitManager.CreateEnemy();
            Game.UnitManagerCompoment.AddUnit(unit);
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            List<Unit> units = Game.UnitManagerCompoment.GetTroops(CampType.CT_Enemy);
            if(units != null && units.Count > 0)
            {
                foreach(Unit unit in units)
                {
                    unit.State = UnitState.Dispose;
                }
            }


        }

        if (Input.GetMouseButtonUp(0))
        {
            touchPos = Input.mousePosition;
            ray = Camera.main.ScreenPointToRay(touchPos);
            if(Physics.Raycast(ray, out RaycastHit hitInfo,Mathf.Infinity,LayeMask.Cube))
            {
                Transform cube = hitInfo.collider.gameObject.transform;
                if(IsUsed(cube) == false)
                {
                    // 放置炮塔
                    SetCubeStatu(cube.GetSiblingIndex(), true);
                    Vector3 pos = cube.transform.position;
                    pos.y = 1;
                    Unit unit = UnitManager.CreateTower(pos);
                    Game.UnitManagerCompoment.AddUnit(unit);
                }
            }
        }
    }


    public bool IsUsed(Transform cube)
    {
        if (cubeDictionary == null || cubeDictionary.Count <= 0) return true;
        if(cubeDictionary.ContainsKey(cube.GetSiblingIndex()))
        {
            CubeData data = cubeDictionary[cube.GetSiblingIndex()];
            return data.isUsed;
        }else
        {
            return true;
        }
    }

    public void SetCubeStatu(int index,bool isUsed)
    {
        if(cubeDictionary.ContainsKey(index))
        {
            var data = cubeDictionary[index];
            data.isUsed = isUsed;
            cubeDictionary[index] = data;
        }
    }


    public struct CubeData
    {
        public int index;
        public bool isUsed;

        public CubeData(int index,bool isUsed)
        {
            this.index = index;
            this.isUsed = isUsed;
        }
    }

    public void InitCubes(Transform cubeRoot)
    {
        if (cubeRoot == null) return;
        for(int i=0; i<cubeRoot.childCount;i++)
        {
            AddCube(i);
        }
    }

    public void AddCube(int index)
    {
        if(cubeDictionary.ContainsKey(index) == false)
        {
            cubeDictionary.Add(index, new CubeData(index, false));
        }
    }

}

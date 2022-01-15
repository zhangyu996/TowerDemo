using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public float _moveSpeed = 5.0f;
    public Vector3 _dir;
    public bool isJump;
    public Transform cacheTrans;

    private void Awake()
    {
        cacheTrans = GetComponent<Transform>();
        _dir = Vector3.zero;
        isJump = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _dir.x = Input.GetAxis("Horizontal");
        _dir.z = Input.GetAxis("Vertical");
        _dir.y = 0;
        _dir= _dir.normalized;
        if (_dir == Vector3.zero) return;
        Vector3 pos = cacheTrans.position;
        Vector3 dif = _dir * _moveSpeed * Time.deltaTime;
        //SetDirection(Quaternion.LookRotation(_dir, Vector3.up));
        if(ColliderWall())
        {

        }else
        {
            pos = pos + dif;
            Vector3 ray = new Vector3(pos.x, pos.y + 1, pos.z);
            if (Physics.Raycast(ray, Vector3.down, out RaycastHit rayHit, Mathf.Infinity, LayeMask.Ground))
            {
                pos.y = rayHit.point.y;
            }
            SetPos(pos);
        }
    }

    // 墙壁监测
    private bool ColliderWall()
    {
        return false;
    }

    private void SetPos(Vector3 pos)
    {
        cacheTrans.position = pos;
    }

    private void SetDirection(Quaternion dir)
    {
        cacheTrans.rotation = dir;
    }
}

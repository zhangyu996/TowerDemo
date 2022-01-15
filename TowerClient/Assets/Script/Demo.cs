using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    Transform CacheTransform;
    Transform UnitA;
    float radius = 2f;
    // Start is called before the first frame update
    private void Awake()
    {
        UnitA = GameObject.Find("UnitA").GetComponent<Transform>();
    }

    void Start()
    {
        //float f1 = Vector3.Dot(Vector3.forward, Vector3.right);
        //float angle = Mathf.Acos(f1) * Mathf.Rad2Deg;
        //Debug.Log(angle);
        CacheTransform = transform.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        float dif = Vector3.Dot(Vector3.forward.normalized, (UnitA.position - CacheTransform.position).normalized);
        float angle = Mathf.Acos(dif) * Mathf.Rad2Deg;
        if(angle <= 60)
        {
            Debug.DrawLine(CacheTransform.position, UnitA.position, Color.red);
        }
    }
}

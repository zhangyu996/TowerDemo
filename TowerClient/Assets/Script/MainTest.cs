using UnityEngine;


public class MainTest
{
    static void Main(string[] args)
    {
        Vector3 forward = Vector3.forward;
        Vector3 v1 = Vector3.left;
        float angle = Mathf.Acos(Vector3.Dot(forward, v1)) * Mathf.Rad2Deg;
        Debug.Log(angle);
    }
}

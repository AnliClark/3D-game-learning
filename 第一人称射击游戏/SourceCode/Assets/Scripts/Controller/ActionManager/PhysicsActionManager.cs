using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PhysicsActionManager : IActionManager
{
    private static PhysicsActionManager _instance;
    public static PhysicsActionManager GetInstance()
    {
        if(_instance == null)
        {
            _instance = new PhysicsActionManager();
        }
        return _instance;
    }
    public void PlayObject(GameObject obj, Vector3 v)
    {

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        //rb.isKinematic = false;  // 禁用运动学模式
        rb.velocity = v;
        //rb.AddTorque(new Vector3(0, 200f * obj.GetComponent<DiskData>().speed, 0));
    }

}
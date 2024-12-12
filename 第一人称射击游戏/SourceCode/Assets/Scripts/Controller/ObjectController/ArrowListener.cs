using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowListener : MonoBehaviour
{
    private ObjectFactory _factory;
    public bool hasCollide;
    // Start is called before the first frame update
    void Start()
    {
        _factory = ObjectFactory.GetInstance();
        hasCollide = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DestroyArrow()
    {
        GetComponentInChildren<TrailRenderer>().Clear();
        _factory.FreeObject(gameObject, "arrow");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasCollide)
        {
            Rigidbody rb = GetComponentInChildren<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            hasCollide = true;
            Invoke("DestroyArrow", 0.5f);
        }
    }
}

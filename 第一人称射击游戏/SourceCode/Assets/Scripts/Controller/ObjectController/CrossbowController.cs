using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowController : MonoBehaviour
{
    private Camera m_Camera;
    private FirstSceneController controller;
    // Start is called before the first frame update
    void Start()
    {
        m_Camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        controller = SSDirector.getInstance().currentSceneController as FirstSceneController;
    }

    void HasShot()
    {
        controller.ArrowShoot();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = m_Camera.transform.rotation;
    }
}

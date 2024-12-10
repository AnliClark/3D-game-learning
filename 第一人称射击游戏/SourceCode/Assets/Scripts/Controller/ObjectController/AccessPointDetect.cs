using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessPointDetect : MonoBehaviour
{
    public int num;
    private bool hasCollided = false;
    private FirstSceneController sceneController;

    void Start()
    {
        
        sceneController = SSDirector.getInstance().currentSceneController as FirstSceneController;
        num = sceneController.firePointNum++;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !hasCollided)
        {
            hasCollided = true;
            sceneController.AccessFirePoint(num);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hasCollided = false;
            sceneController.ExitFirePoint();
        }
    }
}

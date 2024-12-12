using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInteraction : MonoBehaviour
{
    private FirstSceneController controller;
    private IUserAction userAction;
    private Camera cam;
    private Transform cameraTrans;
    private int cameraState;
    private bool enableLeft;
    private bool enableRight;
    // Start is called before the first frame update
    void Start()
    {
        userAction = SSDirector.getInstance().currentSceneController as IUserAction;
        controller = SSDirector.getInstance().currentSceneController as FirstSceneController;
        cam = GameObject.FindGameObjectWithTag("thirdCamera").GetComponent<Camera>();

        cameraTrans = GameObject.Find("PlayerThirdCameraRoot").transform;
        cameraState = 0;

        enableLeft = false;
        enableRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        // 鼠标右键
        if (controller.enableFire)
        {
            if (Input.GetMouseButtonDown(1))
            {
                controller.Hold();
                enableLeft = true;
            }
            if (Input.GetMouseButton(1))
            {
                controller.Fill();
            }
            

        }
        // 鼠标左键
        if (controller.enableFire && enableLeft && Input.GetMouseButtonUp(0))
        {
            enableLeft = false;
            userAction.Shoot();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (cameraState == 0)
            {
                cameraTrans.position += new Vector3(0, 40, 0);
                cameraState++;
            }
            else if (cameraState == 1)
            {
                cam.cullingMask |= (1 << 8);   // 增加第八层
                cam.cullingMask &= ~(1 << 7);  // 除去第七层
                cameraTrans.position += new Vector3(0, 60, 0);
                cameraState++;
            }
            else if (cameraState == 2)
            {

                cameraTrans.position += new Vector3(0, 200, 0);
                cameraState++;
            }
            else
            {
                cam.cullingMask |= (1 << 7);
                cam.cullingMask &= ~(1 << 8);
                cameraTrans.position -= new Vector3(0, 300, 0);
                cameraState = 0;
            }

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            userAction.Restart();
        }
        
    }
}

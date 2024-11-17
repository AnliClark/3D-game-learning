using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MainView : MonoBehaviour
{
    private Camera currentCamera;
    private RoundController controller;
    private IUserAction userAction;
    private Texture restart;
    private GUIStyle style;
    private GUISkin skin;
    public bool isPhysics;
    private int buttonWidth;
    void Start()
    {
        currentCamera = Camera.main;
        userAction = SSDirector.getInstance().currentSceneController as IUserAction;
        controller = SSDirector.getInstance().currentSceneController as RoundController;
        restart = controller.restart;
        skin = controller.skin;
        buttonWidth = Screen.width / 20;
        isPhysics = true;
    }


    private void OnGUI()
    {
        GUI.skin = skin;
        float width = Screen.width;
        float height = Screen.height;
        // 游戏数据展示
        GUI.Label(new Rect(width - buttonWidth*2, 10, buttonWidth*4, buttonWidth),
            "score:" + controller.scoreRecorder.score);
        GUI.Label(new Rect(10, 10, buttonWidth*4, buttonWidth),
            "round:" + controller.round);
        // 重启游戏
        if (GUI.Button(new Rect(width - 10 - buttonWidth, height - buttonWidth - 10, buttonWidth, buttonWidth), restart))
        {
            userAction.Restart();
        }

        // 更改模式
        if (GUI.Button(new Rect(10, height - buttonWidth - 10, buttonWidth * 3, buttonWidth), "  Physics  ", isPhysics ? skin.customStyles[0] : skin.button))
        {
            isPhysics = true;
            userAction.ChangeMode(isPhysics);
        }
        if (GUI.Button(new Rect(10, height - buttonWidth * 2 - 10, buttonWidth * 3, buttonWidth), "Kinematics", !isPhysics ? skin.customStyles[0] : skin.button))
        {
            isPhysics = false;
            userAction.ChangeMode(isPhysics);
        }
        // 游戏结束
        if (controller.over)
        {
            GUI.Label(new Rect(width / 2 - buttonWidth * 2, height / 2 - buttonWidth * 2, buttonWidth * 10, buttonWidth * 2),
            "Game Over", skin.customStyles[1]);
        }
        // 禁止操作
        if (controller.pause)
        {
            return;
        }
        // 等待下一轮
        if (controller.waitForNext)
        {
            GUI.Label(new Rect(width/2 - buttonWidth*2, height/2 - buttonWidth * 2, buttonWidth * 10, buttonWidth*2),
            "Next Round", skin.customStyles[1]);
        }
        // 鼠标点击
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                GameObject obj = hitInfo.collider.gameObject;
                // 击中物体
                if (obj.CompareTag("shootable"))
                {
                    userAction.Hit(obj);  
                }
            }
        }
    }
}

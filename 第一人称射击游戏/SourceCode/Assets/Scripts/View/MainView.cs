using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MainView : MonoBehaviour
{
    //private Camera currentCamera;
    private FirstSceneController controller;
    private IUserAction userAction;
    private Texture restart;
    private GUIStyle style;
    private GUISkin skin;
    private int buttonWidth;

    void Start()
    {
        userAction = SSDirector.getInstance().currentSceneController as IUserAction;
        controller = SSDirector.getInstance().currentSceneController as FirstSceneController;
        //restart = controller.restart;
        skin = controller.skin;
        buttonWidth = Screen.width / 20;
        
    }

    private void OnGUI()
    {
        GUI.skin = skin;
        float width = Screen.width;
        float height = Screen.height;
        // 游戏数据展示
        GUI.Label(new Rect(10, height - buttonWidth - 10, buttonWidth * 4, buttonWidth),
            "score:" + controller.scoreRecorder.score);
        GUI.Label(new Rect(10, 10, buttonWidth * 4, buttonWidth),
            "arrow:" + controller.arrowNum);

        // 游戏结束
        if (controller.over)
        {
            //GUI.Label(new Rect(width / 2 - buttonWidth * 2, height / 2 - buttonWidth * 2, buttonWidth * 10, buttonWidth * 2),
            //"Game Over", skin.customStyles[1]);
        }
        // 禁止操作
        //if (controller.pause)
        //{
        //    return;
        //}
        
    }
}

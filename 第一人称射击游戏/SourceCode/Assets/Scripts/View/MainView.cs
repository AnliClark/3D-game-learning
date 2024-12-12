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
    private GUIStyle style;
    private GUISkin skin;
    private int buttonWidth;
    private bool showInstruction;
    public Texture instruction;

    void Start()
    {
        userAction = SSDirector.getInstance().currentSceneController as IUserAction;
        controller = SSDirector.getInstance().currentSceneController as FirstSceneController;
        skin = controller.skin;
        buttonWidth = Screen.width / 20;
        showInstruction = false;
        instruction = Resources.Load("Arts/Textures/keyMouse") as Texture;
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
        if (showInstruction)
        {
            GUI.DrawTexture(new Rect(width / 10, 10, width * 0.8f, width * 0.8f / 1f), instruction);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            showInstruction = !showInstruction;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 用户交互
public class StartGUI : MonoBehaviour
{
    private IUserAction userAction;
    private Texture instruction;
    private Texture start;
    private GUISkin skin;
    // Start is called before the first frame update
    void Start()
    {
        // 将currentSceneController（FirstSceneController已实现IUserAction）显示转换为IUserAction类型
        userAction = SSDirector.getInstance().currentSceneController as IUserAction;
        skin = ((RoundController)SSDirector.getInstance().currentSceneController).skin;
        instruction = Resources.Load("Arts/Textures/instruction") as Texture;
}

    private void OnGUI()
    {
        float width = Screen.width;
        float height = Screen.height;
        GUI.skin = skin;
        GUI.DrawTexture(new Rect(width / 10, 10, width * 0.8f, width *0.8f /1.77f), instruction);
        if (GUI.Button(new Rect(width / 2 - width / 10f, width * 0.8f / 1.77f - width / 20, width / 5, width / 20), "START"))
        {
            userAction.StartGame();
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 用户交互
public class StartGUI : MonoBehaviour
{
    private IUserAction userAction;
    private Texture instruction;
    private Texture start;
    private GUIStyle style;
    // Start is called before the first frame update
    void Start()
    {
        // 将currentSceneController（FirstSceneController已实现IUserAction）显示转换为IUserAction类型
        userAction = SSDirector.getInstance().currentSceneController as IUserAction;

        instruction = Resources.Load("Arts/instruction") as Texture;
        start = Resources.Load("Arts/start") as Texture;
        

        GUI.backgroundColor = new Color(241, 161, 81, 100);
}

    private void OnGUI()
    {
        float width = Screen.width;
        float height = Screen.height;
        GUI.DrawTexture(new Rect(width / 10, height / 10, width * 0.8f, width *0.8f /1.77f), instruction);
        if (GUI.Button(new Rect(width / 24 * 11, height / 12 + width * 0.45f - width / 12, width / 12, width / 12), new GUIContent(start, "start")))
        {
            userAction.StartGame();
        }
    }


}

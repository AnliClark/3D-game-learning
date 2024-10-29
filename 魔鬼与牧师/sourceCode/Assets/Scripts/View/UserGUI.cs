using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �û�����
public class UserGUI : MonoBehaviour
{
    private IUserAction userAction;
    private Texture restart;
    // Start is called before the first frame update
    void Start()
    {
        // ��currentSceneController��FirstSceneController��ʵ��IUserAction����ʾת��ΪIUserAction����
        userAction = SSDirector.getInstance().currentSceneController as IUserAction;
        restart = ((FirstSceneController)SSDirector.getInstance().currentSceneController).restart;
       
    }

    private void OnGUI()
    {
        float width = Screen.width; 
        float height = Screen.height;   
        if(GUI.Button(new Rect(width - width/10, height/20, width / 12, width / 12), restart))
        {
            userAction.Restart();
        }
    }

    
}

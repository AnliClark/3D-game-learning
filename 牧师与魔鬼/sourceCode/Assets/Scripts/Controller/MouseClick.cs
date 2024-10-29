using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    GameObject gameobject;
    public void Awake()
    {
        // Debug.Log("click ready");
        gameobject = this.gameObject;
    }
    private void OnMouseDown()
    {
        ((FirstSceneController)SSDirector.getInstance().currentSceneController).MoveObject(gameobject);
    }
}

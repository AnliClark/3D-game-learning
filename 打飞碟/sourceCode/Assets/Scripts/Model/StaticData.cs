using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticData 
{
    public static Vector3 screenRightUp;
    public static Vector3 screenLeftDown;
    public static Vector3 bgPosition;
    public static Vector3 bgRotation;

    public StaticData()
    {
        screenRightUp = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 50));   // срио╫г
        screenLeftDown = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 50));
        bgPosition = new Vector3(9.7f, -5.1f, 71.1f);
        bgRotation = new Vector3(0f, -79.698f, 0f);
    }
}

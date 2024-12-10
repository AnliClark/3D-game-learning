using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticData 
{
    public static Vector3 bgPosition;
    public static Vector3 bgRotation;
    public static Vector3 playerPosition;
    public StaticData()
    {
        bgPosition = new Vector3(0f, 0f, 0f);
        bgRotation = new Vector3(0f, 0f, 0f);
        playerPosition = new Vector3(37.7f, -9.3f, 29.2f);

    }
}

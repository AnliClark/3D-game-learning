using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Referee
{
    private static IEndListener endListener;
    public Referee()
    {
        endListener = SSDirector.getInstance().currentSceneController as IEndListener;
    }

    public static void Check()
    {
        Debug.Log("check");
        if (Characters.isWin())
        {
            endListener.OnEnd(true);
            return;
        }
        if(Characters.isLose()) {
            endListener.OnEnd(false);
            return;
        }
    }
}

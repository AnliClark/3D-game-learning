using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Character: AllObject
{
    public bool isInBoat = false;
    
    public bool isPriest;
    public GameObject gameobject;
    public Character(GameObject o, int count, bool isPriest = true)
    {
        gameobject = GameObject.Instantiate(o);
        gameobject.transform.position = Position.leftPosition[count];
        id = gameobject.GetInstanceID();
        isBoat = false;
        this.isPriest = isPriest;
    }

    public override bool Move(out Vector3 target1, out Vector3 target2, out Vector3 target3, out GameObject o1, out GameObject o2) 
    {
        target2 = Vector3.zero;
        target3 = Vector3.zero;
        o1 = null;
        o2 = null;
        FirstSceneController sceneController = ((FirstSceneController)SSDirector.getInstance().currentSceneController);
        return sceneController.characters.moveCharacter22Boat(this, sceneController.boat, out target1);
    }
}

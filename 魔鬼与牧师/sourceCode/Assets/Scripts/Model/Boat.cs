using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Boat: AllObject
{
    GameObject gameobject;

    public readonly Vector3 leftPostion = Position.boatPosition[0]; // 左岸位置 
    public readonly Vector3 rightPostion = Position.boatPosition[1];  // 右岸位置 
    // 座位的位置（初始为船在左边时，座位的位置）
    private Vector3 leftPosition = Position.boatSeatPosition[0];
    private Vector3 rightPosition = Position.boatSeatPosition[1];
    private Character[] seatCharacter = { null, null};  // 对应位置上的gameobject
    public Boat(GameObject b) { 
        gameobject = GameObject.Instantiate(b);
        gameobject.transform.position = leftPostion;
        id = gameobject.GetInstanceID();
        isBoat = true;
    }

    // 当试图让人上船时，输出船上的位置，返回是否可以上船
    public bool loadPerson(Character charc, out Vector3 position)
    {

        if (seatCharacter[0] == null)
        {
            position = leftPosition;
            seatCharacter[0] = charc;
            return true;
        }
        else if (seatCharacter[1] == null)
        {
            position = rightPosition;
            seatCharacter[1] = charc;
            return true;
        }
        else
        {
            position = Vector3.zero;
            return false;
        }

        
    }

    // 船上有人离开
    public void outPerson(Character charc)
    {
        if (seatCharacter[0] == charc)
        {
            seatCharacter[0] = null;
        }
        else{
            seatCharacter[1] = null;
        }
    }
    // 船移动


    public override bool Move(out Vector3 target1, out Vector3 target2, out Vector3 target3, out GameObject o1, out GameObject o2)
    {
        target1 = Vector3.zero;
        target2 = Vector3.zero;
        target3 = Vector3.zero;
        o1 = null; 
        o2 = null;
        if (seatCharacter[0] == null && seatCharacter[1] == null) // 船上没人
        {
            return false;
        }

        isLeft = !isLeft;
        if (isLeft)
        {
            leftPosition = Position.boatSeatPosition[0];
            rightPosition = Position.boatSeatPosition[1];
            target1 = leftPostion;
        }
        else
        {
            leftPosition = Position.boatSeatPosition[2];
            rightPosition= Position.boatSeatPosition[3];
            target1 = rightPostion;
        }

        if (seatCharacter[0] != null)
        {
            target2 = leftPosition;
            o1 = seatCharacter[0].gameobject;
            seatCharacter[0].isLeft = isLeft;  // 更改模型的位置
            
        }
        if (seatCharacter[1] != null)
        {
            target3 = rightPosition;
            o2 = seatCharacter[1].gameobject;
            seatCharacter[1].isLeft = isLeft;
        }
        
        return true;
    }

}


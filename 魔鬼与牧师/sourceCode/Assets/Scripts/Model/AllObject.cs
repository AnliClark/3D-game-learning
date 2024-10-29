using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AllObject
{
    public int id;
    public bool isBoat; // false 表示人，true表示船
    public bool isLeft = true; // 判断在河的哪边

    public abstract bool Move(out Vector3 target1, out Vector3 target2, out Vector3 target3, out GameObject o1, out GameObject o2);
}

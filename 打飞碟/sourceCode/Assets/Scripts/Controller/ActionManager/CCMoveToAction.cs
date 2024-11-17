using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 移动到target
public class CCMoveToAction : SSAction
{
	public Vector3 v;  // 初速度
	public float speed;

	public static CCMoveToAction GetSSAction(Vector3 v, float speed){
		CCMoveToAction action = ScriptableObject.CreateInstance<CCMoveToAction> ();
		action.v = v;
		action.speed = speed;
		return action;
	}

	public override void Update ()
	{
		this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + v, speed * Time.deltaTime);
		this.transform.position = Vector3.MoveTowards (this.transform.position, this.transform.position+new Vector3(0,-1,0), Time.deltaTime);
        this.transform.Rotate(0, speed * 200.0f * Time.deltaTime, 0);
    }

	public override void Start () {

	}
}


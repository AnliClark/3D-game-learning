using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CCActionManager : SSActionManager, IActionManager {
	private static CCActionManager _instance;
	public static CCActionManager GetInstance()
	{
		if(_instance == null)
		{
			_instance = Component.FindObjectOfType<CCActionManager>();
		}
		return _instance;
	}
	protected new void Start() {
	}
	protected new void Update ()
	{
		base.Update ();
	}

    public void PlayDisk(GameObject disk, Vector3 v) {
		// 启用运动学模式
		disk.GetComponent<Rigidbody>().isKinematic = true;
		SSAction action = CCMoveToAction.GetSSAction(v, disk.GetComponent<DiskData>().speed);
		RunAction(disk, action);
	}
}


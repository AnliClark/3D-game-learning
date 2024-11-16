using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CCActionManager : SSActionManager, IActionManager {
	
	private RoundController sceneController;
	private static CCActionManager _instance;
	// public static bool isActionActive;  // 是否可以接收新的动作

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

	// Update is called once per frame
	protected new void Update ()
	{
		base.Update ();
	}

    public void PlayDisk(GameObject disk, Vector3 v) {
		disk.GetComponent<Rigidbody>().isKinematic = true;
		SSAction action = CCMoveToAction.GetSSAction(v, disk.GetComponent<DiskData>().speed);
		RunAction(disk, action);
	}
}


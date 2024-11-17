using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour {

	private Dictionary <int, SSAction> actions = new Dictionary <int, SSAction> ();
	private List <SSAction> waitingAdd = new List<SSAction> (); 

	protected void Update () {
		foreach (SSAction ac in waitingAdd) actions [ac.GetInstanceID ()] = ac;
		waitingAdd.Clear ();

		foreach (KeyValuePair <int, SSAction> kv in actions) {
			SSAction ac = kv.Value;
			if (ac.enable) { 
				ac.Update (); // update action
			}
		}

	}

	// 使gameobject执行action动作，并添加manager来管理回收
	public void RunAction(GameObject gameobject, SSAction action) {
		action.gameobject = gameobject;
		action.transform = gameobject.transform;
		waitingAdd.Add (action);   // 添加到待添加动作列表
		action.Start ();
	}


	// Use this for initialization
	protected void Start () {
	}
}

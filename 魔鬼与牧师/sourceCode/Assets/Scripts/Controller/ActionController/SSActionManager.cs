using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour {

	private Dictionary <int, SSAction> actions = new Dictionary <int, SSAction> ();
	private List <SSAction> waitingAdd = new List<SSAction> (); 
	private List<int> waitingDelete = new List<int> ();

	// Update is called once per frame
	protected void Update () {
		foreach (SSAction ac in waitingAdd) actions [ac.GetInstanceID ()] = ac;
		waitingAdd.Clear ();

		foreach (KeyValuePair <int, SSAction> kv in actions) {
			SSAction ac = kv.Value;
			if (ac.destory) {   // 动作结束
				// 使物体可以重新移动
				CCActionManager.isActionActive = true;
				waitingDelete.Add(ac.GetInstanceID()); // release action
			} else if (ac.enable) { 
				ac.Update (); // update action
			}
		}

		foreach (int key in waitingDelete) {
			SSAction ac = actions[key]; 
			actions.Remove(key); 
			Object.Destroy(ac);
		}
		waitingDelete.Clear ();
	}

	// 使gameobject执行action动作，并添加manager来管理回收
	public void RunAction(GameObject gameobject, SSAction action, ISSActionCallback manager) {
		action.gameobject = gameobject;
		action.transform = gameobject.transform;
		action.callback = manager;
		waitingAdd.Add (action);   // 添加到待添加动作列表
		action.Start ();
	}


	// Use this for initialization
	protected void Start () {
	}
}

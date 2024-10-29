using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CCActionManager : SSActionManager, ISSActionCallback {
	
	private FirstSceneController sceneController;
	public static bool isActionActive;  // 是否可以接收新的动作

	protected new void Start() {
		sceneController = (FirstSceneController)SSDirector.getInstance ().currentSceneController;
		sceneController.actionManager = this;
		Debug.Log("CCActionManager Start");
	}

	// Update is called once per frame
	protected new void Update ()
	{
		base.Update ();
	}

	public void MoveObject(GameObject gameobject)
	{
        // 判断是否可移动（防止在移动过程中被重复点击）
        if (isActionActive)
        {
            isActionActive = false;

        }
        else
        {
            return;
        }
        int instanceID = gameobject.GetInstanceID();
		AllObject parentObject=null;  // 船或人物
        if (sceneController.boat.id == instanceID)
		{
			parentObject = sceneController.boat;
		}
		else
		{
			foreach(Character character in Characters.characterList)
			{
				if(character.id == instanceID)
				{
					parentObject = character;
					break;
				}
			}
		}
        Vector3 originPosition = gameobject.transform.position;
		Vector3 target1, target2, target3;
		GameObject o1, o2;
        if (parentObject.isBoat)  // 船带着人移动
        {
			if(parentObject.Move(out target1, out target2, out target3, out o1, out o2))
			{
				SSAction move1 = CCMoveToAction.GetSSAction(target1, 1.0f);
                this.RunAction(gameobject, move1, this);
                Debug.Log("船移动位置：" + target1.ToString());
                if (target2 != Vector3.zero)
				{
                    SSAction move2 = CCMoveToAction.GetSSAction(target2, 1.0f);
                    this.RunAction(o1, move2, this);
                    Vector3 angles = o1.transform.eulerAngles;
                    o1.transform.eulerAngles = new Vector3(angles[0], 180 - angles[1], angles[2]);
                }
                if(target3 != Vector3.zero)
				{
                    SSAction move3 = CCMoveToAction.GetSSAction(target3, 1.0f);
                    this.RunAction(o2, move3, this);
                    Vector3 angles = o2.transform.eulerAngles;
                    o2.transform.eulerAngles = new Vector3(angles[0], 180 - angles[1], angles[2]);
                }
			}
			else
			{
				isActionActive = true;
			}
        }
        else	// 人的系列移动
        {
			if(parentObject.Move(out target1,out target2, out target3, out o1,out o2))
			{
				SSAction ccs = ObjectAction.CharacterSequenceMove(originPosition, target1);
				this.RunAction(gameobject, ccs, this);
			}
			else
			{
                isActionActive = true;
            }
			Referee.Check();
        }
    }
		
	#region ISSActionCallback implementation1
	public void SSActionEvent (SSAction source, SSActionEventType events = SSActionEventType.Competeted, int intParam = 0, string strParam = null, Object objectParam = null)
	{
		//if (source == moveToA) {
		//	moveToB = CCMoveToAction.GetSSAction (new Vector3 (-5, 0, 0), 1);
		//	this.RunAction (sceneController.move1, moveToB, this);
		//} else if (source == moveToB) {
		//	moveToA = CCMoveToAction.GetSSAction (new Vector3 (5, 0, 0), 1);
		//	this.RunAction (sceneController.move1, moveToA, this);
		//}
	}
	#endregion
}


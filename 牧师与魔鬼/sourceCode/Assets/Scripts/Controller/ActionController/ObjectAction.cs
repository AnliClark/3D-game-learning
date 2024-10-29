using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAction
{
    // character的系列动作
    public static CCSequenceAction CharacterSequenceMove(Vector3 objectPosition, Vector3 targetPosition)
    {
        SSAction moveToTop = CCMoveToAction.GetSSAction(new Vector3(objectPosition.x, objectPosition.y + 0.5f, objectPosition.z), 1);
        SSAction moveToTargetTop = CCMoveToAction.GetSSAction(new Vector3(targetPosition.x, objectPosition.y + 0.5f, targetPosition.z), 1);
        SSAction moveToTarget = CCMoveToAction.GetSSAction(new Vector3(targetPosition.x, targetPosition.y, targetPosition.z), 1);  // todo改一下，有点空隙
        return CCSequenceAction.GetSSAction(1, 0, new List<SSAction> { moveToTop, moveToTargetTop, moveToTarget });
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��طɵ��Ƿ���Ļ�⣬���շɵ�
public class EventListener : MonoBehaviour {
    private Vector3 position;
    private DiskFactory diskFactory;
    private void Start()
    {
        diskFactory = DiskFactory.GetInstance();
    }
    private void Update()
    {
        position = gameObject.transform.position;
        if(position.x > StaticData.screenRightUp.x + 3 || position.x < StaticData.screenLeftDown.x - 3 || position.y > StaticData.screenRightUp.y || position.y < StaticData.screenLeftDown.y)
        {
            diskFactory.FreeDisk(gameObject);
        }
    }
}

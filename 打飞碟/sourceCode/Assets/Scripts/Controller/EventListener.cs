using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 监控飞碟是否到屏幕外，回收飞碟
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

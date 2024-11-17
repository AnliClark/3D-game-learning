using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 管理飞碟生成时的数据配置
public class Ruler
{
    public int round = 0;
    private static Ruler _instance;
    private static Vector3 screenRightUp;
    private static Vector3 screenLeftDown;
    public static Ruler GetInstance()
    {
        if(_instance == null)
        {
            _instance = new Ruler();
            screenRightUp = StaticData.screenRightUp;
            screenLeftDown = StaticData.screenLeftDown;
        }
        return _instance;
    }

    public void SetDiskData(DiskData diskData)
    {
        // 设置飞碟数据
        float size = 0.5f/round+0.5f;
        float speed = round / 4.0f + 4.0f;   
        diskData.size = size + Random.Range(0, 0.1f);
        diskData.speed = speed + Random.Range(0, 0.25f);

        if (diskData.color == 1)
        {
            diskData.score = 1;
        }
        else if (diskData.color == 2)
        {
            speed+=0.5f;
            diskData.score = 2;
        }
        else
        {
            speed += 1;
            diskData.score = 3;
        }
        

        // 设置飞碟初始位置与飞入的方向
        Vector3 spawnPosition = new Vector3();
        Vector3 targetPosition = new Vector3();
        // 随机从左边和右边射出
        if (Random.Range(0, 2) == 0)
        {
            spawnPosition.x = screenLeftDown.x;
            targetPosition.x = screenRightUp.x;
        }
        else
        {
            spawnPosition.x = screenRightUp.x;
            targetPosition.x = screenLeftDown.x;
        }
        spawnPosition.z = 35;
        spawnPosition.y =  Random.Range(screenRightUp.y - (screenRightUp.y - screenLeftDown.y) / 3, screenRightUp.y - (screenRightUp.y - screenLeftDown.y) / 4);
        targetPosition.z = Random.Range(45,70);
        targetPosition.y = spawnPosition.y + Random.Range((screenRightUp.y - screenLeftDown.y) / 10, (screenRightUp.y - screenLeftDown.y) / 4);

        diskData.position = spawnPosition;
        diskData.velocity = (targetPosition - spawnPosition).normalized * diskData.speed;

        
    }
}

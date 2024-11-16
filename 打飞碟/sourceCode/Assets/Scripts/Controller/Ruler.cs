using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        // ���÷ɵ�����
        float size = 0.5f/round+0.5f;
        float speed = round / 3.0f + 3.0f;   
        diskData.size = size + Random.Range(0, 0.1f);
        diskData.speed = speed + Random.Range(0, 2);

        if (diskData.color == 1)
        {
            diskData.score = 1;
        }
        else if (diskData.color == 2)
        {
            speed++;
            diskData.score = 2;
        }
        else
        {
            speed += 2;
            diskData.score = 3;
        }
        

        // ���÷ɵ���ʼλ�������ķ���
        Vector3 spawnPosition = new Vector3();
        Vector3 targetPosition = new Vector3();
        // �������ߺ��ұ����
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
        spawnPosition.z = 50;
        spawnPosition.y =  Random.Range(screenRightUp.y - (screenRightUp.y - screenLeftDown.y) / 3, screenRightUp.y - (screenRightUp.y - screenLeftDown.y) / 4);
        targetPosition.z = Random.Range(60,80);
        targetPosition.y = spawnPosition.y + Random.Range((screenRightUp.y - screenLeftDown.y) / 10, (screenRightUp.y - screenLeftDown.y) / 4);

        diskData.position = spawnPosition;
        diskData.velocity = (targetPosition - spawnPosition).normalized * diskData.speed;

        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ڴ洢��������꣬�����޸�
public class Position {
    // ���ϵ�����
    public static Vector3[] leftPosition;
    // �Ұ��ϵ�����
    public static Vector3[] rightPosition;
    // ������λ������
    public static Vector3[] boatSeatPosition;  // 0,1Ϊ�������ʱ�����꣬2,3Ϊ�ұ�
    // ��������
    public static Vector3[] boatPosition;
    // ����������
    public static Vector3 bgPosition;
    public static Vector3 cameraPosition;
    public static Vector3 cameraRotation;
    public Position()
    {
        leftPosition = new Vector3[6]
        {
            new Vector3(2.6f, 0.42f, 0.4f),
            new Vector3(2.6f, 0.42f, 0.55f),
            new Vector3(2.6f, 0.42f, 0.7f),
            new Vector3(2.6f, 0.42f, 0.85f),
            new Vector3(2.6f, 0.42f, 1f),
            new Vector3(2.6f, 0.42f, 1.15f)
        };
        rightPosition = new Vector3[6]
        {
            new Vector3(2.6f, 0.42f, -1.75f),
            new Vector3(2.6f, 0.42f, -1.9f),
            new Vector3(2.6f, 0.42f, -2.05f),
            new Vector3(2.6f, 0.42f, -2.2f),
            new Vector3(2.6f, 0.42f, -2.35f),
            new Vector3(2.6f, 0.42f, -2.5f)
        };
        boatSeatPosition = new Vector3[4]
        {
            new Vector3(2.6f, 0.32f, 0.08f),
            new Vector3(2.6f, 0.32f, -0.1f),
            new Vector3 (2.6f, 0.32f, -1.25f),
            new Vector3(2.6f, 0.32f, -1.43f)
        };
        boatPosition = new Vector3[2] {
            new Vector3(2.6f, 0.3f, -0.05f),
            new Vector3(2.6f, 0.3f, -1.32f)
        };
        bgPosition = new Vector3(2.24f, 0.22f, -4.45f);
        
        cameraPosition = new Vector3(0.61f, 1.26f, 0.36f);
        cameraRotation = new Vector3(0f, 107f, 0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory
{
    private static DiskFactory _instance;
    public IActionManager actionManager;
    private GameObject disk1,disk2,disk3;
    private GameObject tmpDisk;
    private DiskData tmpDiskData;
    public static List<GameObject> usedDisk;
    public static List<GameObject> freeDisk;

    private static Ruler ruler;

    public static DiskFactory GetInstance()
    {
        if(_instance == null)
        {
            _instance = new DiskFactory();
            usedDisk = new List<GameObject>();
            freeDisk = new List<GameObject>();

            ruler = Ruler.GetInstance();
        }
        return _instance;
    }
    public void SetDiskObject(GameObject d1, GameObject d2, GameObject d3)
    {
        disk1 = d1;
        disk2 = d2;
        disk3 = d3;
    }
    public GameObject GetDisk(int round)  // round todo
    {
        int color = (int)Random.Range(1, 4);
        bool isHave = false;
        // ��ȡ����Ҫ�Ķ���
        foreach (GameObject disk in freeDisk)
        {
            if(disk.GetComponent<DiskData>().color == color)
            {
                tmpDisk = disk;
                tmpDiskData = tmpDisk.GetComponent<DiskData>();
                freeDisk.Remove(disk);
                isHave = true;
                break;
            }
        }
        if (!isHave)
        {
            // ��������ʵ��
            switch (color)
            {
                case 1:tmpDisk = GameObject.Instantiate<GameObject>(disk1);break;
                case 2:tmpDisk = GameObject.Instantiate<GameObject>(disk2);break;
                case 3:tmpDisk = GameObject.Instantiate<GameObject>(disk3);break;
            }
            // ���ӿؼ�
            tmpDisk.AddComponent<EventListener>();  // �����Ƿ����
            tmpDisk.AddComponent<DiskData>();
            tmpDiskData = tmpDisk.GetComponent<DiskData>();
            tmpDiskData.color = color;
        }

        ruler.SetDiskData(tmpDiskData);

        tmpDisk.transform.localScale = new Vector3(tmpDiskData.size, tmpDiskData.size, tmpDiskData.size);
        tmpDisk.transform.position = tmpDiskData.position;

        actionManager.PlayDisk(tmpDisk, tmpDisk.GetComponent<DiskData>().velocity); 

        tmpDisk.SetActive(true);
        usedDisk.Add(tmpDisk);
        return tmpDisk;
    }

    public void FreeDisk(GameObject d)
    {
        d.SetActive(false);
        usedDisk.Remove(d);
        freeDisk.Add(d);
    }
}

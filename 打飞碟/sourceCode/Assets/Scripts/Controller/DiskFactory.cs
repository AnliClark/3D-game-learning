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
        // 获取所需要的对象
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
            // 创建对象实例
            switch (color)
            {
                case 1:tmpDisk = GameObject.Instantiate<GameObject>(disk1);break;
                case 2:tmpDisk = GameObject.Instantiate<GameObject>(disk2);break;
                case 3:tmpDisk = GameObject.Instantiate<GameObject>(disk3);break;
            }
            // 增加控件
            tmpDisk.AddComponent<EventListener>();  // 监听是否出界
            tmpDisk.AddComponent<DiskData>();
            tmpDiskData = tmpDisk.GetComponent<DiskData>();
            tmpDiskData.color = color;
        }

        ruler.SetDiskData(tmpDiskData);

        tmpDisk.transform.localScale = new Vector3(tmpDiskData.size, tmpDiskData.size, tmpDiskData.size);
        tmpDisk.transform.position = tmpDiskData.position;

        // 使飞碟运行
        actionManager.PlayDisk(tmpDisk, tmpDisk.GetComponent<DiskData>().velocity); 
        // 激活飞碟
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

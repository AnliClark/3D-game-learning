using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFactory
{
    private static ObjectFactory _instance;
    public IActionManager actionManager;
    private GameObject arrow,staticObject,dynamicObject;
    private GameObject tmpObj;
    // 箭
    public static List<GameObject> usedArrow;
    public static List<GameObject> freeArrow;
    // 静态靶
    public static List<GameObject> usedStatic;
    public static List<GameObject> freeStatic;
    // 动态靶
    public static List<GameObject> usedDynamic;
    public static List<GameObject> freeDynamic;

    public static ObjectFactory GetInstance()
    {
        if(_instance == null)
        {
            _instance = new ObjectFactory();

            usedArrow = new List<GameObject>();
            freeArrow = new List<GameObject>();
            usedStatic = new List<GameObject>();
            freeStatic = new List<GameObject>();
            usedDynamic = new List<GameObject>();
            freeDynamic = new List<GameObject>();

        }
        return _instance;
    }
    public void SetDiskObject(GameObject d1)
    {
        arrow = d1;
    }
    public GameObject GetObject(string obj)  // round todo
    {

        if (freeArrow.Count > 0)
        {
            tmpObj = freeArrow[freeArrow.Count - 1];
            tmpObj.GetComponent<ArrowListener>().hasCollide = false;
            freeArrow.RemoveAt(freeArrow.Count - 1);
        }
        else
        {
            tmpObj = GameObject.Instantiate(arrow);
            tmpObj.SetActive(false);
        }
        usedArrow.Add(tmpObj);
        tmpObj.transform.position = GameObject.Find("FirePoint").transform.position;
        Debug.Log(arrow.transform.rotation);
        Debug.Log(GameObject.Find("FirePoint").transform.rotation);
        tmpObj.transform.rotation = GameObject.Find("FirePoint").transform.rotation;
            
        
        // 增加控件
        //tmpObj.AddComponent<EventListener>();  // 监听是否出界
        //tmpObj.AddComponent<DiskData>();
        //tmpDiskData = tmpObj.GetComponent<DiskData>();
        //tmpDiskData.color = color;

        //ruler.SetDiskData(tmpDiskData);

        //tmpObj.transform.localScale = new Vector3(tmpDiskData.size, tmpDiskData.size, tmpDiskData.size);
        //tmpObj.transform.position = tmpDiskData.position;

        
        
        return tmpObj;
    }

    public void FreeObject(GameObject d, string obj)
    {
        d.SetActive(false);
        if(obj == "arrow")
        {
            usedArrow.Remove(d);
            freeArrow.Add(d);
        }
        else if(obj == "staticObject")
        {
            usedStatic.Remove(d);
            freeStatic.Add(d);
        }
        else
        {
            usedDynamic.Remove(d);
            usedStatic.Add(d);
        }
        
    }
}

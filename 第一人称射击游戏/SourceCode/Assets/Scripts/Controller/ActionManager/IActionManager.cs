using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public interface IActionManager {
    // disk 以v的初速度前进
    public void PlayObject(GameObject disk, Vector3 v);  
}

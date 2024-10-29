using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEndListener
{
    public void OnEnd(bool WinLose);  // win则为true，lose则为false
}

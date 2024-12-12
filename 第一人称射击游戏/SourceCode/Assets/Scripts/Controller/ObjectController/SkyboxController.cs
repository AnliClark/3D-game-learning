using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    public Material skyboxMaterial; // 过渡材质
    public float skyboxTransSpeed = 0.005f; // 过渡速度

    private int skyboxCount = 5;
    private float skyRange = 0f;

    void Start()
    {
        if (skyboxMaterial != null)
        {
            RenderSettings.skybox = skyboxMaterial;
        }
    }

    void Update()
    {
        skyRange += skyboxTransSpeed;
        skyboxMaterial.SetFloat("_SkyRange", skyRange);
        if (skyRange >= skyboxCount)
        {
            skyRange = 0f;
            skyboxMaterial.SetFloat("_SkyRange", skyRange);
        }
    }
}
